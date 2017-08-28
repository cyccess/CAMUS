using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CAMUS.Models
{
    [Table("Banner")]
    public class Banner
    {
        [Key]
        public int Id { get; set; }

        public string Path { get; set; }

        public DateTime CreateDate { get; set; } 

        public int Order { get; set; }

        public bool IsDelete { get; set; }
    }
}