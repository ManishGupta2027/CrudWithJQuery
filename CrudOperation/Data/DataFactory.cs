using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Web;


namespace CrudOperation.Data
{
	public class DataFactory : IDataFactory
	{
		static MappingSource _sharedMappingSource = new AttributeMappingSource();
		string _connectionString;
		public DataFactoryDBDataContext DataFactoryDBDataContext()
		{
			_connectionString = ConfigurationManager.ConnectionStrings["CRUDConnectionString1"].ConnectionString;
			return new DataFactoryDBDataContext(_connectionString, _sharedMappingSource);
		}
	}
}