using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using frontend.Models;
using frontend.Repository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace frontend.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _empRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ILogger<EmployeeController> _logger;


        public EmployeeController(IEmployeeRepository empRepository, IWebHostEnvironment webHostEnvironment,ILogger<EmployeeController> logger)
        {
            _empRepository = empRepository;
            _webHostEnvironment = webHostEnvironment;
             _logger = logger;
        }



        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Get()
        {
            var data = _empRepository.Alldata();
            return Json(data);
        }

        public IActionResult CityGet()
        {
            var data = _empRepository.FetchCityData();
            return Json(data);
        }

        public IActionResult DeptGet()
        {
            var data = _empRepository.FetchDeptartmentData();
            return Json(data);
        }

        [HttpPost]
        public IActionResult Post([FromForm] EmployeeModel employee, IFormFile c_profiles)
        {
            try
            {
                if (c_profiles != null && c_profiles.Length > 0)
                {
                    var fileName = Path.GetFileName(c_profiles.FileName);
                    var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "photos", fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        c_profiles.CopyTo(stream);
                    }

                    employee.c_profile = fileName;
                }

                _empRepository.AddData(employee);

                return Json(new { message = "Successfully added employee" });
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                return StatusCode(500, new { message = "Internal Server Error" });
            }
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _empRepository.DeleteData(id);
            return Json(new { message = "Successfully deleted employee" });
        }

        [HttpGet]
        public IActionResult GetById(int id)
        {
            var data = _empRepository.GetById(id);
            return Json(data);
        }

        [HttpPost]
        public IActionResult Update([FromForm] EmployeeModel employee, IFormFile? c_profiles)
        {
            if (c_profiles != null)
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, "photos", c_profiles.FileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    c_profiles.CopyTo(stream);
                }
                employee.c_profile = c_profiles.FileName;
            }
            else
            {
                var exist = _empRepository.GetById(employee.c_empid);
                employee.c_profile = exist.c_profile;
            }

            if (employee.c_password == null)
            {
                var exist = _empRepository.GetById(employee.c_empid);
                employee.c_password = exist.c_password;
            }

            _empRepository.UpdateData(employee);
            return Json(new { message = "Successfully updated employee" });
        }
    }
}
