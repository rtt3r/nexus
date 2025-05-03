<!-- PROJECT LOGO -->
<br />
<p align="center">
  <img src="docs/images/logo.png" alt="Logo" width="80" height="80">
  <h3 align="center">API - Sistema Administrativo Empresarial</h3>
  <p align="center">
    Uma API moderna em .NET 9 para sistemas administrativos empresariais.
    <br />
    <a href="#getting-started"><strong>Guia de instalação »</strong></a>
    <br />
    <br />
    <!-- Links opcionais -->
    <a href="https://github.com/your_username/your_repo">Repositório</a>
    ·
    <a href="https://github.com/your_username/your_repo/issues">Reportar Bug</a>
    ·
    <a href="https://github.com/your_username/your_repo/issues">Sugerir Funcionalidade</a>
  </p>
</p>

## 📋 Sobre o Projeto

Esta API foi desenvolvida com o objetivo de servir como base para um sistema administrativo empresarial completo e modular. Utiliza ASP.NET Core (.NET 9) e segue boas práticas de desenvolvimento, visando escalabilidade, segurança e facilidade de manutenção.

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 9 (Web API)
- C#
- Entity Framework Core
- SQL Server / PostgreSQL
- Docker (opcional)
- Autenticação via Keycloak (SSO)

## ⚙️ Primeiros Passos

Estas instruções ajudarão você a configurar o ambiente de desenvolvimento local.

### ✅ Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/)
- [Visual Studio 2022+](https://visualstudio.microsoft.com/) ou [VS Code](https://code.visualstudio.com/)
- Git
- Powershell (para execução de script de certificados)
- (Opcional) Docker e Docker Compose

### 📦 Instalação

1. **Clone o repositório:**

   ```bash
   git clone https://github.com/your_username/your_repo.git
   cd your_repo
   ```

2. **Configure os arquivos de ambiente:**

   - Copie o arquivo `.env` de exemplo (ou utilize o que está incluído):

     ```bash
     cp .env.example .env
     ```

     Preencha os valores necessários (usuários, senhas, strings de conexão, etc).  
     Exemplo de conteúdo do `.env`:

     ```
     MSSQL_USER_ID=sa
     MSSQL_PASSWORD=SuaSenhaSegura123
     KC_ADMIN_PWD=admin
     ...
     ```

3. **Configure os feeds do NuGet:**

   - Se ainda não estiver configurado, adicione o arquivo `nuget.config` ao diretório raiz (o seu já está pronto):

     ```xml
     <?xml version="1.0" encoding="utf-8"?>
     <configuration>
       <packageSources>
         <add key="nuget.org" value="https://api.nuget.org/v3/index.json" />
         <!-- Outras fontes, se necessário -->
       </packageSources>
     </configuration>
     ```

4. **Configure os certificados de desenvolvimento HTTPS:**

   Execute o script abaixo no PowerShell (Windows):

   ```powershell
   ./scripts/dev-certs.ps1
   ```

   Isso criará e aplicará os certificados necessários para HTTPS local.

5. **Restaure os pacotes NuGet:**

   ```bash
   dotnet restore
   ```

6. **Execute a aplicação:**

   ```bash
   dotnet run --project src/SeuProjeto.Api
   ```

   A API estará disponível em: `https://localhost:5001`

## 📌 Uso

Você pode interagir com a API via Swagger ou Postman. O Swagger estará disponível em:

```
https://localhost:5001/swagger
```

## 🛣️ Roadmap

- [ ] Módulo de Autenticação e Autorização
- [ ] CRUD Genérico
- [ ] Módulo Financeiro
- [ ] Módulo de Estoque
- [ ] Documentação completa da API

## 🤝 Contribuindo

Sinta-se à vontade para contribuir! Toda ajuda é bem-vinda.

1. Fork este repositório
2. Crie uma branch (`git checkout -b feature/SuaFeature`)
3. Commit suas alterações (`git commit -m 'Adiciona nova feature'`)
4. Push na branch (`git push origin feature/SuaFeature`)
5. Abra um Pull Request

## 📝 Licença

Distribuído sob a Licença MIT. Veja `LICENSE` para mais informações.

## 📬 Contato

Seu Nome - [@seu_twitter](https://twitter.com/seu_usuario) - seuemail@exemplo.com  
Link do Projeto: [https://github.com/your_username/your_repo](https://github.com/your_username/your_repo)