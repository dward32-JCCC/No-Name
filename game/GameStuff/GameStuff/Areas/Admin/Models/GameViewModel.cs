using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace GameStuff.Models
{
    public class GameViewModel : IValidatableObject
    {
        public Game Game { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
        public IEnumerable<Developer> Developers { get; set; }
        public int[] SelectedDevelopers { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext ctx) {
            if (SelectedDevelopers == null)
            {
                yield return new ValidationResult(
                  "Please select at least one developer.",
                  new[] { nameof(SelectedDevelopers) });
            }
        }

    }
}
