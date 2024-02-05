# StackOverflow Clone (ASP.NET 6 MVC)
## Project Overview  
This is a web application where question, answer and voting system like Stack Overflow is implemented.
This project was created to develop a StackOverflow clone using various technologies and architectural patterns. The aim was to replicate the functionality of the original StackOverflow platform while incorporating modern frameworks and best practices.  

## <ins>System Desing</ins>

### Architecture -Three Layered Architecture ( Presentation, Services, Data Access Layer )

In ASP.NET MVC, the three-layer architecture comprises the Presentation Layer, Services Layer, and Data Access Layer.   
<ins>The Presentation</ins> Layer handles user interactions and interface rendering through views and controllers.  
<ins>The Services</ins> Layer acts as an intermediary, encapsulating business logic and facilitating communication between the Presentation Layer and the Data Access Layer.  
<ins>The Data Access Layer</ins> manages interactions with the database, abstracting data operations for efficient handling within the application. This architecture promotes scalability, maintainability, and separation of concerns in ASP.NET MVC applications.  
Along side Repositoy and UnitOfWork pattern Implemented
## Technologies Used

- **Logging**: Utilized `log4net` for effective logging and monitoring of the application.
- **Framework**: Built on `Asp.Net Core 6`, a modern and efficient framework for web development.
- **User Authentication**: Integrated `NHibernate.AspNetCore.Identity` for seamless user authentication and management.
- **Database**: Employed `MS SQL Server` as the reliable database backend for storing and retrieving data.
- **ORM**: Utilized `NHibernate` as the Object-Relational Mapping (ORM) tool for efficient data access and manipulation.
- **Architecture**: Implemented a `3 Layers Architecture` consisting of Web, Application, and Infrastructure layers to ensure a clear separation of concerns.
### User Roles and Privileges in the System
#### User Privileges:
Users in the system possess specific privileges to their role. They can:
- Create, update, and delete their own questions and answers, empowering them to manage their contributions effectively.
- Vote on any question or answer, enabling them to engage with the community and express their opinions.

#### Admin Authority:
Administrators hold elevated permissions within the platform, enabling them to perform advanced actions. They can:
- Modify any answer or delete content as necessary, ensuring the integrity and quality of the platform's content.
- Maintain the platform's standards and address any issues promptly.

#### Guest User Access:
Guest users, while limited in their interactions, still benefit from the platform's content. They can:
- View questions and answers, providing them with valuable information and insights without requiring an account.
- Browse content passively without actively participating in the community.


### Database Design
User Entity  
![image](https://github.com/mishurahman616/StackOverflowClone/assets/72443968/21a56009-871b-4846-865f-12ae1084ee75)

Question Entity | 
Answer Entity | 
Question Vote Entity | 
Answer Vote Entity


<p float="left">
  <img src="https://github.com/mishurahman616/StackOverflowClone/assets/72443968/d7302d94-f849-4fa6-9690-4629a0f32f38" width="200" />
  <img src="https://github.com/mishurahman616/StackOverflowClone/assets/72443968/b68769dd-788f-4e59-896d-e7777f1b7117" width="200" />
  <img src="https://github.com/mishurahman616/StackOverflowClone/assets/72443968/e893fb46-6e16-4e75-8d4f-8108461e9d1c" width="200" />
  <img src="https://github.com/mishurahman616/StackOverflowClone/assets/72443968/bfe41ce5-7575-4ffe-afdb-956d950be15f" width="200" />
</p>


Relation between them 
![image](https://github.com/mishurahman616/StackOverflowClone/assets/72443968/edfa7e02-5118-434e-98d5-b2f9025be04e)


## Conclusion


In summary, this project has allowed me to showcase my proficiency in web development through the creation of a StackOverflow clone. Leveraging technologies such as log4net, Asp.Net Core 6, and NHibernate, I have demonstrated the ability to design and implement a scalable and efficient web application. The adoption of a 3 Layers Architecture has ensured a structured approach to development, promoting maintainability and scalability. 
