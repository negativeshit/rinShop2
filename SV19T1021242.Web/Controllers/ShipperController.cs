using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SV19T1021242.BusinessLayers;
using SV19T1021242.DataLayers;
using SV19T1021242.DataLayers.SQLServer;
using SV19T1021242.DomainModels;
using SV19T1021242.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SV19T1021242.Web.Controllers
{
    [Authorize(Roles = $"{WebUserRoles.Administrator},{WebUserRoles.Employee}")]
    public class ShipperController : Controller
    {
        private const int PAGE_SIZE = 20;
        private const string SHIPPER_SEARCH = "shipper_search";
        public IActionResult Index()
        {
            PaginationSearchInput? input = ApplicationContext.GetSessionData<PaginationSearchInput>(SHIPPER_SEARCH);
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
            var data = CommonDataService.ListOfShippers(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "");
            var model = new ShipperSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                data = data
            };
            ApplicationContext.SetSessionData(SHIPPER_SEARCH, input);
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Bổ sung thông tin người giao hàng";
            Shipper model = new Shipper()
            {
                ShipperId = 0
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Cập nhật thông tin người giao hàng";
            Shipper? model = CommonDataService.GetShipper(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(Shipper data)
        {
            ViewBag.Title = data.ShipperId == 0 ? "Bổ sung người giao hàng" : "Cập nhật thông tin người giao hàng";
            if (string.IsNullOrWhiteSpace(data.ShipperName))
            {
                ModelState.AddModelError("ShipperName", "Tên không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Phone))
            {
                ModelState.AddModelError("Phone", "Số điện thoại người giao hàng không được để trống");
            }
            if (!ModelState.IsValid)
            {

                return View("Edit", data);
            }
            
            if (data.ShipperId == 0)
            {
                int id = CommonDataService.AddShipper(data);
                if (id <= 0)
                {
                    ModelState.AddModelError(nameof(data.ShipperName), "Tên người giao hàng trùng");
                    ViewBag.Title = "Bổ sung người giao hàng";
                    return View("Edit", data);
                }
            }
            else
            {
                bool result = CommonDataService.UpdateShipper(data);
                if (!result)
                {
                    ModelState.AddModelError("Error", "Không cập nhật được người giao hàng");
                    return View("Edit", data);
                }
            }
            return RedirectToAction("Index");

        }

        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Xóa thông tin người giao hàng";
            if (Request.Method == "POST")
            {
                CommonDataService.DeleteShipper(id);
                return RedirectToAction("Index");
            }

            var model = CommonDataService.GetShipper(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.AllowDelete = !CommonDataService.isUsedShipper(id);

            return View(model);
        }
    }
}

