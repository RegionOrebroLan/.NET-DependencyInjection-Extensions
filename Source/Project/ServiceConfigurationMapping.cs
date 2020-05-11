using System;

namespace RegionOrebroLan.DependencyInjection
{
	public class ServiceConfigurationMapping : IServiceConfigurationMapping
	{
		#region Properties

		public virtual IServiceConfiguration Configuration { get; set; }
		public virtual Type Type { get; set; }

		#endregion
	}
}