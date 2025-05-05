# Simple Social Media App (.NET MVC)

👋 Hi! This is a **simple social media app** created as a **software development project** for my faculty.  
It uses the **MVC architecture in ASP.NET Core**, and it demonstrates basic functionality like user login, posting, and viewing other users' pages.

---

## 📌 Project Description

This project was built to practice:
- ASP.NET Core MVC
- Database First using Entity Framework Core
- Session-based login (✅ without JWT or Identity package)
- Role-based view: Admin vs normal users

---

## 🎯 Features

- 🧑 **Users Table**: with profile photo, birthdate, and role (`admin` or `user`)
- 📝 **Posts Table**: each post is related to a user, and can include a photo
- 🗂️ **Home Page**: shows all posts with the author's name/photo
- 📄 **User Pages**: sidebar lists all users; clicking a user shows their personal page
- 🔒 **Session-based Login**: stores logged-in user info in session
- 🛠️ **Admin Dashboard**: accessible only for users with `admin` role

---

## 🧱 Tech Stack

- ASP.NET Core MVC
- EF Core (Database First)
- SQL Server
- Bootstrap (optional for styling)
- Session for login handling

---

## 🗃 Database Setup

The database is created using **Database First** approach.

### 🔧 How to Set It Up

1. Open `Database/SocialMedia1.sql` in **SQL Server Management Studio**
2. Run the script to create the database, tables, and sample data

### 🔗 Connection String

In `appsettings.json` update this line:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=.;Database=SocialMedia1;Trusted_Connection=True;"
}
