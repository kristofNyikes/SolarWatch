
# SolarWatch

**SolarWatch** is a web application that allows users to retrieve sunrise and sunset data for a specific city. The application integrates ASP.NET Core for the backend, React.js for the frontend, and uses Entity Framework with MSSQL for data management. Authentication is managed with Identity Framework. 

## Features
- Retrieve sunrise and sunset times for any city.
- Authentication with roles (admin and regular users) using Identity Framework.
- Secure API key integration with OpenWeatherMap API.
- Modern frontend built with React.js.

## Technologies Used

- [![C# ASP.NET Core](https://img.shields.io/badge/C%23%20ASP.NET%20Core-512BD4?style=for-the-badge&logo=.net&logoColor=white)](https://dotnet.microsoft.com/)
- [![Entity Framework](https://img.shields.io/badge/Entity%20Framework-6DB33F?style=for-the-badge&logo=ef&logoColor=white)](https://learn.microsoft.com/en-us/ef/)
- [![JavaScript](https://img.shields.io/badge/JavaScript-F7DF1E?style=for-the-badge&logo=javascript&logoColor=black)](https://developer.mozilla.org/en-US/docs/Web/JavaScript)
- [![React](https://img.shields.io/badge/React-20232A?style=for-the-badge&logo=react&logoColor=61DAFB)](https://reactjs.org/)
- [![Identity Framework](https://img.shields.io/badge/Identity%20Framework-35495E?style=for-the-badge&logo=auth0&logoColor=white)](https://learn.microsoft.com/en-us/aspnet/core/security/authentication/)
- [![Node.js](https://img.shields.io/badge/Node.js-339933?style=for-the-badge&logo=node.js&logoColor=white)](https://nodejs.org/)
- [![npm](https://img.shields.io/badge/npm-CB3837?style=for-the-badge&logo=npm&logoColor=white)](https://www.npmjs.com/)
- [![SQL Server](https://img.shields.io/badge/SQL%20Server-CC2927?style=for-the-badge&logo=microsoftsqlserver&logoColor=white)](https://www.microsoft.com/en-us/sql-server)
- [![.NET SDK](https://img.shields.io/badge/.NET%20SDK-512BD4?style=for-the-badge&logo=dotnet&logoColor=white)](https://dotnet.microsoft.com/en-us/download)
- [![Docker](https://img.shields.io/badge/Docker-2496ED?style=for-the-badge&logo=docker&logoColor=white)](https://www.docker.com/) 
- **External API**: [OpenWeatherMap API](https://openweathermap.org/)

## Prerequisites
- **Backend Requirements**:
  - .NET 8.0 SDK or later
  - MSSQL Server
  - OpenWeatherMap API key (sign up [here](https://openweathermap.org/) for an API key).
- **Frontend Requirements**:
  - Node.js (16+ recommended)
  - NPM


## Setup Instructions

### 1. Clone the Repository
```bash
git clone https://github.com/kristofNyikes/SolarWatch
cd SolarWatch
```

### 2. Backend Setup
1. Navigate to the backend directory:
   ```bash
   cd SolarWatchApi
   ```
2. Configure the connection string in `appsettings.Development.json`:
   ```json
   "ConnectionStrings": {
       "DefaultConnection": "Server = <your_server>; Database = <database_name>; User Id = <your_user_id>; Password = <your_db_password>; Encrypt = false;"
   }
   ```
3. Add your OpenWeatherMap API key to a `.env` file in SolarWatchApi folder:
    ```env
    API_KEY=<your_api_key>
    ```
4. Run database migrations:
   ```bash
   dotnet ef database update
   ```
5. Start the backend server:
   ```bash
   dotnet run
   ```

### 3. Frontend Setup
1. Navigate to the frontend directory:
   ```bash
   cd SolarWatchWebApp
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Create a `.env` file in the SolarWatchWebApp directory and add the backend URL:
   ```env
   VITE_API_BASE_URL="https://localhost:7106"
   ```
4. Start the frontend development server:
   ```bash
   npm start
   ```

### 4. Access the Application
- Open your browser and navigate to `http://localhost:5173` to use the application.

## API Integration
This application uses the OpenWeatherMap API to fetch sunrise and sunset data. You can sign up and get your API key [here](https://openweathermap.org/).
