namespace AboneTakip.Core.Entities.Abstract
{
    public interface IModified
    {
        string ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
}