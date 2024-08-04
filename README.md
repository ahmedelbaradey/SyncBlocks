






**System Requirements Specifications**

*Project Name: Sync Block* 

*Author Name: Ahmed Elbaradey*

*04 August 2024*

*Version 1.0*










1. # **About This Document** 
   1. ## **Document Purpose and Scope**
      This document details the following 

- High Level & Low-Level Design
- Functional & non-functional requirements
- Communication between Services inside the System
- Used Storage Providers 

For Sync Block Microservice release [1.0].

1. ## **GitHub Repository**
   <https://github.com/ahmedelbaradey/SyncBlocks>

1. ## **MVB Links**
   1. ### **Block Service UI**
`                        `<https://blockservice.azurewebsites.net/>

1. ### **Metadata Service**
`                          `<https://syncblockmetadataservice.azurewebsites.net/>

1. ### **Postman Collection Link**
   <https://www.postman.com/ahmedelbaradey/workspace/syncblok-service/collection/5429344-52c3998f-2f9b-430b-9f25-76aecc601394?action=share&creator=5429344>

1. ## **Document Context** 

|Document Name |Type   |Attachment|
| :-: | :-: | :-: |
|**Business Case**|word||
||||

1. ## **Approvals**

|**Role**|**Name**|**Title**|**Signature**|**Date**|
| :-: | :-: | :-: | :-: | :-: |
|**Project Sponsor**|||||
|**Business Owner**|||||
|**Project Manager**|||||
|**System Architect**|||||
|**Development Lead**|||||
|**User Experience Lead**|||||
|**Quality Lead**|||||
|**Content Lead**|||||



1. # **About the Project** 
   1. ## <a name="_toc163449926"></a><a name="_toc182888881"></a>**Stakeholders**

|**#**|**Name**|**Title**|**Organization**|**Role in the Project** |**Email/Mobile No.**||||||||
| :- | :- | :- | :- | :- | :- | :- | :- | :- | :- | :- | :- | :- |
|**1**||||Project Sponsor| ||||||||
|**2**||||Product Owner| ||||||||
|**3**||||Project Consultant| ||||||||
|**4**||||Tech Lead|||||||||
|**5**||||Business Analyst|||||||||
|**6**||||Tester|||||||||


1. ## **Business Background**
   **Business Overview:**

   Building Storage Hub

   **As-is Situation:**

   Syn Block is now working on Drive. 

   **Solution:**

   Providing new system that cover all features that supported by Google Drive and provide additional features the help the day to day operations 

<a name="_toc257655374"></a>
1. ## **Project Purpose and Scope**
   1. ### **Project Purpose** 
The scope of the project mainly is to control the process of Uploading, Saving, Editing, and Sharing Files for each person on Organization in details that in scope and out of scope points.
1. ### **In Scope** 
**Installation and Configuration**

The installation and configuration activities will be in MS AZURE on two environments (Production, and Testing)

- **Implementation**

**Providing web-based system contain**s **the following modules:**

1. Uploading Files with any extension
1. Sharing files within the system or outside 
- For more details, please refer to “**Proposed Solution**” section 

- Training
- Organization will be responsible of holding the following training sessions on the proposed systems based on train-the-trainer concept.
- The training courses include:Includes:
  - **Business User Training** 
  - **Technical Training** 

- Support 
- Organization provides 1 year1-year post implementation support starts after the final acceptance for the implementation services according to the defined scope of work.
  - **This support includes fixing bugs from Sunday to Thursday (09:00 A.M to 05:00 P.M)**
    1. ### ` `**Out of Scope** 
- **Implementation**
- **Any other module that not mentioned in the “Proposed Solution”.**

- Integration 
  1. Integration Internal and External systems	
  1. Mobile application	
  1. Desktop Application
  1. Web Application
  - Flexible products

    Hardware 

- All hardware required for the solution.

Migration

1. **Migrating any physical/electronic contents to the proposed solution**


1. ## **System Perspective**
   1. ### **Dependencies**
      1. #### ***Azure Blob Storage & Why***
         The following points describe the use case scenarios:

- Serving images or documents directly to a browser
- Storing Files for distributed access
- Streaming video and audio
- Writing to log Files
- Storing data for backup, restore, disaster recovery and archiving
- Storing data for analysis by an on-premises or Azure-hosted service
- Data stored inside a blob container is classified and the blob itself is divided into three based on the data being stored on it.
- Block blobs store text and binary data. Block blobs are made up of blocks of data that can be managed individually. Block blobs can store up to about 190.7 TiB.
- Append blobs are made up of blocks like block blobs but are optimized for append operations. Append blobs are ideal for scenarios such as logging data from virtual machines.
- Page blobs store random access files up to 8 TiB in size. Page blobs store virtual hard drive (VHD) files and serve as disks for Azure virtual machines.


1. #### ***RabbitMQ & Why***
   RabbitMQ is well-suited for use cases that require instant messaging, task queues, and communication between microservices. It provides reliable message delivery, supports various messaging patterns, and offers rich message routing and processing features.
1. ### **Assumptions**
- Project will be divided into two phases during the implementation.
- Training will be done for our internal business consultant,consultant; our business consultant will train end users on how to use the system.

1. ### **Constraints**
- System should be accessible online.
- Application should be hosted. The project should be launched within 2 months maximum to catch the business opportunity. 
- The project has no defined budget.
- Project cost should approvedbe approved by the management before proceeding in low level requirements elicitation. Project fund should be planned and approved before starting the development.
  1. ### **Risks**
Too many configurations/customizations could be required by product owner who will use the system. 




1. ## <a name="_toc257655377"></a>**Glossary of Terms**

|Term|Definition|
| :- | :- |
|**User** |Upload, Edit, Download, Share His files |
|**System Admin**|Responsible For Administration|

1. # **About the System** 
   1. ## <a name="_toc257655379"></a>**HLD (High Level Design)**
      1. ### **Overview**
![Aspose Words 854bc419-a3bf-4036-bf86-66e3dbe74122 001](https://github.com/user-attachments/assets/25ce2d05-2a15-453d-bb23-02283a6ffd28)

1. ### **Specifications**
   1. #### ***User Uploading***
      Users interact with the client application or web interface to initiate file uploads. The client application communicates with the Load Balancer then Block Service on the server side.
   1. #### ***Block Service***
      Block service Receives file upload requests from clients 

- Chunks large files into smaller chunks (Blocks 4MB each) for efficient transfer. 
- Hash every Block and get ready for Metadata sending
- After successful upload and Committing blocks to Azure Blob Storage Block Service Sent Notification to RabbitMQ with Metadata payload
  1. #### ***Metadata Service***
     It updates the Metadata Database with file details and send notification to RabbitMQ that Metadata successfully added to DB

     to Notify Client
  1. #### ***RabbitMQ***
     Deals as Message Queueing Service can be used for several functions in our System

- Receives Metadata from Block Service and Send it to Metadata Service to be Updated on Db 
- Notify Clients with Updates
- Using SignalR, push or any Real Time communication service we can push notifications to client, we are using SignalR in our System




  1. ## **LLD (Low Level Design)**
     1. ### **Overview**
        ![Aspose Words 854bc419-a3bf-4036-bf86-66e3dbe74122 002](https://github.com/user-attachments/assets/bc416f92-23e5-44cb-a3b8-b650698c2247)


        **Dividing the File into 4MB Blocks**

        Instead of treating a file as one large block, it's divided into smaller, manageable pieces or chunks, Using the chunk-based approach, the 1 GB file might be split into 250 chunks of 4MB each this savings in time and bandwidth are evident.

        By break the files into multiple chunks we overcome the following problems:

- Bandwidth and time consuming 
- Latency or Concurrency Utilization.
- Cloud space utilization

`                         `There is no need to upload/download the whole single file after making any changes in the file

You just need to save the chunk which is updated (this will take less memory and time). It will be easier to keep the different versions of the files in various chunks.

We have considered one file which is divided into various chunks. If there are multiple files, then we need to know which chunks belong to which file , so here is comes Metadata DB and Metadata Service 
1. #### ***Dividing File into 4MB Blocks***
   Instead of treating a file as one large block, it's divided into smaller, manageable pieces or chunks, Using the chunk-based approach, the 1 GB file might be split into 250 chunks of 4MB each this savings in time and bandwidth are evident.

   By break the files into multiple chunks we overcome the following problems:

- Bandwidth and time consuming 
- Latency or Concurrency Utilization.
- Cloud space utilization

  There is no need to upload/download the whole single file after making any changes in the file

  You just need to save the chunk which is updated (this will take less memory and time). It will be easier to keep the different versions of the files in various chunks.

  We have considered one file which is divided into various chunks. If there are multiple files, then we need to know which chunks belong to which file, so here comes Metadata DB and Metadata Service 
  1. #### ***Hashing Blocks***
     `                                `Now, with 250 blocks ready, the system must ensure that each block can be uniquely identified. For this purpose, it utilizes a cryptographic hashing function, such as MD5. This function takes in the data from each 4MB block and outputs a unique hash value. Even a minor change in the block's data would result in a drastically different hash, ensuring that each block's content can be reliably identified and verified.
  1. #### ***Storing Metadata*** 
     Having History of file changes over time is essentially necessary requirement so when we save Blocks to Metadata DB we must need to know which version of file this Blocks related to, here comes Journal purpose as it every Journals must have Timestamp and Type list of Blocks identifying the File Current Change 


1. #### ***Database Diagram*** 
![Aspose Words 854bc419-a3bf-4036-bf86-66e3dbe74122 003](https://github.com/user-attachments/assets/ebfdfa1d-b7eb-4842-9def-e37c0161ae21)

1. ### **Specifications**

1. #### ***User Cases***
- When User Created For First Time he must has at least One Device, One Shared Object (File, Directory) , by default system will create root folder for every user when signed up and add current device 
- ` `By default, every Shared Object created by user will be assigned to him (UserObjects Table) and will be assigned aa Owner of Object (UserObjectPermissions -> Permissions)
- Every Shared Object Created will have Journals List to track Changes over time 
- Every Journal must have List of Blocks in case of Shared Object is File to determine which version of Shared Object related to change in time 
- Also Every Journal must Have user and Device or just Device and linked to User how made the change from (UserDevices)
  1. ## **System Overview**
     1. ### <a name="_toc257655380"></a>**Overview** 
        The solution is web-based application aims to provide the Organization’s Users the ability to Upload , Edit , Share , Download , Remove & Track their files 

1. ### **Main Features**
1. ### <a name="_toc257655385"></a>**User Groups & Access Privileges**
   Every User on system will have full access to his root directory and every file and directory he created or moved to him will inherit the full access permissions 

   |**User Type**|**Functions**|**Permissions**|
   | :- | :- | :- |
   |Regular User|<p>- Login/logout</p><p>- Upload Files</p><p>- Remove Files</p><p>- Share Files</p><p>- Download</p>|<p>- Read</p><p>- Write</p><p>- Delete</p><p>- Owner</p>|

1. ## <a name="_toc399083060"></a>**Non-Functional Requirements**
   1. ### **Availability**
      1. #### ***Availability requirements***
         We need to achieve 5 nines 99.999%, to achieve that we need to concentrate on the following improvements

- PAAS providers 
- System Design
- Database Design
- Used Technology 

1. ### **Durability**
Backups & Applying Point on Time Restoring Data techniques will achieve this easily 
1. ### **Reliability**
1. ### **Scalability**
   1. #### ***Overview***
      To achieving this, we need to know the maximum expected capacity for the system for two purposes 

- Integrate plans that will help us to easily scale the system 
- Design the system to be integrable with these scaling plans

  This following resource estimator allows us to define the scale of the system with different parameters 

![Aspose Words 854bc419-a3bf-4036-bf86-66e3dbe74122 004](https://github.com/user-attachments/assets/46137251-b737-43b0-81fe-c06c457b15b1)


1. #### ***Horizontal Scaling***
   We can add more servers behind the load balancer to increase the capacity of each service. This is known as Horizontal Scaling and each service can be independently scaled horizontally in our design.

1. #### ***Database Sharding***
   Metadata DB is sharded based on SharedObjectId . Our hash function will map each SharedObjectId to a random server where we can store the file/folder metadata. To query for a particular SharedObjectId , service can determine the database server using same hash function and query for data. This approach will distribute our database load to multiple servers making it scalable.

1. ### **Parallelism**
   Uploading or downloading several chunks simultaneously can maximize bandwidth utilization. For our system, this means quicker backup and retrieval times.
1. ### **Deduplication**
   Over time, our editor might have multiple users with some shared footage. Rather than storing duplicate data, the system can recognize identical files and store them just once. This not only saves storage space but also upload time.
1. ### **Security**
   Each chunk can be encrypted individually. If there were a security breach and a chunk's encryption was compromised, the entire file wouldn't necessarily be vulnerable.
1. ### **Quality Requirements** 
   1. #### <a name="_toc257655397"></a>***Capacity & Performance***

      |Factor|Requirement|
      | :- | :- |
      |**How many users will use the system?**||
      |**How many times a particular transaction will be performed per unit of time?**||
      |**How many records will be stored in the database of the main business entities (such as customers)?**||
      |**Any potential growth in the coming few years: records and users? What is it? (Ex: if the customer plans on providing a promotional event, this may increase the number of users/records).**||
      |**How many users will concurrently use the system (find the worst case scenario)? And optionally, how much time they expect to reach the peak traffic and for how long it remains?**||
      |**What is the acceptable system response time?**||
      |**What is the pages hit ratio (which pages are more likely to be used by the users)?**||
      |**How much resources will the system use of the machine?**||
      |**Will users use a LAN or a WAN?**||
      |**If WAN, Internet or leased line?**||
      |**What is the bandwidth, latency, and packet loss?**||


1. ### <a name="_toc257655399"></a>**Other Requirements**
   1. #### <a name="_toc257655400"></a> ***Technology Requirements***
   1. #### <a name="_toc257655401"></a> ***Data Migration***
   1. #### <a name="_toc257655402"></a>***System Conventions***
   1. #### <a name="_toc257655403"></a> ***Usability***

1. #### <a name="_toc257655404"></a>***Compliance Requirements***
- System must be compliance with the security and coding guidelines.

1. ### <a name="_toc257655406"></a>**Training and Documentation Requirements**


1. ## <a name="_toc163449940"></a><a name="_toc182888897"></a>**Functional Requirements**
   1. ### **Add, Edit, Delete & Share Files and Directories**
      1. #### ***Overview***
         User Can Add, Edit, Delete and Share Files and Directories 
      1. #### ***Use Cases***
      1. #### ***Data Forms***
      1. #### ***UI Wireframes***
   1. ### **Upload Files with any extension**
   1. ### **Synchronize User Directories and Files between User Clients**
   1. ### **Send Notification to User whenever data updated**
   1. ### **Download Files**
   1. ### **Authentication – Security Module**
      1. #### ***Overview***
         This module includes the general functions between all users. Each user can use the system after authenticating their accessibility, to view the system modules according to his/her assigned permissions. 

      1. #### ***Use Cases***
         1. ##### Log In

|**Use Case ID:**|**UC-A-01**|
| -: | :- |
|**Description:**|This function to authenticate the user who shall login first to view the system modules  |
|**Actors:**|Any user  |
|**Pre-conditions:**|<p>- The user is already added in the system   </p><p>- User hasn’t saved his/her login information </p>|
|**Business Rules** |None |
|**Basic Flow:**|<p>1\. User opens the system URL </p><p>2\. System displays the log-in form </p><p>3\. User fills-in the [Log-in Form], and clicks "Enter" </p><p>- User can check “Remember Me” option to save the username and password (optional) </p><p>4\. System displays the home page </p>|
|**Alternative Flows:**|**NA**|
|**Exceptions:**|<p>- **Basic flow, in step 3:**</p><p>- If the user entered invalid name or password:</p><p>- System displays an alert below the form: "The username or password you have entered is not valid"</p><p>- The “remember me” option of saving the username and password shall be deactivated. </p>|
|**Post-conditions:**|` `The user can use the system according to his/her privileges. |
|**Includes:**|-  |

1. ##### Log Out

|**Use Case ID:**|**UC-A-02**|
| -: | :- |
|**Description:**|This function allows the user to log-out from the system. |
|**Actors:**|Any User  |
|**Pre-conditions:**|The user shall be logged-in |
|**Business Rules**|<p>System shall log-out the user automatically (session expiry), in the following cases: </p><p>- After no more than **(60) minutes** of inactivity</p>|
|**Basic Flow:**|<p>1\. User clicks on "log-out" that displayed from any page of the system</p><p>2\. System displays the log-in page. </p>|
|**Alternative Flows:**|None|
|**Exceptions:**|None|
|**Post-conditions:**|The user can't use any function that open in another page until he/she logs in again. |
|**Includes:**|None|


1. #### ***Data Forms***
   1. ##### Log In

|**Field name**|**Type**|<p>**Mandatory**</p><p>**(Yes/No)**</p>|**Comments**|
| :-: | :-: | :-: | :-: |
|**Username** |Text|Yes||
|**Password** |Password  |Yes||
|**Remember Me**|Checkbox |No |If checked, the log-in credentials will be saved |
|**Forget Password** |Link |No|This link sends link to the user’s e-mail to create new password. |
|**Log-in**|Button|No|This button verifies the user’s authentication, then display the home page according to the user’s role. |

1. #### ***UI Wireframes***
   1. ##### Log In
`                                                         `![Text

Description automatically generated with medium confidence](Aspose.Words.854bc419-a3bf-4036-bf86-66e3dbe74122.005.png)
1. # **Appendices**
   1. ## <a name="_toc257655408"></a>**Appendix I: Dropped/Changed Requirements**
   1. ## <a name="_toc257655409"></a>**Appendix II: Client’s Wish List for Future Releases**

   1. ## <a name="_toc257655410"></a>**Appendix III: Sample Documents:![](Aspose.Words.854bc419-a3bf-4036-bf86-66e3dbe74122.006.png)**

1. ## <a name="_toc257655411"></a>**Appendix IV: Open/Closed Issues**



