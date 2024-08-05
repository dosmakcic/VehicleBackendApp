# Vehicle Backend App

## Overview

The Vehicle Backend App is a web application built using ASP.NET Core MVC that allows users to manage vehicle makes and models. It supports CRUD operations, pagination, sorting, and searching features. The application uses Entity Framework Core for data access and can be connected to SQL Server.

## Features

- **CRUD Operations**: Create, Read, Update, and Delete vehicle makes and models.
- **Pagination**: Displays vehicle models with pagination support.
- **Sorting**: Sort vehicle models by name, abbreviation, and make ID.
- **Search**: Search vehicle models by name or abbreviation.

## Getting Started

### Prerequisites

- [Visual Studio](https://visualstudio.microsoft.com/) (version 2022 or later recommended)
- [.NET SDK](https://dotnet.microsoft.com/download) (version 6.0 or later)
- SQL Server or SQL Server Express (or any compatible SQL database provider)

### Installation

1. **Clone the Repository**

   Open Visual Studio and use the following steps:
   
   - Go to `File` -> `Open` -> `Project from Source Control`.
   - Choose `Git` as the source control system.
   - Enter the repository URL: `https://github.com/dosmakcic/VehicleBackendApp.git`.
   - Click `Clone` to download the repository.

2. **Restore NuGet Packages**

   - Open the solution in Visual Studio.
   - Right-click on the solution in Solution Explorer and select `Restore NuGet Packages`. This will download and install the necessary dependencies.

3. **Configure the Database Connection**

   - Open the `appsettings.json` file in the project.
   - Update the connection string to match your SQL Server instance:

     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=VehicleDb;Trusted_Connection=True;MultipleActiveResultSets=true"
     }
     ```

   - You can modify the connection string according to your SQL Server configuration.
   - You can copy the name of database by going to `View`-> `SQL Server Object Explorer` and copying the name at this position:
     ![image](https://github.com/user-attachments/assets/faf3fc83-2557-4c8f-8939-decf85ae9c60)

   - After that you can paste it in appsettings.json in DefaultConnection part.


4. **Apply Migrations**

   - Open the `Package Manager Console` from `Tools` -> `NuGet Package Manager` -> `Package Manager Console`.
   - Run the following commands to create and apply database migrations:

     ```powershell
     Add-Migration InitialCreate
     Update-Database
     ```

5. **Run the Application**

   - Press `F5` or click on the `Start` button to build and run the application.
   - Visual Studio will start the application and open  main page.
     ![image](https://github.com/user-attachments/assets/8f6befcc-ce12-4967-8c3e-c66e459f1c46)
   -You can test makes and models by going to the one of those two buttons at the main page. 




### Features

- **View Vehicle Makes and Models**
  - This is view of Make and Model with pagination,filtering and sorting with CRUD possibilities.
    ***Vehicle makes***
    ![image](https://github.com/user-attachments/assets/d1182f25-9cab-4e74-a713-516d1194bdba)

    ***Vehicle models***
    ![image](https://github.com/user-attachments/assets/a053cb9c-7923-40fd-99bf-70b7cac08617)

    

    


- **Create a New Vehicle Make/Model**
  - Click on the "Create" button to add new vehicle makes or models.
    ***Vehicle makes***
   ![image](https://github.com/user-attachments/assets/008037fa-3673-4ad2-bf50-0e8be0ba065e)

    ***Vehicle model***
   ![image](https://github.com/user-attachments/assets/ead26110-1689-4cf9-862d-dad977a5e9a6)




- **Edit Existing Vehicle Make/Model**
  - Select the "Edit" button next to the vehicle make/model you wish to modify.
    ***Vehicle makes***
    ![image](https://github.com/user-attachments/assets/47821d7e-75c5-4ee6-b897-afb93892432f)

    ***Vehicle model***
    ![image](https://github.com/user-attachments/assets/6436dc30-00a7-4ba8-8dc6-dad1c06a6fbe)



- **Delete a Vehicle Make/Model**
  - Click the "Delete" button to remove a vehicle make/model from the system.
    

- **Search and Filter**
  - Use the search bar to find specific vehicle makes or models.
    ![image](https://github.com/user-attachments/assets/21e18287-ea97-4c6b-b4a2-80a8f5119a03)

  - Filter by vehicle make using the dropdown menu.
    ![image](https://github.com/user-attachments/assets/55978115-eb31-4493-8562-7b8eaf8281dc)


- **Sorting**
  - Click on the column headers to sort vehicle models by name, abbreviation, or make ID.
   ![image](https://github.com/user-attachments/assets/cfa4bcb5-1c0e-4c06-8307-4eff997e61a4)







