using Microsoft.Extensions.Caching.Memory;
using ScrumBoardLibrary;
using Task = ScrumBoardLibrary.Task;

namespace DataAccessLayer.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly IBoardRepository _boardRepository;

    public TaskRepository(IMemoryCache memoryCache, IBoardRepository boardRepository)
    {
        _memoryCache = memoryCache;
        _boardRepository = boardRepository;
    }

    public List<ITask> GetAllTasks()
    {
        _memoryCache.TryGetValue("tasks", out List<ITask> tasks);
        return tasks;
    }

    public ITask Get(int id)
    {
        return GetAllTasks().Find(t => t.Id == id);
    }

    public void Create(int boardId, int id, string name, string desc, int prior)
    {
        var tasks = GetAllTasks();

        if (tasks is null) tasks = new List<ITask>();

        var task = new Task(id, name, desc, prior);
        tasks.Add(task);

        var board = _boardRepository.Get(boardId);
        board.AddTask(task);


        _memoryCache.Set("tasks", tasks);
    }

    public void Remove(int boardId, int id)
    {
        var tasks = GetAllTasks();
        for (int i = 0; i < tasks.Count; i++)
            if (tasks[i].Id == id)
            {
                tasks.RemoveAt(i);
                break;
            }

        foreach (var col in _boardRepository.Get(boardId).Columns)
        {
            var task = col.GetAllTasks().Find(t => t.Id == id);
            if (task is not null)
                col.GetAllTasks().Remove(task);
        }
        
        

        _memoryCache.Set("tasks", tasks);
    }

    public void Update(int taskId, string? newName, string? newDesc, int? newPrior)
    {
        var task = Get(taskId);
        task.Name = newName ?? task.Name;
        task.Description = newDesc ?? task.Description;
        task.Priority = newPrior ?? task.Priority;
    }

    public void Move(int boardId, int taskId, int colToId)
    {
        var task = Get(taskId);
        Remove(boardId,taskId);
        var col = _boardRepository.Get(boardId).Columns.Find(c => c.Id == colToId);
        col.AddTask(task);
        
    }
}