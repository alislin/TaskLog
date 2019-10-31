using System;
using System.ComponentModel.DataAnnotations;

namespace TaskLog.DataModel
{
    public class OperatorUser:ICreated
    {
        [Key]
        public string Key { get; set; }
        public DateTime Created { get; set; }
        public string Creator { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
