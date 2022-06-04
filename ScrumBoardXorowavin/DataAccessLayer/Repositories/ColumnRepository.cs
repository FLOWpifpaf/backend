using Microsoft.Extensions.Caching.Memory;
using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public class ColumnRepository : IColumnRepository
{
    private readonly IMemoryCache _memoryCache;
    private readonly IBoardRepository _boardRepository;

    public ColumnRepository(IMemoryCache memoryCache, IBoardRepository boardRepository)
    {
        _memoryCache = memoryCache;
        _boardRepository = boardRepository;
    }

    public List<IColumn> GetAllColumns()
    {
        _memoryCache.TryGetValue("columns", out List<IColumn> columns);
        return columns;
    }

    public IColumn Get(int id)
    {
        return GetAllColumns().Find(c => c.Id == id);
    }

    public void Create(int id, string name)
    {
        var columns = GetAllColumns();

        if (columns is null) columns = new List<IColumn>();

        var col = new Column(id, name);
        columns.Add(col);

        _memoryCache.Set("columns", columns);
    }

    public void Remove(int id)
    {
        var columns = GetAllColumns();
        for (int i = 0; i < columns.Count; i++)
            if (columns[i].Id == id)
            {
                columns.RemoveAt(i);
                break;
            }

        foreach (var board in _boardRepository.GetAllBoards())
        {
            var col = board.Columns.Find(c => c.Id == id);
            if (col != null)
            {
                board.Columns.Remove(col);
                break;
            }
        }


        _memoryCache.Set("columns", columns);
    }

    public void Update(int id, string newName)
    {
        Get(id).ColumnName = newName;

        foreach (var board in _boardRepository.GetAllBoards())
        {
            var col = board.Columns.Find(c => c.Id == id);
            if (col is not null)
                col.ColumnName = newName;
        }
    }
}