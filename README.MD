# Repository Example

Based heavily on [Gary Woodfine's](https://garywoodfine.com/generic-repository-pattern-net-core/)
[Repository Pattern Library](https://github.com/threenine/Threenine.Data).

## Connecting to MySQL with ASP.NET Core

You'll need to remember to switch bewteen a local and deployed connection string in `Startup.cs`.

* First, if you need to blow our your current MySQL install and start over:
    * `sudo apt purge mysql-*` - Get rid of *everything* mysql
    * `sudo apt autoremove` - Get rid of any redidual generated files
* The, follow this tutorial - [How To Install MySQL on Ubuntu 18.04](https://www.digitalocean.com/community/tutorials/how-to-install-mysql-on-ubuntu-18-04)
    * Create user - `CREATE USER 'dbuser'@'localhost' IDENTIFIED BY 'passsword';`
    * Give user privileges - `GRANT ALL PRIVILEGES ON *.* TO 'dbuser'@'localhost' WITH GRANT OPTION;`
    * Reset priviliges on running server - `FLUSH PRIVILEGES;`
* Use the [Pomelo Foundation](https://github.com/PomeloFoundation/Pomelo.EntityFrameworkCore.MySql) [MySQL EF Core Connector](https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/) - `dotnet add package Pomelo.EntityFrameworkCore.MySql --version 3.1.1`
* Develop a MySQL Connection String - [MySQL connection strings](https://www.connectionstrings.com/mysql/)

Of course, you'll need a database to point to, so that needs to be created on the command line:
* Login as the user you'll want to access the database with (e.g. `dbuser`)
* Create the database: `CREATE DATABASE <database_name_here>` (e.g. `CREATE DATABASE wizard`)

It will also be necessary to switch the `ASPNETCORE_ENVIRONMENT` environment variable from *Development* to *Production* when deploying.  See [Use multiple environments in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/environments?view=aspnetcore-3.1).  Please also remember that the [Tutorial: Create a Razor Pages web app with ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/?view=aspnetcore-3.1) covers this in the [Add a model to a Razor Pages app in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/model?view=aspnetcore-3.1&tabs=visual-studio) section of the tutorial.

## Information on Session Variables
Since HTTP is a stateless protocol, we must have a place to store page state between calls to the page.  Here are some resources specific to RazorPages:
* [Session and state management in ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/app-state?view=aspnetcore-3.1)
* Tips on how I got my Dispostions to show on Page2: [Checkboxes in a Razor Pages Form](https://www.learnrazorpages.com/razor-pages/forms/checkboxes#collections-of-complex-objects)
* Reference [Bootstrap Documentation](https://getbootstrap.com/) if you want to know how I made my pages and forms.

## Notes

Things to look out for:
* when running a new project, don't forget it is neccesary to run the Entity Framework tool commands:
    * `dotnet ef migrations add InitialCreate`
    * `dotnet ef database update`
* When placing an EF database context in its own project, you need to be mindful of the *other target frameworks* section of [this article](https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet#other-target-frameworks)
* this means you need to run the `ef` tooling like this: `dotnet ef migrations add InitialCreate --startup-project ../wizardui/wizardui.csproj`
* You may need to periodically go to the *Windows Task Manager* and kill `.NET Host Service` when dotnet commands fail (happenend when running above commands)
* the `dotnet` command apparently builds `.gitignore` files for you with this command: `dotnet new gitignore` (run it within the project directory)
* Commands for working with Solution Files:
    * `dotnet add sln` (do this in your upper main folder)
    * `dotnet sln add <project>`
* Command for making the projects *talk* to each other (the web project likely needs to talk to the others):
    * `dotnet add <project> reference <other-project>`
* testing hint: the testing project needs to talk to the libraries
    * time permitting, we can test the RazorPages app as well


