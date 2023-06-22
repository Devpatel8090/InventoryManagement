using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Repository.Interface
{
    public interface IDataAccessRepository
    {
        public SqlConnection CreateConnection();
        public void CloseConnection(SqlConnection connection);
         Task<IEnumerable<T>> GetData<T, P>(string spName, P parameters);

         Task SaveData<T>(string spName, T parameters);
        //Task GetSingleValue<P>(string spName, P parameters);

        /*Task GetSingleData<T>(string spName, T parameters);*/

    }
}
