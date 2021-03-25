using Microsoft.EntityFrameworkCore;

namespace DAL.App.EF.AppDataInit
{
    public static class DataInit
    {
        public static void DropDataBase(AppDbContext ctx)
        {
            ctx.Database.EnsureDeleted();
        }
        public static void MigrateDataBase(AppDbContext ctx)
        {
            ctx.Database.Migrate();
        }
        public static void SeedIdentity(AppDbContext ctx)
        {
            
        }
        public static void SeedAppData(AppDbContext ctx)
        {
            
        }
    }
}