// <copyright file="ObjectExt.cs" author="linya">
// Create time：       2019/9/16 16:09:46
// </copyright>

using Microsoft.AspNetCore.Mvc;

namespace TaskLog.Libs
{
    public static class ObjectExt
    {
        public static JsonResult ToJsonResult(this object obj)
        {
            var ret = new JsonResult(obj);
            return ret;
        }
    }
}
