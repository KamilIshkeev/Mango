using MangoApi.Models;
using static MangoApi.Interfaces.IPassinglvl;

namespace MangoApi.Interfaces
{
    
        public interface IPassinglvl
        {
            IEnumerable<Passinglvl> GetAllPassinglvls();
            Passinglvl GetPassinglvlById(int id);
            Passinglvl GetPassinglvlByUserId(int user_id, int lvl_id);
            Passinglvl AddPassinglvl(Passinglvl Passinglvl);
            Passinglvl UpdatePassinglvl(int id, Passinglvl updatedPassinglvl);
            void DeletePassinglvl(int id);


        }
    
}
