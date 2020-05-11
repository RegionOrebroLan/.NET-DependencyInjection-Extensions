using RegionOrebroLan.DependencyInjection;

namespace FirstAssembly
{
	[ServiceConfiguration]
	public class Second
	{
		#region Properties

		public virtual string SecondValue => "Second value";

		#endregion
	}
}