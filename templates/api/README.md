# MyProjectName Usage Documentation

## Introduction
This document explains the structure and usage of the .NET Core template project created using the BBT.Aether SDK. This template includes all the necessary configurations and structure for developers to get started quickly.

## Folder Structure

The project folder structure is as follows:

```
.vscode
etc
  └── dapr
      └── components
          └── config.yaml
          └── pubsub.yaml
          └── secretstore.yaml
          └── state.yaml
      └── config.yaml
  └── docker
      └── config
          └── otel
          └── prometheus
          └── vault
      └── docker-compose.yml
src
  └── BBT.MyProjectName.Application
  └── BBT.MyProjectName.Domain
  └── BBT.MyProjectName.Infrastructure
  └── BBT.MyProjectName.HttpApi.Host
test
  └── BBT.MyProjectName.Application.Tests
  └── BBT.MyProjectName.Domain.Tests
  └── BBT.MyProjectName.Infrastructure.Tests
  └── BBT.MyProjectName.TestBase
 
.gitattributes
.gitignore
.prettierrc
BBT.MyProjectName.sln
BBT.MyProjectName.sln.DotSettings
common.props
delete-bin-obj.ps1
global.json
NuGet.Config
Dockerfile
run.ps1
```

### .vscode
Contains configuration files for Visual Studio Code.

### etc
Contains configuration files.
- **dapr**: Contains Dapr components and configuration files.
  - `components`: Dapr component configurations.
  - `config.yaml`: Dapr general configuration file.
- **docker**: Contains Docker configuration files.
  - `config`: Docker configurations.
  - `docker-compose.yml`: Docker Compose configuration file.

### src
Contains the application source code.
- **BBT.MyProjectName.Application**: Application layer.
- **BBT.MyProjectName.Domain**: Domain layer.
- **BBT.MyProjectName.Infrastructure**: Infrastructure configurations (Data, AutoMapper, Cache etc.).
- **BBT.MyProjectName.HttpApi.Host**: HTTP API Host.

### test
Contains the test projects.
- **BBT.MyProjectName.Application.Tests**: Tests for the Application layer.
- **BBT.MyProjectName.Domain.Tests**: Tests for the Domain layer.
- **BBT.MyProjectName.Infrastructure.Tests**: Tests for the Infrastructure layer.
- **BBT.MyProjectName.TestBase**: Base classes and utilities for tests.

### Other Files
- **.gitattributes**: Specifies Git attributes.
- **.gitignore**: Specifies files to be ignored by Git.
- **.prettierrc**: Prettier configuration file.
- **BBT.MyProjectName.sln**: Visual Studio solution file.
- **BBT.MyProjectName.sln.DotSettings**: Visual Studio settings file.
- **common.props**: Contains common project properties.
- **delete-bin-obj.ps1**: PowerShell script to clean `bin` and `obj` folders.
- **global.json**: Specifies the .NET SDK version.
- **NuGet.Config**: Contains NuGet sources and settings.
- **run.ps1**: Script to run the project.

---

## Required Tools and Setup Steps

### Prerequisites
- [.NET SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started)
- [Dapr CLI](https://docs.dapr.io/getting-started/install-dapr-cli/)

### Setup Steps
1. Clone the repository:
    ```sh
    git clone https://github.com/burgan-tech/aether-template.git
    cd MyProjectName
    ```

2. Install the required .NET SDK version:
    ```sh
    dotnet --version
    ```

3. Restore the dependencies:
    ```sh
    dotnet restore
    ```

4. Build the project:
    ```sh
    dotnet build
    ```

5. Run the project:
    ```sh
    ./run.ps1
    ```

## Building and Running the Project
To build and run the project, use the provided `run.ps1` script. This script will ensure that all necessary services are started and the application is running.

```sh
./run.ps1
```

#### Mac
To run the script on MacOS, you need to install PowerShell. You can find the official documentation for installing PowerShell on MacOS [here](https://docs.microsoft.com/en-us/powershell/scripting/install/installing-powershell-core-on-macos).

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

