# Email Validator App

## Overview

This repository contains a C# Windows Forms application named `Emailch` that performs email validation and checks the entered email through an API. It integrates both basic email validation using regular expressions and network checks for online validation using a third-party API. The application is also capable of handling network connectivity issues and supports asynchronous HTTP requests.

## Features

- **Email Validation**: Validates email format using regular expressions.
- **Network Connectivity Check**: Verifies internet connectivity using `Ping` to Google DNS (8.8.8.8).
- **API Email Validation**: Sends the email to an external API for verification using HTTP POST requests.
- **Real-time Email Validation**: The validation can be triggered by the "Enter" key or button press.
- **Error Handling**: Handles network and API-related errors with user-friendly messages.

## Technologies Used

- **C# .NET Framework**: The core language and framework used.
- **Windows Forms**: Used to create the UI for the application.
- **HttpClient**: For making HTTP requests to external APIs.
- **Ping**: Used to check network connectivity.
- **Regex**: For validating email format.
- **Task-based Asynchronous Pattern**: To handle API requests asynchronously.

## Dependencies

- **Newtonsoft.Json**: Used for serializing and deserializing JSON data in API requests and responses. You can add this package via NuGet:
    ```bash
    Install-Package Newtonsoft.Json
    ```

## Installation and Setup

1. **Clone the repository**:
    ```bash
    git clone https://github.com/your-username/email-validator-app.git
    ```

2. **Open in Visual Studio**:
   Open the solution (`Emailch.sln`) in Visual Studio.

3. **Build the solution**:
   Press `Ctrl+Shift+B` to build the project.

4. **Run the application**:
   Press `F5` or click the green "Start" button to run the application.

## Usage

1. **Email Input**: Enter an email address in the provided text box.
2. **Real-time Validation**: Upon pressing the "Enter" key or clicking the validate button, the app will:
   - Check the validity of the email format using a regular expression.
   - If valid, check network connectivity.
   - If network connectivity is available, it will make an API request to check if the email is valid.

3. **View Results**: The results of the API request are displayed in a `RichTextBox` on the form.

## API Integration

The app sends a POST request to the following API endpoint to verify emails:
- API URL: `https://throwaway.cloud/api/v2/email/`
  
The request body is a JSON object containing the email:
```json
{
    "email": "test@example.com"
}
```

## Error Handling

- **Invalid Email Format**: Shows an error message if the email format is incorrect.
- **Network Error**: Displays an error if the application cannot reach the API due to a network problem.
- **API Error**: If the API responds with an error or failure, the error message is shown in the application.

## Contributing

1. Fork the repository.
2. Create a new branch for your feature or bugfix.
3. Commit your changes.
4. Submit a pull request.

## License

This project is licensed under the MIT License. See the `LICENSE` file for more details.

---

**Note**: Ensure you replace any third-party API URLs or configurations as needed for your use case.

## Contact

For any questions or feedback, please contact [newtoxton@gmail.com].
