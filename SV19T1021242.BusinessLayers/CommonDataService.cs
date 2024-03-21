using System;
using SV19T1021242.DataLayers;
using SV19T1021242.DataLayers.SQLServer;
using SV19T1021242.DomainModels;

namespace SV19T1021242.BusinessLayers
{
	/// <summary>
 /// Cung cap xu ly du lieu chung
 /// Tinh thanh`, khac'h hang`, nha` cung cap', loai hang`,nguoi` giao hang`,nhan vien
 /// </summary>
	public static class CommonDataService
	{
		private static readonly ICommonDAL<Province> ProvinceDB;
		private static readonly ICommonDAL<Customer> CustomerDB;
		private static readonly ICommonDAL<Category> CategoryDB;
        private static readonly ICommonDAL<Supplier> SupplierDB;
        private static readonly ICommonDAL<Shipper> ShipperDB;
        private static readonly ICommonDAL<Employee> EmployeeDB;
        static CommonDataService()
		{
			string connectionString = Configuration.ConnectionString;
			ProvinceDB = new ProvinceDAL(connectionString);
			CustomerDB = new CustomerDAL(connectionString);
			CategoryDB = new CategoryDAL(connectionString);
            SupplierDB = new SupplierDAL(connectionString);
            ShipperDB = new ShipperDAL(connectionString);
            EmployeeDB = new EmployeeDAL(connectionString);

        }
		/// <summary>
		/// danh sách tỉnh thành
		/// </summary>
		/// <returns></returns>
		public static List<Province> ListOfProvinces()
		{
			return ProvinceDB.List().ToList();
		}

		/// <summary>
		/// tìm kiếm lấy danh sách khách hàng
		/// </summary>
		/// <param name="rowCount"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="searchValue"></param>
		/// <returns></returns>
		public static List<Customer> ListOfCustomers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
		{
			rowCount = CustomerDB.Count(searchValue);
			return CustomerDB.List(page, pageSize, searchValue).ToList();
		}
		/// <summary>
		/// Lấy thong tin của 1 khách hàng
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static Customer? GetCustomer(int id)
		{
			return CustomerDB.Get(id);
		}
		/// <summary>
		/// thêm khách hàng
		/// </summary>
		/// <param name="customer"></param>
		/// <returns></returns>
		public static int AddCustomer(Customer customer)
		{
			return CustomerDB.Add(customer);
		}
		/// <summary>
		/// Cập nhật khách hàng
		/// </summary>
		/// <param name="customer"></param>
		/// <returns></returns>
		public static bool UpdateCustomer(Customer customer)
		{
			return CustomerDB.Update(customer);
		}
		/// <summary>
		/// Xoá khách hàng
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public static bool DeleteCustomer(int id)
		{
			if (CustomerDB.IsUsed(id))
				return false;
			return CustomerDB.Delete(id);
		}
		/// <summary>
		/// Kiểm tra khách hàng có dữ liệu liên quan không
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public  static bool isUsedCustomer(int id)
		{
			return CustomerDB.IsUsed(id);
		}
		/// <summary>
		/// Tìm kiếm loại hàng
		/// </summary>
		/// <param name="rowCount"></param>
		/// <param name="page"></param>
		/// <param name="pageSize"></param>
		/// <param name="searchValue"></param>
		/// <returns></returns>
        public static List<Category> ListOfCategories(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
		{
            rowCount = CategoryDB.Count(searchValue);
            return CategoryDB.List(page, pageSize, searchValue).ToList();
        }
		/// <summary>
		/// Lấy thông tin 1 loại hàng
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public static Category? GetCategory(int id)
        {
            return CategoryDB.Get(id);
        }
		/// <summary>
		/// Bổ sung loại hàng mới
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
        public static int AddCategory(Category category)
        {
            return CategoryDB.Add(category);
        }
		/// <summary>
		/// Cập nhật loại hàng
		/// </summary>
		/// <param name="category"></param>
		/// <returns></returns>
        public static bool UpdateCategory(Category category)
        {
            return CategoryDB.Update(category);
        }
		/// <summary>
		/// Xoá loại hàng
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public static bool DeleteCategory(int id)
        {
            if (CategoryDB.IsUsed(id))
                return false;
            return CategoryDB.Delete(id);
        }
		/// <summary>
		/// Kiểm tra xem có mã id có dữ liệu lq hay không?
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
        public static bool isUsedCategory(int id)
        {
            return CategoryDB.IsUsed(id);
        }



        /// <summary>
        /// Tìm kiếm nhân viên
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Employee> ListOfEmployees(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin 1 nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Employee? GetEmployee(int id)
        {
            return EmployeeDB.Get(id);
        }
        /// <summary>
        /// Bổ sung nhân viên mới
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee employee)
        {
            return EmployeeDB.Add(employee);
        }
        /// <summary>
        /// Cập nhật nhân viên
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee employee)
        {
            return EmployeeDB.Update(employee);
        }
        /// <summary>
        /// Xoá nhân viên
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteEmployee(int id)
        {
            if (EmployeeDB.IsUsed(id))
                return false;
            return EmployeeDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem có mã id có dữ liệu lq hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isUsedEmployee(int id)
        {
            return EmployeeDB.IsUsed(id);
        }

        /// <summary>
        /// Tìm kiếm NCC
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Supplier> ListOfSuppliers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin 1 ncc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Supplier? GetSupplier(int id)
        {
            return SupplierDB.Get(id);
        }
        /// <summary>
        /// Bổ sung NCC mới
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static int AddCategory(Supplier supplier)
        {
            return SupplierDB.Add(supplier);
        }
        /// <summary>
        /// Cập nhật NCC
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier supplier)
        {
            return SupplierDB.Update(supplier);
        }
        /// <summary>
        /// Xoá NCC
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteSupplier(int id)
        {
            if (SupplierDB.IsUsed(id))
                return false;
            return SupplierDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem có mã id có dữ liệu lq hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isUsedSupplier(int id)
        {
            return SupplierDB.IsUsed(id);
        }
        /// <summary>
        /// Tìm kiếm Shipper
        /// </summary>
        /// <param name="rowCount"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public static List<Shipper> ListOfShippers(out int rowCount, int page = 1, int pageSize = 0, string searchValue = "")
        {
            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue).ToList();
        }
        /// <summary>
        /// Lấy thông tin 1 ncc
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Shipper? GetShipper(int id)
        {
            return ShipperDB.Get(id);
        }
        /// <summary>
        /// Bổ sung NCC mới
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper shipper)
        {
            return ShipperDB.Add(shipper);
        }
        public static int AddSupplier(Supplier supplier)
        {
            return SupplierDB.Add(supplier);
        }
        /// <summary>
        /// Cập nhật NCC
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper shipper)
        {
            return ShipperDB.Update(shipper);
        }
        /// <summary>
        /// Xoá NCC
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeleteShipper(int id)
        {
            if (ShipperDB.IsUsed(id))
                return false;
            return ShipperDB.Delete(id);
        }
        /// <summary>
        /// Kiểm tra xem có mã id có dữ liệu lq hay không?
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool isUsedShipper(int id)
        {
            return ShipperDB.IsUsed(id);
        }
    }

}