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
    public class SupplierController : Controller
    {
     
        private const int PAGE_SIZE = 20;
        private const string SUPPLIER_SEARCH = "supplier_search";
        public IActionResult Index()
        {
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(SUPPLIER_SEARCH);
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
            var data = CommonDataService.ListOfSuppliers(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new SupplierSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            ApplicationContext.SetSessionData(SUPPLIER_SEARCH, input);
            return View(model);
        }

        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung nhà cung cấp";

            Supplier model = new Supplier()
            {
                SupplierId = 0
            };

            return View("Edit", model);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin nhà cung cấp";
           
            Supplier? model = CommonDataService.GetSupplier(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Supplier data)
        {
            
            try
            {
                ViewBag.Title = data.SupplierId == 0 ? "Bổ sung nhà cung cấp" : "Cập nhật thông tin nhà cung cấp";

                if (string.IsNullOrWhiteSpace(data.SupplierName))
                    ModelState.AddModelError(nameof(data.SupplierName), "Tên nhà cung cáp không được để trống");
                if (string.IsNullOrWhiteSpace(data.ContactName))
                    ModelState.AddModelError(nameof(data.ContactName), "Tên giao dịch không được để trống");
                if (string.IsNullOrWhiteSpace(data.Email))
                    ModelState.AddModelError(nameof(data.Email), "Vui lòng nhập Email của nhà cung cấp");
                if (string.IsNullOrWhiteSpace(data.Province))
                    ModelState.AddModelError("Province", "Vui lòng chọn tỉnh thành");
                if (!ModelState.IsValid)
                {
                    return View("Edit", data);
                }
                
                if (data.SupplierId == 0)
                {
                    int id = CommonDataService.AddSupplier(data);
                }
                else
                {
                    bool result = CommonDataService.UpdateSupplier(data);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Không thể lưu được dữ liệu. Vui lòng thử lại sau");
                return View("Edit", data);
            }
        }


        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Xóa thông tin nhà cung cấp";

            if (Request.Method == "POST")
            {
                CommonDataService.DeleteSupplier(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetSupplier(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }

            ViewBag.AllowDelete = !CommonDataService.isUsedSupplier(id);

            return View(model);
        }
    }
}

