using Pharmacy_Udan.controller;
using Pharmacy_Udan.module;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace Pharmacy_Udan
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {

        [WebMethod]
        public int InsertsupplierWeb(int supplierid, string suppliername, string Supplieraddress, string Supplieremail, int Suppliercontact_number, string SupplierPassword)

        {
            Supplier d = new Supplier();

            d.suplierId = supplierid;
            d.name = suppliername;
            d.address = Supplieraddress;
            d.email = Supplieremail;
            d.contact_number = Suppliercontact_number;
            d.Password = SupplierPassword;
            return new supplierController().InsertNewsupplier(d);

        }


        [WebMethod]
        public int UpdateSupplierWeb(int supplierid, string suppliername, string Supplieraddress, string Supplieremail, int Suppliercontact_number, string SupplierPassword)
        {
            Supplier d = new Supplier
            {
                suplierId = supplierid,
                name = suppliername,
                address = Supplieraddress,
                email = Supplieremail,
                contact_number = Suppliercontact_number,
                Password = SupplierPassword,
            };
            return new supplierController().UpdateSupplier(d);
        }


        [WebMethod]
        public int DeleteSupplierWeb(int supplierid)
        {
            return new supplierController().DeleteSupplier(supplierid);
        }


        [WebMethod]
        public Supplier SearchSupplierWeb(int supplierid)
        {
            return new supplierController().SearchSupplier(supplierid);
        }


        [WebMethod]
        public bool SupplierLoginWeb(string supplierName, string Supplierpass)
        {
            return new supplierController().VerifySupplierLogin(supplierName, Supplierpass);
        }






        [WebMethod]
        public int InsertNewsstock(string drugName, int quantity, decimal price, string manufacturer, string addedBy)
        {
            Stock stock = new Stock
            {
                DrugName = drugName,
                Quantity = quantity,
                Price = price,
                Manufacturer = manufacturer,
                AddedBy = addedBy,
                AddedDate = DateTime.Now // Set the current date
            };

            return new StockController().InsertNewsstock(stock);  // Call the method in the controller
        }

        [WebMethod]
        public int UpdateStockWeb(int stockId, string drugName, int quantity, decimal price, string manufacturer, string addedBy)
        {
            Stock stock = new Stock
            {
                StockId = stockId,
                DrugName = drugName,
                Quantity = quantity,
                Price = price,
                Manufacturer = manufacturer,
                AddedBy = addedBy,
                AddedDate = DateTime.Now // Assuming update also sets the current date
            };

            return new StockController().UpdateStock(stock);
        }

        [WebMethod]
        public int DeleteStockWeb(int stockId)
        {
            return new StockController().DeleteStock(stockId);
        }

        [WebMethod]
        public List<Stock> GetStockData()
        {
            return new StockController().GetAllStock();
        }













        [WebMethod]
        public int InsertNewPharmacyWeb(string pharmacyName, string address, string phoneNumber, string username, string password, string verifyPassword)
        {
            Pharmacy newPharmacy = new Pharmacy
            {
                PharmacyName = pharmacyName,
                Address = address,
                PhoneNumber = phoneNumber,
                Username = username,
                Password = password,
                VerifyPassword = verifyPassword
            };

            return new PharmacyController().InsertNewPharmacy(newPharmacy);
        }

        [WebMethod]
        public int UpdatePharmacyWeb(int pharmacyID, string pharmacyName, string address, string phoneNumber, string username, string password, string verifyPassword)
        {
            Pharmacy pharmacy = new Pharmacy
            {
                PharmacyID = pharmacyID,
                PharmacyName = pharmacyName,
                Address = address,
                PhoneNumber = phoneNumber,
                Username = username,
                Password = password,
                VerifyPassword = verifyPassword
            };

            return new PharmacyController().UpdatePharmacy(pharmacy);
        }

        [WebMethod]
        public int DeletePharmacyWeb(int pharmacyID)
        {
            return new PharmacyController().DeletePharmacy(pharmacyID);
        }

        [WebMethod]
        public List<Pharmacy> GetPharmacyData()
        {
            return new PharmacyController().GetAllPharmacy();
        }
        [WebMethod]
        public bool PharmacyLoginWeb(string username, string password)
        {
            return new PharmacyController().VerifyPharmacyLogin(username, password);
        }








        [WebMethod]
        public decimal GetUnitPriceByStockId(int stockId)
        {
            OrderController controller = new OrderController();
            return controller.GetUnitPrice(stockId);
        }
        [WebMethod]
        public string GetDrugNameByStockId(int stockId)
        {
            OrderController controller = new OrderController();
            return controller.GetDrugName(stockId);
        }


        [WebMethod]
        public int AddOrder(int stockId, string drugName, decimal unitPrice, int quantity, decimal totalPrice)
        {
            OrderController controller = new OrderController();
            Order order = new Order()
            {
                StockId = stockId,
                DrugName = drugName,
                UnitPrice = unitPrice,
                Quantity = quantity,
                TotalPrice = totalPrice,
                OrderDate = DateTime.Now
            };
            return controller.AddOrder(order);
        }

        [WebMethod]
        public int DeleteOrder(int orderId)
        {
            OrderController controller = new OrderController();
            return controller.DeleteOrder(orderId);
        }

        [WebMethod]
        public List<Order> GetAllOrders()
        {
            OrderController controller = new OrderController();
            return controller.GetAllOrders();
        }








        TenderController controller = new TenderController();

        [WebMethod]
        public int InsertTender(int supplierId, string tenderDrugName, decimal tenderAmount, int tenderQuantity, string tenderDate)
        {
            try
            {
                Tender tender = new Tender
                {
                    SupplierId = supplierId,
                    TenderDrugName = tenderDrugName,
                    TenderAmount = tenderAmount,
                    TenderQuantity = tenderQuantity,
                    TenderDate = DateTime.Parse(tenderDate),
                    TenderStatus = "Pending"
                };

                TenderController controller = new TenderController();
                return controller.InsertTender(tender);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in InsertTender: " + ex.Message);
                return -1;  // Indicating failure
            }
        }

        [WebMethod]
        public List<Tender> GetTenders()
        {
            return controller.GetAllTenders();
        }

        [WebMethod]
        public int ConfirmTender(int tenderId)
        {
            return controller.ConfirmTender(tenderId);
        }










        [WebMethod]
        public int InsertNewPlantWeb(string plantName, string location, int contactNumber, string email, string password)
        {
            ManufacturingPlant newPlant = new ManufacturingPlant
            {
                PlantName = plantName,
                Location = location,
                ContactNumber = contactNumber,
                Email = email,
                Password = password
            };

            return new ManufacturingPlantController().InsertNewPlant(newPlant);
        }

        [WebMethod]
        public int UpdatePlantWeb(int plantID, string plantName, string location, int contactNumber, string email, string password)
        {
            ManufacturingPlant plant = new ManufacturingPlant
            {
                PlantID = plantID,
                PlantName = plantName,
                Location = location,
                ContactNumber = contactNumber,
                Email = email,
                Password = password
            };

            return new ManufacturingPlantController().UpdatePlant(plant);
        }

        [WebMethod]
        public int DeletePlantWeb(int plantID)
        {
            return new ManufacturingPlantController().DeletePlant(plantID);
        }

        [WebMethod]
        public List<ManufacturingPlant> GetPlantData()
        {
            return new ManufacturingPlantController().GetAllPlants();
        }

        [WebMethod]
        public bool PlantLoginWeb(string email, string password)
        {
            return new ManufacturingPlantController().VerifyPlantLogin(email, password);
        }







        [WebMethod]
        public int InsertNewStaffWeb(string fullName, string position, int contactNumber, string email, string password)
        {
            WarehouseStaff newStaff = new WarehouseStaff
            {
                FullName = fullName,
                Position = position,
                ContactNumber = contactNumber,
                Email = email,
                Password = password
            };

            return new WarehouseStaffController().InsertNewStaff(newStaff);
        }

        [WebMethod]
        public int UpdateStaffWeb(int staffID, string fullName, string position, int contactNumber, string email, string password)
        {
            WarehouseStaff staff = new WarehouseStaff
            {
                StaffID = staffID,
                FullName = fullName,
                Position = position,
                ContactNumber = contactNumber,
                Email = email,
                Password = password
            };

            return new WarehouseStaffController().UpdateStaff(staff);
        }

        [WebMethod]
        public int DeleteStaffWeb(int staffID)
        {
            return new WarehouseStaffController().DeleteStaff(staffID);
        }

        [WebMethod]
        public List<WarehouseStaff> GetWarehouseStaffData()
        {
            return new WarehouseStaffController().GetAllStaff();
        }

        [WebMethod]
        public bool WarehouseStaffLoginWeb(string email, string password)
        {
            return new WarehouseStaffController().VerifyStaffLogin(email, password);
        }










    }
}












