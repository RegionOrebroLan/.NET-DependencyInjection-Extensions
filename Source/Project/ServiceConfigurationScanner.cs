using System;
using System.Collections.Generic;
using System.Linq;

namespace RegionOrebroLan.DependencyInjection
{
	public class ServiceConfigurationScanner : IServiceConfigurationScanner
	{
		#region Methods

		public virtual IEnumerable<IServiceConfigurationMapping> Scan(IEnumerable<Type> types)
		{
			if(types == null)
				throw new ArgumentNullException(nameof(types));

			types = types.ToArray();

			if(types.Any(type => type == null))
				throw new ArgumentException("The type-collection can not contain null-values.", nameof(types));

			var mappings = new List<IServiceConfigurationMapping>();

			foreach(var type in types)
			{
				foreach(var configuration in type.GetCustomAttributes(typeof(IServiceConfiguration), true).Cast<IServiceConfiguration>())
				{
					if(configuration.ServiceType != null && !configuration.ServiceType.IsAssignableFrom(type))
						throw new InvalidOperationException($"The service-type \"{configuration.ServiceType}\" is not assignable from type \"{type}\".");

					mappings.Add(new ServiceConfigurationMapping
					{
						Configuration = new ServiceConfigurationAttribute
						{
							Lifetime = configuration.Lifetime,
							ServiceType = configuration.ServiceType ?? type
						},
						Type = type
					});
				}
			}

			return mappings.ToArray();
		}

		#endregion
	}
}