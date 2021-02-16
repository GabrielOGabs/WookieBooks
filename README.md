# Wookie Books API
## This is the test API created for a Senior Software Developer Position on Savvyy.ai

Wookie Books API is a .Net Core 3.1 Web API [RESTful] to perform CRUD operations for a mocked in-memory database
The database contains already:

- 3 users
- 10 books

## Features

- CRUD operations for Books
- CRUD operations for Users
- In-Memory Database integrated with Entity Framework ORM
- Authentication with JWT Bearer token
- Swagger Documentation

## Installation
This API requires .Net Core Framework 3.1

1st - Clone this repository on your desired folder
```sh
cd "C:\Dev\WookieBooks"
git clone "https://github.com/GabrielOGabs/WookieBooks.git"
```

2nd - Open the Solution on Visual Studio and Build the entire solution
It should download the required packages and be able to run.

3rd - Hit the Play (Run) Button

## Testing

Wookie Books can be tested on the API home page created with Swagger UI or using a Postman Request Collection
[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/3ca199fce7bd0d70761f)

#### Testing on Postman
```sh
Run one of the Authorization Methods to get the JWT token (it will automatic store the key on a global variable)
After the token is loaded you can access all the endpoints from Books and Users folders
```

## License

MIT