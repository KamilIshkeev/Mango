using MangoApi.DataBaseContext;
using MangoApi.Interfaces;
using MangoApi.Models;

namespace MangoApi.Services
{
    
        public class PassinglvlService : IPassinglvl
        {
            private readonly MangoDbContext _context;

            public PassinglvlService(MangoDbContext context)
            {
                _context = context;
            }

            public IEnumerable<Passinglvl> GetAllPassinglvls()
            {
                return _context.Passinglvl.ToList();
            }

            public Passinglvl GetPassinglvlById(int id)
            {
                return _context.Passinglvl.FirstOrDefault(m => m.Id == id);
            }

            public Passinglvl GetPassinglvlByUserId(int user_id, int lvl_id)
            {
                return _context.Passinglvl.FirstOrDefault(m => m.User_Id == user_id && m.Levels_Id == lvl_id);
            }


            public Passinglvl AddPassinglvl(Passinglvl Passinglvl)
            {
                _context.Passinglvl.Add(Passinglvl);
                _context.SaveChanges();
                return Passinglvl;
            }

            public Passinglvl UpdatePassinglvl(int id, Passinglvl updatedPassinglvl)
            {
                var Passinglvl = _context.Passinglvl.FirstOrDefault(m => m.Id == id);
                if (Passinglvl == null) return null;

                Passinglvl.CountStars = updatedPassinglvl.CountStars;
                _context.SaveChanges();
                return Passinglvl;
            }

            public void DeletePassinglvl(int id)
            {
                var Passinglvl = _context.Passinglvl.FirstOrDefault(m => m.Id == id);
                if (Passinglvl != null)
                {
                    _context.Passinglvl.Remove(Passinglvl);
                    _context.SaveChanges();
                }
            }
        }
    
}
