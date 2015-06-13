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
            var viewmodel = new AppViewModel(ass);
            Assert.Same(ass, viewmodel.AstroService);

            Assert.Throws<ArgumentNullException>(() => new AppViewModel(null));
        }

        [Fact]
        public void TestInitialize()
        {
            var ass = new Mock<IAstroService>().Object;
            var viewmodel = new AppViewModel(ass);
            viewmodel.Initialize();
        }

    }
}
