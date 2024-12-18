
# SolarWatch

This is a web application that allows users to retrieve sunrise and sunset data for a specific city. The application integrates ASP.NET Core for the backend, React.js for the frontend, and uses Entity Framework with MSSQL for data management. Authentication is managed with Identity Framework. 

## Features
- Retrieve sunrise and sunset times for any city.
- Authentication with roles (admin and regular users) using Identity Framework.
- Secure API key integration with OpenWeatherMap API.
- Modern frontend built with React.js.

## Technologies Used
- **Backend**: ASP.NET Core
- **Database**: MSSQL with Entity Framework
- **Frontend**: React.js
- **Authentication**: Identity Framework
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
