using BiodataManagement.Data.Identity;
using BiodataManagement.Service.AccountService;

namespace BiodataManagement.Service.Accounts.AccountService;

public class AccountService
{
    private readonly IAccountRepository _repo;
    public AccountService(IAccountRepository repo)
    {
        _repo = repo;
    }

}

public static class AccountServiceExtensions
{
    public static AppUser MapSignUpToUser(this SignupDTO model)
    {
        return new AppUser
        {
            NamaLengkap = model.NamaLengkap,
            UserName = model.Email,
            Email = model.Email,
        };
    }
}
