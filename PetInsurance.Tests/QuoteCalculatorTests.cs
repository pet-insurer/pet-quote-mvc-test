using PetInsurance.API.Interface;
using PetInsurance.API.Services;
using PetInsurance.Shared.Request;

namespace PetInsurance.Tests;

public class QuoteCalculatorTests
{
    private readonly IQuoteCalculator _calculator = new QuoteCalculator();

    [Fact]
    public void CalculatePremium_AgeExactlySix_ProducesNoAgeLoading()
    {
        var req = new QuotePremiumRequest { Breed = "Other", Age = 6, CoverLevel = "Standard" };

        var premium = _calculator.CalculatePremium(req);

        Assert.Equal(10.00m, premium);
    }

    [Fact]
    public void CalculatePremium_CoverLevelCaseSensitivity_OtherValuesTreatedAsStandard()
    {
        var req = new QuotePremiumRequest { Breed = "Other", Age = 7, CoverLevel = "premium" };

        var premium = _calculator.CalculatePremium(req);

        Assert.Equal(11.00m, premium);
    }

    [Fact]
    public void CalculatePremium_EmptyCoverLevel_TreatedAsStandard()
    {
        var req = new QuotePremiumRequest { Breed = "Other", Age = 5, CoverLevel = "" };
        var premium = _calculator.CalculatePremium(req);
        Assert.Equal(10.00m, premium);
    }

    [Fact]
    public void CalculatePremium_NegativeAge_ReturnsBasePremiumOnly()
    {
        var req = new QuotePremiumRequest { Breed = "Other", Age = -5, CoverLevel = "Standard" };
        var premium = _calculator.CalculatePremium(req);
        Assert.Equal(10.00m, premium);
    }

    [Fact]
    public void CalculatePremium_HighAgeValue_ComputesAgeLoading()
    {
        var req = new QuotePremiumRequest { Breed = "Other", Age = 100, CoverLevel = "Premium" };
        var premium = _calculator.CalculatePremium(req);
        Assert.Equal(145.60m, premium);
    }

    [Fact]
    public void CalculatePremium_SpecificBreed_ReturnsPremium()
    {
        var req = new QuotePremiumRequest { Breed = "French Bulldog", Age = 5, CoverLevel = "Standard" };
        var premium = _calculator.CalculatePremium(req);
        Assert.Equal(15.00m, premium);
    }
}