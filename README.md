# Event Management System 🌟 
<div style="float: right;">
  
  ![.NET Core](https://img.shields.io/badge/.NET_Core-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)
  ![MSSQL](https://img.shields.io/badge/MSSQL-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)
  ![React](https://img.shields.io/badge/React-61DAFB?style=for-the-badge&logo=react&logoColor=black)
  ![CSS](https://img.shields.io/badge/CSS-2965F1?style=for-the-badge&logo=css3&logoColor=white)
</div>





Welcome to the **Event Management System** web app! This repository contains a robust backend built with **.NET Core** and **MSSQL**, and a dynamic frontend developed using **React**. Below are the detailed instructions to set up and run the project on your local machine.

## Project Structure 🌐

```
Event-Management-System/
├── Event-Management-System-Backend/
└── Event-Management-System-Frontend/
```

## Prerequisites 📜

Ensure you have the following installed on your system:

- **.NET Core SDK version 8**
- **Microsoft SQL Server**
- **Node.js** and **npm**

## Setup Instructions 🌄

### Backend Setup 🛠️

1. **Navigate to the backend folder:**
   ```bash
   cd Event-Management-System-Backend
   ```

2. **Restore packages:**
   ```bash
   dotnet restore
   ```

3. **Update the `appsettings.json`:**
   - Set your MSSQL connection string:
     ```json
     "ConnectionStrings": {
       "DefaultConnection": "Your_MSSQL_Connection_String"
     }
     ```

4. **Apply migrations and run the project:**
   ```bash
   dotnet ef database update
   dotnet run
   ```

### Frontend Setup 🌄

1. **Navigate to the frontend folder:**
   ```bash
   cd Event-Management-System-Frontend
   ```

2. **Install dependencies:**
   ```bash
   npm install
   ```

3. **Start the development server:**
   ```bash
   npm run dev
   ```

## Features 🚀

- 📅 **Event Creation**: Create and manage events seamlessly.
- 🕑 **Real-Time Updates**: Update or change event related data.
- 💻 **Event Management**: Manage attendees, organizers, and dates update efficiently.
- 🔍 **Search & Filter**: Easily search and filter events.
- 📈 **Responsive and Reusable**: Responsive CSS and reusable react components.

## Technologies Used 🔧

- **Backend**: .NET Core, Entity Framework Core, MSSQL
- **Frontend**: React, CSS, React-Icons



## Contact 📧

For any queries, reach out to us at [asirialwis23@gmail.com](mailto:asirialwis23@gmail.com).

---

Made with ❤️ by [Asiri Alwis]

