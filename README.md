# MedUnity 

## 🛠️ Tech Stack
* **Framework:** .NET 8.0  (ASP.NET Core MVC)
* **Database:** SQL Server
* **ORM:** Entity Framework Core (Code First)
* **Frontend:** Bootstrap 5, Razor Views (CSHTML)

## ⚙️ Setup & Configuration

### 1. Database Setup
Update your connection string in `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=YOUR_SERVER;Database=MedUnity_Db;Trusted_Connection=True;TrustServerCertificate=True;"
}