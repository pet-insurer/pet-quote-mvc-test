namespace PetInsurance.Shared.Response;

/// <summary>
/// Represents the response containing the insurance premium quote.
/// </summary>
public class QuoteResponse
{
    /// <summary>
    /// The calculated premium for the insurance quote.
    /// </summary>
    public decimal Premium { get; set; }
}