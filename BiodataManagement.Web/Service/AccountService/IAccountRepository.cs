using BiodataManagement.Domain.Entities;

namespace BiodataManagement.Service.Accounts;

public interface IAccountRepository
{
    public Task<AppUserBiodata> GetUserIdAndEmailAsync(string email);

}