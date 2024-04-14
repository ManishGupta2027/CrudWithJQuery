using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrudOperation.Models;

namespace CrudOperation.Service
{
	public interface IProductService
	{
		bool SaveProduct(Product product);
	}
}
