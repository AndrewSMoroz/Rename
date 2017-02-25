using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Runtime.Serialization;

namespace Rename.Utility
{
	[DataContract]
	public abstract class ObservableObject : INotifyPropertyChanged
	{

        //--------------------------------------------------------------------------------------------------------------
		/// <summary>
		/// Raised when a property on this object has a new value.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

#region INotifyPropertyChanged

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
		/// Raises this object's PropertyChanged event.
		/// </summary>
		/// <param name="propertyName">The property that has a new value.</param>
		protected void OnPropertyChanged(string propertyName)
		{
			this.VerifyPropertyName(propertyName);

			PropertyChangedEventHandler handler = this.PropertyChanged;
			if (handler != null)
			{
				var e = new PropertyChangedEventArgs(propertyName);
				handler(this, e);
			}
		}

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
		/// Raises this object's PropertyChanged event through a lambda expression.
		/// </summary>
		/// <param name="propertyExpression">Lambda expression to access property, i.e. '() => this.MyProperty'</param>
		protected void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
		{
			string propertyName = PropertyHelper.GetPropertyName<T>(propertyExpression);
			OnPropertyChanged(propertyName);
		}

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
		/// Helper managing the update of a property and raising 'PropertyChanged' if necessary.
		/// </summary>
		/// <param name="oldValue">Reference to the variable 'storing' the property's data</param>
		/// <param name="newValue">Potential new property value</param>
		/// <param name="propertyExpression">Lambda expression to access property, i.e. '() => this.MyProperty'</param>
		/// <param name="onChanged">Action to execute after/if the value has changed</param>
		/// <param name="onChanging">Action to execute before/if the value is changing</param>
		protected void SetProperty<T>(ref T oldValue, T newValue,
			Expression<Func<T>> propertyExpression,
			Action onChanged = null,
			Action onChanging = null)
		{
			string propertyName = PropertyHelper.GetPropertyName<T>(propertyExpression);

			SetProperty(ref oldValue, newValue, propertyName, onChanged, onChanging);
		}

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
		/// Helper managing the update of a property and raising "PropertyChanged" if necessary.
		/// </summary>
		/// <param name="oldValue">Reference to the variable 'storing' the property's data</param>
		/// <param name="newValue">Potential new property value</param>
		/// <param name="propertyName">Property name</param>
		/// <param name="onChanged">Action to execute after/if the value has changed</param>
		/// <param name="onChanging">Action to execute before/if the value is changing</param>
		protected void SetProperty<T>(ref T oldValue, T newValue,
			string propertyName,
			Action onChanged = null,
			Action onChanging = null)
		{
			this.VerifyPropertyName(propertyName);

			if (EqualityComparer<T>.Default.Equals(oldValue, newValue)) return;

			if (onChanging != null)
			{
				onChanging();
			}

			oldValue = newValue;

			if (onChanged != null)
			{
				onChanged();
			}

			OnPropertyChanged(propertyName);
		}

#endregion

#region Debugging helpers

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
		/// Warns the developer if this object does not have
		/// a public property with the specified name. This 
		/// method does not exist in a Release build.
		/// </summary>
		[Conditional("DEBUG")]
		[DebuggerStepThrough]
		public void VerifyPropertyName(string propertyName)
		{
			// If you raise PropertyChanged and do not specify a property name,
			// all properties on the object are considered to be changed by the binding system.
			if (string.IsNullOrEmpty(propertyName))
				return;

			// Verify that the property name matches a real,  
			// public, instance property on this object.
			if (TypeDescriptor.GetProperties(this)[propertyName] == null)
			{
				string msg = "Invalid property name: " + propertyName;

				if (this.ThrowOnInvalidPropertyName)
					throw new ArgumentException(msg);
				else
					Debug.Fail(msg);
			}
		}

        //--------------------------------------------------------------------------------------------------------------
        /// <summary>
		/// Returns whether an exception is thrown, or if a Debug.Fail() is used
		/// when an invalid property name is passed to the VerifyPropertyName method.
		/// The default value is false, but subclasses used by unit tests might 
		/// override this property's getter to return true.
		/// </summary>
		protected virtual bool ThrowOnInvalidPropertyName { get; private set; }

#endregion

	}

}
