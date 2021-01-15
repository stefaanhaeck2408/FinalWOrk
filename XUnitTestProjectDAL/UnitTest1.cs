using DL.Context;
using Microsoft.EntityFrameworkCore;
using System;
using Xunit;

namespace XUnitTestProjectDAL
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
               .UseInMemoryDatabase(databaseName: "Testing_InMemoryDatabase")
               .Options;
        }
    }
}
