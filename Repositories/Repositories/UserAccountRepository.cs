using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class UserAccountRepository
    {
        public UserAccount Login(string email, string password)
        {
            try
            {
                var _context = new WatercolorsPainting2024DbContext();

                var result = _context.UserAccounts.FirstOrDefault(c => c.UserEmail.Equals(email) && c.UserPassword.Equals(password));

                if (result == null) throw new Exception("This action is failed");

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
