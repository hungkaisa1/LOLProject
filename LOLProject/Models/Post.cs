using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOLProject.Models
{
    public class Post
    {
        public int PostId { get; set; }
        [Display(Name ="Posted By")]
        public string AccountId { get; set; }

        [Required(ErrorMessage = "Please enter the Title!!")]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the Content!!")]
        public string Content { get; set; }
        [Required]
        public DateTime PostDate { get; set; }
    }
}
