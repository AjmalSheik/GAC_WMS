# GAC_WMS – Warehouse Management System API

This is an ASP.NET Core Web API project that provides basic CRUD operations for managing Customers, Products, Purchase Orders, and Sales Orders using Entity Framework Core and SQL Server.

It also includes a scheduled background job using Quartz.NET to poll a folder for XML files and post parsed Purchase Orders to the API.

---


## Technologies Used

- ASP.NET Core Web API 
- Entity Framework Core
- SQL Server
- Quartz.NET (Scheduler)
- xUnit (Testing)


---

## Folder Polling Job

- A job runs every minute (configurable via cron expression in `appsettings.json`)
- Watches a folder (also configurable)
- Parses XML files and sends data to Purchase Order API

### appsettings.json Example:
```json
"QuartzConfig": {
    "CronExpression": "0 * * * * ?",
    "DirectoryPath": "C:/Ajmal/Documents/TEST_FOLDER/",
    "ApiUrl": "http://localhost:5225/"
  }





----------------- Steps to Run the Project ----------------------
    1. Clone the repository
        git clone https://github.com/AjmalSheik/GAC_WMS.git
        cd GAC_WMS

    2.Update appsettings.json

        * Set your SQL Server connection string under ConnectionStrings.

        * *Update the folder path to monitor under QuartzConfig:DirectoryPath (in same format as in above example).

        * *Adjust the cron expression if you want to change the polling interval.
        * Update the correct API url  in "ApiUrl"

           "QuartzConfig": {
                "CronExpression": "0 * * * * ?",
                "DirectoryPath": "C:/Ajmal/Documents/TEST_FOLDER/",
                "ApiUrl": "http://localhost:5225/"
              }


    3.Apply EF Core migrations  

        *  dotnet ef database update
        * Or Please copy and run the SQL commands from the script file ("script_file_for_gac_wms.sql") in gac_wms db._


    4.Run the application
        * dotnet run

    5. Drop a valid XML file into the polling folder

       *  Example folder: C:/Ajmal/Documents/TEST_FOLDER

       * The job will parse and send it to the Purchase Order API.


------------------- How to Run Unit Tests---------------

cd GAC_WMS.Tests
dotnet test


-------------- For DOING SALES ORDER AND PURCAHSE ORDER APIS  TEST ---------------

    

    Please create one customer and one product through the API ( Swagger Can be used.)

    Please use the created productid and created customerid ,while creating purchase order and 
    Sales order through API.


 

------FOR DOING POLLING  JOB TEST -----
 
i have created one test xml in this folder (PO_XML_Test_files) ,please use that format .
Note : In that file ,please update customer id and product id from the database.


  ----------DOCKER -------------------

  i have created the web api project with container support .







    

