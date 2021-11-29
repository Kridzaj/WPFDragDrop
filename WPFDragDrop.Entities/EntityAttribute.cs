using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDragDrop.Entities
{
    [Table("EntityAttributes")]
    public class EntityAttribute
    {
        public EntityAttribute()
        {

        }

        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string DataType { get; set; }

        [Required]
        public int EntityId { get; set; }
    }
}
