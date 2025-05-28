using MangoApi.Models;

namespace MangoApi.Interfaces
{
    public interface ILevels
    {
        IEnumerable<Levels> GetAllLevels();
        Levels GetLevelsById(int id);
        Levels GetLevelsByTitle(string title);
        Levels AddLevels(Levels Levels);
        Levels UpdateLevels(int id, Levels updatedLevels);
        void DeleteLevels(int id);
    }
}
