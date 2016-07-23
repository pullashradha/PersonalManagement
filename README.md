# Personal Management Webpage

#### By Shradha Pulla, July 2016

## Description

This program is a personal management web application. A user is able to create a private, secure account, and manage their todo list, notes, address book, and budget online.

## Setup/Installation Requirements

This program can only be accessed on a PC with Windows 10, and with Git, Atom, and Sql Server Management Studio (SSMS) installed.

* Clone this repository
* Import the database and test database:
  * Open SSMS
  * Select the following buttons from the top nav bar to open the database scripts file: File>Open>File>"Desktop\PersonalManagement\Sql Databases\personal_management.sql"
  * Save the personal_management.sql file
  * To create the database: click the "!Execute" button on the top nav bar
  * To create the database another way, type the following into the top of the sql file:
    * CREATE DATABASE personal_management;
    * GO
  * Refresh SSMS
  * Repeat the above steps to import the test database
* Test the program:
  * Type the following command into PowerShell > dnu restore
  * Next type > dnx test
  * All tests should be passing, if not run dnx test again. Otherwise, fix the errors before launching the program on the browser.
* View the web page:
  * Type following command into PowerShell > dnx kestrel
  * Open Chrome and type in the following address: localhost:5004

## Database Creation Instructions

To build the databases from scratch, type the commands below in the Windows PowerShell:
  * Desktop> SQLCMD -S "Server-Name";
    * 1> CREATE DATABASE personal_management;
    * 2> GO
    * 3> USE personal_management;
    * 4> GO
    * 5> CREATE TABLE tasks
    * 6> (
    * 7> id INT IDENTITY(1,1),
    * 8> description VARCHAR(255)
    * 9> due_date DATETIME
    * 10> );
    * 11> GO
    * 12> CREATE TABLE categories
    * 13> (
    * 14> id INT IDENTITY(1,1),
    * 15> name VARCHAR(100)
    * 16> );
    * 17> GO
    * 18> CREATE TABLE notes
    * 19> (
    * 20> id INT IDENTITY(1,1),
    * 21> title VARCHAR(100),
    * 22> content VARCHAR(5000)
    * 23> );
    * 24> GO
    * 25> CREATE TABLE todo_list
    * 26> (
    * 27> id INT IDENTITY(1,1),
    * 28> task_id INT,
    * 29> category_id INT
    * 30> );
    * 31> GO
  * Exit out of SQLCMD by typing> QUIT
  * Open SSMS, click open Databases folder and check that the personal_management database has been created
  * Click "New Query" button on top nav bar (above "!Execute")
  * Type following command into query text space to backup database: BACKUP DATABASE personal_management TO DISK = 'C:\Users\[Account-Name]\personal_management.bak'
  * Click "!Execute"
  * Right click personal_management in the Databases folder: Tasks>Restore>Database
  * Confirm that there is a database to restore in the "Backup sets to restore" option field
  * Under the "Destination" input form, change the database name to: "personal_management_test"
  * Click "OK", refresh SSMS, and view the new test database in the Database folder

If SQL is not connected in the PowerShell, open SSMS and click the "New Query" button (in nav bar above "!Execute"). Type commands shown above into the text space, starting from "CREATE DATABASE...".

## Known Bugs

No known bugs.

## Specifications

The program should ... | Example Input | Example Output
----- | ----- | -----
Have CRUD functionality for tasks in todo list | --- | ---
Have CRUD functionality for categories in todo list | --- | ---
Have CRUD functionality for notes | --- | ---
Have CRUD functionality for tags | --- | ---

## Future Features

HTML | CSS | C#
----- | ----- | -----
--- | --- | Add a shopping list, calculator, calendar & period tracker
--- | --- | Add account creation capabilities & verification
--- | --- | Develop web app into a mobile app

## Support and Contact Details

Contact Epicodus for support in running this program.

## Technologies Used

* HTML
* CSS
* Bootstrap
* C#

## Links

Git Hub Repository: https://github.com/pullashradha/PersonalManagement

## License

*This software is licensed under the Microsoft ASP.NET license.*

Copyright (c) 2016 Shradha Pulla
