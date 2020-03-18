# Zendesk Search Tool ReadMe

[![Zendesk](https://assets.phenompeople.com/CareerConnectResources/ZENDUS/en_us/desktop/assets/images/header-logo.png)](https://www.zendesk.com/)


# Introduction

The Solution is written in C#  (.NET Core 3.1) and the setup details etc are included below.

It has been created to meet the following specified requirements: 

- Command Line Application
- User is able to search the data and the results are returned in a human readable format  (JSON key value pairs + Titles and Subtitles to annotate)
- Where the data exists, values from all related entities are included in the results, i.e. matching tickets should return the relevant users (assignees, submitters) and organisations (and simialr for Users' and their organization's).
- The user can  search for empty values, e.g. where search text is empty.

Notes have been added below to also outline Code Practices and a section detailing my thought process regarding other solution implementation options that were considered (and why they were each not choosen).

If there are any questions then please feel free to contact me.

Kind Regards,

Paul Kernaghan
pgkernaghan@hotmail.com
0402727796


# Technical Design Overview

Within the Solution there are 7 Projects:
-  1 Main Console Project to launch the application, receive input and display output results (ZemDeskCodeChallenge)
-   3 Domain specific Projects. ZendeskSearchManager, ZendeskSearchProcessor, and ZendeskSearchRepository
	- ZendeskSearchManager manages the Search Requests and Search Response to and from the SearchProcessor. It also takes responsibility for parsing and displaying to the console the search results returned by the Search Processor
    - ZendeskSearchProcessor Performs the Asynchronous Search Requests to ZendeskSearchRepository. It can be extended as required should the search become more complicated i.e. to take in more parameters, pre-processing and post-processing of results
    - ZendeskSearchRepository loads the data upon instantiation into 3 entity lists (User, Organization, Ticket). It provides methods for these lists to be searched (by Id); and to return ComplexObject list for each entity i.e. the entity plus the objects it relates to as a single object.
- 3 Unit Test Project for each of the domain specific projects to verify their key class's functionality.

How it works...

The ZemDeskCodeChallenge will create an instance of the ZendeskSearchManager (which in turn invokes the SearchProcessor; that similarly invokes the SearchRepository methods). The results are then passed back the chain and SearchRequest/ SearchResult objects amended as required.

Note: The repository usese Dependency Injection upon startup to gather the filePath settings of the json files holding the respective search data format i.e. Tickets, Users and Organizations and the loads these in. These are held within Static collections to only be instantiated once.
Note: Key method calls in the chain are Asynchronous to allow for concurrent calls and searches as and when required.

Within the Console the User can specify the 'SearchText' i.e. the text to search for.
This will result in all items within each of the 3 lists (Users, Tickets, Organizations) being searched (via their JsonData properties holding all the JSON). If for any entity a match is found then it's key details are returned as distinct properties to a lazy object - for example a matching User will return it's Id and OrganisationId.
Note: Each list is search Asynchronously to ensure that they can run concurrently in order to reduce overall search time and make best use of available resources.
Once the LazyObject list for each entity has been collated (containing all matching entities), each object will be retreived as a Complex Object i.e. in an object containing all properties of both the entity itself and of it's related objects. These lists are then returned in the SearchResult object
Note: Objects and Interfaces have been used to pass values so that their respective classes can encapsulate the inner working and allow the system to be more easily maintained, modified and Tested.
To terminate the application once it has reported all it's values, simply press 'Return' as per the Console Text.

# Setup/ Installation

# How to Run the Code (Windows or Linux)

Pre-Requisite: This solution is to be run only on a machine with the .NET Core 3.1 Runtime already installed.  Download link here: https://dotnet.microsoft.com/download

0. Open the files supplied 
1. In Command Prompt/ Terminal, navigate to \ZemdeskCodeChallenge directory
2.  Execute the following commands:
        dotnet clean
        dotnet build 

 3. a   cd PrintCostDriver
         dotnet run ZemdeskCodeChallenge.csproj
  OR
3. b. Via the Operating System UI, Navigate to subdirectory where the release executable can be found and run the exe. 
          Directory subfolder = ZemdeskCodeChallenge\ZemdeskCodeChallenge\bin\Release\netcoreapp3.1
          double click on PrintCostDriver.exe

# How to Run the Automated Tests (if not using the Visual Studio IDE wit the VS Extension 'NUnit Test Runner Adapter' installed) 

To run all tests, after doing steps 0 - 2 above  then do the following, go to the main /ZemdeskCodeChallenge directory and run the following command: dotnet test

To execute Unit Tests for only specific Projects then do the following:

ZendeskSearchManager Tests. To Oprtionally run the ZendeskSearchProcessor Unit tests, after doing steps 0 - 2 above  then do the following: 

1. Navigate to filepath where Tests are located i.e. ZemdeskCodeChallenge/ZendeskSearchManager.Tests
2. Type:  dotnet test ZendeskSearchManager.Tests.csproj
3. Verify Test Results

ZendeskSearchRepository Tests. To Oprtionally run the ZendeskSearchRepository Unit tests, after doing steps 0 - 2 above  then do the following: 

1. Navigate to filepath where Tests are located i.e. ZemdeskCodeChallenge/ZendeskSearchRepository.Tests
2. Type:  dotnet test ZendeskSearchRepository.Tests
3. Verify Test Results


# Coding Features, Notes and Details

As requested, within this Challenge Response's codebase I've attempted to showcase how I'd mostly approach a similar problem/ solution in a workplace evironment.
Some of the feature and choices included here are:

- Created a solution desgined to be inline with the SOLID Principles and Clean Code
- Dependency Injection on startup to read-in the necessary values
- Using centralised Configuration files and setup to define settings and injecting these in rather then being hidden in the application
- Used appropriate Data Types, inheritance and abstraction as an when required. Appropriate use of data collections, common libraries and common pattern; plus choosing to avoid unnecessary complexity on occasions
- Where appropriate injecting dependencies into class constructors and instance methods, thus helping with reusablity, readability and testability
- Using Models and Interfaces to abstract out what is being passed around to make the code more mantainable i.e. when a change arises it's better to update one model rather than numerous verbose method signatures
- Test Driven Development and good level oof Test coverage. Unit Test Stubs were created and functionality driven from the outside in i.e. identify functionality, write test stub, implement (note sometimes plans changed as detailed in GIT commit notes).
-'Constant' Files used to hold Text string and Error Message text that can be updated and localised/ replaced as required (and referenced in unit tests) and to avoid magic strings within the code
- Utility Helper Classes where apt 
- Unit Tests also refactored to remove various duplicate code into methods that can be written-once and used many times
- Plus normal good coding practices i.e. meaningful variables namesm Self Describing code, Breaaking up methods into small methods where too long and abstracting out into appropriate classes etc.

Note: You many wonder why I've seldom used private and instead opted for Protected. It's simply based on previous experience were oftern classes are inherited from and propeties need to be accessed (especially testing).

# Solution Design Choice - Personal Notes and Viewpoint

Within this solution I don't use a DB or anything more complicated than built-in common in-memory data collections, this approach is different to how I initially thought I might implement the Solution. The other options and how they were evaluated are discussed here:

AWS: Initally I thought of using the following approach (since ZenDesk are AWS Oriented for Cloud atm)...
- Cloudformaion script to code the deployment of the solution stack
- Load JSON files into S3 Bucket -> Use AWS Glue and a custom Classifier to load from S3 in Glue -> Transfer from Glue into Dyanmo DB (i.e. Highly Available, Highly Scalable, Cost Effective, Fault Tolerant NoSQL)
- Via Cloudformation define an API Gateway -> Lambda -> DynamoDB stack to do HTTPGet Requests to search and return the relevant Data; serverless being highly scalable and elastic (and use good practices such as enabling Cloudwatch logs,  API logging, IAM Roles with Least Privelege for various Resource Policies, and deploy to region of lowest latency (aus) or most cost-effective (USA) )
however I didn't go for this as one of the key criteria was stated as:
'Simplicity - aim for the simplest solution that gets the job done whilst
readable, extensible and testable.' and another 'Robustness - should handle and report errors.' The AWS approach would be overly engineered and due to the distributed nature of the application (ie across numerous services) any application issues would be more challenging to pinpoint and remedy (as the relevant Engineer would need to understand all components)

- MongoDB: With all the unstructured JSON a NoSQL solution was another port of call on the ideas journey. MongoDB, AS the most popular NoSql DB, was a prime candidate however I didn't go down this path for 2 reasons:
i)  Complexity (almost circular relationships between) collections.
ii) Deployment/ Setup: I was reluctant to create a solution that couldn't be easily deployed without either installing extra libraries (beyond .net) or reusing a Cloud Version

- EF6: I consider a RDS/ NoSql hybrid via SQL Server and using OpenJSON standard to hold amd parse the JSON data. This way a balance could be found between holding key values in specific Id columns within their respective tables and the rest of the data in the JSON column. Again this was abandoned due to weight of setting up and tearing down the DB each would make it overly complicated.