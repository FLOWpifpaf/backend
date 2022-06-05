using BisnessLogicLayer.DTO;
using BisnessLogicLayer.Interfaces;
using BisnessLogicLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/boards")]
[ApiController]
public class BoardController : Controller
{
    private readonly IBoardService _boardService;
    private readonly IColumnService _columnService;
    private readonly ITaskService _taskService;

    public BoardController(IBoardService boardService, IColumnService columnService, ITaskService taskService)
    {
        _boardService = boardService;
        _columnService = columnService;
        _taskService = taskService;
    }


    [HttpGet]
    public IActionResult GetAllBoards()
    {
        List<BoardDTO> boards;
        try
        {
            boards = _boardService.GetAllBoards();
        }
        catch (Exception e)
        {
            boards = new List<BoardDTO>();
        }

        return new OkObjectResult(boards);
    }

    [HttpPost]
    public IActionResult CreateBoard(int id, string name)
    {
        try
        {
            _boardService.CreateBoard(id, name);
        }
        catch
        {
            return BadRequest();
        }

        return Ok();
    }

    [HttpGet("{id}")]
    public IActionResult GetBoard(int id)
    {
        BoardDTO board;
        try
        {
            board = _boardService.GetBoard(id);
        }
        catch
        {
            return NotFound();
        }

        return Ok(board);
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBoard(int id)
    {
        try
        {
            _boardService.RemoveBoard(id);
        }
        catch
        {
            return NotFound();
        }

        return Ok();
    }

    [HttpPost("{boardId}/columns")]
    public IActionResult CreateColumn(int boardId, int columnId, string columnName)
    {
        try
        {
            _columnService.CreateColumn(boardId, columnId, columnName);
            // _boardService.AddColumn(boardId,columnId,columnName);
            // var z = _boardService.GetBoard(boardId).Columns;
            
        }
        catch
        {
            return NotFound("No such board");
        }

        return Ok();
    }
    
    [HttpPut("{boardId}/columns")]
    public IActionResult UpdateColumn(int boardId, int columnId, string newName)
    {
        try
        {
            _columnService.UpdateColumn(columnId,newName);
        }
        catch
        {
            return NotFound("No such column");
        }
        return Ok();
    }
    
    [HttpDelete("{boardId}/columns/{columnId}")]
    public IActionResult RemoveColumn(int boardId, int columnId)
    {
        try
        {
            _columnService.RemoveColumn(columnId);
        }
        catch
        {
            return NotFound("No such column");
        }
        return Ok();
    }
    
    [HttpPost("{boardId}/tasks")]
    public IActionResult CreateTask(int boardId, int columnId, int taskId,string taskName, string taskDescription, int priority)
    {
        try
        {
            _taskService.CreateTask(boardId, columnId,taskId,taskName,taskDescription,priority);
        }
        catch
        {
            return NotFound("No such column or board");
        }
        return Ok();
    }
    
    [HttpPut("tasks/{taskId}")]
    public IActionResult UpdateTask(int taskId, string? newName, string? newDesc, int? newPrior)
    {
        try
        {
            _taskService.UpdateTask(taskId,newName,newDesc,newPrior);
        }
        catch
        {
            return NotFound("No such task or board");
        }
        return Ok();
    }
    
    [HttpDelete("{boardId}/task/{taskId}")]
    public IActionResult RemoveTask(int boardId, int taskId)
    {
        try
        {
            _taskService.RemoveTask(boardId,taskId);
        }
        catch
        {
            return NotFound("No such task or board");
        }
        return Ok();
    }
    
    [HttpPut("{boardId}/task/{taskId}")]
    public IActionResult MoveTask(int boardId, int taskId, int colToId)
    {
        try
        {
            _taskService.MoveTask(boardId,taskId,colToId);
        }
        catch
        {
            return NotFound("No such task or column");
        }
        return Ok();
    }
}