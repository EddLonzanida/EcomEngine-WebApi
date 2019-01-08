# EcomEngine
* Sample SOA using .NetCore2.2 with Angular7. 
* Demo app using [Eml*](https://www.nuget.org/packages?q=EddLonzanida) NuGets.
* Check out [EmlExtensions.vsix](https://marketplace.visualstudio.com/items?itemName=eDuDeTification.EmlExtensions) to automate the creation of controllers, views, seeders, and more!.

## Seed the database
1. Open the solution using Visual Studio 2017, compile and build (don't run yet)
2. Right click EcomEngine project and Set as **startup project**
3. Open Package manager console
4. In the 'Default project' **drop down**, select **EcomEngine.DataMigration** (this is important)
5. In the console, type the command below then press enter to execute.
```javascript
update-database -verbose
```

## Run the application
1. In the Visual Studio 2017 **Standard toolbar**, select **EcomEngine.Api** from the dropdown list, replacing the default IIS Express
2. Press F5 to run the back-end webapi using **Kestrel**
3. Open **Powershell**
4. Navigate to EcomEngine\Hosts\EcomEngine.Spa
5. In the console, type the command below then press enter to execute
```javascript
npm start
```

### Quick View
![](https://github.com/EddLonzanida/EcomEngine-WebApi/blob/master/Docs/Art/MainScreen.png)