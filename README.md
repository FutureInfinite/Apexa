# Strategy
- The specifications indicated that basic .NET frameworks were to be used. The entire solution was put together using the latest .NET technology.

# Design
The solution uses a multi-layers approach. It works from the back-end data access point to the front-end exposure. The following is the general layout of the layers.

- Apexa.DataAccess
	-  This layer provides the basic/core access to the DB. It was indicated to use memory-based DB capabilities and used a .NET library that provided a memory-based DB construct worked into Entity Framework
	-  This layer has a second level that provides DB explicit operations (named repositories). It provides the main data operations. This is also the main entry point into this layer.
- Apexa.Services
	-  This layer provides the main data migration aspect of the system. It provides the ability to access data operations using standardized data Contracts that will be used as stabilization definitions for the data model. This will be how the outside world sees the system. This provides a means to have the data layer isolated and not have an impact on higher levels. It provides a good means of separation of the system using modularization and componentization.
- Apexa.BLL
	-	This layer is the main access point into the system. It provides general exposure to the solution using the general data contracts for the system. It provides the means to isolate the data layer and keeps the intricacies of the lower levels away from the upper levels. 

The solution uses typical Dependency Injection but some aspects of DI injection into constructors were not possible so manual construction was done.

# Execution
- The general design of the solution is API-based using Microsoft development technology. I can see no direct access to the solution without using advanced tools like Powershell. 
- The solution was extended and a REST-based service was put together to demonstrate/give access to the underlying data operations.
	-	Apexa.REST.Service
		- This service provides HTTP access points to the data operations.
		- This also is a good means to generalize access to the system as the use of HTTP access techniques can be applied and any development tool that can access the service can be used. Somewhat a separation like capability
- There are test cases also implemented and a review of how data operations can be seen here also
- An additional Project was put together that provides a WEB application front end that accesses the above service of the data operations.
	-	ApexaApp
		- This is a Microsoft Blazor based project that has pages that use all the operations defined in the data layer

- The service was configured to have swagger active also for release. Can access via the debug url at:
	- https://localhost:7274/swagger/index.html
- The web app is testable in debug mode and can be deployed to IIS in release mode (which was done and validated that it worked).
- Can use the following PowerShell script operations to access the API via the DEBUG version of the service
	- $Url = "https://localhost:7274/api/Advisor/Create?SIN=160028304&Name=John Doe&Address=123 this Road, This Place&Phone=0123456789"
	- Invoke-RestMethod -Method 'Post' -Uri $url -Credential $Cred 

	- $Url = "https://localhost:7274/api/Advisor/Get/160028304"
	- Invoke-RestMethod -Method 'Get' -Uri $url -Credential $Cred 

	- $Url = "https://localhost:7274/api/Advisor/Update?SIN=160028304&Name=Jane Doe&Address=124 this Road, This Place&Phone=0123456789"
	- Invoke-RestMethod -Method 'Put' -Uri $url -Credential $Cred 

	- $Url = "https://localhost:7274/api/Advisor/Get/160028304"
	- Invoke-RestMethod -Method 'Get' -Uri $url -Credential $Cred 

	- $Url = "https://localhost:7274/api/Advisor/Delete/160028304"
	- Invoke-RestMethod -Method 'Delete' -Uri $url -Credential $Cred 	
 
# Documentation
- It was indicated that swagger was to be used for documentation. I am not familiar with using Swagger for general libraries. I did prepare the REST service described above to use Swagger for operation exposure and simple documentation.

		

