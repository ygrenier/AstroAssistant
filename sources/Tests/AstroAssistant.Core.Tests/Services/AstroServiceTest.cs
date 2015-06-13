using AstroAssistant.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AstroAssistant.Core.Tests
{
    public class AstroServiceTest
    {
        [Fact]
        public void TestCreateAndDispose()
        {
            var fsMock = new Mock<IEphemerisService>();
            var fs = fsMock.Object;
            AstroService astro = null;
            using (astro = new AstroService(fs))
            {
                Assert.NotNull(astro.AstroEngine);
                Assert.Same(fs, astro.AstroEngine.EphemerisProvider);
            }
            Assert.Null(astro.AstroEngine);
        }
    }
}
