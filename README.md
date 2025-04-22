# ✅ Todo List API — .NET 9, EF Core, MySQL

This is a RESTful Web API built using ASP.NET Core 9 and Entity Framework Core 9.  
It allows full CRUD (Create, Read, Update, Delete) operations on todo items stored in a MySQL database.  
The project includes unit tests, test coverage reports, code quality checks, and AI-assisted development feedback.

---

## 🚀 How to Run This Application

### Step 1: Install Requirements

- [.NET 9 SDK (Preview)](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [MySQL Server](https://dev.mysql.com/downloads/mysql/)
- MySQL user credentials with create database permissions

---

### Step 2: Configure the Database

Open `appsettings.json` and replace with your MySQL credentials:

```json
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=TodoDb;user=root;password=yourpassword;"
}
```
### Step 3: Apply Migrations
Open terminal or command prompt inside the project folder and run:
dotnet ef database update
### Step 4: Run Application
dotnet run
The API will be available at:https://localhost:{your-port}
You can test the endpoints using Swagger:https://localhost:{your-port}/swagger

📚 Available Endpoints
Method	Route	        Description
GET	    /api/todo	    Get all todos
GET	    /api/todo/{id}	Get todo by ID
POST	/api/todo	    Create new todo
PUT	    /api/todo/{id}	Update existing todo
DELETE	/api/todo/{id}	Delete a todo


How to Run Unit Tests
Navigate to the root of the project and run:
dotnet test --collect:"XPlat Code Coverage"
 How to View Test Coverage
Install the report generator (if not already installed):

dotnet tool install -g dotnet-reportgenerator-globaltool

Then run:reportgenerator -reports:**/coverage.cobertura.xml -targetdir:coveragereport -reporttypes:Html

Open:coveragereport/index.html
You will see a detailed coverage report (line + branch coverage).
Note:Migrations,Dbcontext Snapshot model and Program.cs is not included

🧠 AI Usage Feedback
Was it easy to complete the task using AI?
Yes guidance was clear and step-by-step.

How long did the task take you to complete?
3 hours including writing, testing, styling, and fixing warnings.

Was the code ready to run after generation?
Mostly yes.When it does not i changed the prompts and clarify the things because of that problem solved.

Which challenges did you face during the task?

*Fixing StyleCop warnings

*Understanding and improving test coverage

*Managing package versions (EF Core 9 preview)

Which specific prompts helped you the most?

-There isn't any specif prompt for that.
