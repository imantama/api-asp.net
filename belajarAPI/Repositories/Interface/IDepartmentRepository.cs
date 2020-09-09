using belajarAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace belajarAPI.Repositories.Interface
{
    public interface IDepartmentRepository
    {
        Task<IEnumerable<Department>> Get();
        Department Get(int id);
        int Create(Department department);
        int Update(int id, Department department);
        int Delete(int id);
    }
}
