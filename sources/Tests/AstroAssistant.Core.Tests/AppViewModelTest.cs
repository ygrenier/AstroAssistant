using Astro;
using AstroAssistant.ViewModels;
using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AstroAssistant.Core.Tests
{
    public class AppViewModelTest
    {
        [Fact]
        public void TestCreate()
        {
            //var provider = new Mock<IEphemerisProvider>();
            var avm = new Mock<AppViewModel>();

            var viewmodel = avm.Object;
            Assert.Null(viewmodel.AstroEngine);
        }

        [Fact]
        public void TestInitialize()
        {
            var mockProvider = new Mock<IEphemerisProvider>();
            var provider = mockProvider.Object;

            var mockViewmodel = new Mock<AppViewModel>();
            mockViewmodel.Protected().Setup<IEphemerisProvider>("CreateEphemerisProvider").Returns(provider);

            // Not initialized
            var viewmodel = mockViewmodel.Object;
            Assert.Null(viewmodel.AstroEngine);

            // First initialization
            viewmodel.Initialize();
            Assert.NotNull(viewmodel.AstroEngine);
            Assert.Same(provider, viewmodel.AstroEngine.EphemerisProvider);


            // Second initialization change nothing
            viewmodel.Initialize();
            Assert.NotNull(viewmodel.AstroEngine);
            Assert.Same(provider, viewmodel.AstroEngine.EphemerisProvider);

        }

        [Fact]
        public void TestClose()
        {
            var mockProvider = new Mock<IEphemerisProvider>();
            var provider = mockProvider.Object;

            var mockViewmodel = new Mock<AppViewModel>();
            mockViewmodel.Protected().Setup<IEphemerisProvider>("CreateEphemerisProvider").Returns(provider);

            // Not initialized
            var viewmodel = mockViewmodel.Object;
            Assert.Null(viewmodel.AstroEngine);

            // First initialization
            viewmodel.Initialize();
            Assert.NotNull(viewmodel.AstroEngine);
            Assert.Same(provider, viewmodel.AstroEngine.EphemerisProvider);

            // Close
            viewmodel.Close();
            Assert.Null(viewmodel.AstroEngine);
            viewmodel.Close();
            Assert.Null(viewmodel.AstroEngine);
        }

        [Fact]
        public void TestDisposable()
        {
            var mockProvider = new Mock<IEphemerisProvider>();
            var provider = mockProvider.Object;

            var mockViewmodel = new Mock<AppViewModel>() {
                CallBase = true
            };
            mockViewmodel.Protected().Setup<IEphemerisProvider>("CreateEphemerisProvider").Returns(provider);

            AppViewModel viewmodel = null;
            // Not initialized
            using (viewmodel = mockViewmodel.Object)
            {
                Assert.Null(viewmodel.AstroEngine);

                // First initialization
                viewmodel.Initialize();
                Assert.NotNull(viewmodel.AstroEngine);
                Assert.Same(provider, viewmodel.AstroEngine.EphemerisProvider);
            }
            Assert.Null(viewmodel.AstroEngine);
            viewmodel.Close();
            Assert.Null(viewmodel.AstroEngine);
        }

    }
}
