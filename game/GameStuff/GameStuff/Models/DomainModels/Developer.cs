using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GameStuff.Models
{
    public class Developer
    {
        public int DeveloperId { get; set; }

        [Required(ErrorMessage = "Please enter developers name.")]
        [StringLength(200)]
        public string DevName { get; set; }

        [Remote("CheckDeveloper", "Validation", "Area", 
            AdditionalFields = "DevName, Operation")]

        public string FullName => $"{DevName}";

        public ICollection<GameDeveloper> GameDevelopers { get; set; }
    }
}
