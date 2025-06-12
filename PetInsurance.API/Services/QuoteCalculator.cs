using PetInsurance.API.Interface;
using PetInsurance.Shared.Request;

namespace PetInsurance.API.Services;

public class QuoteCalculator : IQuoteCalculator
{
    public decimal CalculatePremium(QuotePremiumRequest request)
    {
        const decimal basePremium = 10m;

        decimal breedMultiplier = request.Breed switch
        {
            "French Bulldog" => 1.5m,
            "Golden Retriever" => 1.2m,
            _ => 1.0m
        };

        decimal fullYearsOverSix = Math.Floor(Math.Max(0, request.Age - 6));
        decimal ageLoading = fullYearsOverSix * 1.0m;

        decimal coverMultiplier = request.CoverLevel == "Premium" ? 1.4m : 1.0m;

        decimal totalPremium = (basePremium + ageLoading) * breedMultiplier * coverMultiplier;

        return Math.Round(totalPremium, 2);
    }
}