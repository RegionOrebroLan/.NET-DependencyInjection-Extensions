using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.DependencyInjection;
using RegionOrebroLan.DependencyInjection.Extensions;
using TheSecond.Assembly;

namespace IntegrationTests.Extensions
{
	[TestClass]
	public class ServiceConfigurationScannerExtensionTest
	{
		#region Fields

		private static readonly IServiceConfigurationScanner _serviceConfigurationScanner = new ServiceConfigurationScanner();

		#endregion

		#region Properties

		protected internal virtual IServiceConfigurationScanner ServiceConfigurationScanner => _serviceConfigurationScanner;

		#endregion

		#region Methods

		[TestMethod]
		public void Scan_ByAssemblies_ShouldWorkProperly()
		{
			IEnumerable<System.Reflection.Assembly> assemblies = new[] {typeof(FirstAssembly.First).Assembly};
			Assert.AreEqual(3, this.ServiceConfigurationScanner.Scan(assemblies).Count());

			assemblies = new[] {typeof(FirstAssembly.First).Assembly, typeof(First).Assembly};
			Assert.AreEqual(6, this.ServiceConfigurationScanner.Scan(assemblies).Count());
		}

		[TestMethod]
		public void Scan_ByAssembly_ShouldWorkProperly()
		{
			Assert.AreEqual(3, this.ServiceConfigurationScanner.Scan(typeof(FirstAssembly.First).Assembly).Count());
			Assert.AreEqual(0, this.ServiceConfigurationScanner.Scan(typeof(string).Assembly).Count());
		}

		[TestMethod]
		public void Scan_ByAssemblyParams_ShouldWorkProperly()
		{
			Assert.AreEqual(6, this.ServiceConfigurationScanner.Scan(typeof(FirstAssembly.First).Assembly, typeof(First).Assembly).Count());
		}

		[TestMethod]
		public void Scan_ByType_ShouldWorkProperly()
		{
			Assert.AreEqual(1, this.ServiceConfigurationScanner.Scan(typeof(FirstAssembly.First)).Count());
			Assert.AreEqual(0, this.ServiceConfigurationScanner.Scan(typeof(string)).Count());
		}

		[TestMethod]
		public void Scan_ByTypeParams_ShouldWorkProperly()
		{
			Assert.AreEqual(2, this.ServiceConfigurationScanner.Scan(typeof(FirstAssembly.First), typeof(FirstAssembly.Second)).Count());
			Assert.AreEqual(2, this.ServiceConfigurationScanner.Scan(typeof(FirstAssembly.First), typeof(FirstAssembly.Second), typeof(string)).Count());
		}

		#endregion
	}
}