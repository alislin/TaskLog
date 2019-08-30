namespace TaskLog.DataModel
{
    public interface IProject : ICreated, IPriority
    {
        string Id { get; set; }
        string Name { get; set; }
    }
}
