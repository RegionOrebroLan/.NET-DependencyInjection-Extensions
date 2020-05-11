using System;

namespace RegionOrebroLan.DependencyInjection
{
	public interface IServiceConfigurationMapping
	{
		#region Properties

		IServiceConfiguration Configuration { get; }
		Type Type { get; }

		#endregion
	}
}