using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class StyleRepository
    {
        public List<Style> GetAll()
        {
            try
            {
                var context = new WatercolorsPainting2024DbContext();
                return context.Styles.ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
