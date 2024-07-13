namespace PT_EDI_Indonesia_MVC.Service.Accounts;

public interface IAccountRepository
{
    public Task<UserIdAndEmail> GetUserIdAndEmailAsync(string email);

}