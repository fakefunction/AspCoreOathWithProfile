namespace CalculatorLib.AspCoreOathWithProfile.Web.Services
{
    using CalculatorLib.AspCoreOathWithProfile.Web.Models;
    using System.Threading.Tasks;

    public interface IProfileService
    {
        Task CreateAsync(Profile profile);
        Task<Profile> RetrieveAsync(string username);
    }
}
