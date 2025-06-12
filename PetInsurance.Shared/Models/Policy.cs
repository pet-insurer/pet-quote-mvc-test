using PetInsurance.Shared.Request;

namespace PetInsurance.Shared.Models
{
    public class Policy
    {
        // Policy unique identifier
        public Guid Id { get; set; } = Guid.NewGuid();

        // Quote request related to the policy
        public QuoteRequest Request { get; set; }

        // Premium amount for the policy
        public decimal Premium { get; set; }
    }
}
