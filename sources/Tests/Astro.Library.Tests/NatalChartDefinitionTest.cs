using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class NatalChartDefinitionTest
    {
        [Fact]
        public void TestCreate()
        {
            var theme = new NatalChartDefinition();
            Assert.NotNull(theme.BirthDate);
        }
    }
}
