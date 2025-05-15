using Repositories.Models;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class UserAccountService
    {
        UserAccountRepository repository = new();

        public UserAccount Login(string email, string password) => repository.Login(email, password.Trim());
    }
}
