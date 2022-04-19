namespace ScrumBoardLibrary;

public class Board : IBoard
{
    int MAX = 10;

    public List<IColumn> Columns { get; set; }

    public string Name { get; set; }

    public Board(string name)
    {
        Name = name;
        Columns = new List<IColumn>();
    }

    public void AddColumn(string name)
    {
        if (Columns.Count < MAX)
        {
            IColumn newColumn = new Column(name);
            Columns.Add(newColumn);
        }
    }

    public void AddTask(ITask task)
    {
        IColumn column = Columns[0];
        column.AddTask(task);
    }

    public void MoveTask(IColumn columnOne, IColumn columnTwo, ITask task)
    {
        columnOne.DeleteTask(task);
        columnTwo.AddTask(task);
    }
}