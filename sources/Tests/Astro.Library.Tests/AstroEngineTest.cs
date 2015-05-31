using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Astro.Library.Tests
{
    public class AstroEngineTest
    {
        [Fact]
        public void TestCreate()
        {
            var engine = new AstroEngine();
        }
    }
}
