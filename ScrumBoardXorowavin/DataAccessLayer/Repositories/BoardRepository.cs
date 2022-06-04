using Microsoft.Extensions.Caching.Memory;
using ScrumBoardLibrary;

namespace DataAccessLayer.Repositories;

public class BoardRepository : IBoardRepository
{
    private readonly IMemoryCache _memoryCache;

    public BoardRepository(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public List<IBoard> GetAllBoards()
    {
        _memoryCache.TryGetValue("boards", out List<IBoard> boards);
        return boards;
    }

    public IBoard Get(int id)
    {
        return GetAllBoards().Find(b => b.Id == id);
    }

    public void Create(int id, string name)
    {
        var boards = GetAllBoards();

        if (boards is null) boards = new List<IBoard>();

        var board = new Board(id, name);
        boards.Add(board);

        _memoryCache.Set("boards", boards);
    }

    public void Remove(int id)
    {
        var boards = GetAllBoards();
        for (int i = 0; i < boards.Count; i++)
            if (boards[i].Id == id)
            {
                boards.RemoveAt(i);
                break;
            }

        _memoryCache.Set("boards", boards);
    }
}