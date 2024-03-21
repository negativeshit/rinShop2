using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV19T1021242.BusinessLayers;
using SV19T1021242.DomainModels;
using SV19T1021242.Web.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SV19T1021242.Web.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Administrator},{WebUserRoles.Employee}")]
    public class CategoryController : Controller
    {
        private const int PAGE_SIZE = 20;
        private const string CATEGORY_SEARCH = "category_search";
        public IActionResult Index()
        {
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(CATEGORY_SEARCH);
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
            var data = CommonDataService.ListOfCategories(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new CategorySearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                Data = data
            };
            ApplicationContext.SetSessionData(CATEGORY_SEARCH, input);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung loại hàng";
            Category model = new Category()
            {
                CategoryId = 0
            };

            return View("Edit", model);
        }

        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin loại hàng";
            Category? model = CommonDataService.GetCategory(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Category data)
        {
            try
            {
                ViewBag.Title = data.CategoryId == 0 ? "Bổ sung loại hàng" : "Cập nhật loại hàng";
                if (string.IsNullOrWhiteSpace(data.CategoryName))
                {
                    ModelState.AddModelError("CategoryName", "Tên loại hàng không được để trống!");
                }
                if (string.IsNullOrWhiteSpace(data.Description))
                {
                    ModelState.AddModelError("Description", "Ghi chú không được để trống!");
                }
                

                if (data.CategoryId == 0)
                {
                    int id = CommonDataService.AddCategory(data);
                }
                else
                {
                    bool result = CommonDataService.UpdateCategory(data);
                }
                return RedirectToAction("Index");

            }
            catch (Exception e)
            {
                ModelState.AddModelError("Error", "Đã xảy ra lỗi, vui lòng thử lại sau!");
                return View("Edit", data);
            }
        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Xóa thông tin loại hàng";

            if (Request.Method == "POST")
            {
                CommonDataService.DeleteCategory(id);
                return RedirectToAction("Index");
            }
            var model = CommonDataService.GetCategory(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.isUsedCategory(id);
            return View(model);
        }
    }
}
