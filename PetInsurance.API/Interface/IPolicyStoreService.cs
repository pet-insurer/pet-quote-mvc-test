using PetInsurance.Shared.Models;

namespace PetInsurance.API.Interface;

public interface IPolicyStoreService
{
    void Add(Policy policy);
    List<Policy> GetAll();
}