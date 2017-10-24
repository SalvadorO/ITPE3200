using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Controllers;
using WebApp.BLL;
using WebApp.DAL;
using WebApp.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;

namespace UnitTest
{
    [TestClass]
    public class AdminControllerTest
    {
        [TestMethod]
        public void List_Employee()
        {
            //Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var expectedList = new List<Employee>();
            var employee = new Employee()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                Username = "Olanord"
            };
            expectedList.Add(employee);
            expectedList.Add(employee);
            expectedList.Add(employee);

            //Act
            var actionresult = (PartialViewResult)controller.ListAllEmployees();
            var result = (List<Employee>)actionresult.Model;

            //Assert
            Assert.AreEqual(actionresult.ViewName, "_ListEmployee");

            for(var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedList[i].ID, result[i].ID);
                Assert.AreEqual(expectedList[i].FirstName, result[i].FirstName);
                Assert.AreEqual(expectedList[i].LastName, result[i].LastName);
                Assert.AreEqual(expectedList[i].Username, result[i].Username);
            }
        }

        [TestMethod]
        public void Edit_Employee()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var actionResult = (ViewResult)controller.EditEmployee(1);

            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Edit_Employee_Not_Found_View()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var actionResult = (ViewResult)controller.EditEmployee(0);
            var result = (Employee)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(result.ID, 0);

        }

        [TestMethod]
        public void Edit_Employee_Not_Found_Post()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var inEmp = new Employee()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                Username = "Olanord"
            };

            var actionResult = (RedirectToRouteResult)controller.EditEmployee(0, inEmp);
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
            Assert.AreEqual(actionResult.RouteName, "");

        }

        [TestMethod]
        public void Edit_Employee_Error_validate_Post()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var inEmp = new Employee();

            var actionResult = (RedirectToRouteResult)controller.EditEmployee(0, inEmp);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
            Assert.AreEqual(actionResult.RouteName, "");
        }

        [TestMethod]
        public void Edit_Employee_found()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inEmp = new Employee()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                Username = "Olanord"
            };
            var actionResultat = (RedirectToRouteResult)controller.EditEmployee(1, inEmp);

            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListEmployee");
        }

        [TestMethod]
        public void Delete_Employee()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var actionResult = (RedirectToRouteResult)controller.DeleteEmployee(1);

            Assert.AreEqual(actionResult.RouteName, "");
        }
        [TestMethod]
        public void Delete_Employee_found_Post()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inEmp = new Employee()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                Username = "Olanord"
            };

            var actionResult = (RedirectToRouteResult)controller.DeleteEmployee(1);

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListEmployee");
        }
        [TestMethod]
        public void Delete_Employee_Not_Found_Post()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inEmp = new Employee()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                Username = "Olanord"
            };

            var actionResult = (RedirectToRouteResult)controller.DeleteEmployee(0);

            Assert.AreEqual(actionResult.RouteName, "");
        }

        [TestMethod]
        public void Insert_Employee()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var actionResult = (ViewResult)controller.Register();

            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void Insert_Employee_Post_OK()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));


            var inEmp = new EmployeeRegister()
            {
                Password = "passord",
                ConfirmPassword = "passord",
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                Username = "Olanord"
            };
            var result = (ViewResult)controller.Register(inEmp);

            Assert.AreEqual(result.ViewName, "");
        }
        [TestMethod]
        public void Insert_Employee_Post_Model_Error()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inEmp = new EmployeeRegister();
            controller.ViewData.ModelState.AddModelError("fornavn", "Ikke oppgitt fornavn");
            var actionResult = (RedirectToRouteResult)controller.Register(inEmp);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
            Assert.AreEqual(actionResult.RouteName, "");
        }
        [TestMethod]
        public void Insert_Employee_Post_DB_Error()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inEmp = new EmployeeRegister();
            inEmp.FirstName = "";

            var actionResult = (RedirectToRouteResult)controller.Register(inEmp);
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
            Assert.AreEqual(actionResult.RouteName, "");
        }









        [TestMethod]
        public void List_Contactperson()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var expectedList = new List<AdminCustomer>();
            var customer = new AdminCustomer()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                ContactPerson = true
            };
            expectedList.Add(customer);
            expectedList.Add(customer);
            expectedList.Add(customer);

            var actionResult = (PartialViewResult)controller.ListContactPersons();
            var resultat = (List<AdminCustomer>)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "_ListCustomer");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(expectedList[i].ID, resultat[i].ID);
                Assert.AreEqual(expectedList[i].FirstName, resultat[i].FirstName);
                Assert.AreEqual(expectedList[i].LastName, resultat[i].LastName);
                Assert.AreEqual(expectedList[i].ContactPerson, resultat[i].ContactPerson);
            }
        }
       
        [TestMethod]
        public void Detail_Customer()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var expected = new AdminCustomer()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                ContactPerson = false
            };
            var actionResult = (ViewResult)controller.DetailCustomer(1);
            var resultat = (List<AdminCustomer>)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(expected.ID, resultat[0].ID);
            Assert.AreEqual(expected.FirstName, resultat[0].FirstName);
            Assert.AreEqual(expected.LastName, resultat[0].LastName);
            Assert.AreEqual(expected.PhoneNumber, resultat[0].PhoneNumber);
            Assert.AreEqual(expected.EMail, resultat[0].EMail);
            Assert.AreEqual(expected.ContactPerson, resultat[0].ContactPerson);
            Assert.AreEqual(expected.Address, resultat[0].Address);
            Assert.AreEqual(expected.ZipCode, resultat[0].ZipCode);
            Assert.AreEqual(expected.City, resultat[0].City);
        }
        [TestMethod]
        public void Delete_Customer()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));


            var actionResult = (RedirectToRouteResult)controller.DeleteCustomer(1);

            Assert.AreEqual(actionResult.RouteValues.Values.First(),"ListCustomer");
            Assert.AreEqual(actionResult.RouteName, "");


        }
        [TestMethod]
        public void Delete_Customer_Found_Post()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inCust = new AdminCustomer()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                ContactPerson = false
            };

            var actionResult = (RedirectToRouteResult)controller.DeleteCustomer(1);

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListCustomer");
        }
        [TestMethod]
        public void Delete_Customer_Not_Found_Post()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inCust = new AdminCustomer()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                ContactPerson = false
            };

            var actionResult = (RedirectToRouteResult)controller.DeleteCustomer(0);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Customer()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));


            var actionResult = (ViewResult)controller.EditCustomer(1);

            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void Edit_Customer_Not_Found_In_View()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));


            var actionResult = (ViewResult)controller.EditCustomer(0);
            var result = (AdminCustomer)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(result.ID, 0);
        }
        [TestMethod]
        public void Edit_Customer_Not_Found_Post()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inCust = new AdminCustomer()
            {
                ID = 0,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                ContactPerson = false
            };

            var actionResult = (RedirectToRouteResult)controller.EditCustomer(0, inCust);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Customer_Error_Validation_Post()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inCust = new AdminCustomer();

            var actionResult = (RedirectToRouteResult)controller.EditCustomer(0, inCust);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Customer_Found()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inCust = new AdminCustomer()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                PhoneNumber = "12345678",
                EMail = "Ola@mail.no",
                Address = "Osloveien 4",
                ZipCode = "1234",
                City = "Oslo",
                ContactPerson = false
            };
            var actionResultat = (RedirectToRouteResult)controller.EditCustomer(1, inCust);

            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListCustomer");
        }

    }
}
