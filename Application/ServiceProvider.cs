using Application.Services;

namespace Application;
public class ServiceProvider
{
    public IAccountService _accountService { get; set; }
    public IBusinessTripService _businessTripService { get; set; }
    public IProfileService _profileService { get; set; }
    public ServiceProvider(IAccountService accountService, IBusinessTripService businessTripService, IProfileService profileService)
    {
        _accountService = accountService;
        _businessTripService = businessTripService;
        _profileService = profileService;
    }
}
