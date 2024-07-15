using PT_EDI_Indonesia_MVC.Domain.Entities;

namespace PT_EDI_Indonesia_MVC.Service.Accounts;

public interface IAccountRepository
{
    public Task<AppUserBiodata> GetUserIdAndEmailAsync(string email);

}