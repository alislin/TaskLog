using Microsoft.VisualStudio.TestTools.UnitTesting;
using TaskLog.DataModel;
/* Ceated by Ya Lin. 2019/10/11 10:43:12 */

using System;
using System.Collections.Generic;
using System.Text;

namespace TaskLog.DataModel.Tests
{
    [TestClass()]
    public class IdExtensionsTests
    {
        [TestMethod()]
        public void GetIdNameTest()
        {
            var title = "招生系统_20191010";
            var name = title.GetIdName();
            Assert.IsTrue(name == "招生系统");
        }
    }
}