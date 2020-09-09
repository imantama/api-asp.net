using belajarAPI.Models;
using belajarAPI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace belajarAPI.Repositories.Interface
{
    interface IDivisionRepository
    {
        Task<IEnumerable<DivisionVm>> Get();
        Task<DivisionVm> Get(int id);
        int Create(DivisionVm divisionVm);
        int Update(int id, DivisionVm divisionVm);
        int Delete(int id);
    }
}
