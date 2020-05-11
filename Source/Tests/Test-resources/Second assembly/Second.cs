using RegionOrebroLan.DependencyInjection;

namespace TheSecond.Assembly
{
	[ServiceConfiguration]
	public class Second
	{
		#region Properties

		public virtual string SecondValue => "Second value";

		#endregion
	}
}