######################################################################################################################################################
To create a PowerShell cmdlet in Visual Studio, you should use the "Class Library" template within a C# project, 
as you need to write your cmdlet logic in C# to leverage the full functionality of creating a compiled cmdlet; 
essentially, you'll be building a .NET assembly that can be imported into PowerShell as a module. 
######################################################################################################################################################
Key points to remember:
Project type: Create a new C# Class Library project. 
Reference assemblies: Ensure you include the necessary PowerShell reference assemblies in your project to access 
the cmdlet base classes. 
Code structure: Within your C# classes, inherit from the appropriate PowerShell cmdlet base class (like Cmdlet or PSCmdlet) 
and implement the required methods like BeginProcessing, ProcessRecord, and EndProcessing to define your cmdlet behavior.
######################################################################################################################################################
Cmdlet vs. PSCmslet - Windows PowerShell
Thursday, January 24, 2008
C# Windows PowerShell
When you write a Command-Let in Windows PowerShell, you must derive from one of the following classes: System.Management.Automation.Cmdlet or 
System.Management.Automation.PSCmdlet.

One of the most popular questions from beginners PowerShell developers, is "What's the difference?".
The answer is simple - PSCmdlet derives from Cmdlet and give more power and functionality. When you derive from PSCmdlet, you have a better 
interaction with the PowerShell runtime environment. it means that you can access the session state information, call script, access providers - and 
more access to the powershell runtime than when you derive from Cmdlet class.

Anyway, PSCmdlet, derives from Cmdlet too.

So, When derives from Cmdlet and when from PSCmdlet?
To answer, it's important to understand what's the disadvantage of deriving from PSCmdlet - you more depend in the PowerShell runtime.
When command-let (which every command-let is actually a class) derives from Cmdlet, it can be invoked directly, without using Runspace in some cases.
But, if you derive from PSCmdlet, you can't invoke the command lets directly (by simply create instance of them), and you must use Runspace to run 
commands that use your command-let.

In conclusion, deriving from Cmdlet is the best choice except when you need fully integration with powershell runtime, access to session state data, 
call scripts etc. Then, you have to derive from PSCmdlet.
######################################################################################################################################################
References:
https://www.red-gate.com/simple-talk/development/dotnet-development/using-c-to-create-powershell-cmdlets-the-basics/
https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/cmdlet-development-guidelines?view=powershell-7.4
https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/how-to-write-a-simple-cmdlet?view=powershell-7.4
https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/tutorials-for-writing-cmdlets?view=powershell-7.4
https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/cmdlet-overview?view=powershell-7.4
https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/cmdlet-development-guidelines?view=powershell-7.4
To confirm that it is really a cmdlet, first build your project,  then load and examine what is in your module:
PS> Import-Module .\bin\Debug\net8.0\bluepen.powershell.cmdlets.dll
PS> Get-Command -module bluepen.powershell.cmdlets
