<!-- PROJECT LOGO -->
<br />
<p align="center">
  <img src="docs/images/logo.png" alt="Logo" width="80" height="80">
  <h3 align="center">Nexus - Business Administration System</h3>
  <p align="center">
    A modern API in .NET 9 for business administration systems.
    <br />
    <a href="#getting-started"><strong>Installation Guide »</strong></a>
    <br />
    <br />
    <a href="https://github.com/rtt3r/nexus">Repository</a>
    ·
    <a href="https://github.com/rtt3r/nexus/issues">Report Bug</a>
    ·
    <a href="https://github.com/rtt3r/nexus/issues">Request Feature</a>
  </p>
</p>

## 📋 About the Project

This API was developed as a base for a complete and modular business administration system. It uses ASP.NET Core (.NET 9) and follows development best practices, focusing on scalability, security, and maintainability.

## 🚀 Technologies Used

- ASP.NET Core 9 (Web API)
- C#
- Entity Framework Core
- SQL Server / PostgreSQL
- Docker
- Keycloak Authentication (SSO)

## ⚙️ Getting Started

These instructions will help you set up the development environment locally.

### ✅ Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) or [VS Code](https://code.visualstudio.com/)
- [Git](https://git-scm.com/downloads)
- Powershell (to run certificate scripts)
- (Optional) [Docker + Docker Compose](https://www.docker.com/products/docker-desktop/)

### 📦 Installation

1. **Clone the repository:**

   ```bash
   git clone https://github.com/rtt3r/nexus.git
   cd nexus
   ```

2. **Configure the environment files:**

   - Create a `.env` file in the project root:

     Fill in the required values (usernames, passwords, connection strings, etc).

     Example `.env` content:

     ```
     SEQ_PWD_HASH=YourPasswordHashHere
     POSTGRES_USER=postgress
     POSTGRES_PASSWORD=postgress
     MSSQL_USER_ID=sa
     MSSQL_PASSWORD=YourSuperSecurePasswordHere
     KC_ADMIN_PWD=YourSuperSecurePasswordHere
     KC_DB_USERNAME=postgress
     KC_DB_PASSWORD=postgress
     RMQ_USER=guest
     RMQ_PWD=guest
     ```

     See [https://blog.datalust.co/setting-an-initial-password-when-deploying-seq-to-docker](https://blog.datalust.co/setting-an-initial-password-when-deploying-seq-to-docker)

3. **Configure NuGet feeds:**

   - If not already configured, add the `nuget.config` file to the root directory:

   ```xml
   <?xml version="1.0" encoding="utf-8"?>
   <configuration>
     <packageSources>
       <add key="packages.ritter.co"
         value="https://pkgs.dev.azure.com/andersonritter/_packaging/packages.ritter.co/nuget/v3/index.json" />
     </packageSources>
     <packageSourceCredentials>
       <packages.ritter.co>
         <add key="username"
           value="YourPersonalAccessTokensHere" />
         <add key="cleartextpassword"
           value="YourPersonalAccessTokensHere" />
       </packages.ritter.co>
     </packageSourceCredentials>
   </configuration>
   ```

4. **Set up HTTPS development certificates:**

   Run the follow script in PowerShell (Windows):

   ```powershell
   mkdir $Env:USERPROFILE/.aspnet/https

   dotnet dev-certs https -ep $Env:USERPROFILE/.aspnet/https/Development.pfx -p c1bc6816-f70f-42e3-a71f-4ab75a294755
   dotnet dev-certs https --trust
   dotnet dev-certs https --check
   ```

   This will create and apply the necessary certificates for local HTTPS.

5. **Restore NuGet packages:**

   ```bash
   dotnet restore
   ```

6. **Run the application:**

   ```bash
   ./docker-up.sh vscode
   ```

   The API will be available at: `https://localhost:4432`

## 📌 Usage

You can interact with the API via OpenAPI or Postman. The OpenAPI documentation will be available at:

```
https://localhost:4432/api-docs
```

## 🛣️ Roadmap

- [ ] Authentication and Authorization Module
- [ ] Generic CRUD
- [ ] Finance Module
- [ ] Inventory Module
- [ ] Full API Documentation

## 🤝 Contributing

See [CONTRIBUTING](./CONTRIBUTING.md)

## 📝 License

Distributed under the MIT License. See `LICENSE` for more information.

## 📬 Contact

- Anderson Ritter de Souza - [@ritter.ander](https://www.instagram.com/ritter.ander) - anderdsouza@gmail.com
- Project Link: [https://github.com/rtt3r/nexus](https://github.com/rtt3r/nexus)
