Для використання додатку потрібно створити базу даних (виконати команду Update-Database в Package Manager Console).

Endpoints:
    POST /users/register: To register a new user.
    POST /users/login: To authenticate a user and return a JWT.
    POST /tasks: To create a new task (authenticated).
    GET /tasks: To retrieve a list of tasks for the authenticated user, with optional filters (e.g., status, due date, priority).
    GET /tasks/{id}: To retrieve the details of a specific task by its ID (authenticated).
    PUT /tasks/{id}: To update an existing task (authenticated).
    DELETE /tasks/{id}: To delete a specific task by its ID (authenticated).
Filtering and Sorting:
    Allow filtering of tasks based on Status, DueDate, and Priority.
Implement sorting options for DueDate and Priority.
Security:
    Secure all task-related endpoints using JWT authentication.
    Ensure proper authorization checks to confirm users can only access their own tasks.
Pagination: Implement pagination for the GET /tasks endpoint.

Для обробки помилок використовується IExceptionHandler, який перехоплює усі помилки та опрацьовує їх. Було вирішено використати паттерн Repository для майбутнього переходу проекту на іншу базу даних, без зміни коду запитів до бази даних.  Для мапінгу даних використовується AutoMapper. 

Огляньмо архітектуру проектів 
LE.WebApi - наш веб сервер який відповідає за обробку запитів
LE.Common - проект який утримує загальну логіку проекту, такі як контракти, та помилки, та emums 
LE.DataAccess - проект потрібн для ORM EntityFrameworkCore
LE.DomainEntities - проект який утримує модель бази данних
LE.Infrastructure - проект який утримує усі інтерфейси для інфрастуктури
LE.Repositories - проект який реалізує репозиторії інфраструктури 
LE.Services -  проект який реалізує сервіси інфраструктури