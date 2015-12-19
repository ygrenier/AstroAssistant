using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AstroAssistant.ViewModels
{
    /// <summary>
    /// Base of ViewModel
    /// </summary>
    public abstract class ViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Raise a PropertyChanged event
        /// </summary>
        /// <param name="propertyName">Name of the property changed, or empty if all properties are changed</param>
        protected virtual void RaisePropertyChanged(String propertyName)
        {
            var h = PropertyChanged;
            if (h != null)
                h(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Raise a PropertyChanged event
        /// </summary>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> property)
        {
            var propertyName = this.GetPropertyName(property);
            RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Try to set a property and raise PropertyChanged event if done
        /// </summary>
        protected bool SetProperty<T>(ref T field, T value, String propertyName)
        {
            if (Equals(field, value))
                return false;

            field = value;
            RaisePropertyChanged(propertyName);
            return true;
        }

        /// <summary>
        /// Try to set a property and raise PropertyChanged event if done
        /// </summary>
        protected bool SetProperty<T>(ref T field, T value, Expression<Func<T>> property)
        {
            var propertyName = this.GetPropertyName(property);
            return SetProperty(ref field, value, propertyName);
        }

        /// <summary>
        /// Event raised when a property changed
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

    }
}
