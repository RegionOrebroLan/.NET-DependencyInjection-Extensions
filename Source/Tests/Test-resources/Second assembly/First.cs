using RegionOrebroLan.DependencyInjection;

namespace TheSecond.Assembly
{
	[ServiceConfiguration(ServiceType = typeof(IFirst))]
	public class First : IFirst
	{
		#region Properties

		public virtual string FirstValue => "First value";

		#endregion
	}
}