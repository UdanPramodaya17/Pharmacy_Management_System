
-----

# 🏥 Pharmacy Management System (PMS)


A comprehensive, distributed **Pharmacy Management System** built with **C\# .NET**. This application provides a centralized platform to manage the complex supply chain of pharmaceuticals, seamlessly connecting Pharmacies, State Pharmaceuticals Corporation (SPC), Suppliers, Manufacturing Plants, and Warehouse Staff through a robust Web Service architecture.

-----

## 🚀 Features

The system is designed with role-based access and specialized dashboards for different entities in the pharmaceutical supply chain:

  * **🏪 Pharmacy Module:** Register pharmacies, manage local drug stock, and place orders directly to the central warehouse or SPC.
  * **🏢 State Pharmaceuticals Corporation (SPC):** Manage global orders, issue tenders for drug procurement, and oversee the national supply.
  * **🏭 Manufacturing Plant:** Register plants, view manufacturing requests, and supply newly manufactured drugs.
  * **📦 Supplier Module:** View open tenders, submit quotations, and fulfill SPC drug orders.
  * **👷 Warehouse Management:** Warehouse staff can monitor central inventory, update stock levels, and dispatch drugs to pharmacies.
  * **🔐 Authentication & Security:** Secure login portals for each specific role (Pharmacy, Plant, Supplier, Warehouse Staff).

-----

## 🧠 Architecture & Tech Stack

This project uses an N-tier architecture, separating the client-side UI from the server-side business logic and database access.

| Component | Technology |
| :--- | :--- |
| **Language** | C\# (C-Sharp) |
| **Frontend (Desktop Client)**| Windows Forms (WinForms) .NET Framework |
| **Backend API** | ASP.NET Web Services (`.asmx`) |
| **Database Access** | ADO.NET (`DataAccessLayer.cs`) |
| **Service Communication** | WCF / SOAP (Connected Services) |
| **IDE** | Visual Studio |

-----

## 📂 Project Structure

  * **`Pharmacy_Udan.sln`** - The main Visual Studio Solution file.
  * **`Pharmacy_Udan/` (Backend)** - Contains the ASP.NET Web Service application.
      * `web.asmx` & `web.asmx.cs`: The core SOAP web service exposing methods for database interaction.
      * `controller/`: Business logic controllers (e.g., `OrderController`, `StockController`).
      * `module/`: Data models/entities representing database tables (e.g., `Pharmacy`, `Order`, `Supplier`).
      * `data/DataAccessLayer.cs`: Centralized database connection and query execution.
  * **`pharmacufrom/` (Frontend)** - The Windows Forms desktop application.
      * Contains the UI forms (`Login.cs`, `OrderForm.cs`, `SPC.cs`, `SupplierMain.cs`, etc.).
      * `Connected Services/`: Contains the generated proxy classes used to communicate with the `web.asmx` service.
      * `Resources/`: Contains application assets, icons, and images.

-----

## ⚙️ Getting Started

### Prerequisites

  * **Visual Studio** (2019 or 2022 recommended) with the **.NET desktop development** and **ASP.NET and web development** workloads installed.
  * **SQL Server** (Express or Developer edition).

### Installation & Setup

1.  **Clone the repository:**

    ```bash
    git clone https://github.com/yourusername/pharmacy_management_system.git
    ```

2.  **Database Setup:**

      * Open SQL Server Management Studio (SSMS).
      * Create the required database schema (ensure your tables match the models in the `module/` directory like Orders, Tenders, Stock, etc.).
      * Update the connection string located in `Pharmacy_Udan/data/DataAccessLayer.cs` or the `Web.config` file to point to your local SQL Server instance.

3.  **Open the Solution:**

      * Open `Pharmacy_Udan.sln` in Visual Studio.

4.  **Run the Web Service (Backend):**

      * Set `Pharmacy_Udan` as the Startup Project.
      * Press `F5` or click **Start** to run the ASMX web service in IIS Express. Note the local URL (e.g., `http://localhost:XXXX/web.asmx`).

5.  **Update Connected Services (If necessary):**

      * In the `pharmacufrom` project, expand **Connected Services**.
      * Right-click the Service Reference and select **Update Service Reference** to ensure it points to your running local ASMX service.

6.  **Run the Client Application (Frontend):**

      * Right-click the `pharmacufrom` project in the Solution Explorer and select **Set as Startup Project**.
      * Press `F5` to launch the Windows Forms application.

-----


## 🤝 Contributing

Contributions, issues, and feature requests are welcome\!

1.  Fork the Project
2.  Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3.  Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4.  Push to the Branch (`git push origin feature/AmazingFeature`)
5.  Open a Pull Request

-----

## 📝 License

This project is licensed under the [MIT License](https://www.google.com/search?q=LICENSE) - see the https://www.google.com/search?q=LICENSE file for details.
