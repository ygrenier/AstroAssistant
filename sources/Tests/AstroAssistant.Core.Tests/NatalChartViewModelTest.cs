using AstroAssistant.ViewModels;
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
            var vm = new NatalChartViewModel();
            Assert.Null(vm.FileName);
            Assert.False(vm.IsDirty);
        }
    }
}
