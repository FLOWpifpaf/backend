using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public interface IBoardRepository
{
    List<IBoard> GetAllBoards();
    IBoard Get(int id);
    void Create(int id, string name);
    void Remove(int id);
}