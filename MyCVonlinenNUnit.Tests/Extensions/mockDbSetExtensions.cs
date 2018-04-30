using Moq;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MyCVonlinenNUnit.Tests.Extensions
{
    public static class mockDbSetExtensions
    {
        //MOCKING DBSet of T, so I can call it when I need to mock a DbSet of jobs or qualification or whatever.
        public static void setSource<T>(this Mock<DbSet<T>> mockSet, IList<T> source) where T : class
        {
            var data = source.AsQueryable();


            mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        }
    }
}
