using Astro;
using AstroAssistant.Services;
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
            var ass = new Mock<IAstroService>().Object;
            var ds = new Mock<IDialogService>().Object;
            var rsMock = new Mock<IResolverService>();
            rsMock
                .Setup(p => p.CreateViewModel<NatalChartViewModel>())
                .Returns(() => new NatalChartViewModel(new Mock<IFileService>().Object, new Mock<ITimeZoneProvider>().Object, ass));
            var rs = rsMock.Object;
            var viewmodel = new AppViewModel(ass, ds, rs);
            Assert.Same(ass, viewmodel.AstroService);
            Assert.Same(ds, viewmodel.DialogService);
            Assert.NotNull(viewmodel.CurrentNatalChart);

            Assert.Throws<ArgumentNullException>(() => new AppViewModel(null, ds, rs));
            Assert.Throws<ArgumentNullException>(() => new AppViewModel(ass, null, rs));
            Assert.Throws<ArgumentNullException>(() => new AppViewModel(ass, ds, null));
        }

        [Fact]
        public void TestInitialize()
        {
            var ass = new Mock<IAstroService>().Object;
            var ds = new Mock<IDialogService>().Object;
            var rsMock = new Mock<IResolverService>();
            var rs = rsMock.Object;
            var viewmodel = new AppViewModel(ass, ds, rs);
            viewmodel.Initialize();
        }

    }
}
