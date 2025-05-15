using Microsoft.EntityFrameworkCore;
using Repositories.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Repositories
{
    public class WatercolorsPaintingRepository
    {
        public List<WatercolorsPainting> GetAll()
        {
            try
            {
                var context = new WatercolorsPainting2024DbContext();
                return context.WatercolorsPaintings.Include(c => c.Style).ToList();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Update(string id, WatercolorsPainting model)
        {
            try
            {
                model.PaintingId = id;

                var _context = new WatercolorsPainting2024DbContext();

                var result = _context.WatercolorsPaintings.AsNoTracking().FirstOrDefault(c => c.PaintingId == id);

                if (result == null)
                    throw new Exception("This id is not existed");

                result = model;

                _context.WatercolorsPaintings.Update(result);

                if (_context.SaveChanges() == 0) throw new Exception("This action is failed");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Delete(string id)
        {
            try
            {
                var _context = new WatercolorsPainting2024DbContext();

                var result = _context.WatercolorsPaintings.AsNoTracking().FirstOrDefault(c => c.PaintingId == id);

                if (result == null)
                    throw new Exception("This id is not existed");

                _context.WatercolorsPaintings.Remove(result);

                if (_context.SaveChanges() == 0) throw new Exception("This action is failed");
            }
            catch (Exception)
            {

                throw;
            }
        }

        public void Add(WatercolorsPainting model)
        {
            try
            {
                var _context = new WatercolorsPainting2024DbContext();

                var result = _context.WatercolorsPaintings.Find(model.PaintingId);

                if (result != null) throw new Exception("This id is already existed");

                _context.WatercolorsPaintings.Add(model);

                if (_context.SaveChanges() == 0) throw new Exception("This action is failed");
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
