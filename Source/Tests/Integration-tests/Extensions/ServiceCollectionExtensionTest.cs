using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RegionOrebroLan.DependencyInjection.Extensions;

namespace IntegrationTests.Extensions
{
	[TestClass]
	public class ServiceCollectionExtensionTest
	{
		#region Methods

		[TestMethod]
		public async Task Remove_Test()
		{
			await Task.CompletedTask;

			var services = new ServiceCollection();
			services.AddScoped<FirstAssembly.IFirst, FirstAssembly.First>();
			services.AddScoped<FirstAssembly.IFirst, FirstAssembly.First>();
			services.AddScoped<FirstAssembly.IFirst, FirstAssembly.First>();
			services.AddSingleton<FirstAssembly.Second>();
			services.AddSingleton<FirstAssembly.Third>();
			services.AddTransient<TheSecond.Assembly.IFirst, TheSecond.Assembly.First>();
			Assert.AreEqual(6, services.Count);

			Assert.AreEqual(3, services.Remove<FirstAssembly.IFirst>());
			Assert.AreEqual(3, services.Count);

			Assert.AreEqual(1, services.Remove(typeof(FirstAssembly.Second)));
			Assert.AreEqual(2, services.Count);

			services.AddSingleton<FirstAssembly.Second>();
			Assert.AreEqual(3, services.Count);
			Assert.AreEqual(1, services.Remove(typeof(FirstAssembly.Second), typeof(FirstAssembly.Second), typeof(FirstAssembly.Second)));
			Assert.AreEqual(2, services.Count);

			Assert.AreEqual(2, services.Remove(typeof(FirstAssembly.Third), typeof(TheSecond.Assembly.IFirst)));
			Assert.AreEqual(0, services.Count);
		}

		[TestMethod]
		public void ScanDependencies_Force_Test()
		{
			var services = new ServiceCollection();
			services.AddSingleton(Mock.Of<FirstAssembly.IFirst>());
			services.ScanDependencies(false, "FirstAssembly");
			var serviceProvider = services.BuildServiceProvider();
			Assert.AreEqual(1, serviceProvider.GetServices<FirstAssembly.IFirst>().Count());
			Assert.AreEqual(Mock.Of<FirstAssembly.IFirst>().GetType(), serviceProvider.GetService<FirstAssembly.IFirst>().GetType());

			services = new ServiceCollection();
			services.AddSingleton(Mock.Of<FirstAssembly.IFirst>());
			services.ScanDependencies(true, "FirstAssembly");
			serviceProvider = services.BuildServiceProvider();
			Assert.AreEqual(2, serviceProvider.GetServices<FirstAssembly.IFirst>().Count());
			Assert.AreEqual(typeof(FirstAssembly.First), serviceProvider.GetService<FirstAssembly.IFirst>().GetType());
		}

		[TestMethod]
		public void ScanDependencies_Test()
		{
			var services = new ServiceCollection();
			services.ScanDependencies("FirstAssembly");
			Assert.AreEqual(3, services.Count);

			services = new ServiceCollection();
			services.ScanDependencies("First*");
			Assert.AreEqual(3, services.Count);

			services = new ServiceCollection();
			services.ScanDependencies("FirstAssembly", "TheSecond.*");
			Assert.AreEqual(6, services.Count);

			services = new ServiceCollection();
			services.ScanDependencies("*irst*", "*econd*");
			Assert.AreEqual(6, services.Count);

			services = new ServiceCollection();
			services.ScanDependencies("313b5888-dfaa-48be-87f5-371ae9a85ed4");
			Assert.AreEqual(0, services.Count);
		}

		#endregion
	}
}