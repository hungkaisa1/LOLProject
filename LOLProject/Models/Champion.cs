using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LOLProject.Models
{
    public class Champion
    {
        public int ChampionId { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string ChampionName { get; set; }

        [Display(Name = "Role")]
        public int RoleId { get; set; }

        [Display(Name = "Price(IP)")]
        [Range(0, 10000)]
        public decimal Price { get; set; }

        [Display(Name = "Position")]
        public int PositionId { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public Role Role { get; set; }

        public Position Position { get; set; }

    }
}
