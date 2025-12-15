namespace Api.Models
{
    public class MyTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateOnly EndOfTask { get; set; }
        public Status Status { get; set; }

    }

    public enum Status
    {
        Completed,
        During,
        Cancelled
    }
}
