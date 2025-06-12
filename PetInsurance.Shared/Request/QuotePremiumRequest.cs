namespace PetInsurance.Shared.Request
{
    public class QuotePremiumRequest
    {
        /// <summary>
        /// Age of the pet in years.
        /// </summary>
        public decimal Age { get; set; }

        /// <summary>
        /// Breed of the pet (e.g., Labrador, Persian Cat).
        /// </summary>
        public string Breed { get; set; } = string.Empty;

        /// <summary>
        /// Desired cover level (e.g., Basic, Standard, Premium).
        /// </summary>
        public string CoverLevel { get; set; } = string.Empty;
    }
}