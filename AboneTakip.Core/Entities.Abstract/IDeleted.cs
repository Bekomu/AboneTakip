namespace AboneTakip.Core.Entities.Abstract
{
    public interface IDeleted
    {
        string DeletedBy { get; set; }
        DateTime? DeletedDate { get; set; }
    }
}