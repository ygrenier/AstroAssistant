using AstroAssistant.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AstroAssistant.Core.Tests
{
    public class FileInformationTest
    {

        [Fact]
        public void TestCreate()
        {
            var ms = new MemoryStream();
            var fi = new FileInformation("file", ms);
            Assert.Equal("file", fi.FileName);
            Assert.Same(ms, fi.Stream);

            Assert.Throws<ArgumentNullException>(() => new FileInformation(null, ms));
            Assert.Throws<ArgumentNullException>(() => new FileInformation("file", null));
        }

    }
}
