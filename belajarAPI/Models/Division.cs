using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace belajarAPI.Models
{
    [Table("Tb-M_Division")]
    public class Division
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public int Department_Id { get; set; }
        [ForeignKey("Department_Id")]
        public virtual Department Department { get; set; }

        public Division() { }
    }
}