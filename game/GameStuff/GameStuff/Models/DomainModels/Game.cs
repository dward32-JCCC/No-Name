using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStuff.Models
{
    public partial class Game
    {
        public int GameId { get; set; }

        [Required(ErrorMessage = "Please enter a title.")]
        [StringLength(200)]
        public string Title { get; set; }

        [Range(0.0, 1000000.0, ErrorMessage = "Price must be more than 0.")]
        public double Price { get; set; }

        [Required(ErrorMessage = "Please select a genre.")]
        public string GenreId { get; set; }

        public Genre Genre { get; set; }
        public ICollection<GameDeveloper> GameDevelopers { get; set; }
    }
}
