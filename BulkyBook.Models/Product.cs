using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ISBN { get; set; }
        [Required]
        public string Author { get; set; }
        [Range(1, 10000)]
        public string ListPrice { get; set; }
        [Range(1, 10000)]
        public string Price { get; set; }
        [Range(1, 10000)]
        public string Price50 { get; set; }
        [Range(1, 10000)]
        public string? Price100 { get; set; }
        public string? ImageUrl { get; set; }
        [DisplayName("Category")]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        [DisplayName("Cover Type")]

        public int CoverTypeId { get; set; }
        public CoverType? CoverType { get; set; }
    }
}
