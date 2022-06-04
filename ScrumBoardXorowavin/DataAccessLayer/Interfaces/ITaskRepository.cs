using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public interface ITaskRepository
{
    List<ITask> GetAllTasks();
    ITask Get(int id);
    void Create(int boardId, int id, string name, string desc, int prior);
    void Remove(int boardId,int id);
    void Update( int taskId, string? newName, string? newDesc, int? newPrior);
    void Move(int boardId, int taskId, int colToId);
}