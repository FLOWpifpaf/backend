namespace ScrumBoardLibrary;

public interface IColumn
{
    public string ColumnName { get; set; }
    public void AddTask(ITask task);
    public void DeleteTask(ITask task);
    public void ChangeName(string name);
    public void Clear();
    public ITask GetTask(string name);
    public List<ITask> GetAllTasks();
}
