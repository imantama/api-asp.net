using belajarAPI.Models;
using belajarAPI.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using belajarAPI.MyContext;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using System.Data;

namespace belajarAPI.Repositories
{
    public class DepartmentRepository : Department
    {
        //Context context = new Context();
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);
        DynamicParameters param = new DynamicParameters();
        public int Create(Department department)
        {
            var procedureName = "SPCreateDepartment";
            param.Add("@name", department.name);
            var create = con.Execute(procedureName, param, commandType:CommandType.StoredProcedure);
            return create;
        }

        public int Delete(int id)
        {
            var procedureDelete = "SPDeleteDepartment";
            param.Add("@id", id);
            var delete = con.Execute(procedureDelete, param, commandType:CommandType.StoredProcedure);
            return delete;
        }

        public async Task<IEnumerable<Department>> Get()
        {
            var procedureGet = "SPGetDepartment";
            var get = await con.QueryAsync<Department>(procedureGet, param, commandType: CommandType.StoredProcedure);
            return get;
        }

        public Department Get(int id)
        {
            var procedureGetbyId = "SPGetDepartmentbyId";
            param.Add("@id", id);
            var getbyId =con.Query<Department>(procedureGetbyId, param, commandType: CommandType.StoredProcedure);
            return getbyId.FirstOrDefault();
        }

        public int Update(int id, Department department)
        {
            var procedureUpdate = "SPUpdateDepartment";
            param.Add("@id", id);
            param.Add("@nama", department.name);
            var update = con.Execute(procedureUpdate, param, commandType:CommandType.StoredProcedure);
            return update;
        }
    }
}