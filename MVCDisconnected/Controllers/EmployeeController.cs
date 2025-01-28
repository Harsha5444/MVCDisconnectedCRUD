using MVCDisconnected.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MVCDisconnected.Controllers
{
    public class EmployeeController : Controller
    {
        EmployeeDBContext DbContext = new EmployeeDBContext();
        public ActionResult Index()
        {
            //DataTable dt = DbContext.GetEmployees();
            List<Employee> emps = DbContext.GetEmployeesList();
            return View(emps);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Add(Employee emp)
        {
            if (DbContext.insertEmployee(emp))
            {
                TempData["addsuccess"] = "New Employee Added Succuesfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["addfailed"] = "Failed To add, try again";
                return View();
            }            
        }
        public ActionResult Details(int id)
        {
            List<Employee> emps = DbContext.GetEmployeesList();
            var emp = emps.FirstOrDefault(row => row.Eno == id);
            return View(emp);
        }
        public ActionResult Edit(int id)
        {
            List<Employee> emps = DbContext.GetEmployeesList();
            var emp = emps.FirstOrDefault(row => row.Eno == id);
            return View(emp);
        }
        [HttpPost]
        public ActionResult Edit(Employee emp)
        {
            if (DbContext.updateEmployee(emp))
            {
                TempData["updatesuccess"] = "Employee Updated Succuesfully";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["updatefailed"] = "Failed To Update, try again";
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            if (DbContext.deleteEmployee(id))
            {
                TempData["deletesuccess"] = "Employee Deleted Succuesfully";                
            }
            else
            {
                TempData["deletefailed"] = "Failed To Delete, try again";
            }
            return RedirectToAction("Index");
        }
    }
}