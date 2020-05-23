using System;

namespace RegionOrebroLan.DependencyInjection
{
	public class InstanceFactory : IInstanceFactory
	{
		#region Fields

		private const string _nullAsFormatArgument = "NULL";

		#endregion

		#region Properties

		protected internal virtual string NullAsFormatArgument => _nullAsFormatArgument;

		#endregion

		#region Methods

		public virtual object Create(string type)
		{
			try
			{
				return Activator.CreateInstance(this.ResolveType(type));
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException($"Could not create an instance from type {this.ValueAsFormatArgument(type)}.", exception);
			}
		}

		protected internal virtual Type ResolveType(string type)
		{
			try
			{
				return Type.GetType(type, true, true);
			}
			catch(Exception exception)
			{
				throw new InvalidOperationException($"Could not resolve the type from string-representation {this.ValueAsFormatArgument(type)}.", exception);
			}
		}

		protected internal virtual string ValueAsFormatArgument(string value)
		{
			return value != null ? $"\"{value}\"" : this.NullAsFormatArgument;
		}

		#endregion
	}
}