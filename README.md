# 🛒 Ecommerce API (.NET 9)

A **robust, scalable, and production-ready Backend REST API** built with **.NET 9**, following **Clean Architecture principles**.

This project manages **users, products, shopping carts, and orders**, with a strong focus on **data integrity, maintainability, and real-world backend practices**.

---

## 🚀 Key Features

### 👤 User Management
- Full CRUD operations (Create, Read, Update, Delete)
- Secure registration and profile updates
- DTO-based architecture for safe and controlled data transfer

---

### 📦 Product & Category Catalog
- Dynamic product and category management
- Products are linked to categories for better organization
- Easily extendable filtering and searching capabilities

---

### 🛒 Shopping Cart System
- Persistent cart stored in the database
- Real-time updates (add/remove/update quantity)
- Seamless user-cart relationship

---

### 💳 Order Management
- One-click checkout process
- Automatic stock deduction after order placement
- Full order history tracking per user
- Ensures transactional consistency

---

## 🛠 Tech Stack & Highlights

- **Framework:** ASP.NET Core Web API (.NET 9)
- **Database:** SQL Server
- **ORM:** Entity Framework Core  
- **Mapping:** AutoMapper (with null safety handling)
- **Validation:** FluentValidation  
- **Logging:** Serilog (Console & File logging)

---

## 🧠 Architecture & Design

This project follows **Clean Architecture**, ensuring separation of concerns and scalability.

### 🏗 Layers

- **📂 Domain**  
  Core business logic and entities (User, Product, Cart, Order)

- **📂 Application**  
  DTOs, Interfaces, Validators, and Mapping Profiles

- **📂 Infrastructure**  
  Database access (DbContext), Repository & Service implementations

- **📂 API**  
  Controllers, Middleware, and global exception handling

---
