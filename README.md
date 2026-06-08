# Send-QuickApplicant CommandLet (Powershell 7)

It has been created as a shareable draft to showcase how to create binary powershell 7 module and commandlet within it. This commandlet sends individual messages/notifications via external mail service of choice while a user uses Powershell 7 (x64) environment. In addition, this solution shows how to works with **System.Management.Automation** namespace, and design binary powershell framework by utilizing **Abstract Factory and Facade Design Patterns**, and how to work with **MailKit package** and **MimeKit package** in Bluepen Powershell Binary Module (bluepen.powershell.cmdlets).

## Table of Contents

- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)

## Project Structure

This Visual Studio solution (`bluepen.powershell.sln`) contains the following three projects:

*   **`bluepen.powershell.cmdlets`**: This is a startup project that contains main QuickApplicant commandlet to execute at Powershell 7 command prompt.
*   **`bluepen.powershell.domain`**: The class library contains domain entities, abstracts and service interfaces that define signatures for creation and communication between layers and serve as foundation for pattern-based design framework.
*   **`bluepen.powershell.services`**: This class library contains implementations for notification services, factories, extension methods and custom exceptions that provide core pieces for powershell binary module framework.

## Getting Started

Instructions on how to get the solution up and running on a local machine.

### Prerequisites

Before you begin, ensure you have the following installed on your machine:

* Visual Studio 2022: Download Visual Studio 2022 (https://learn.microsoft.com/en-us/visualstudio/releases/2022/release-notes) or higher at https://visualstudio.microsoft.com/downloads/
* NuGet Package Manager   6.14.3 / NuGet Package Manager in Visual Studio. For more information about NuGet, visit https://docs.nuget.org/
* Yahoo email account / Google email account

### Installation

Step-by-step commands or instructions to clone the repository and open the solution.

1.  Clone the repository:
    ```bash
    git clone https://github.com/bluepensoftwareworks/bluepen.powershell.git
    ```
2.  Open the solution file (`bluepen.powershell.sln`) in Visual Studio.
3.  Restore NuGet packages if they don't restore automatically.
4.  To generate a Yahoo App Password so you can connect your Yahoo account to a third-party email app (like Outlook, Apple Mail, or Thunderbird), follow these quick steps:
    <img width="1561" height="871" alt="image" src="https://github.com/user-attachments/assets/37bee0b3-91ac-4293-b2f1-c1e3c9fbcbc3" />
    1. Sign in to your Yahoo Account Security page.
    2. Scroll down to the "Other ways to sign in" or "External connections" section.
    3. Click "Generate app password" or "Manage app passwords".
    4. Click "Get started," then enter a name for the app (e.g., "Outlook") in the text field.
    5. Click "Generate password".
    6. Copy the generated one-time password and use it as your password when setting up your account in your third-party app.      
5. To Create Google Application Password for your personal google account at https://myaccount.google.com/apppasswords. For references, please use the following video training session: https://www.youtube.com/watch?v=wniM7sU0bmU
    <img width="1432" height="831" alt="image" src="https://github.com/user-attachments/assets/d50bd9b5-9aba-4230-8fae-dad8b502625d" />

## Usage

Provide examples of how to use our solution, including code snippets or screenshots if applicable...

*   To run the main application, set `bluepen.powershell.cmdlets` as the **Startup Project** in the Solution Explorer and press F5.
*   To run tests for `Project3.Name`, use the Visual Studio Test Explorer.

## Contributing


## License


## Contact


