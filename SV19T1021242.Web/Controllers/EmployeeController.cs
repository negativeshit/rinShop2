using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV19T1021242.BusinessLayers;
using SV19T1021242.DomainModels;
using SV19T1021242.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SV19T1021242.Web.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Administrator}")]
    public class EmployeeController : Controller
    {
        private const string EMPLOYEE_SEARCH = "emloyee_search";
        private const int PAGE_SIZE = 20;
        public IActionResult Index()
        {
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(EMPLOYEE_SEARCH);
            if (input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = ""
                };
            }
            return View(input);
        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfEmployees(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new EmployeeSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            ApplicationContext.SetSessionData(EMPLOYEE_SEARCH, input);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhân viên";
            Employee model = new Employee()
            {
                EmployeeId = 0,
                Photo = "nophoto.jpeg",
                BirthDate = new DateTime(1990,1,1)
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Chỉnh sửa nhân viên";
            Employee? model = CommonDataService.GetEmployee(id);
            if (model == null)
                return RedirectToAction("Index");
            if (string.IsNullOrEmpty(model.Photo))
                model.Photo = "nophoto.jpeg";
            return View(model);
        }
        public IActionResult Delete(int id)
        {
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteEmployee(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetEmployee(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.allowDelete = !CommonDataService.isUsedEmployee(id);
            //return Json(model);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Employee data,string BirthDateInput,IFormFile? uploadPhoto)
        {
            /// Xử lí ngày sinh
            DateTime? BirthDate = BirthDateInput.StringToDateTime();

            if (BirthDate.HasValue)
            {
                data.BirthDate = BirthDate.Value;
            }
            ///Xử lí ảnh => Upload
            if(uploadPhoto != null)
            {
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string folder = Path.Combine(ApplicationContext.HostEnviroment.WebRootPath, @"images/employees");
                string filepath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    uploadPhoto.CopyTo(stream);
                }
                data.Photo = fileName;
            }
            

            try
            {
                if (data.EmployeeId == 0)
                {
                    int id = CommonDataService.AddEmployee(data);

                }
                else
                {
                    bool result = CommonDataService.UpdateEmployee(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                return Content(e.Message);
            }
        }
    }
}

