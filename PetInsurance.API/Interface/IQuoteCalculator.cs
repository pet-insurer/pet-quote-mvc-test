using PetInsurance.Shared.Request;

namespace PetInsurance.API.Interface;

public interface IQuoteCalculator
{
    decimal CalculatePremium(QuotePremiumRequest request);
}