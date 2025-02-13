Asset Tracker

Overview

Asset Tracker is a database-driven application designed to manage and track assets efficiently. It allows users to store, update, and retrieve asset-related information, including asset details, purchase history, office locations, and warranty periods.


Features

Dynamic Column Width: Automatically adjusts column widths based on content length.

Database Integration: Stores and retrieves asset data from a database.

Sorting & Filtering: Assets can be sorted by office location and purchase date.

Console-Based Interface: Provides a user-friendly console-based asset display.

Visual Indicators: Assets nearing warranty expiration are highlighted for easy tracking.


Technologies Used

C# (.NET Core)

SQL Server / MySQL (for database management)

Entity Framework (ORM for database interactions)

Console UI (for displaying asset lists)


Installation

Clone the repository:

git clone https://github.com/yourusername/asset-tracker.git
cd asset-tracker

Install dependencies:

dotnet restore

Configure the database connection in appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Server=yourserver;Database=AssetTrackerDB;User Id=youruser;Password=yourpassword;"
  }
}

Run database migrations:

dotnet ef database update

Build and run the application:

dotnet run


Usage

Launch the application.

View the list of assets.

Add, update, or delete asset information.

Use sorting and filtering options to find specific assets.

Track warranty expiration dates for asset maintenance.


