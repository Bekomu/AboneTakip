namespace AboneTakip.Core.Entities.Abstract
{
    public interface ICreated
    {
        string CreatedBy { get; set; }
        DateTime CreatedDate { get; set; }
    }
}