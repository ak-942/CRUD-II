using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeDetails.DAL;
using EmployeeDetails.Models;

namespace EmployeeDetails.Controllers
{
    public class EmployeeController : Controller
    {
        Employee_DAL _employeeDAL = new Employee_DAL();
        public ActionResult Index()
        {
            var employeeList = _employeeDAL.GetAllEmployees();

            if (employeeList.Count == 0)
            {
                TempData["InfoMessage"] = "No employee details available";
            }
            return View(employeeList);
        }

        // GET: Employee/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(Employee employee)
        {
            bool IsInserted = false;

            try
            {
                var inputcode = employee.Empcode;

                var duplicates = (from d in _employeeDAL.GetAllEmployees()
                                  where d.Empcode == inputcode  select d).ToList();

                if (duplicates.Count == 0)
                {
                    if (ModelState.IsValid)
                    {
                        IsInserted = _employeeDAL.InsertDetail(employee);

                        if (IsInserted)
                            TempData["SuccessMessage"] = "Employee details saved";
                        else
                            TempData["ErrorMessage"] = "Employee details could not be saved";
                    }

                    return RedirectToAction("Index");
                }
                else
                {
                    var warning = "This employee is already exists!";
                    throw new InvalidOperationException(warning);
                }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            var employee = _employeeDAL.GetEmployeesbyID(id).FirstOrDefault();

            if (employee == null)
            {
                TempData["InfoMessage"] = "Employee not found";
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // POST: Employee/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateEmployee(Employee emp)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool IsUpdated = _employeeDAL.UpdateDetail(emp);

                    if (IsUpdated)
                        TempData["SuccessMessage"] = "Employee details updated";
                    else
                        TempData["ErrorMessage"] = "Employee details could not be saved";

                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var employee = _employeeDAL.GetEmployeesbyID(id).FirstOrDefault();

                if (employee == null)
                {
                    TempData["InfoMessage"] = "Employee details not available with id: " + id.ToString();
                    return RedirectToAction("Index");
                }

                return View(employee);
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
        // POST: Employee/Delete/5
        [HttpPost, ActionName("DELETE")]
        public ActionResult DeleteConfirmation(int id)
        {
            try
            {
                string result = _employeeDAL.DeleteDetail(id);
                if (result.Contains("deleted"))
                {
                    TempData["SuccessMessage"] = result;
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }
    }
}