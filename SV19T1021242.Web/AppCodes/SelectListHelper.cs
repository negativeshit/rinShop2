using System;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Mvc.Rendering;
using SV19T1021242.BusinessLayers;

namespace SV19T1021242.Web
{

  
    public static class SelectListHelper
	{
        /// <summary>
        /// Danh sách tỉnh thành
        /// </summary>
        /// <returns></returns>
        private const int PAGE_SIZE = 20;
        public static List<SelectListItem> Provinces()
		{
			List<SelectListItem> list = new List<SelectListItem>();
			list.Add(new SelectListItem()
			{
				Value = "",
				Text ="-- Chọn tỉnh thành --",
			}) ;
			foreach( var item in CommonDataService.ListOfProvinces())
			{
				list.Add(new SelectListItem()
				{
					Value = item.ProvinceName,
					Text = item.ProvinceName 
				});
			}
			return list;
		}
        public static List<SelectListItem> SelectSuppliers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "0", Text = "--Nhà cung cấp--" });
            int rowCount = 0;
            foreach (var c in CommonDataService.ListOfSuppliers(out rowCount, 1,0,""))
            {

                list.Add(new SelectListItem()
                {

                    Value = c.SupplierId.ToString(),
                    Text = c.SupplierName

                });


            }
            return list;
        }
        public static List<SelectListItem> SelectCategories()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "0", Text = "--Loại hàng--" });
            int rowCount = 0;
            foreach (var c in CommonDataService.ListOfCategories(out rowCount, 1, 0, ""))
            {
                list.Add(new SelectListItem()
                {

                    Value = c.CategoryId.ToString(),
                    Text = c.CategoryName

                });
            }
            return list;
        }
        public static List<SelectListItem> SelectShippers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "0", Text = "--Chọn người giao hàng--" });
            int rowCount = 0;
            foreach (var c in CommonDataService.ListOfShippers(out rowCount, 1, 0, ""))
            {

                list.Add(new SelectListItem()
                {

                    Value = c.ShipperId.ToString(),
                    Text = c.ShipperName

                });


            }
            return list;
        }
        public static List<SelectListItem> SelectCustomers()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "0", Text = "-- Chọn khách hàng--" });
            int rowCount = 0;
            foreach (var c in CommonDataService.ListOfCustomers(out rowCount, 1, 0, ""))
            {
                list.Add(new SelectListItem()
                {

                    Value = c.CustomerId.ToString(),
                    Text = c.CustomerName

                });
            }
            return list;
        }
        
    }
}

