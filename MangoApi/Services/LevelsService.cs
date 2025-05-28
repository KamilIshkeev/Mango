using MangoApi.DataBaseContext;
using MangoApi.Interfaces;
using MangoApi.Models;

namespace MangoApi.Services
{
    public class LevelsService : ILevels
    {
            private readonly MangoDbContext _context;

            public LevelsService(MangoDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Levels> GetAllLevels()
            {
                return _context.Levels.ToList();
            }

            public Levels GetLevelsById(int id)
            {
                return _context.Levels.FirstOrDefault(m => m.Id == id);
            }

            public Levels GetLevelsByTitle(string title)
            {
                return _context.Levels.FirstOrDefault(m => m.Name == title);
            }


            public Levels AddLevels(Levels Levels)
            {
                _context.Levels.Add(Levels);
                _context.SaveChanges();
                return Levels;
            }

            public Levels UpdateLevels(int id, Levels updatedLevels)
            {
                var Levels = _context.Levels.FirstOrDefault(m => m.Id == id);
                if (Levels == null) return null;

                Levels.Name = updatedLevels.Name;
                _context.SaveChanges();
                return Levels;
            }

            public void DeleteLevels(int id)
            {
                var Levels = _context.Levels.FirstOrDefault(m => m.Id == id);
                if (Levels != null)
                {
                    _context.Levels.Remove(Levels);
                    _context.SaveChanges();
                }
            }
        
    }
}
