using System.ComponentModel.DataAnnotations;

namespace PetInsurance.Shared.Models
{
    public class Pet
    {
        // Ensure Name is not empty
        [Required(ErrorMessage = "Pet name is required")]
        public string Name { get; set; }

        // Ensure Breed is not empty and has a minimum length
        [Required(ErrorMessage = "Breed is required")]
        public string Breed { get; set; }

        // Age is required and should be between 0 and 100 (inclusive)
        [Required(ErrorMessage = "Pet age is required")]
        [Range(0, 100, ErrorMessage = "Pet age must be under 100.")]
        public decimal Age { get; set; }
    }
}