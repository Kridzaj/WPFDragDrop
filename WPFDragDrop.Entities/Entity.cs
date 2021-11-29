using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFDragDrop.Entities
{
    [Table("Entities")]
    public class Entity
    {
        public Entity()
        {
            Attributes = new List<EntityAttribute>();
        }

        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public virtual List<EntityAttribute> Attributes { get; set; }
    }
}
