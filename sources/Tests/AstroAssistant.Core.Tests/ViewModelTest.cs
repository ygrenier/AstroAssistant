using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AstroAssistant.Core.Tests
{
    public class ViewModelTest
    {
        class TestViewModel : ViewModels.ViewModel
        {
            public void CallRaisePropertyChanged(String propertyName)
            {
                RaisePropertyChanged(propertyName);
            }
            public void CallRaisePropertyChanged<T>(Expression<Func<T>> property)
            {
                RaisePropertyChanged(property);
            }
            public bool CallSetProperty<T>(ref T field, T value, String propertyName)
            {
                return SetProperty(ref field, value, propertyName);
            }
            public bool CallSetProperty<T>(ref T field, T value, Expression<Func<T>> property)
            {
                return SetProperty<T>(ref field, value, property);
            }
            public int PropertyOne { get; set; }
        }

        [Fact]
        public void TestRaiseProperty()
        {
            var viewmodel = new TestViewModel();
            String rPropertyName = null;
            viewmodel.PropertyChanged += (s, e) => rPropertyName = e.PropertyName;

            viewmodel.CallRaisePropertyChanged("TestProperty");
            Assert.Equal("TestProperty", rPropertyName);

            rPropertyName = null;
            viewmodel.CallRaisePropertyChanged(() => viewmodel.PropertyOne);
            Assert.Equal("PropertyOne", rPropertyName);
        }

        [Fact]
        public void TestSetProperty()
        {
            int propertyValue = 0;
            var viewmodel = new TestViewModel();
            String rPropertyName = null;
            viewmodel.PropertyChanged += (s, e) => rPropertyName = e.PropertyName;

            Assert.True(viewmodel.CallSetProperty(ref propertyValue, 123, "TestProperty"));
            Assert.Equal("TestProperty", rPropertyName);
            Assert.Equal(123, propertyValue);

            rPropertyName = null;
            propertyValue = 0;
            Assert.False(viewmodel.CallSetProperty(ref propertyValue, 0, "TestProperty"));
            Assert.Null(rPropertyName);
            Assert.Equal(0, propertyValue);

            rPropertyName = null;
            propertyValue = 0;
            Assert.True(viewmodel.CallSetProperty(ref propertyValue, 123, () => viewmodel.PropertyOne));
            Assert.Equal("PropertyOne", rPropertyName);
            Assert.Equal(123, propertyValue);

            rPropertyName = null;
            propertyValue = 0;
            Assert.False(viewmodel.CallSetProperty(ref propertyValue, 0, () => viewmodel.PropertyOne));
            Assert.Null(rPropertyName);
            Assert.Equal(0, propertyValue);
        }

    }
}
