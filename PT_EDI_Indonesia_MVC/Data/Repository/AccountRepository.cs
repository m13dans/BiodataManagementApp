using Dapper;
using PT_EDI_Indonesia_MVC.Data.Context;

namespace PT_EDI_Indonesia_MVC.Data.Repository
{
    public class AccountRepository
    {
        private readonly DapperContext _context;
        public AccountRepository(DapperContext context)
        {
            _context = context;

        }

        public async Task<UserIdAndEmail> GetUserIdAndEmailAsync(string email)
        {
            var query = @"SELECT B.ID, B.EMAIL FROM Biodata b
                        join AspNetUsers U ON B.Email = @UEmail";

            using var connection = _context.CreateConnection();
            connection.Open();

            var biodatas = await connection.QueryAsync<UserIdAndEmail>(
                query, new { @UEmail = email });

            return biodatas.FirstOrDefault();
        }
    }

    public class UserIdAndEmail
    {
        public int Id { get; set; }
        public string Email { get; set; }
    }
}