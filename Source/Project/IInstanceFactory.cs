namespace RegionOrebroLan.DependencyInjection
{
	public interface IInstanceFactory
	{
		#region Methods

		object Create(string type);

		#endregion
	}
}