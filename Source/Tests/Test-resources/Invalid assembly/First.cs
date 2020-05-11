using RegionOrebroLan.DependencyInjection;

namespace InvalidAssembly
{
	[ServiceConfiguration(ServiceType = typeof(IFirst))]
	public class First
	{
		#region Properties

		public virtual string FirstValue => "First value";

		#endregion
	}
}