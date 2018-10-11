using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Article
    {
        public int Id { get; set; }
        public virtual Store Store { get; set; }
        public int StoreId { get; set; }
        [Required]
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [StringLength(200)]
        public string Description { get; set; }
        [Required]
        public float Price { get; set; }
        public float Total_In_Shelf { get; set; }
        public float Total_In_Vault { get; set; }
       

    }
}
