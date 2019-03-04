# Infra Watcher

Infra Watcher is a .Net Core application that can connect and execute commands on servers. The application supports execution of basic operations as well as custom scripts.
Queries can be customized to run a custom command or script.

## Getting Started

* Copy the files <br />
`git clone 'https://github.com/jhonnyelhelou91/InfraWatcher.git' 'C:\git\C#\InfraWatcher'`
* Open the solution <br />
`C:\git\C#\InfraWatcher\InfraWatcher.sln`
* Build solution <br />
`Ctrl + Shift + B`

### Prerequisites

* Visual Studio
* .Net Core SDK

### Running the tests

To run the tests, you need the following:

* Build solution <br />
`Ctrl + Shift + B`
* Open Test Explorer from Visual Studio `Ctrl + Q` then type `Test Explorer` and hit `Enter`
* Update the hosts and credentials
* Run All tests

- - - -

## Default Command Providers

### DefaultCommandProvider

Loads commands as string texts. This is useful to:
* Support simple commands.
* Install libraries
* Perform common commands: uname, hostname, df -k, ls, grep...


### ScriptCommandProvider

Loads scripts as command texts. This is useful to:
* Support complex commands.
* Commands are different between operating system, version, processor type...
* Use elevated commands.
* Support retry logic.
* Support logging on the server.
* etc...

- - - -

## Default Connection Services

### WMI (Windows Management Instrumentation) Connection

Opens a connection that is able to execute WMI queries. This is useful to:
* Connect to Windows servers
* Support simple commands that manage CIM entities
* Get machine information (since multiple providers exist for multiple windows operating systems)
* Support [WQL query](https://docs.microsoft.com/en-us/windows/desktop/WmiSdk/wql-sql-for-wmi "WQL query")
* Other [features](https://en.wikipedia.org/wiki/Windows_Management_Instrumentation#Features "features")

### SSH Connection

Opens an SSH connection where you can execute commands or scripts. This is useful to:
* Connect to Linux servers
* Support simple to complex operations
* Other [features](https://en.wikipedia.org/wiki/Secure_Shell#Uses "features")

### Remote PowerShell Connection

Opens a connection to [WinRM](https://docs.microsoft.com/en-us/windows/desktop/winrm/portal "WinRM") service on a remote server. This is useful to:
* Connect to Windows servers
* Support simple to complex operations
* Use PowerShell commands or scripts to perform operations
