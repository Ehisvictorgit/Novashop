# 🛍️ Novashop – Online Shopping App

**Novashop** is a personal project simulating a modern, modular online shopping application.

Currently, it allows:
- Listing products
- Categorizing products
- Searching by name or category

---

## 📅 Changelog

- **2025-05-02**: Initial upload — includes architecture, backend, frontend, and database.

---

## 🎯 Objective

To practice and apply knowledge in:

- **.NET (C#)** + **MongoDB** (via Docker)
- **Angular** (TypeScript)
- **Design Patterns**
- **Onion Architecture**
- Use of **Kendo UI for Angular**: a professional, out-of-the-box library for building enterprise Angular applications.

---

## 🚀 Features

### 🔧 Backend

- Follows **Onion Architecture**
- Implements several **Design Patterns**:
  - Repository
  - Unit of Work
  - Factory
  - Singleton
  - Base Entity
  - Data Transfer Object (DTO)

- **Key Libraries**:
  - 🔐 Authentication: [`BCrypt.Net-Next`](https://www.nuget.org/packages/BCrypt.Net-Next)
  - 🧾 Logging: `Serilog`, `Serilog.Sinks.File`, `Serilog.Extensions.Logging`
  - 🧬 MongoDB ORM: `MongoDB.Bson`, `MongoDB.Driver`

---

### 💻 Frontend

- Built with **Angular 16+** (`19.2.9`)
- Implements **parent-child component** communication for data exchange
- Modals and dialogs with **Angular Material**:
  - `@angular/material` `18.2.14`
  - `@angular/animations`, `@angular/cdk`
- Uses **Kendo UI for Angular**: [Official site](https://www.telerik.com/kendo-angular-ui)
- Modular project structure (core, shared, features, etc.)

---

### 🗄️ Database

- **MongoDB**, running via **Docker Desktop**
- Includes:
  - Entity-Relationship Diagram (ERD)
  - Sample data insertion scripts (`.js` / `.json`)

---

## 🧪 Installation

### ✅ Prerequisites

Make sure the following are installed:

- [.NET SDK 9.0.200](https://dotnet.microsoft.com/)
- [Docker Desktop 4.40.0+](https://www.docker.com/)
- [Node.js + npm](https://nodejs.org/) (for frontend)

---

### ⚙️ Setup Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/waltermillan/Novashop.git
