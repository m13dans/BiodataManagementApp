using PT_EDI_Indonesia_MVC.Domain.Entities;
using PT_EDI_Indonesia_MVC.Service.AccountService;

namespace PT_EDI_Indonesia_MVC.Service.Accounts.AccountService;

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
    public static User MapSignUpToUser(this SignupDTO model)
    {
        return new User
        {
            NamaLengkap = model.NamaLengkap,
            UserName = string.Empty,
            Email = model.Email,
        };
    }
}
