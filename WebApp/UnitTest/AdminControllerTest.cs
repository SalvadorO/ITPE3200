using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApp.Controllers;
using WebApp.BLL;
using WebApp.DAL;
using WebApp.Model;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
using MvcContrib.TestHelper;

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
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            controller.TempData["SelfDelete"] = false;


            var actionResult = (ViewResult)controller.EditEmployee(1);

            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Edit_Employee_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionResult = (RedirectToRouteResult)controller.EditEmployee(1);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }

        [TestMethod]
        public void Edit_Employee_Not_Found_View()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

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
        public void Delete_Employee_SelfDelete()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["Username"] = "Olanord";
            controller.TempData["SelfDelete"] = "";
            var actionResult = (RedirectToRouteResult)controller.DeleteEmployee(1);
            var result = actionResult.RouteValues.Values.ToList();

            Assert.AreEqual(result[0], 1);
            Assert.AreEqual(result[1], "EditEmployee");

        }
        [TestMethod]
        public void Delete_Employee_OK()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["Username"] = "olaNy";

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
                Username = "OlaNy"
            };

            var actionResult = (RedirectToRouteResult)controller.DeleteEmployee(1);

            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListEmployee");
        }
        [TestMethod]
        public void Delete_Employee_Not_Found_Post()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["Username"] = "Olanord";

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

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }

        [TestMethod]
        public void Detail_Employee()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            var expected = new Employee()
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
            var actionResult = (ViewResult)controller.DetailEmployee(1);
            var resultat = (Employee)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(expected.ID, resultat.ID);
            Assert.AreEqual(expected.FirstName, resultat.FirstName);
            Assert.AreEqual(expected.LastName, resultat.LastName);
            Assert.AreEqual(expected.PhoneNumber, resultat.PhoneNumber);
            Assert.AreEqual(expected.EMail, resultat.EMail);
            Assert.AreEqual(expected.Username, resultat.Username);
            Assert.AreEqual(expected.Address, resultat.Address);
            Assert.AreEqual(expected.ZipCode, resultat.ZipCode);
            Assert.AreEqual(expected.City, resultat.City);
        }

        [TestMethod]
        public void Detail_Employee_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionResult = (RedirectToRouteResult)controller.DetailEmployee(1);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
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
            var result = (RedirectToRouteResult)controller.Register(inEmp);

            Assert.AreEqual(result.RouteValues.Values.First(), "ListEmployee");
        }
        [TestMethod]
        public void Insert_Employee_Username_Exists()
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
                Username = "admin"
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
        public void Detail_Customer_Logged_In_OK()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

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
        public void Detail_Customer_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionResult = (RedirectToRouteResult)controller.DetailCustomer(1);
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");


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
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;


            var actionResult = (ViewResult)controller.EditCustomer(1);

            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void Edit_Customer_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;


            var actionResult = (RedirectToRouteResult)controller.EditCustomer(1);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void Edit_Customer_Not_Found_In_View()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;


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


        [TestMethod]
        public void List_Flights()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var expected = new List<AdminViewFlight>();
            var flight = new AdminViewFlight()
            {
                 ID = 1,
                  Departure = "Oslo",
                  DepartureTime = "00:00",
                  Destination = "Bardufoss",
                  DestinationTime = "00:00",
                  Airplane = "Boeing737",
                  ClassType = "Økonomi",
                  Price = 500,
                  Seats = 500,
                  TravelDate = "00/00/00"
            };
            expected.Add(flight);
            expected.Add(flight);
            expected.Add(flight);

            // Act
            var actionResult = (PartialViewResult)controller.ListAllFlights();
            var resultat = (List<AdminViewFlight>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "_ListFlight");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(expected[i].ID, resultat[i].ID);
                Assert.AreEqual(expected[i].Departure, resultat[i].Departure);
                Assert.AreEqual(expected[i].DepartureTime, resultat[i].DepartureTime);
                Assert.AreEqual(expected[i].Destination, resultat[i].Destination);
                Assert.AreEqual(expected[i].DestinationTime, resultat[i].DestinationTime);
                Assert.AreEqual(expected[i].Airplane, resultat[i].Airplane);
                Assert.AreEqual(expected[i].ClassType, resultat[i].ClassType);
                Assert.AreEqual(expected[i].Price, resultat[i].Price);
                Assert.AreEqual(expected[i].Seats, resultat[i].Seats);
                Assert.AreEqual(expected[i].TravelDate, resultat[i].TravelDate);
            }
        }
        [TestMethod]
        public void New_Flight()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;


            // Act
            var actionResult = (ViewResult)controller.NewFlight();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void New_Flight_Not_Logged_In()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;


            // Act
            var actionResult = (RedirectToRouteResult)controller.NewFlight();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void New_Flight_Post_OK()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));


            var inFlight = new AdminFlight()
            {
                ID = 1,
                Departure = 1,
                DepartureTime = "00:00",
                Destination = 2,
                DestinationTime = "00:00",
                Airplane = 1,
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };
            // Act
            var result = (RedirectToRouteResult)controller.NewFlight(inFlight);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListFlight");
        }
        [TestMethod]
        public void New_FLight_Post_Model_error()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inFlight = new AdminFlight();
            controller.ViewData.ModelState.AddModelError("DepartureTime", "Ikke oppgitt avgangstid");

            // Act
            var actionResult = (RedirectToRouteResult)controller.NewFlight(inFlight);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void New_Flight_Post_DB_error()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inflight = new AdminFlight();
            inflight.DepartureTime = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.NewFlight(inflight);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }

        [TestMethod]
        public void Delete_Flight()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteFlight(1);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListFlight");


        }
        [TestMethod]
        public void Delete_Flight_Found_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inflight = new AdminFlight()
            {
                ID = 1,
                Departure = 1,
                DepartureTime = "00:00",
                Destination = 2,
                DestinationTime = "00:00",
                Airplane = 1,
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteFlight(1);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListFlight");
        }
        [TestMethod]
        public void Delete_Flight_Not_Found_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inflight = new AdminFlight()
            {
                ID = 1,
                Departure = 1,
                DepartureTime = "00:00",
                Destination = 2,
                DestinationTime = "00:00",
                Airplane = 1,
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteFlight(0);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Flight()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;


            // Act
            var actionResult = (ViewResult)controller.EditFlight(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void Edit_Flight_Not_Logged_In()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;


            // Act
            var actionResult = (RedirectToRouteResult)controller.EditFlight(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void Edit_Flight_Not_Found_In_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;


            // Act
            var actionResult = (ViewResult)controller.EditFlight(0);
            var kundeResultat = (AdminFlight)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(kundeResultat.ID, 0);
        }
        [TestMethod]
        public void Edit_Flight_Not_Found_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inFlight = new AdminFlight()
            {
                ID = 1,
                Departure = 1,
                DepartureTime = "00:00",
                Destination = 2,
                DestinationTime = "00:00",
                Airplane = 1,
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditFlight(0, inFlight);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Flight_error_Validation_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inflight = new AdminFlight();

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditFlight(0, inflight);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Flight_Found()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inflight = new AdminFlight()
            {
                ID = 1,
                Departure = 1,
                DepartureTime = "00:00",
                Destination = 2,
                DestinationTime = "00:00",
                Airplane = 1,
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EditFlight(1, inflight);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListFlight");
        }

        [TestMethod]
        public void Get_Info()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            var expected = new List<int>();
            expected.Add(1111);
            expected.Add(2222);
            expected.Add(3333);
            expected.Add(4444);
            expected.Add(5555);


            var actionResult = (ViewResult)controller.MainPage();
            var resultat = (List<int>)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < resultat.Count; i++)
            {
                Assert.AreEqual(expected[i], resultat[i]);
            }

        }
        [TestMethod]
        public void Get_Info_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;
            var actionResult = (RedirectToRouteResult)controller.MainPage();
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }

        [TestMethod]
        public void List_Airport()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            var expected = new List<AdminAirport>();
            var airport = new AdminAirport()
            {
                 ID = 1,
                  Name = "Oslo",
                   Country = "Norge"
            };
            var airport2 = new AdminAirport()
            {
                ID = 2,
                Name = "Bardufoss",
                Country = "Norge"
            };
            expected.Add(airport);
            expected.Add(airport2);

            // Act
            var actionResult = (ViewResult)controller.ListAirport();
            var result = (List<AdminAirport>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expected[i].ID, result[i].ID);
                Assert.AreEqual(expected[i].Name, result[i].Name);
                Assert.AreEqual(expected[i].Country, result[i].Country);
            }

        }

        [TestMethod]
        public void List_Airport_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionResult = (RedirectToRouteResult)controller.ListAirport();
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");


        }
        [TestMethod]
        public void New_Airport()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            // Act
            var actionResult = (ViewResult)controller.NewAirport();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void New_Airport_Not_Logged_In()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            // Act
            var actionResult = (RedirectToRouteResult)controller.NewAirport();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void New_Airport_Post_OK()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));


            var inAirport = new AdminAirport()
            {
                ID = 3,
                Name = "Bergen",
                Country = "Norge"
            };
            // Act
            var result = (RedirectToRouteResult)controller.NewAirport(inAirport);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListAirport");
        }
        [TestMethod]
        public void New_Airport_Post_Model_error()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairport = new AdminAirport();
            controller.ViewData.ModelState.AddModelError("Name", "Ikke oppgitt navn");

            // Act
            var actionResult = (RedirectToRouteResult)controller.NewAirport(inairport);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void New_Airport_Post_DB_error()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairport = new AdminAirport();
            inairport.Name = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.NewAirport(inairport);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        
        [TestMethod]
        public void Delete_Airport()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteAirport(1);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListAirport");


        }
        
        [TestMethod]
        public void Delete_Airport_Not_Found()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteAirport(0);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Airport()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;


            // Act
            var actionResult = (ViewResult)controller.EditAirport(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Edit_Airport_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditAirport(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void Edit_Airport_Not_Found_In_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EditAirport(0);
            var result = (AdminAirport)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(result.ID, 0);
        }
        [TestMethod]
        public void Edit_Airport_Not_Found_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairport = new AdminAirport()
            {
                ID = 0,
                Name = "Oslo",
                Country = "Norge"
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditAirport(0, inairport);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Airport_error_validation_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairport = new AdminAirport();
            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditAirport(0, inairport);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Airport_Found()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairport = new AdminAirport()
            {
                Name = "Bergen",
                Country = "Norge"
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EditAirport(1, inairport);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListAirport");
        }

        [TestMethod]
        public void List_Airplane()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            var expected = new List<AdminAirplane>();
            var airplane = new AdminAirplane()
            {
                ID = 1,
                Name = "Boeing737",
                Seats = 500
            };
            expected.Add(airplane);
            expected.Add(airplane);
            expected.Add(airplane);

            // Act
            var actionResult = (ViewResult)controller.ListAirplane();
            var result = (List<AdminAirplane>)actionResult.Model;
            // Assert

            Assert.AreEqual(actionResult.ViewName, "");

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expected[i].ID, result[i].ID);
                Assert.AreEqual(expected[i].Name, result[i].Name);
                Assert.AreEqual(expected[i].Seats, result[i].Seats);
            }

        }

        [TestMethod]
        public void List_Airplane_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionResult = (RedirectToRouteResult)controller.ListAirplane();
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");


        }

        [TestMethod]
        public void New_Airplane()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            // Act
            var actionResult = (ViewResult)controller.NewAirplane();

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }
        [TestMethod]
        public void New_Airplane_Not_Logged_In()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            // Act
            var actionResult = (RedirectToRouteResult)controller.NewAirplane();

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void New_Airplane_Post_OK()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));


            var inairplane = new AdminAirplane()
            {
                ID = 1,
                Name = "Boeing737",
                Seats = 500
            };
            // Act
            var result = (RedirectToRouteResult)controller.NewAirplane(inairplane);

            // Assert
            Assert.AreEqual(result.RouteName, "");
            Assert.AreEqual(result.RouteValues.Values.First(), "ListAirplane");
        }
        [TestMethod]
        public void New_Airplane_Post_Model_error()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairplane = new AdminAirplane();
            controller.ViewData.ModelState.AddModelError("Name", "Ikke oppgitt navn");

            // Act
            var actionResult = (RedirectToRouteResult)controller.NewAirplane(inairplane);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void New_Airplane_Post_DB_error()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairplane = new AdminAirplane();
            inairplane.Name = "";

            // Act
            var actionResult = (RedirectToRouteResult)controller.NewAirplane(inairplane);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }

        [TestMethod]
        public void Delete_Airplane()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteAirplane(1);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "ListAirplane");


        }

        [TestMethod]
        public void Delete_Airplane_Not_Found()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            // Act
            var actionResult = (RedirectToRouteResult)controller.DeleteAirplane(0);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Airplane()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EditAirplane(1);

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
        }

        [TestMethod]
        public void Edit_Airplane_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditAirplane(1);

            // Assert
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void Edit_Airplane_Not_Found_In_View()
        {
            // Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            // Act
            var actionResult = (ViewResult)controller.EditAirplane(0);
            var result = (AdminAirplane)actionResult.Model;

            // Assert
            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(result.ID, 0);
        }
        [TestMethod]
        public void Edit_Airplane_Not_Found_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairplane = new AdminAirplane()
            {
                ID = 0,
                Name = "Boeing737",
                Seats = 500
            };

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditAirplane(0, inairplane);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Airplane_error_validation_Post()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairplane = new AdminAirplane();
            controller.ViewData.ModelState.AddModelError("feil", "ID = 0");

            // Act
            var actionResult = (RedirectToRouteResult)controller.EditAirplane(0, inairplane);

            // Assert
            Assert.AreEqual(actionResult.RouteName, "");
            Assert.AreEqual(actionResult.RouteValues.Values.First(), "Error");
        }
        [TestMethod]
        public void Edit_Airplane_Found()
        {
            // Arrange
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var inairplane = new AdminAirplane()
            {
                Name = "Boeing737",
                Seats = 500
            };
            // Act
            var actionResultat = (RedirectToRouteResult)controller.EditAirplane(1, inairplane);

            // Assert
            Assert.AreEqual(actionResultat.RouteName, "");
            Assert.AreEqual(actionResultat.RouteValues.Values.First(), "ListAirplane");
        }


        [TestMethod]
        public void Get_Passengers()
        {
            //Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            var expectedList = new List<AdminCustomer>();
            var passenger = new AdminCustomer()
            {
                ID = 1,
                FirstName = "Ola",
                LastName = "Nordmann",
                ContactPerson = false
            };
            expectedList.Add(passenger);
            expectedList.Add(passenger);
            expectedList.Add(passenger);

            //Act
            var actionresult = (ViewResult)controller.Passengers(1);
            var result = (List<AdminCustomer>)actionresult.Model;

            //Assert
            Assert.AreEqual(actionresult.ViewName, "");

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedList[i].ID, result[i].ID);
                Assert.AreEqual(expectedList[i].FirstName, result[i].FirstName);
                Assert.AreEqual(expectedList[i].LastName, result[i].LastName);
                Assert.AreEqual(expectedList[i].ContactPerson, result[i].ContactPerson);
            }
        }

        [TestMethod]
        public void Get_Passengers_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionresult = (RedirectToRouteResult)controller.Passengers(1);
            Assert.AreEqual(actionresult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void Get_Passengers_Not_Found()
        {
            //Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            //Act
            var actionresult = (ViewResult)controller.Passengers(0);
            var result = (List<AdminCustomer>)actionresult.Model;

            //Assert
            Assert.AreEqual(actionresult.ViewName, "");
            Assert.AreEqual(result[0].ID, 0);
        }

        [TestMethod]
        public void Search_Customer_OK()
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
            var actionresult = (PartialViewResult)controller.SearchCustomers(1);
            var result = (List<AdminCustomer>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "_ListCustomer");
            Assert.AreEqual(expected.ID, result[0].ID);
            Assert.AreEqual(expected.FirstName, result[0].FirstName);
            Assert.AreEqual(expected.LastName, result[0].LastName);
            Assert.AreEqual(expected.PhoneNumber, result[0].PhoneNumber);
            Assert.AreEqual(expected.EMail, result[0].EMail);
            Assert.AreEqual(expected.ContactPerson, result[0].ContactPerson);
            Assert.AreEqual(expected.Address, result[0].Address);
            Assert.AreEqual(expected.ZipCode, result[0].ZipCode);
            Assert.AreEqual(expected.City, result[0].City);

           


        }

        [TestMethod]
        public void Search_Customer_error()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var actionresult = (PartialViewResult)controller.SearchCustomers(0);
            var result = (List<AdminCustomer>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "_ListCustomer");
            Assert.AreEqual(result[0].ID, 0);
        }


        [TestMethod]
        public void Search_Employee_OK()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var expected = new Employee()
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
            var actionresult = (PartialViewResult)controller.SearchEmployee("Olanord");
            var result = (List<Employee>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "_ListEmployee");
            Assert.AreEqual(expected.ID, result[0].ID);
            Assert.AreEqual(expected.FirstName, result[0].FirstName);
            Assert.AreEqual(expected.LastName, result[0].LastName);
            Assert.AreEqual(expected.PhoneNumber, result[0].PhoneNumber);
            Assert.AreEqual(expected.EMail, result[0].EMail);
            Assert.AreEqual(expected.Username, result[0].Username);
            Assert.AreEqual(expected.Address, result[0].Address);
            Assert.AreEqual(expected.ZipCode, result[0].ZipCode);
            Assert.AreEqual(expected.City, result[0].City);
        }

        [TestMethod]
        public void Search_Employee_error()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var actionresult = (PartialViewResult)controller.SearchEmployee("");
            var result = (List<Employee>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "_ListEmployee");
            Assert.AreEqual(result[0].ID, 0);
        }

        [TestMethod]
        public void Search_Booking_OK()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var expected = new AdminBooking()
            {
                ContactName = "Ola Nordmann",
                cID = 1,
                ID = 1,
                RTString = "Tur/Retur",
                Travelers = 1,
                TravelDate = "00/00/00"
            };
            var actionresult = (PartialViewResult)controller.SearchBooking(1);
            var result = (List<AdminBooking>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "ListBooking");
            Assert.AreEqual(expected.ContactName, result[0].ContactName);
            Assert.AreEqual(expected.cID, result[0].cID);
            Assert.AreEqual(expected.ID, result[0].ID);
            Assert.AreEqual(expected.RTString, result[0].RTString);
            Assert.AreEqual(expected.TravelDate, result[0].TravelDate);
            Assert.AreEqual(expected.Travelers, result[0].Travelers);
        }

        [TestMethod]
        public void Search_Booking_error()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var actionresult = (PartialViewResult)controller.SearchBooking(0);
            var result = (List<AdminBooking>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "ListBooking");
            Assert.AreEqual(result[0].ID, 0);
        }


        [TestMethod]
        public void Search_Flight_OK()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var search = new SearchFlight()
            {
                 Date = "00/00/00",
                 From = "Oslo",
                 To = "Bardufoss"
            };

            var expected = new List<AdminViewFlight>();
            var flight = new AdminViewFlight()
            {
                ID = 1,
                Departure = "Oslo",
                DepartureTime = "00:00",
                Destination = "Bardufoss",
                DestinationTime = "00:00",
                Airplane = "Boeing737",
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };
            expected.Add(flight);
            expected.Add(flight);
            expected.Add(flight);

            var actionresult = (PartialViewResult)controller.SearchFlights(search);
            var result = (List<AdminViewFlight>)actionresult.Model;

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(actionresult.ViewName, "_ListFlight");
                Assert.AreEqual(expected[i].ID, result[i].ID);
                Assert.AreEqual(expected[i].Departure, result[i].Departure);
                Assert.AreEqual(expected[i].DepartureTime, result[i].DepartureTime);
                Assert.AreEqual(expected[i].Destination, result[i].Destination);
                Assert.AreEqual(expected[i].DestinationTime, result[i].DestinationTime);
                Assert.AreEqual(expected[i].Airplane, result[i].Airplane);
                Assert.AreEqual(expected[i].ClassType, result[i].ClassType);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Seats, result[i].Seats);
                Assert.AreEqual(expected[i].TravelDate, result[i].TravelDate);

            }


        }

        [TestMethod]
        public void Search_Flight_error()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var search = new SearchFlight();
            search.Date = "";
            controller.ViewData.ModelState.AddModelError("Date", "Ikke oppgitt reisedato");



            var actionresult = (PartialViewResult)controller.SearchFlights(search);
            var result = (List<AdminViewFlight>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "_ListFlight");
            Assert.AreEqual(result[0].ID, 0);
        }

        [TestMethod]
        public void Search_Change_Flight_OK()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var search = new SearchFlight()
            {
                Date = "00/00/00",
                From = "Oslo",
                To = "Bardufoss"
            };

            var expected = new List<AdminViewFlight>();
            var flight = new AdminViewFlight()
            {
                ID = 1,
                Departure = "Oslo",
                DepartureTime = "00:00",
                Destination = "Bardufoss",
                DestinationTime = "00:00",
                Airplane = "Boeing737",
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };
            expected.Add(flight);
            expected.Add(flight);
            expected.Add(flight);

            var actionresult = (PartialViewResult)controller.SearchChangeFlights(search);
            var result = (List<AdminViewFlight>)actionresult.Model;

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(actionresult.ViewName, "_ListChangeFlight");
                Assert.AreEqual(expected[i].ID, result[i].ID);
                Assert.AreEqual(expected[i].Departure, result[i].Departure);
                Assert.AreEqual(expected[i].DepartureTime, result[i].DepartureTime);
                Assert.AreEqual(expected[i].Destination, result[i].Destination);
                Assert.AreEqual(expected[i].DestinationTime, result[i].DestinationTime);
                Assert.AreEqual(expected[i].Airplane, result[i].Airplane);
                Assert.AreEqual(expected[i].ClassType, result[i].ClassType);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Seats, result[i].Seats);
                Assert.AreEqual(expected[i].TravelDate, result[i].TravelDate);

            }


        }

        [TestMethod]
        public void Search_Change_Flight_error()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var search = new SearchFlight();
            search.Date = "";
            controller.ViewData.ModelState.AddModelError("Date", "Ikke oppgitt reisedato");


            var actionresult = (PartialViewResult)controller.SearchChangeFlights(search);
            var result = (List<AdminViewFlight>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "_ListChangeFlight");
            Assert.AreEqual(result[0].ID, 0);
        }


        [TestMethod]
        public void Get_Customer_Booking_OK()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            controller.TempData["ChangeFlight"] = true;

            var expected = new List<AdminViewFlight>();
            var flight = new AdminViewFlight()
            {
                ID = 1,
                Departure = "Oslo",
                DepartureTime = "00:00",
                Destination = "Bardufoss",
                DestinationTime = "00:00",
                Airplane = "Boeing737",
                ClassType = "Økonomi",
                Price = 500,
                Seats = 500,
                TravelDate = "00/00/00"
            };
            expected.Add(flight);
            expected.Add(flight);
            expected.Add(flight);

            var actionresult = (ViewResult)controller.CustomerBooking(1);
            var result = (List<AdminViewFlight>)actionresult.Model;

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(actionresult.ViewName, "");
                Assert.AreEqual(expected[i].ID, result[i].ID);
                Assert.AreEqual(expected[i].Departure, result[i].Departure);
                Assert.AreEqual(expected[i].DepartureTime, result[i].DepartureTime);
                Assert.AreEqual(expected[i].Destination, result[i].Destination);
                Assert.AreEqual(expected[i].DestinationTime, result[i].DestinationTime);
                Assert.AreEqual(expected[i].Airplane, result[i].Airplane);
                Assert.AreEqual(expected[i].ClassType, result[i].ClassType);
                Assert.AreEqual(expected[i].Price, result[i].Price);
                Assert.AreEqual(expected[i].Seats, result[i].Seats);
                Assert.AreEqual(expected[i].TravelDate, result[i].TravelDate);

            }
        }

        [TestMethod]
        public void Get_Customer_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionresult = (RedirectToRouteResult)controller.CustomerBooking(1);
            Assert.AreEqual(actionresult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void Get_Customer_Booking_error()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            controller.TempData["ChangeFlight"] = true;

            var actionresult = (ViewResult)controller.CustomerBooking(0);
            var result = (List<AdminViewFlight>)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "");
            Assert.AreEqual(result[0].ID, 0);
        }

        [TestMethod]
        public void List_Bookings()
        {
            //Arrange
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            var expectedList = new List<AdminBooking>();
            var booking = new AdminBooking()
            {
                ContactName = "Ola Nordmann",
                cID = 1,
                ID = 1,
                RTString = "Tur/Retur",
                Travelers = 1,
                TravelDate = "00/00/00"
            };
            expectedList.Add(booking);
            expectedList.Add(booking);
            expectedList.Add(booking);

            //Act
            var actionresult = (ViewResult)controller.ListBooking();
            var result = (List<AdminBooking>)actionresult.Model;

            //Assert
            Assert.AreEqual(actionresult.ViewName, "");

            for (var i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectedList[i].ID, result[i].ID);
                Assert.AreEqual(expectedList[i].cID, result[i].cID);
                Assert.AreEqual(expectedList[i].ContactName, result[i].ContactName);
                Assert.AreEqual(expectedList[i].RTString, result[i].RTString);
                Assert.AreEqual(expectedList[i].TravelDate, result[i].TravelDate);
                Assert.AreEqual(expectedList[i].Travelers, result[i].Travelers);

            }
        }

        [TestMethod]
        public void List_Bookings_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionresult = (RedirectToRouteResult)controller.ListBooking();
            Assert.AreEqual(actionresult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void Edit_Login_OK()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            var actionResult = (ViewResult)controller.EditLogIn(1);
            var result = (EmployeeEditLogin)actionResult.Model;

            Assert.AreEqual(actionResult.ViewName, "");
            Assert.AreEqual(result.Username, "Olanord");

        }
        [TestMethod]
        public void Edit_Login_Not_Logged_In()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionResult = (RedirectToRouteResult)controller.EditLogIn(1);

            Assert.AreEqual(actionResult.RouteValues.Values.First(), "LogIn");

        }

        [TestMethod]
        public void Edit_Login_error()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            var actionresult = (ViewResult)controller.EditLogIn(0);
            var result = (EmployeeEditLogin)actionresult.Model;

            Assert.AreEqual(actionresult.ViewName, "");
            Assert.AreEqual(result.Username, "");
        }

        [TestMethod]
        public void Edit_Login_Post_OK()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var EEL = new EmployeeEditLogin()
            {
                Username = "Olanord",
                Password = "Ola1",
                ConfirmPassword = "Ola1",
                OldPassword = "PassordOla"
            };
            var actionresult = (ViewResult)controller.EditLogIn(1,EEL);
            Assert.AreEqual(actionresult.ViewName, "");
        }

        [TestMethod]
        public void Edit_Login_Post_error()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var EEL = new EmployeeEditLogin();
            EEL.Username = "";
            controller.ViewData.ModelState.AddModelError("Username", "Ikke oppgitt brukernavn");

            var actionresult = (RedirectToRouteResult)controller.EditLogIn(1, EEL);

            Assert.AreEqual(actionresult.RouteValues.Values.First(), "Error");

        }

        [TestMethod]
        public void Login_OK()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            var login = new EmployeeLogin()
            {
                Username = "Olanord",
                Password = "PassordOla"
            };

            var actionresult = (RedirectToRouteResult)controller.LogIn(login);

            Assert.AreEqual(actionresult.RouteValues.Values.First(), "MainPage");
        }

        [TestMethod]
        public void Login_fail()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var login = new EmployeeLogin()
            {
                Username = "olanord",
                Password = "PassordOle"
            };

            var actionresult = (ViewResult)controller.LogIn(login);

            Assert.AreEqual(actionresult.ViewName, "");
        }

        [TestMethod]
        public void Login_Model_fail()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            var login = new EmployeeLogin()
            {
                Username = "",
                Password = "PassordOle"
            };
            controller.ViewData.ModelState.AddModelError("Username", "Ikke oppgitt brukernavn");


            var actionresult = (RedirectToRouteResult)controller.LogIn(login);

            Assert.AreEqual(actionresult.RouteValues.Values.First(), "Error");
        }

        [TestMethod]

        public void Error_View()
        {
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));

            var actionresult = (ViewResult)controller.Error();

            Assert.AreEqual(actionresult.ViewName, "");
        }

        [TestMethod]

        public void List_Customer_View()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            var actionresult = (ViewResult)controller.ListCustomer();

            Assert.AreEqual(actionresult.ViewName, "");
        }

        [TestMethod]

        public void List_Customer_View_Not_logged_in()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionresult = (RedirectToRouteResult)controller.ListCustomer();

            Assert.AreEqual(actionresult.RouteValues.Values.First(), "LogIn");
        }

        [TestMethod]

        public void List_Employee_View()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            controller.TempData["NewEmployee"] = true;

            var actionresult = (ViewResult)controller.ListEmployee();

            Assert.AreEqual(actionresult.ViewName, "");
        }

        [TestMethod]

        public void List_Employee_View_Not_logged_in()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionresult = (RedirectToRouteResult)controller.ListEmployee();

            Assert.AreEqual(actionresult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]

        public void List_Flight_View()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            controller.TempData["FlightInsert"] = true;
            controller.TempData["FlightEdit"] = true;

            var actionresult = (ViewResult)controller.ListFlight();

            Assert.AreEqual(actionresult.ViewName, "");
        }

        [TestMethod]

        public void List_Flight_View_Not_logged_in()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionresult = (RedirectToRouteResult)controller.ListFlight();

            Assert.AreEqual(actionresult.RouteValues.Values.First(), "LogIn");
        }
        [TestMethod]
        public void Login_Test_Redirect_To_Main()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;

            var actionresult = (RedirectToRouteResult)controller.LogIn();

            Assert.AreEqual(actionresult.RouteValues.Values.First(), "MainPage");
        }
        [TestMethod]
        public void Login_Test_Redirect_To_Login()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = false;

            var actionresult = (ViewResult)controller.LogIn();

            Assert.AreEqual(actionresult.ViewName, "");
        }
        [TestMethod]
        public void LogOut_Test()
        {
            var SessionMock = new TestControllerBuilder();
            var controller = new AdminController(new AdminBLL(new AdminRepositoryStub()));
            SessionMock.InitializeController(controller);
            controller.Session["LoggedIn"] = true;
            controller.Session["Username"] = "Olanord";

            var actionresult = (RedirectToRouteResult)controller.LogOut();

            Assert.AreEqual(actionresult.RouteValues.Values.First(), "LogIn");
        }
    }
}
