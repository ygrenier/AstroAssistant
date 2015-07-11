using AstroAssistant.Services;
using AstroAssistant.ViewModels;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AstroAssistant.Core.Tests
{
    public class NatalChartViewModelTest
    {
        static string NatalChart1 = @"
<natal-chart>
    <name>Test</name>
    <birth-date>
        <date>2015-06-03</date>
        <time>07:57:32.456</time>
        <timezone>Romance Standard Time</timezone>
    </birth-date>
    <birth-place-name>Localisation</birth-place-name>
    <birth-place-position>
        <longitude>1e</longitude>
        <longitude>2s</longitude>
    </birth-place-position>
</natal-chart>
";
        [Fact]
        public void TestCreate()
        {
            var fs = new Mock<IFileService>().Object;
            var ass = new Mock<IAstroService>().Object;
            var vm = new NatalChartViewModel(fs, null, ass);
            Assert.Null(vm.FileName);
            Assert.NotNull(vm.Definition);
            Assert.Null(vm.NatalChart);
            Assert.False(vm.IsDirty);
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public void TestAsINatalChartViewModel()
        {
            var fs = new Mock<IFileService>().Object;
            var ass = new Mock<IAstroService>().Object;
            INatalChartViewModel vm = new NatalChartViewModel(fs, null, ass);
            Assert.Null(vm.FileName);
            Assert.NotNull(vm.Definition);
            Assert.Null(vm.NatalChart);
            Assert.False(vm.IsDirty);
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public async Task TestReset()
        {
            var fsMock = new Mock<IFileService>();
            fsMock
                .Setup(f => f.OpenLoadAsNatalChart())
                .Returns(() => Task.FromResult(new FileInformation("file.ext", new MemoryStream(Encoding.UTF8.GetBytes(NatalChart1)))));
            var fs = fsMock.Object;
            var ass = new Mock<IAstroService>().Object;
            var vm = new NatalChartViewModel(fs, null, ass);
            Assert.True(await vm.LoadFromFile());
            Assert.NotNull(vm.FileName);
            Assert.NotNull(vm.Definition);
            Assert.NotNull(vm.NatalChart);
            Assert.False(vm.IsDirty);
            Assert.False(vm.IsBusy);
            vm.IsDirty = true;
            var def = vm.Definition;

            vm.Reset();
            Assert.Null(vm.FileName);
            Assert.NotNull(vm.Definition);
            Assert.NotSame(def, vm.Definition);
            Assert.Null(vm.NatalChart);
            Assert.False(vm.IsDirty);
            Assert.False(vm.IsBusy);
        }

        [Fact]
        public async Task TestLoad()
        {
            // Test de chargement normaux
            var fsMock = new Mock<IFileService>();
            fsMock
                .Setup(f => f.OpenLoadAsNatalChart())
                .Returns(() => Task.FromResult(new FileInformation("file.ext", new MemoryStream(Encoding.UTF8.GetBytes(NatalChart1)))));
            fsMock
                .Setup(f => f.OpenLoadNatalChart(It.IsAny<String>()))
                .Returns<String>(n => Task.FromResult(new FileInformation(n, new MemoryStream(Encoding.UTF8.GetBytes(NatalChart1)))));
            var fs = fsMock.Object;
            var ass = new Mock<IAstroService>().Object;
            var vm = new NatalChartViewModel(fs, null, ass);
            vm.IsDirty = true;
            Assert.Null(vm.FileName);
            Assert.True(await vm.LoadFromFile());
            Assert.Equal("file.ext", vm.FileName);
            Assert.Equal("Test", vm.Definition.Definition.Name);
            Assert.NotNull(vm.NatalChart);
            var chart = vm.NatalChart;
            Assert.True(await vm.LoadFromFile("other-file.ext"));
            Assert.Equal("other-file.ext", vm.FileName);
            Assert.NotNull(vm.NatalChart);
            Assert.NotSame(chart, vm.NatalChart);
            Assert.False(vm.IsDirty);
            Assert.False(vm.IsBusy);
            await Assert.ThrowsAsync<ArgumentNullException>(() => vm.LoadFromFile(null));

            fsMock.Verify(f => f.OpenLoadAsNatalChart(), Times.Once());
            fsMock.Verify(f => f.OpenLoadNatalChart("other-file.ext"), Times.Once());

            // Test de chargement avec un délai 
            fsMock = new Mock<IFileService>();
            fsMock
                .Setup(f => f.OpenLoadAsNatalChart())
                .Returns(Task.Delay(100).ContinueWith<FileInformation>(_ => new FileInformation("file.ext", new MemoryStream(Encoding.UTF8.GetBytes(NatalChart1)))));
            fs = fsMock.Object;
            vm = new NatalChartViewModel(fs, null, ass);
            var task = vm.LoadFromFile();
            vm.IsDirty = true;
            Assert.True(vm.IsBusy);
            Assert.False(await vm.LoadFromFile());
            Assert.True(await task);
            Assert.False(vm.IsDirty);
            Assert.False(vm.IsBusy);

        }

        [Fact]
        public async Task TestSave()
        {
            // Test d'enregistrements normaux
            var fsMock = new Mock<IFileService>();
            fsMock
                .Setup(f => f.OpenSaveAsNatalChart())
                .Returns(() => Task.FromResult(new FileInformation("file.ext", new MemoryStream())));
            fsMock
                .Setup(f => f.OpenSaveNatalChart(It.IsAny<String>()))
                .Returns<String>(n => Task.FromResult(new FileInformation(n, new MemoryStream())));
            var fs = fsMock.Object;
            var ass = new Mock<IAstroService>().Object;
            var vm = new NatalChartViewModel(fs, null, ass);
            vm.IsDirty = true;
            Assert.Null(vm.FileName);
            Assert.True(await vm.Save());
            Assert.Equal("file.ext", vm.FileName);
            Assert.False(vm.IsDirty);
            Assert.True(await vm.SaveAs("other-file.ext"));
            Assert.Equal("other-file.ext", vm.FileName);
            Assert.True(await vm.Save());
            Assert.Equal("other-file.ext", vm.FileName);
            Assert.False(vm.IsDirty);
            Assert.False(vm.IsBusy);

            Assert.True(await vm.SaveAs(null));
            Assert.Equal("file.ext", vm.FileName);
            Assert.False(vm.IsDirty);

            fsMock.Verify(f => f.OpenSaveAsNatalChart(), Times.Exactly(2));
            fsMock.Verify(f => f.OpenSaveNatalChart("other-file.ext"), Times.Exactly(2));

            // Test de chargement avec un délai 
            fsMock = new Mock<IFileService>();
            fsMock
                .Setup(f => f.OpenSaveAsNatalChart())
                .Returns(() => Task.Delay(100).ContinueWith<FileInformation>(_ => new FileInformation("file.ext", new MemoryStream())));
            fs = fsMock.Object;
            vm = new NatalChartViewModel(fs, null, ass);
            var task = vm.Save();
            vm.IsDirty = true;
            Assert.True(vm.IsBusy);
            Assert.False(await vm.Save());
            Assert.True(await task);
            Assert.False(vm.IsDirty);
            Assert.False(vm.IsBusy);

        }

    }
}
