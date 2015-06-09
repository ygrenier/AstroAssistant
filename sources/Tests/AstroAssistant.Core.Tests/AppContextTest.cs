using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AstroAssistant.Core.Tests
{
    public class AppContextTest
    {
        [Fact]
        public void TestBuild()
        {
            var ioe = Assert.Throws<InvalidOperationException>(() => AppContext.Current);
            Assert.Equal("Le constructeur de contexte d'application n'est pas défini.", ioe.Message);

            AppContext.Build(() => null);
            ioe = Assert.Throws<InvalidOperationException>(() => AppContext.Current);
            Assert.Equal("Echec de création d'un contexte d'application.", ioe.Message);

            var mockContext = new Mock<AppContext>();
            var context1 = mockContext.Object;
            AppContext.Build(() => context1);
            Assert.Same(context1, AppContext.Current);

            mockContext = new Mock<AppContext>();
            var context2 = mockContext.Object;
            AppContext.Build(() => context2);
            Assert.NotSame(context2, AppContext.Current);
            Assert.Same(context1, AppContext.Current);

            Assert.Throws<ArgumentNullException>(() => AppContext.Build(null));
        }
    }
}
