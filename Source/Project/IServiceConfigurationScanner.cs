using System;
using System.Collections.Generic;

namespace RegionOrebroLan.DependencyInjection
{
	public interface IServiceConfigurationScanner
	{
		#region Methods

		IEnumerable<IServiceConfigurationMapping> Scan(IEnumerable<Type> types);

		#endregion
	}
}