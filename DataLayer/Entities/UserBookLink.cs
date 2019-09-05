using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.DynamicData;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataLayer
{
    public class UserBookLinks
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Users")]
        public virtual int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual Users Users { get; set; }

        [Required]
        [Display(Name = "Books")]
        public virtual int BookId { get; set; }
        [ForeignKey("BookId")]
        public virtual Books Books { get; set; }

        [Required]
        public DateTime CreationDate { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime Deadline { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ReturnDate { get; set; } = null;
    }
}