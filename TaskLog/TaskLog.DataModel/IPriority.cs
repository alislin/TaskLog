namespace TaskLog.DataModel
{
    public interface IPriority
    {
        int Priority { get; set; }
        int Point { get; set; }
        /// <summary>
        /// 实际得分
        /// </summary>
        int EndPoint { get; set; }
    }
}
