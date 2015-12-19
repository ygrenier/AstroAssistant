using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AstroAssistant.Core.Tests
{
    public class PropertyExtensionsTest
    {
        class TestObj
        {
            public int FieldOne = 0;
            public int MethodOne() { return 0; }
            public int PropertyOne { get; set; }
            public static int StaticPropertyOne { get; set; }
        }

        [Fact]
        public void TestGetPropertyName()
        {
            var o=new TestObj();

            Assert.Equal("PropertyOne", o.GetPropertyName(() => o.PropertyOne));
            Assert.Equal("PropertyOne", o.GetPropertyName(() => -o.PropertyOne));

            Assert.Throws<ArgumentNullException>(() => o.GetPropertyName<int>(null));
            var ex = Assert.Throws<ArgumentException>(() => o.GetPropertyName(() => o.MethodOne()));
            Assert.Equal("Expression invalide. Doit être appelé avec une expression de type\r\nn() => PropertyName\r\nNom du paramètre : expression", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => o.GetPropertyName(() => -o.MethodOne()));
            Assert.Equal("Expression unaire invalide. Doit être appelé avec une expression de type\r\nn() => PropertyName\r\nNom du paramètre : expression", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => o.GetPropertyName(() => o.FieldOne));
            Assert.Equal("Expression invalide. Doit être appelé avec une expression de type\r\nn() => PropertyName\r\nNom du paramètre : expression", ex.Message);

            int varOne = o.MethodOne();
            ex = Assert.Throws<ArgumentException>(() => o.GetPropertyName(() => varOne));
            Assert.Equal("Expression invalide. Doit être appelé avec une expression de type\r\nn() => PropertyName\r\nNom du paramètre : expression", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => o.GetPropertyName(() => -varOne));
            Assert.Equal("Expression invalide. Doit être appelé avec une expression de type\r\nn() => PropertyName\r\nNom du paramètre : expression", ex.Message);

            var anony = new {
                PropertyOne = 1
            };
            ex = Assert.Throws<ArgumentException>(() => o.GetPropertyName(() => anony.PropertyOne));
            Assert.Equal("Expression invalide. Doit être appelé avec une expression de type\r\nn() => PropertyName\r\nNom du paramètre : expression", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => o.GetPropertyName(() => TestObj.StaticPropertyOne));
            Assert.Equal("Expression invalide. Doit être appelé avec une expression de type\r\nn() => PropertyName\r\nNom du paramètre : expression", ex.Message);

            ex = Assert.Throws<ArgumentException>(() => o.GetPropertyName(() => 123));
            Assert.Equal("Expression invalide. Doit être appelé avec une expression de type\r\nn() => PropertyName\r\nNom du paramètre : expression", ex.Message);

        }
    }
}
