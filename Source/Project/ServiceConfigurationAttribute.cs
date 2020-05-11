using System;
using Microsoft.Extensions.DependencyInjection;

namespace RegionOrebroLan.DependencyInjection
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class ServiceConfigurationAttribute : Attribute, IServiceConfiguration
	{
		#region Properties

		public ServiceLifetime Lifetime { get; set; } = ServiceLifetime.Singleton;
		public Type ServiceType { get; set; }

		#endregion
	}
}