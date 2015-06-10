using AstroAssistant.Services;
using AstroAssistant.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AstroAssistant.Core.Tests
{
    public class NatalChartViewModelTest
    {
        [Fact]
        public void TestCreate()
        {
            var fs = new Mock<IFileService>().Object;
            var vm = new NatalChartViewModel(fs);
            Assert.Null(vm.FileName);
            Assert.False(vm.IsDirty);
        }
    }
}
