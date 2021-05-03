using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegionOrebroLan.DependencyInjection;

namespace UnitTests
{
	[TestClass]
	public class ServiceConfigurationAttributeTest
	{
		#region Methods

		[TestMethod]
		public void Lifetime_ShouldReturnSingletonByDefault()
		{
			Assert.AreEqual(ServiceLifetime.Singleton, new ServiceConfigurationAttribute().Lifetime);
		}

		[TestMethod]
		public void ServiceType_ShouldReturnNullByDefault()
		{
			Assert.IsNull(new ServiceConfigurationAttribute().ServiceType);
		}

		#endregion
	}
}