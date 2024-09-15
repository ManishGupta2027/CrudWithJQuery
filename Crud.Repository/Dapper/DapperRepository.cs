using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
namespace Crud.Data.Dapper
{
	public class DapperRepository : IDapperRepository
	{
		private readonly IConfiguration _config;
		public DapperRepository(IConfiguration config)
		{
			_config = config;
		}

		public void Dispose()
		{

		}

		public int Execute(string sp, DynamicParameters parms, string connectionName, CommandType commandType = CommandType.StoredProcedure)
		{
			using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionName));
			return db.Query(sp, parms, commandType: commandType).FirstOrDefault();
		}

		public T Get<T>(string sp, DynamicParameters parms, string connectionName, CommandType commandType = CommandType.StoredProcedure)
		{
			using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionName));
			return db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
		}

		public List<T> GetAll<T>(string sp, DynamicParameters parms, string connectionName, CommandType commandType = CommandType.StoredProcedure)
		{
			using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionName));
			return db.Query<T>(sp, parms, commandType: commandType).ToList();
		}

		public DbConnection GetDbconnection(string connectionName)
		{
			return new SqlConnection(_config.GetConnectionString(connectionName));
		}

		public T Insert<T>(string sp, DynamicParameters parms, string connectionName, CommandType commandType = CommandType.StoredProcedure)
		{
			T result;
			using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionName));
			result = db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
			return result;
		}

		public T Update<T>(string sp, DynamicParameters parms, string connectionName, CommandType commandType = CommandType.StoredProcedure)
		{
			T result;
			using IDbConnection db = new SqlConnection(_config.GetConnectionString(connectionName));
			result = db.Query<T>(sp, parms, commandType: commandType).FirstOrDefault();
			return result;
		}
	}
}
