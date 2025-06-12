using System.ComponentModel.DataAnnotations;

namespace PetInsurance.Shared.Models
{
    /// <summary>
    /// Represents the owner of a pet, including first and last name information.
    /// </summary>
    public class Owner
    {
        /// <summary>
        /// Gets or sets the owner's first name.
        /// </summary>
        [Required(ErrorMessage = "Owner first name is required.")]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the owner's last name.
        /// </summary>
        [Required(ErrorMessage = "Owner last name is required.")]
        public string LastName { get; set; }
    }
}