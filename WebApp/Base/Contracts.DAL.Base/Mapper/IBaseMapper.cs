namespace Contracts.DAL.Base.Mapper
{
    public interface IBaseMapper<TLeftObject, TRightObject>
    {
        TLeftObject? Map(TRightObject? inObject);
        TRightObject? Map(TLeftObject? inObject);
    }
}