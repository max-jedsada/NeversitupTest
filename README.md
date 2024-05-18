# ProjectNeversitupAPI

## Structure Project

### Project.API
- Properties : launchSettings.json file : launch config
- Controller : route api
- ServicesRegister : keeb service / interface to register injection service.
- Program.cs file : setting program initial before run.
- appsettings.json file : config etc, by environment.
  
### Project.Business
- Exception folder : Exception error from custom
- Interfaces : Interfaces
- Services : code all on logic.
- ViewModel : model data return to api.

### Project.Entities
- Entity : model entity sync DB. 
- DbContext : DB Entity + connection = read data anything or everything we need it :: modyfy by EF framework. 

### Project.Utility
- Middleware : everything action or task in process on program running.

---
