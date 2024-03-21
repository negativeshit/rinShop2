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
    [Authorize(Roles = $"{WebUserRoles.Administrator},{WebUserRoles.Employee}")]
    public class CustomerController : Controller
    {
        // GET: /<controller>/
        private const int PAGE_SIZE = 20;
        private const string CUSTOMER_SEARCH = "customer_search";

        public IActionResult Index()
        {
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(CUSTOMER_SEARCH);
            if(input == null)
            {
                input = new PaginationSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                };
            }
            return View(input);
        }
        public IActionResult Search(PaginationSearchInput input)
        {
            int rowCount = 0;
            var data = CommonDataService.ListOfCustomers(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new CustomerSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                RowCount = rowCount,
                Data = data,
            };
            ApplicationContext.SetSessionData(CUSTOMER_SEARCH, input);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Thêm người dùng";  
            Customer model = new Customer()
            {
                CustomerId = 0
            };
            return View("Edit",model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Chỉnh sửa thông tin người dùng";
            Customer? model = CommonDataService.GetCustomer(id);
            if (model == null)
                return RedirectToAction("Index");
            return View(model);
        }
        public IActionResult Delete(int  id = 0)
        {
            if(Request.Method == "POST")
            {
                CommonDataService.DeleteCustomer(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCustomer(id);
            if(model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.allowDelete = !CommonDataService.isUsedCustomer(id);
            
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Customer data)
        {
            try
            {
                ViewBag.Title = data.CustomerId == 0 ? "Bổ sung khách hàng" : "Cập nhật khách hàng";
                if (string.IsNullOrWhiteSpace(data.CustomerName))
                {
                    ModelState.AddModelError("CustomerName", "Tên không được để trống!");
                }
                if (string.IsNullOrWhiteSpace(data.ContactName))
                {
                    ModelState.AddModelError("ContactName", "Tên giao dịch không được để trống!");
                }
                if (string.IsNullOrWhiteSpace(data.Email))
                {
                    ModelState.AddModelError("Email", "Email không được để trống!");
                }
                if (string.IsNullOrWhiteSpace(data.Address))
                {
                    ModelState.AddModelError("Address", "Địa chỉ không được để trống!");
                }
                if (!ModelState.IsValid)
                {
                    
                    return View("Edit", data);
                }
                if (data.CustomerId == 0)
                {
                    int id = CommonDataService.AddCustomer(data);
                    if( id <= 0)
                    {
                        ModelState.AddModelError("Email", "Địa chỉ Email đã được sử dụng");
                    }

                }
                else
                {
                    bool result = CommonDataService.UpdateCustomer(data);
                    if (!result)
                    {
                        ModelState.AddModelError("Email", "Địa chỉ Email bị trùng với tk khác");
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "Đã xảy ra lỗi, vui lòng thử lại sau!");
                return View("Edit", data);
            }
        }
    }
}

