using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SV19T1021242.Web.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Create()
        {
            var model = new Models.Person()
            {
                Name = "Test",
                BirthDate = DateTime.Now,
                Salary = 10.25m
            };
            return View(model);
        }

        public IActionResult Save(Models.Person model, string birthDateInput = "")
        {
            DateTime? dValue = StringToDateTime(birthDateInput);
            if (dValue.HasValue)
            {
                model.BirthDate = dValue.Value;
            }
            return Json(model);
        }
        private DateTime? StringToDateTime(string s, string formats= "d/M/yyyy;d-M-yyyy;d.M.yyyy")
        {
            try
            {
                return DateTime.ParseExact(s, formats.Split(";"),CultureInfo.InvariantCulture);
            }
            catch
            {
                return null;
            }
        }
    }
}

