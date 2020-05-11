using System;
using Microsoft.Extensions.DependencyInjection;

namespace RegionOrebroLan.DependencyInjection
{
	public interface IServiceConfiguration
	{
		#region Properties

		ServiceLifetime Lifetime { get; }
		Type ServiceType { get; }

		#endregion
	}
}