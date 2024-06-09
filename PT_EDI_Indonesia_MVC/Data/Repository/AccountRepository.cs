using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PT_EDI_Indonesia_MVC.Core.Models;
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

        public async Task<int> GetUserBioIdAsync(string email)
        {
            var query = @"SELECT TOP 1 B.ID FROM Biodata B
                        join AspNetUsers U ON B.Email = @UEmail";

            using var connection = _context.CreateConnection();
            connection.Open();

            var userId = await connection.QuerySingleOrDefaultAsync<int>(
                query, new { UEmail = email });

            return userId;
        }
    }

    public class UserIdAndEmail
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}