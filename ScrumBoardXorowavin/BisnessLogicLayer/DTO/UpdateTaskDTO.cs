namespace BisnessLogicLayer.DTO;

public class UpdateTaskDTO
{
    public int Id { get; set; }
    public string NewName { get; set; }
    public string NewDescription { get; set; }
    public int NewPriority { get; set; }
}