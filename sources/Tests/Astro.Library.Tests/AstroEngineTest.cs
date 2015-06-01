using Moq;
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
            var mockProvider = new Mock<IEphemerisProvider>();
            var provider = mockProvider.Object;

            using (var engine = new AstroEngine(provider))
            {
                Assert.Same(provider, engine.EphemerisProvider);
            }

            mockProvider.Verify(p => p.Dispose(), Times.Once());

            Assert.Throws<ArgumentNullException>(() => new AstroEngine(null));

        }
    }
}
