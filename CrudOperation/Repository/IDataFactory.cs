using CrudOperation.Data;

namespace CrudOperation.Repository
{
	public interface IDataFactory
	{
		DataFactoryDBDataContext DataFactoryDBDataContext();
	}
}