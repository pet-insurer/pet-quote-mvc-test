using PetInsurance.Shared.Models;

namespace PetInsurance.Shared.Request;

/// <summary>
/// Represents a request for an insurance quote.
/// </summary>
public class QuoteRequest
{
    /// <summary>
    /// The pet to be insured.
    /// </summary>
    public Pet Pet { get; set; }

    /// <summary>
    /// The owner of the pet.
    /// </summary>
    public Owner Owner { get; set; }

    /// <summary>
    /// The level of insurance coverage requested.
    /// </summary>
    public string CoverLevel { get; set; }
}