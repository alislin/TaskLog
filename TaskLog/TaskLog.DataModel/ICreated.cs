using System;
using System.ComponentModel.DataAnnotations;

namespace TaskLog.DataModel
{
    public interface ICreated
    {
        [Key]
        string Key { get; set; }
        DateTime Created { get; set; }
        string Creator { get; set; }
    }
}
