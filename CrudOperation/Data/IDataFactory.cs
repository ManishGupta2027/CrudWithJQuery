using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudOperation.Data;

namespace CurdwithMVC.Data
{
	public interface IDataFatory
	{
		DataFactoryDBDataContext DataFatoryDBDataContext();

	}
}
