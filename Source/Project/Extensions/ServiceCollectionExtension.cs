using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.DependencyModel;

namespace RegionOrebroLan.DependencyInjection.Extensions
{
	public static class ServiceCollectionExtension
	{
		#region Methods

		public static IServiceCollection ScanDependencies(this IServiceCollection services, bool force)
		{
			return services.ScanDependencies(force, Array.Empty<string>());
		}

		public static IServiceCollection ScanDependencies(this IServiceCollection services, params string[] patterns)
		{
			return services.ScanDependencies(false, patterns);
		}

		public static IServiceCollection ScanDependencies(this IServiceCollection services, bool force, params string[] patterns)
		{
			return services.ScanDependencies(force, new ServiceConfigurationScanner(), patterns);
		}

		/// <summary>
		/// Scan dependencies by service-configuration-attributes.
		/// </summary>
		/// <param name="services">The service-collection instance.</param>
		/// <param name="force">Forces adding of service-descriptors. If true "Add" is used, otherwise "TryAdd" is used.</param>
		/// <param name="scanner">The service-configuration-scanner to use.</param>
		/// <param name="patterns">Patterns for assembly-names to scan. Eg. "RegionOrebroLan", "RegionOrebroLan*", "RegionOrebroLan.*", "*". The default is <code>new[] {"RegionOrebroLan", "RegionOrebroLan.*"}</code>.</param>
		/// <returns></returns>
		public static IServiceCollection ScanDependencies(this IServiceCollection services, bool force, IServiceConfigurationScanner scanner, params string[] patterns)
		{
			if(services == null)
				throw new ArgumentNullException(nameof(services));

			if(scanner == null)
				throw new ArgumentNullException(nameof(scanner));

			if(!patterns.Any())
				patterns = new[] {"RegionOrebroLan", "RegionOrebroLan.*"};

			var libraryNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
			const RegexOptions regexOptions = RegexOptions.Compiled | RegexOptions.IgnoreCase;
			var regexPatterns = patterns.Select(pattern => "^" + Regex.Escape(pattern).Replace("\\*", ".*") + "$");

			foreach(var regexPattern in regexPatterns)
			{
				foreach(var library in DependencyContext.Default.RuntimeLibraries)
				{
					if(Regex.IsMatch(library.Name, regexPattern, regexOptions))
						libraryNames.Add(library.Name);
				}
			}

			var assemblies = libraryNames.Select(Assembly.Load);

			foreach(var mapping in scanner.Scan(assemblies))
			{
				var serviceDescriptor = new ServiceDescriptor(mapping.Configuration.ServiceType, mapping.Type, mapping.Configuration.Lifetime);

				if(force)
					services.Add(serviceDescriptor);
				else
					services.TryAdd(serviceDescriptor);
			}

			return services;
		}

		#endregion
	}
}