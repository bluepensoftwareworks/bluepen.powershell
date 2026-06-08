# Send-QuickApplicant CommandLet

It has been created as a shareable draft to showcase how to create binary powershell 7 module and commandlet within it. This commandlet sends individual messages/notifications via external mail service of choice while a user uses Powershell 7 (x64) environment. In addition, this solution shows how to works with **System.Management.Automation** namespace, and design binary powershell framework by utilizing **Abstract Factory and Facade Design Patterns**, and how to work with **MailKit package** and **MimeKit package** in Bluepen Powershell Binary Module (bluepen.powershell.cmdlets).

## Table of Contents

- [Project Structure](#project-structure)
- [Getting Started](#getting-started)

## Project Structure

This Visual Studio solution (`bluepen.powershell.sln`) contains the following three projects:

*   **`bluepen.powershell.cmdlets`**: This is a startup project that contains main QuickApplicant commandlet to execute at Powershell 7 command prompt.
*   **`bluepen.powershell.domain`**: The class library contains domain entities, abstracts and service interfaces that define signatures for creation and communication between layers and serve as foundation for pattern-based design framework.
*   **`bluepen.powershell.services`**: This class library contains implementations for notification services, factories, extension methods and custom exceptions that provide core pieces for powershell binary module framework.

## Getting Started



### Prerequisites


### Installation


## Usage


## Contributing


## License


## Contact


