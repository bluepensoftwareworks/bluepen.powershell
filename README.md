# Send-QuickApplicant CommandLet (Powershell 7)

It has been created as a shareable draft to showcase how to create binary powershell 7 module and commandlet within it. This commandlet sends individual messages/notifications via external mail service of choice while a user uses Powershell 7 (x64) environment. In addition, this solution shows how to works with **System.Management.Automation** namespace, and design binary powershell framework by utilizing **Abstract Factory and Facade Design Patterns**, and how to work with **MailKit package** and **MimeKit package** in Bluepen Powershell Binary Module (bluepen.powershell.cmdlets).

The project is **reuseable workflow framework** for building binary PowerShell modules with Cmdlets. QuickApplicant can be shown as one concrete implementation, while the real value is the architecture behind it: separation of concerns, design patterns, SOLID principles, OOP/OOD, service classes, factories, validation, packaging, and structured cmdlet output.

## Table of Contents

- [Project Structure](#project-structure)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
- [Usage](#usage)
- [Packaging and Distribution](#packaging-distribution)
- [How to Extend This Framework](#how-to-extend-this-framework)
- [Contributing](#contributing)
- [License](#license)
- [Contact](#contact)
- [References](#references)

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

*   To run the main application, set `bluepen.powershell.cmdlets` as the **Startup Project** in the Solution Explorer.
*   Rebuild Solution.
*   Invoke Powershell 7 (x64)
*   Install Module via
    **PS C:\{directory-what-have-you}> Import-Module -Name C:\{directory-what-have-you}\bluepen.powershell\bluepen.powershell.cmdlets\bin\Release\net8.0\bluepen.powershell.cmdlets.dll**
    <img width="1347" height="516" alt="image" src="https://github.com/user-attachments/assets/d2050551-5fe0-4856-9be7-fcc09367b229" />
*   Attach Visual Studio to a process
    <img width="785" height="590" alt="image" src="https://github.com/user-attachments/assets/54c1ad0b-887a-448a-abd3-ccdd67f42238" />
*   Execute CommandLet with the following...
*   **PS C:\{directory-where-you-have-installed-binary-module-dll}**>**Send-QuickApplicant** **-m** {either Y or G} **-cr** **(Get-Credential)** (**-r** {you can list recipients as a comma separate list -OR- **-rp** (provide full file path to: recipients.txt if -File switch is present)}) **-s** {provide subject matter of your notification. if multiple words enclose in double quotes} **-t** {what is your topic} (**-c** {what is your content. content can be written in-line and enclosed in double quotes -OR- **-cp** (provide full file path to content.txt if -File switch is present)}) (**-a** {provide full file path to attachment if File switch is present, **the attachment is optional** }) **-sg** {what is your signature} **-File** (switch can be present or absent.)

     <img width="1687" height="643" alt="image" src="https://github.com/user-attachments/assets/727aedb3-2f1f-416e-a32b-1bc14cd155f7" />
     <img width="1740" height="843" alt="image" src="https://github.com/user-attachments/assets/cb79afe0-f636-4246-9f96-67bb9fbeb2de" />




* Command Examples
*     Send-QuickApplicant -m {either Y or G} -cr (Get-Credential) -r tom.hanks@movies.com,dwayne.jonsohn@movies.com,ebabayan@bluepensoftware.com -s "Oscar movies" -t "Nominee" -c "Who is to present awards this evening?" -sg "Billy Christal"
*     Send-QuickApplicant -m {either Y or G} -cr (Get-Credential) -rp f:\recipients.txt -s "Oscar Movies" -t "Nominee" -cp f:\content.txt -a f:\attachment.pdf -sg "Billy Christal" **-File**


## Packaging and Distribution

* Because PowerShell 7 runs on .NET Core/.NET 8, we need to ensure that your dependencies (MailKit, MimeKit, and your two class libraries) are copied into the output directory, while System.Management.Automation is excluded (since PowerShell already provides it at runtime).

   * Here is the step-by-step guide to building, packaging, and preparing your module for distribution:
   
     **Step 1**: Configure Your Profile file (.csproj)

     Open your main Binary PowerShell Module project in Visual Studio 2022. We need to edit the **'bluepen.powershell.cmdlets.csproj** file directly to handle dependency copying and targeting framework alignment.
    
     Right-click your project in the Solution Explorer, select **Edit Project File**, and configure it like this:

     <img width="1193" height="606" alt="image" src="https://github.com/user-attachments/assets/c35c67aa-8512-4284-ad15-c0ae08a80845" />
     <img width="1068" height="386" alt="image" src="https://github.com/user-attachments/assets/ceabcbf6-81ce-4cfc-8d7e-a65ac6450b78" />

     You need to tell MSBuild to copy your PDF and TXT files into the build output directory alongside your compiled module DLL. Open your .csproj file and add an <ItemGroup> using the None or Content include patterns.
     Set CopyToOutputDirectory to PreserveNewest.
     <img width="948" height="351" alt="image" src="https://github.com/user-attachments/assets/5e3c23f7-91be-48e3-b603-5e09f397efe0" />

     When you run dotnet build or dotnet publish, these files will be placed in the output folder relative to your DLL. 

     **Step 2**: Create the Module Manifest (.psd1)
     PowerShell modules require a manifest file so PowerShell knows how to load them.
     1. Open PowerShell 7 and navigate to your project folder: **bluepen.powershell.cmdlets**
     2. Run the following command to generate a template manifest:
        **New-ModuleManifest** -Path .\**bluepen.powershell.cmdlets.psd1** -RootModule '**bluepen.powershell.cmdlets.dll**' -Author 'Bluepen Software' -CompanyName 'Bluepen Software' -Description 'Module description'
     3. Open the generated **bluepen.powershell.cmdlets.psd1** file and ensure the following keys are set:
     4. RootModule = 'bluepen.powershell.cmdlets.dll'
     5. RequiredAssemblies = @('bluepen.powershell.cmdlets.dll','bluepen.powershell.domain.dll', 'bluepen.powershell.services.dll', 'MailKit.dll', 'MimeKit.dll')
     6. FileList = @('attachment.pdf','content.txt','recipients.txt');
    
     **Step 3**: Automate the Layout (Optional but Recommended)
     When you distribute a module, all files must sit inside a folder named exactly like the module itself. You can automate this layout creation using a **Post-Build Event** in Visual Studio
     1. Right-click your project ->**Properties**->**Build**->**Events**.
     2. In the Post-build event box, paste the following script to create a clean **Publish** folder
     3. xcopy "$(TargetDir)*.dll" "$(TargetDir)Publish\bluepen.powershell.cmdlets\" /Y /I
     4. xcopy "$(ProjectDir)bluepen.powershell.cmdlets.psd1" "$(TargetDir)Publish\bluepen.powershell.cmdlets\" /Y
     5. (Alternatively, you can just manually grab the files from your bin/Debug/net8.0 folder later).
    
     **Step 4**: 
     1. Set your build configuration to **Release**
     2. Click **Build > Build Solution** (or Ctrl+Shift+B)
     3. Navigate to your output folder (e.g. bin/Release/net8.0/Publish/bluepen.powershell.cmdlets)
     Your final distribution folder must look like this:
     <img width="1135" height="477" alt="image" src="https://github.com/user-attachments/assets/84242b95-ed7d-46c8-b5a6-0c488d2e7acb" />
      Note: Ensure System.Management.Automation.dll is NOT in this folder.

     **Step 5**: Test Locally
     Before distributing, make sure it works on your machine.
     1. Open a fresh PowerShell 7 session.
     2. Import the module directly using the path to your manifest:
     3. Import-Module "C:\Path\To\Your\Project\bin\Release\net8.0\Publish\bluepen.powershell.cmdlets\bluepen.powershell.cmdlets.psd1" -Force
     4. Run one of your cmdlets to verify that MailKit and your class libraries load without assembly resolution errors.
     5. <img width="1161" height="332" alt="image" src="https://github.com/user-attachments/assets/2bef755e-4ae6-4931-bee9-558ee8ad219e" />

     **Step 6**: Distribute the module
     You have two primary ways to distribute this package to others:

     **Option A: Manual Zip Distribution (Internal/Fileshare)**
     1. Zip the bluepen.powershell.cmdlets folder
     2. Instruct users to extract it into one of their $env:PSModulePath directories, such as: **C:\Users\{username}\Documents\Powershell\Modules\bluepen.powershell.cmdlets**
     3. They can then simply run Import-Module -Name bluepen.powershell.cmdlets
    
     **Option B: Publish to PowerShell Gallery (or Internal NuGet Repository)**
     PowerShell 7 uses Microsoft.PowerShell.PSResourceGet for publishing
     1. Ensure your .psd1 file has a property filled-out ModuleVersion, Description, and Author.
     2. Run the following command to publish your module folder to the PowerShell Gallery (require an API Key)
     3. Publish-PSResource -Path "C:\Path\To\Publish\bluepen.powershell.cmdlets" -Repository PSGallery -APIKey "your-api-key"
     4. Once published, anyone can install it by running: Install-PSResource bluepen.powershell.cmdlets

## How to Extend This Framework
(more to come...) <br/>
<img width="403" height="837" alt="image" src="https://github.com/user-attachments/assets/053329ef-d130-4c89-9295-59abb35fb323" />


    
## Contributing

* At this time, current solution is a draft that is open for cloning and improving at your own pace and use. You are welcome to copy this framework for Powershell Binary Module development and extensability. At this time project is open for contributions only to specific bluepen software team members.

## License

* Free library developed by Bluepen Software team members as RnD work and experiment.

## Contact

* If you have any questions or would like to learn more, please free to reach out at support@bluepensoftware.com.

## References

* [using-c-to-create-powershell-cmdlets-the-basics](https://www.red-gate.com/simple-talk/development/dotnet-development/using-c-to-create-powershell-cmdlets-the-basics/)
* [cmdlet-development-guidelines](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/cmdlet-development-guidelines?view=powershell-7.4)
* [how-to-write-a-simple-cmdlet](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/how-to-write-a-simple-cmdlet?view=powershell-7.4)
* [how-to-write-a-powershell-binary-module](https://learn.microsoft.com/en-us/powershell/scripting/developer/module/how-to-write-a-powershell-binary-module?view=powershell-7.6)
* [installing-a-powershell-module](https://learn.microsoft.com/en-us/powershell/scripting/developer/module/installing-a-powershell-module?view=powershell-7.4)
* [tutorials-for-writing-cmdlets](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/tutorials-for-writing-cmdlets?view=powershell-7.4)
* [creating-a-cmdlet-without-parameters](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/creating-a-cmdlet-without-parameters?view=powershell-7.4)
* [adding-parameters-that-process-command-line-input](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/adding-parameters-that-process-command-line-input?view=powershell-7.4)
* [adding-user-messages-to-your-cmdlet](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/adding-user-messages-to-your-cmdlet?view=powershell-7.4)
* [adding-aliases-wildcard-expansion-and-help-to-cmdlet-parameters](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/adding-aliases-wildcard-expansion-and-help-to-cmdlet-parameters?view=powershell-7.4)
* [cmdlet-overview](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/cmdlet-overview?view=powershell-7.4)
* [cmdlet-development-guidelines](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/cmdlet-development-guidelines?view=powershell-7.4)
* [declaring-properties-as-parameters](https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/declaring-properties-as-parameters?view=powershell-7.4)


**Prompts just in case:**
* how to package and distribute additional artifacts like pdf and txt with binary powershell module in C#
* Provide step by step instructions how to package and distribute Binary Powershell Module that has dependency on two other class library assemblies and System.Management.Automation, MailKit and MimeKit packages in Visual Studio 2022 for Powershell 7
* should -RootModule specify fullpath to dll or not for New-ModuleManifest
* Install-Module has parameter -Repository, what is the right value for this parameter?
* how to prepare C# PowerShell module for distribution and to package in Visual Studio 2022
* how to prepare C# PowerShell module for distribution and to package in Visual Studio 2022 with multiple class libraries and extra files
* C# cmdlet powershell output type custom object

