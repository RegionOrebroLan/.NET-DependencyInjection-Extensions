using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.DependencyInjection;

namespace IntegrationTests
{
	[TestClass]
	public class ServiceConfigurationScannerTest
	{
		#region Methods

		[TestMethod]
		[ExpectedException(typeof(InvalidOperationException))]
		public void Scan_IfThereAreInvalidAttributeSettings_ShouldThrowAnInvalidOperationException()
		{
			try
			{
				new ServiceConfigurationScanner().Scan(typeof(InvalidAssembly.First).Assembly.GetTypes());
			}
			catch(InvalidOperationException exception)
			{
				if(exception.Message.Equals("The service-type \"InvalidAssembly.IFirst\" is not assignable from type \"InvalidAssembly.First\".", StringComparison.Ordinal))
					throw;
			}
		}

		[TestMethod]
		public void Scan_ShouldWorkProperly()
		{
			var mappings = new ServiceConfigurationScanner().Scan(typeof(FirstAssembly.First).Assembly.GetTypes()).ToArray();

			Assert.AreEqual(3, mappings.Length);

			Assert.AreEqual(ServiceLifetime.Singleton, mappings[0].Configuration.Lifetime);
			Assert.AreEqual(typeof(FirstAssembly.IFirst), mappings[0].Configuration.ServiceType);
			Assert.AreEqual(typeof(FirstAssembly.First), mappings[0].Type);

			Assert.AreEqual(ServiceLifetime.Singleton, mappings[1].Configuration.Lifetime);
			Assert.AreEqual(typeof(FirstAssembly.Second), mappings[1].Configuration.ServiceType);
			Assert.AreEqual(typeof(FirstAssembly.Second), mappings[1].Type);

			Assert.AreEqual(ServiceLifetime.Transient, mappings[2].Configuration.Lifetime);
			Assert.AreEqual(typeof(FirstAssembly.Third), mappings[2].Configuration.ServiceType);
			Assert.AreEqual(typeof(FirstAssembly.Third), mappings[2].Type);
		}

		#endregion
	}
}