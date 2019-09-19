using System;

namespace TaskLog.DataModel
{
    public interface ICreated
    {
        string Key { get; set; }
        DateTime Created { get; set; }
        string Creator { get; set; }
    }
}
