# Infra Watcher

Infra Watcher is a .Net Core application that can connect and execute commands on servers. The application supports execution of basic operations as well as custom scripts.
Queries can be customized to run a custom command or script.

## Getting Started

* Copy the files
<br />
`git clone 'https://github.com/jhonnyelhelou91/InfraWatcher.git' 'C:\git\C#\InfraWatcher'`
* Open the solution
`C:\git\C#\InfraWatcher\InfraWatcher.sln`
* Build solution
`Ctrl + Shift + B`

### Prerequisites

* Visual Studio
* .Net Core SDK

### Running the tests

To run the tests, you need the following:

* Build solution 
<br />
`Ctrl + Shift + B`
* Open Test Explorer from Visual Studio `Ctrl + Q` then type `Test Explorer` and hit `Enter`
* Update the hosts and credentials
* Run All tests

- - - -

## Definitions

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
