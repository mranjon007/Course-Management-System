# Course-Management-System


CMS is a web application aims to help Course Coordinator (CCO) of CSE department of ULAB (University of Liberal Arts Bangladesh) by providing an online environment for managing all courses and Faculty member’s information in a reliable way.

What Course Management System Does?

•	Coordinator can easily access CMS with username and password 
•	Coordinator can add, remove, edit and update any courses information in a very convenient way
•	Can add, remove and update faculty members profile
•	Can assign and remove faculty for any courses
•	Automated report generating system (For Register office, Departmental Head and for individual Teacher)
•	If one faculty member is assigning for two courses at the same time, CMS will notify.
•	If one faculty member is being filled by his number of courses (fixed by CCO) but CCO want to assign more course, CMS will notify
•	CCO can change time slot for any course in CMS
•	In a nutshell, this application will notify and guide CCO to any type of overlapping of scheduling between courses. 

What Languages and framework we Use to build this System? 
•	Use Asp.net MVC framework to build this system. 
•	Javascript(Jquery) have been used for client side validation. 
•	For real-time response (submit and retrieve information without loading), we use Ajax. 
•	As I am not aware of Authentication and Authorization feature of Asp.net MVC during the building of this Application, I used Session information to login and logout process, which is not a good practice but that time I don’t have another choice.   
•	For PDF report generation, we used .NET iTextSharp library and Javascript css2pdf plugin. 
•	The Excelpackage class is used to generate excel report. 
•	Use SMTP server for email reports. 

