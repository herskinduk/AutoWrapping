using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using AutoWrapping;

namespace AutoWrapping.Tests
{
    public class RoslynTypeExplorerTests
    {
        [Fact(Skip = "true")]
        public void Explore_TypeIsNotNull_ReturnsMetadata()
        {
            var sut = new RoslynTypeExplorer();

            var result = sut.Explore(typeof(Sitecore.Configuration.Factory));

            Assert.NotNull(result);
        }

        [Fact(Skip = "true")]
        public void Explore_TypeIsNotNull_ReturnsMetadata2()
        {
            var sut = new RoslynTypeExplorer();

            var result = sut.BuildInterface();

            Assert.NotNull(result);
        }
    }
}
