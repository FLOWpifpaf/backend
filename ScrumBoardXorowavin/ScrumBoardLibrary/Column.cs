namespace ScrumBoardLibrary;

public class Column : IColumn
{
    public int Id { get; set; }

    public string ColumnName { get; set; }
    private List<ITask> _tasks;

    public Column(int id, string name)
    {
        this.Id = id;
        ColumnName = name;
        _tasks = new List<ITask>();
    }

    public void ChangeName(string name)
    {
        ColumnName = name;
    }

    public void AddTask(ITask task)
    {
        _tasks.Add(task);
    }

    public ITask GetTask(string name)
    {
        return _tasks.Find(x => x.Name == name);
    }

    public List<ITask> GetAllTasks()
    {
        return _tasks;
    }

    public void DeleteTask(ITask task)
    {
        _tasks.Remove(task);
    }

    public void Clear()
    {
        _tasks.Clear();
    }

}
