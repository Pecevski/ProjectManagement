**# Project Management application**

1. Assignment Goals

Following the logic of your Midterm 1 assignment you are expected to implement authorization and authentication to your project management application using industry standard protocols and libraries. Using the knowledge and experience you acquired during week 7 Implement JWT bearer token authentication and authorization for the application.


2. Required Tasks

Authentication

The User should be able to login to the Project Management API with his username and password. When the user logs in for a first time and this is the first application execution there should be two users, the administrator and a manager.


Username: admin Password: adminpass 
Username: manager Password: managerpass 

For a User without administrative privileges the access to the Users Management Endpoints needs to be restricted.

For a User without administrative or manager privileges the access to the Teams Management Endpoints needs to be restricted.

Login should be implemented using the IdentityServer4 library with JWT bearer tokens.

All endpoints should be accessible only by registered users, by providing the token as a standard Authorization header.

Using Roles based authorization and Policy based authorization implement the following endpoints with the proper restrictions.


Admins should have unrestricted access.

Users Management

Create User - Accessible only by admin (Role of new user should be provided)
List user - Accessible only by authenticated users
Update - Accessible only by admin
Delete - Accessible only by admin
Teams Management

Create - Accessible only by admin or manager
List - Accessible only by admin or manager
Update - Accessible only by admin or manager
Delete - Accessible only by admin or manager
Add Team Member- Accessible only by admin or manager (or this could be done with the Update endpoint)
Projects Management

Create - Accessible only by authenticated users
List - Accessible only by authenticated users
ListMy - Accessible only by authenticated users (data returned is related to user)
Update - Owner of the Project
Delete - Owner of the Project
Assign team - Owner of the Project
Task Management

List - Any owner or team member of the project the Task is in
Create - Any owner or team member of the project the Task is in
Update - Any owner or team member of the project the Task is in
Delete - Any owner or team member of the project the Task is in
Get - Accessible only by authenticated users
Reassign - The assignee of the Task (the new Assignee should be a team member)
Work Log Management

Create - Only assignee of the Task
Get - Any Team member or owner of the Project the Task is in.
Update - Only assignee of the Task
Delete - Only assignee of the Task

**Technologies implemented:**

  Microsoft.EntityFrameworkCore 5.0; 
  Microsoft.EntityFrameworkCore.SqlServer; 
  Microsoft.EntityFrameworkCore.Tools; 
  Microsoft.EntityFrameworkCore.Proxies; 
  ASP.NET Core (MVC) (with .NET 5.0); 
  Swashbuckle.AspNetCore (Swagger); 

  - Microsoft Exstension NuGet packages:

	Microsoft.Extensions.Configuration; 
	Microsoft.Extensions.Configuration.Binder; 
	Microsoft.Extensions.Configuration.EnvironmentVariables; 
	Microsoft.Extensions.Configuration.FileExtensions; 
	Microsoft.Extensions.Configuration.Json; 
	Microsoft.ASP.NET Core.Identity.UI
	Microsoft.ASP.NET Core.Authentication.JwtBearer;
	Microsoft.ASP.NET Core.Diagnostics.EntityFrameworkCore;
	IdentityServer4;
