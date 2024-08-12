using BiodataManagement.Data.Identity;

namespace BiodataManagement.Service.AccountService;

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
