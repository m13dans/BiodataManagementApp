using Dapper;
using PT_EDI_Indonesia_MVC.Data.Context;
using PT_EDI_Indonesia_MVC.Domain.Entities;
using PT_EDI_Indonesia_MVC.Service.Accounts;

namespace PT_EDI_Indonesia_MVC.Data.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly DapperContext _context;
        public AccountRepository(DapperContext context)
        {
            _context = context;

        }

        public async Task<AppUserBiodata> GetUserIdAndEmailAsync(string email)
        {
            var query = @"SELECT B.ID, B.EMAIL FROM Biodata b
                        join AspNetUsers U ON B.Email = @UEmail";

            using var connection = _context.CreateConnection();
            connection.Open();

            var biodatas = await connection.QueryAsync<AppUserBiodata>(
                query, new { @UEmail = email });

            return biodatas.FirstOrDefault();
        }
    }


}