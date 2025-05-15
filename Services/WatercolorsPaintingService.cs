using Repositories.Models;
using Repositories.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class WatercolorsPaintingService
    {
        WatercolorsPaintingRepository repository = new();

        public List<WatercolorsPainting> GetAll() => repository.GetAll();

        public void Add(WatercolorsPainting model) => repository.Add(model);

        public void Remove(string id) => repository.Delete(id);

        public void Update(string id, WatercolorsPainting model) => repository.Update(id, model);
    }
}
