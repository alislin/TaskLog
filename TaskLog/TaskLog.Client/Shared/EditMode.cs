using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskLog.Client.Shared
{
    /// <summary>
    /// 编辑模式
    /// </summary>
    public enum EditMode
    {
        /// <summary>
        /// 查看
        /// </summary>
        View,
        /// <summary>
        /// 编辑
        /// </summary>
        Edit,
        /// <summary>
        /// 创建
        /// </summary>
        Create
    }
}
