# Strategy
- The specifications indicated that basic .NET frameworks were to be used. The entire solution was put together using the latest .NET technology.

# Design
The solution uses a multi layers approach. It works from the back end data access point to the front end exposure. The following is the general layout of the layers.

- Apexa.DataAccess
	-  This layer provides the basic/core access to ther DB. It was indcated to use memory based DB capabilities and used a .NET library that provided a memory based DB construct worked into Entity Framework
	-  This layer has a second level that provides DB explicit operations (named repositories). It provides the main data operations. This is also the main entry point into this layer.
- Apexa.Services
	-  This layer provides the main data migration aspect of the system. It provides the ability to access data operations using standardized data Contracts that will be used as stabalization defintions for the data model. This will be how the outside wolrd sees the system. This provides a means to have the data layer isolated and not have impact on higher levels. It provides a good means of seperation of the system using modularization and componentization.
- Apexa.BLL
	-	This layer is the main access point into the system. It provides general exposure of the solution using the general data contracts for the system. It provides the means to isolate the data layer and keeps the intricacies of the lower levels away from the upper levels. 

The solutoin uses typical Depenmdency Injection but some aspects of DI injection into constructos was not possible so manual construction was done.

# Execution
- The general design of the solution is API based using Microsoft development technology. There is no direct access to the solution that I can see possible without using advanded tools like Powershell. And I did not have enough time to investigate how to use.
- The solution was extended and a REST based service was put together to demonstrate / give acces to the underlying data operations.
	-	Apexa.REST.Service
		- This service provides HTTP access points to the data operations.
		- This also is a good means to generalize access to the system as the use of HTTP access techniques can be applied and any development tool that can access the service can be used. Somewhat a sepearation like capability
- There are test cases also implemented and review of how data operations can be seen here also
- An additional Project was put together that provides a WEB application front end that accesses the above service of the data operations.
	-	ApexaApp
		- This is a Microsoft Blazor based project that has pages that use all the operations defined iun the data layer

- The service was configured to have swagger active also for release. Can access via the reelase url at:
	- http://[URL BASE]/swagger/index.html
- The web app is testable in debug mode and can be deployed to IIS in release mode (which was done and validated that it worked).

 
# Documentation
- It was indicated that swagger was to be used for documentation. I am not familiair with using swagger for general libraries and was able to determine how to integrate with them. I did prepare the REST service described above to use Swagger for operation exposure.

		
