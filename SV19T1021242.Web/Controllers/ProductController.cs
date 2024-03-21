using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using SV19T1021242.BusinessLayers;
using SV19T1021242.DomainModels;
using SV19T1021242.Web.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SV19T1021242.Web.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        // GET: /<controller>/
        private const int PAGE_SIZE = 20;
        private const string PRODUCT_SEARCH = "product_search";

        public IActionResult Index()
        {
            //Lấy dầu vào tìm kiếm hiên đang lưu lại trong session
            PaginationSearchInput? input = ApplicationContext.GetSessionData<ProductSearchInput>(PRODUCT_SEARCH);
            //Trường hợp trong session chưa có điều kiên thì tạo điều kiên mới
            if (input == null)
            {
                input = new ProductSearchInput()
                {
                    Page = 1,
                    PageSize = PAGE_SIZE,
                    SearchValue = "",
                    CategoryID = 0,
                    SupplierID = 0,
                    minPrice = 0,
                    maxPrice = 0,
                };
            }

            return View(input);
        }

        public IActionResult Search(ProductSearchInput input)
        {
            int rowCount = 0;
            var data = ProductDataService.ListProducts(out rowCount, input.Page, input.PageSize, input.SearchValue ?? "",
                                                           input.CategoryID, input.SupplierID, input.minPrice, input.maxPrice);
            ApplicationContext.SetSessionData(PRODUCT_SEARCH, input);
            var model = new ProductSearchResult()
            {
                Page = input.Page,
                PageSize = input.PageSize,
                SearchValue = input.SearchValue ?? "",
                RowCount = rowCount,
                data = data
            };
            return View(model);
        }
        public IActionResult Create()
        {
            ViewBag.Title = "Thêm hàng hoá";
            
            Product model = new Product()
            {
                ProductID = 0,
               
                
            };
            return View("Edit", model);
        }
        public IActionResult Edit(int id)
        {
            ViewBag.Title = "Chỉnh sửa thông tin hàng hoá";
            
            Product? model = ProductDataService.GetProduct(id);
            if (model == null)
                return RedirectToAction("Index");

            return View(model);
        }
        
        public IActionResult Delete(int id)
        {
            if (Request.Method == "POST")
            {
                ProductDataService.DeleteProduct(id);
                return RedirectToAction("Index");
            }
            var model = ProductDataService.GetProduct(id);
            if (model == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.allowDelete = !ProductDataService.IsUsedProduct(id);
            //return Json(model);
            return View(model);

        }
        public IActionResult Photo(int id, string method,int photoId = 0) 
        {
           
            ProductPhoto? model = new ProductPhoto();
            switch (method)
            {
                case "add":
                    
                    ViewBag.Title = "Bổ sung ảnh";
                    model = new ProductPhoto()
                    {
                        PhotoID = 0,
                        ProductID = id
                    };
                    return View(model);
                case "edit":
                    model = ProductDataService.GetPhoto(photoId);
                    if (model == null)
                        return RedirectToAction("Edit", new { id = id });

                    return View(model);
                case "delete":
                    ProductDataService.DeletePhoto(photoId);
                    return RedirectToAction("Edit", new { id = id });
                default:
                    return RedirectToAction("Index");
            }
        }
        public IActionResult Attribute(int id, string method, int attributeId = 0)
        {
            ProductAttribute? model = new ProductAttribute();
            switch (method)
            {
                case "add":

                    ViewBag.Title = "Bổ sung ảnh";
                    model = new ProductAttribute()
                    {
                        AttributeID = 0,
                        ProductID = id
                    };
                    return View(model);
                case "edit":
                    model = ProductDataService.GetAttribute(attributeId);
                    if (model == null)
                        return RedirectToAction("Edit", new { id = id });

                    return View(model);
                case "delete":
                    ProductDataService.DeleteAttribute(Convert.ToInt64(attributeId));
                    return RedirectToAction("Edit", new { id = id });
                default:
                    return RedirectToAction("Index");
            }
        }
        [ValidateAntiForgeryToken]
        public IActionResult Save(Product data, IFormFile? uploadPhoto)
        {
            if (string.IsNullOrWhiteSpace(data.ProductDescription))
            {
                ModelState.AddModelError("ProductDescription", "Mô tả không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.ProductName))
            {
                ModelState.AddModelError("ProductName", "Tên mặt hàng không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.SupplierID.ToString()))
            {
                ModelState.AddModelError("SupplierID", "Tên nhà cung cấp không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.CategoryId.ToString()))
            {
                ModelState.AddModelError("CategoryID", "Tên loại hàng không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Unit))
            {
                ModelState.AddModelError("Unit", "Đơn vị tính không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Price.ToString()))
            {
                ModelState.AddModelError("Price", "Giá không được để trống");
            }
            if (uploadPhoto != null)
            {
                string fileName = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string folder = Path.Combine(ApplicationContext.HostEnviroment.WebRootPath, @"images/products");
                string filepath = Path.Combine(folder, fileName);
                using (var stream = new FileStream(filepath, FileMode.Create))
                {
                    uploadPhoto.CopyTo(stream);
                }
                data.Photo = fileName;
            }
            
            if (!ModelState.IsValid)
            {
                
                ViewBag.Title = data.ProductID == 0 ? "Bổ sung mặt hàng" : "Cập nhật mặt hàng";
                return View("Edit", data);
            }
            if (data.ProductID == 0)
            {
                
                ProductDataService.AddProduct(data);
                

            }
            else
            {
                ProductDataService.UpdateProduct(data);

            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult SavePhoto(ProductPhoto data, IFormFile uploadPhoto)
        {
            //Xử lý ảnh upload
            if (uploadPhoto != null)
            {
                string filename = $"{DateTime.Now.Ticks}_{uploadPhoto.FileName}";
                string folder = Path.Combine(ApplicationContext.HostEnviroment.WebRootPath, @"images/products");
                string filePath = Path.Combine(folder, filename);// Đương dẫn đến file cần lưu

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadPhoto.CopyTo(stream);
                }
                data.Photo = filename;
            }
            if (string.IsNullOrWhiteSpace(data.Photo))
            {
                ModelState.AddModelError("Photo", "Ảnh không được để trống");
            }
            if (string.IsNullOrWhiteSpace(data.Description))
            {
                ModelState.AddModelError("Description", "Mô tả không được để trống");
            }

            if (string.IsNullOrWhiteSpace(data.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự không được để trống");
            }

            
            if (!ModelState.IsValid)
            {
                return View("Photo", data);
            }
            if (data.PhotoID == 0)
            {
                ProductDataService.AddPhoto(data);
               
            }
            else
            {
                ProductDataService.UpdatePhoto(data);

            }

            return RedirectToAction("Edit", new { productID = data.ProductID });
        }
        [HttpPost]
        public IActionResult SaveAttribute(ProductAttribute? model)
        {
            if (string.IsNullOrWhiteSpace(model.AttributeName))
            {
                ModelState.AddModelError("AttributeName", "Tên thuộc tính không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.AttributeValue))
            {
                ModelState.AddModelError("AttributeValue", "Giá trị không được để trống");
            }
            if (string.IsNullOrWhiteSpace(model.DisplayOrder.ToString()))
            {
                ModelState.AddModelError("DisplayOrder", "Thứ tự không được để trống");
            }

            if (!ModelState.IsValid)
            {

                return View("Attribute", model);
            }

            if (model.AttributeID == 0)
            {

                ProductDataService.AddAttribute(model);
                return View("Edit", model);

            }
            else
            {
                ProductDataService.UpdateAttribute(model);
                return RedirectToAction("Edit", new { productID = model.ProductID });
            }
           


        }

    }
}

