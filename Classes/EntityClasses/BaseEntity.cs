using System.ComponentModel.DataAnnotations;

namespace MoOnlineStore.Core.EntityClasses
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}