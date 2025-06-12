using Microsoft.Extensions.Logging;
using PetInsurance.API.Interface;
using PetInsurance.Shared.Models;

namespace PetInsurance.API.Services;

public class PolicyStoreService : IPolicyStoreService
{
    private static readonly List<Policy> _policies = new();
    private readonly ILogger<PolicyStoreService> _logger;

    public PolicyStoreService(ILogger<PolicyStoreService> logger)
    {
        _logger = logger;
    }

    public void Add(Policy policy)
    {
        if (policy == null)
        {
            _logger.LogWarning("Attempted to add null policy");
            return;
        }

        _logger.LogInformation("Adding new policy for pet: {PetName}, Owner: {OwnerName}",
            policy.Request.Pet.Name,
            $"{policy.Request.Owner.FirstName} {policy.Request.Owner.LastName}");

        _policies.Add(policy);
        _logger.LogInformation("Total policies in store: {Count}", _policies.Count);
    }

    public List<Policy> GetAll()
    {
        _logger.LogInformation("Retrieving all policies. Count: {Count}", _policies.Count);
        return _policies;
    }
}
