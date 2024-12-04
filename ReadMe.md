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