# BookLibrary
Book library ASP .NET MVC Web application for university

## Overview 

### ASP .NET MVC project

1. Microsoft Visual Studio Community 2017 version 15.8.5
2. JetBrains ReSharper Ultimate 2018.2.3

___

## Open and run the project

**1.** To open the project you just have to go to **BookLibrary** folder and open the `.sln` file with **Visual Studio**.

**2.** Click `Ctrl+Shift+ B` to **build** the project.

**3.** `Ctrl+F5` to start the project.

## Re-create database

1. Go to **Solution Explorer**, click show **All Files** icon.

2. Go to **App_Data**, right click and delete all `.mdf` files for this project.

3. Delete **Migrations** folder by right click and delete.

4. Go to **SQL Server Management Studio**, make sure the DB for this project is not there, otherwise delete it.

5. Execute the following commands: 

    **1. Enable migration to the project**
      - *enable-migrations -contexttypename [context.name]* (ex. **BookCatalogDbContext**)

    **2. Create migration**
      - *add-migration [name]*

    **3. Update database with applying all changes on the context**
      - *update-database*
      
___

## Reference

### Atanas Arshinkov (me) - Java Developer

[GitHub account](https://www.github.com/aarshinkov)

[LinkedIn account](https://www.linkedin.com/in/atanas-arshinkov)
