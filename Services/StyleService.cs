using Repositories.Models;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class StyleService
    {
        StyleRepository repository = new();
        public List<Style> GetAll() => repository.GetAll();
    }
}
