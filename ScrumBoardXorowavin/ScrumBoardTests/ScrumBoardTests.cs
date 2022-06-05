using System.Collections.Generic;
using Xunit;
using ScrumBoardLibrary;

namespace ScrumBoardTests
{
    public class ScrumBoardTests
    {
        [Fact]
        public void Create_new_task()
        {
            ITask task = new Task(1,"Name", "Description task", 2);

            Assert.Equal("Name", task.Name);
            Assert.Equal("Description task", task.Description);
            Assert.Equal(2, task.Priority);
        }

        [Fact]
        public void Change_task_name()
        {
            ITask task = new Task(1,"Name", "Description task", 3);
            task.ChangeName("New name");

            Assert.Equal("New name", task.Name);
        }

        [Fact]
        public void Change_task_description()
        {
            ITask task = new Task(1,"Name", "Description task", 2);
            task.ChangeDescription("New description task");

            Assert.Equal("New description task", task.Description);
        }

        [Fact]
        public void Change_task_priority()
        {
            ITask task = new Task(1,"Name", "Description task", 3);
            task.ChangePriority(1);

            Assert.Equal(1, task.Priority);
        }

        [Fact]
        public void Create_new_column()
        {
            IColumn column = new Column(1,"Column name");

            Assert.Equal("Column name", column.Name);
        }

        [Fact]
        public void Add_new_task()
        {
            IColumn column = new Column(1,"Column name");
            ITask task = new Task(1,"Task name", "Description", 5);

            column.AddTask(task);
            ITask actualTask = column.GetTask("Task name");

            Assert.Equal(task, actualTask);
        }

        [Fact]
        public void Change_column_name()
        {
            IColumn column = new Column(1,"Column name");

            column.ChangeName("Column name");

            Assert.Equal("Column name", column.Name);
        }

        [Fact]
        public void Delete_task_in_column()
        {
            IColumn column = new Column(1,"Column name");
            ITask task = new Task(1,"Name", "Description", 1);

            column.AddTask(task);
            column.DeleteTask(task);
            ITask actualTask = column.GetTask("Name");

            Assert.NotEqual(task, actualTask);
        }

        [Fact]
        public void Clear_the_column()
        {
            IColumn column = new Column(1,"Column name");
            ITask task = new Task(1,"Task name", "Description task", 2);

            column.AddTask(task);
            column.Clear();
            List<ITask> expectedTasks = new List<ITask>();
            List<ITask> tasks = column.GetAllTasks();

            Assert.Equal(expectedTasks, tasks);
        }

        [Fact]
        public void Create_new_board()
        {
            IBoard newBoard = new Board(1,"Board name");
            List<IColumn> colmns = new List<IColumn>();

            Assert.Equal("Board name", newBoard.Name);
            Assert.Equal(colmns, newBoard.Columns);
        }

        [Fact]
        public void Add_new_column()
        {
            IBoard board = new Board(1,"Board name");

            board.AddColumn(1,"Column first");

            Assert.Single(board.Columns);
        }

        [Fact]
        public void Add_new_task_to_first_column()
        {
            IBoard board = new Board(1,"Board name");
            ITask task = new Task(1,"Task name", "Description", 1);

            board.AddColumn(1,"abc");
            board.AddTask(task);

            Assert.Equal(task, board.Columns[0].GetTask("Task name"));
        }

        [Fact]
        public void Move_task()
        {
            IBoard board = new Board(1,"Board name");
            ITask task = new Task(1,"Task name", "Description", 1);

            board.AddColumn(1,"Column first");
            board.AddColumn(1,"Column second");
            board.AddTask(task);

            Assert.Equal(task, board.Columns[0].GetTask("Task name"));
            Assert.NotEqual(task, board.Columns[1].GetTask("Task name"));

            board.MoveTask(board.Columns[0], board.Columns[1], task);

            Assert.Equal(task, board.Columns[1].GetTask("Task name"));
            Assert.NotEqual(task, board.Columns[0].GetTask("Task name"));
        }
    }
}
