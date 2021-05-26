using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using DAL.App.EF;
using Microsoft.AspNetCore.Authorization;
using Domain.App.Identity;
using Base.Resources.Areas.Identity.Pages.Account;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace WebApp.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class ExternalLoginModel : PageModel
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<ExternalLoginModel> _logger;
        private readonly AppDbContext _ctx;

        public ExternalLoginModel(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            ILogger<ExternalLoginModel> logger,
            IEmailSender emailSender,
            AppDbContext ctx
            )
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _ctx = ctx;
        }

        [BindProperty] public InputModel Input { get; set; } = default!;

        public string ProviderDisplayName { get; set; } = default!;

        public string? ReturnUrl { get; set; }

        [TempData] public string ErrorMessage { get; set; } = default!;

        public class InputModel
        {
            [Required(ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "ErrorMessage_Required")]
 [EmailAddress(ErrorMessageResourceType = typeof(Base.Resources.Common),
                ErrorMessageResourceName = "ErrorMessage_Email")]
 
            [Display(Name = nameof(Email), ResourceType = typeof(ExternalLogin))] 
            public string Email { get; set; } = default!;
            
            [Required(ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "ErrorMessage_Required")]

            [Display(Name = nameof(FirstName), ResourceType = typeof(ExternalLogin))]
            public string FirstName { get; set; } = default!;
            
            [Required(ErrorMessageResourceType = typeof(Base.Resources.Common), ErrorMessageResourceName = "ErrorMessage_Required")]

            [Display(Name = nameof(LastName), ResourceType = typeof(ExternalLogin))]
            public string LastName { get; set; } = default!;
            
        }

        public IActionResult OnGetAsync()
        {
            return RedirectToPage("./Login");
        }

        public IActionResult OnPost(string provider, string? returnUrl = null)
        {
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./ExternalLogin", pageHandler: "Callback", values: new {returnUrl});
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        public async Task<IActionResult> OnGetCallbackAsync(string? returnUrl = null, string? remoteError = null)
        {
            returnUrl ??= Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl});
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl});
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.",
                    info?.Principal?.Identity?.Name, info?.LoginProvider);
                return LocalRedirect(returnUrl); 
            }

            if (result.IsLockedOut)
            {
                return RedirectToPage("./Lockout");
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;

                Input = new InputModel();

                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input.Email = info.Principal.FindFirstValue(ClaimTypes.Email);
                }
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Name))
                {
                    Input.FirstName = info.Principal.FindFirstValue(ClaimTypes.Name);
                }
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Surname))
                {
                    Input.LastName = info.Principal.FindFirstValue(ClaimTypes.Surname);
                }

                return Page();
            }
        }

        public async Task<IActionResult> OnPostConfirmationAsync(string? returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            // Get the information about the user from the external login provider
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information during confirmation.";
                return RedirectToPage("./Login", new {ReturnUrl = returnUrl});
            }

            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    UserName = Input.Email, 
                    Email = Input.Email, 
                    Firstname = Input.FirstName, 
                    Lastname = Input.LastName,
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);


                        // save all the claims
                        // TODO: we need more info, like DOB
                        foreach (var claim in info.Principal.Claims)
                        {
                            await _userManager.AddClaimAsync(user, claim
                            );

                            switch (claim.Type)
                            {
                                case ClaimTypes.GivenName:
                                    user.Firstname = claim!.Value;
                                    break;
                                case ClaimTypes.Surname:
                                    user.Lastname = claim!.Value;
                                    break;
                            }
                            
                        }
                        await _userManager.UpdateAsync(user);



                        var userId = await _userManager.GetUserIdAsync(user);
                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                        code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new {area = "Identity", userId = userId, code = code},
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        // If account confirmation is required, we need to show the link if we don't have a real email sender
                        if (_userManager.Options.SignIn.RequireConfirmedAccount)
                        {
                            return RedirectToPage("./RegisterConfirmation", new {Email = Input.Email});
                        }

                        await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

                        return LocalRedirect(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ProviderDisplayName = info.ProviderDisplayName;
            ReturnUrl = returnUrl;

            
            return Page();
        }
    }
}