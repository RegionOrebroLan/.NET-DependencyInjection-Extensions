using Microsoft.Extensions.DependencyInjection;
using RegionOrebroLan.DependencyInjection;

namespace TheSecond.Assembly
{
	[ServiceConfiguration(Lifetime = ServiceLifetime.Transient)]
	public class Third
	{
		#region Properties

		public virtual string ThirdValue => "Third value";

		#endregion
	}
}