# InstagramLink API

## Project Overview
InstagramLink is a web API designed to manage links that can be inserted or updated on a user's page. The project includes features for user authentication, link management, and testing.

## Project Structure (So Far)

- **Controllers**:
  - `OnPageLinkController.cs`: Manages CRUD operations for on-page links.
  - `UsersController.cs`: Handles user-related operations.
  - `WeatherForecastController.cs`: Sample controller for testing.

- **Models**:
  - `LoginRequest.cs`: Model for user login data.
  - `OnPageLink.cs`: Model for storing links on a user's page.
  - `User.cs`: Model representing a user entity.

- **Repositories**:
  - `IUserRepository.cs`: Interface for user repository operations.
  - `UserRepository.cs`: Implementation of user repository.

- **Services**:
  - `IUserService.cs`: Interface for user service operations.
  - `UserService.cs`: Business logic implementation for user management.

- **Tests**:
  - `OnPageLinksControllerTests.cs`: Unit tests for `OnPageLinkController`.
