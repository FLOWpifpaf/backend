using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public interface IColumnRepository
{
    List<IColumn> GetAllColumns();
    IColumn Get(int id);
    void Create(int id, string name);
    void Remove(int id);
    void Update(int id, string newName);
}