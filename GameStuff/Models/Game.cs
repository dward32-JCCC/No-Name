using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameStuff.Models {

    public class Game {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int game_id { get; set; }

        [Display(Name = "Enter Game Code")]
        [StringLength(20)]
        [Required(ErrorMessage = "{0} Is a required field.")]
        public string game_code { get; set; }

        [Display(Name = "Enter Game Type")]
        [StringLength(20)]
        [Required(ErrorMessage = "{0} Is a required field.")]
        public string game_type { get; set; }

        [Display(Name = "Enter Game Description")]
        [StringLength(150)]
        public string game_description { get; set; }

        [Display(Name = "Game Price")]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [Required(ErrorMessage = "{0} Is a required field.")]
        public decimal game_price { get; set; }

        [Display(Name = "Game Stock")]
        [Required(ErrorMessage = "{0} Is a required field.")]
        public int game_stock { get; set; }
    }
}