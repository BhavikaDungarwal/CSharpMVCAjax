using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using frontend.Models;

namespace frontend.Repository
{
    public interface IEmployeeRepository
    {
        List<EmployeeModel> Alldata();

        bool AddData(EmployeeModel employee);

        bool DeleteData(int id);

        bool UpdateData(EmployeeModel employee);

        EmployeeModel GetById(int id);

        List<CityModel> FetchCityData();

        List<DepartmentModel> FetchDeptartmentData();

    }
}