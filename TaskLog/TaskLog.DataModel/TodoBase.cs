﻿using System;

namespace TaskLog.DataModel
{
    public class TodoBase:IProject
    {
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime Created { get; set; }
        /// <summary>
        /// 任务ID({任务名称}_{启动年月日})
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime Start { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime End { get; set; }
        /// <summary>
        /// 计划
        /// </summary>
        public string Plan { get; set; }
        /// <summary>
        /// 目标
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 得分
        /// </summary>
        public int Point { get; set; }
        /// <summary>
        /// 实际得分
        /// </summary>
        public int EndPoint { get; set; }

        public TodoBase()
        {
        }

        public TodoBase(string name)
        {
            Created = DateTime.Now;
            if (name != null)
            {
                Name = name;
                Id = this.MakeId();
            }
        }

    }

    public static class IdExtensions
    {
        public static string MakeId(this IProject value)
        {
            var n = value?.Name;
            var t = value?.Created ?? DateTime.Now;
            return $"{n}_{t.ToString("yyyyMMdd")}";
        }
    }

}
