using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CMSApplication.Data.Entity
{

    public interface IBaseEntity
    {
        object Id { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
    }
    public interface IBaseEntity<T> : IBaseEntity
    {
        new T Id { get; set; }
    }

    public abstract class BaseEntity<T> : IBaseEntity<T>
    {
        [Key]
        public T Id { get; set; }
        private DateTime? createdDate = null;

        object IBaseEntity.Id
        {
            get => this.Id;
            set => Id = (T)Convert.ChangeType(value, typeof(T));
        }
        public DateTime CreatedDate
        {
            get => createdDate ?? DateTime.Now;
            set => createdDate = DateTime.Now;
        }

        public DateTime? ModifiedDate { get; set; }
    }
}
