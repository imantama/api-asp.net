using belajarAPI.ViewModels;
using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using belajarAPI.Repositories.Interface;
namespace belajarAPI.Repositories
{
    public class DivisionRepositories:DivisionVm
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["myConnection"].ConnectionString);
        DynamicParameters param = new DynamicParameters();
        public int Create(DivisionVm divisionVm) {
            var procedureCreate = "SPCreateDivision";
            param.Add("@name", divisionVm.name);
            param.Add("@department_id", divisionVm.department_id);
            var create = con.Execute(procedureCreate, param, commandType: CommandType.StoredProcedure);
            return create;
        }
        public int Delete(int id) {
            var procedureDelete = "SPDivisonDelete";
            param.Add("@id", id);
            var delete = con.Execute(procedureDelete, param, commandType: CommandType.StoredProcedure);
            return delete;
            
        }
        public async Task<IEnumerable<DivisionVm>> GetAll()
        {
            var procedureGetAll = "SPGetDivision";
            var getAll = await con.QueryAsync<DivisionVm>(procedureGetAll, commandType: CommandType.StoredProcedure);
            return getAll;
        }
        public DivisionVm GetID(int id)
        {
            var procedureGetId = "SPGetDivisionById";
            param.Add("@id", id);
            var getID = con.Query<DivisionVm>(procedureGetId, param, commandType: CommandType.StoredProcedure).SingleOrDefault();
            return getID;
        }
        public int Update(int id, DivisionVm divisionVm) {
            var procedureUpdateDiv = "SPUpdateDivision";
            param.Add("@id", id);
            param.Add("@nama", divisionVm.name);
            param.Add("@dep_id", divisionVm.department_id);
            var update = con.Execute(procedureUpdateDiv, param, commandType: CommandType.StoredProcedure);
            return update;
        }

    }
}