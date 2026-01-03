# 🏥 Healthcare Appointment Management System

A web-based healthcare appointment management system built using **ASP.NET Core Razor Pages**.  
This project allows users to book appointments, manage wellness data, and view appointment history through a clean and responsive UI.

---

## 🚀 Tech Stack

![.NET](https://img.shields.io/badge/.NET-ASP.NET%20Core-blueviolet)
![C#](https://img.shields.io/badge/C%23-Language-blue)
![Razor Pages](https://img.shields.io/badge/Razor-Pages-purple)
![Bootstrap](https://img.shields.io/badge/Bootstrap-5-blue)
![Azure](https://img.shields.io/badge/Azure-Deployment-blue)
![SQL Server](https://img.shields.io/badge/SQL%20Server-Database-red)

- **Backend:** .NET (C#) – Razor Pages  
- **Frontend:** HTML, CSS, Bootstrap  
- **Database:** SQL Server  
- **Deployment:** Microsoft Azure  

---

## ✨ Features

- User authentication (Login)
- Book healthcare appointments
- View and manage appointments
- Update wellness information
- Responsive UI using Bootstrap
- Secure database integration

---

## 📸 Screenshots

### 🏠 Home Page
![Home Page](docs/HomePage.jpeg)

### 🔐 Login Page
![Login Page](docs/Login.jpeg)

### 📅 Book Appointment
![Book Appointment](docs/BookAppointment.jpeg)

### 📋 My Appointments
![My Appointments](docs/MyAppointments.jpeg)

### 🧘 Manage Wellness Update
![Manage Wellness](docs/ManageWellnessUpdate.jpeg)

---

## 🗂️ System Design

### ER Diagram
![ER Diagram](docs/ER Diagram.jpeg)

---

---

## 🔐 Login Credentials

Use the following demo accounts to access the system:

### 👤 Patient Account
- **Email:** `patient@email.com`
- **Password:** `patient`

### 🛠️ Admin Account
- **Email:** `admin@email.com`
- **Password:** `admin`

> These accounts are provided for testing and demonstration purposes only.



## 🗄️ Local Database Setup (Data Seeding)

When running the project **locally**, you need to seed initial data (admin, patient, sample appointments).

### 🔧 Steps

1. Open `Program.cs`

2. **Uncomment** the following code block:

```csharp
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    DbSeeder.SeedData(context);
}