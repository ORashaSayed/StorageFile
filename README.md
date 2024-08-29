# File Storage API

This project is a .NET 8-based File Storage API that allows users to upload, download, delete, and retrieve metadata about files. It uses MediatR for handling commands, queries, and events, and integrates with Azure Blob Storage for file storage.

## Table of Contents

- [Features](#features)
- [Technologies](#technologies)
- [Getting Started](#getting-started)
  - [Prerequisites](#prerequisites)
  - [Installation](#installation)
  - [Running the Application](#running-the-application)
- [API Endpoints](#api-endpoints)
- [Folder Structure](#folder-structure)
- [Known Issues](#known-issues)
- [Contributing](#contributing)
- [License](#license)

## Features

- **File Upload**: Upload files to Azure Blob Storage.
- **File Download**: Download files stored in Azure Blob Storage.
- **File Deletion**: Delete files from Azure Blob Storage.
- **Metadata Retrieval**: Retrieve metadata such as file name, size, content type, and upload date.
- **Command and Query Separation**: Uses the CQRS pattern to separate write and read operations.
- **Event-Driven Architecture**: Publishes and handles domain events using MediatR.

## Technologies

- [.NET 8](https://dotnet.microsoft.com/)
- [MediatR](https://github.com/jbogard/MediatR)
- [Azure Blob Storage](https://azure.microsoft.com/en-us/services/storage/blobs/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [Swagger](https://swagger.io/)

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Azure Account](https://azure.microsoft.com/) (for Azure Blob Storage)
- SQL Server (or Azure SQL Database)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/your-username/file-storage-api.git
   cd file-storage-api
{
  "ConnectionStrings": {
    "DefaultConnection": "Your-SQL-Server-Connection-String"
  },
  "AzureBlobStorage": {
    "ConnectionString": "Your-Azure-Blob-Storage-Connection-String",
    "ContainerName": "your-container-name"
  }
}

