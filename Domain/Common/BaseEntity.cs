namespace Domain.Common.BaseEntity
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public bool isDeleted { get; set; }
    }
}
