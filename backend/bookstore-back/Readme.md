# BookStore WebAPI - .NET Core 8

Este projeto é uma API desenvolvida em .NET Core 8 para gerenciamento de livros, incluindo cadastro, atualização e exclusão, com relacionamento entre autores e assuntos.

## Requisitos

- **SDK .NET Core 8**
- **Banco de SQLite** (ou outro compatível com Entity Framework Core)
- **IDE Recomendado**: Visual Studio 2022 / Visual Studio Code

## Configuração Inicial

### 1. Clonar o Repositório
```bash
   git clone https://github.com/clt-pereira/bookstore.git
```

### 2. Configurar o Banco de Dados

1. Atualize a string de conexão em `appsettings.json`:
   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Data Source=BookStoreDatabase.db"
   }
   ```

2. Execute as migrações do Entity Framework Core:
```bash
 dotnet ef database update
```

### 3. Executar o Projeto
```bash
 dotnet run
```

## Endpoints Principais

### **Livros**
- **GET** /api/livro - Lista todos os livros
- **GET** /api/livro/{id} - Obtém um livro pelo ID
- **POST** /api/livro - Cadastra um novo livro
- **PUT** /api/livro/{id} - Atualiza um livro existente
- **DELETE** /api/livro/{id} - Remove um livro

### **Assuntos**
- **GET** /api/assunto - Lista todos os assuntos
- **GET** /api/assunto/{id} - Obtém um assunto pelo ID
- **POST** /api/assunto - Cadastra um novo assunto
- **PUT** /api/assunto/{id} - Atualiza um assunto existente
- **DELETE** /api/assunto/{id} - Remove um assunto

### **Autores**
- **GET** /api/autor - Lista todos os autores
- **GET** /api/autor/{id} - Obtém um autor pelo ID
- **POST** /api/autor - Cadastra um novo autor
- **PUT** /api/autor/{id} - Atualiza um autor existente
- **DELETE** /api/autor/{id} - Remove um autor

## Tecnologias Utilizadas

- **.NET Core 8** - Framework principal
- **Entity Framework Core** - ORM para banco de dados
- **SQLite** - Banco de Dados
- **AutoMapper** - Mapeamento de objetos
- **FluentValidation** - Validação de dados
- **Swagger** - Documentação de API

## Estrutura do Projeto

```bash

src
 ├── BookStore.Api            # Camada de API
 │    ├── Configurations      # Configurações gerais
 │    ├── Controllers         # Controladores de API
 │    └── ViewModels          # Modelos de visão
 ├── BookStore.Business       # Regras de Negócio
 │    ├── Interfaces          # Interfaces de Serviços e Repositórios
 │    ├── Models              # Modelos de Domínio
 │    ├── Notificacoes        # Sistema de Notificações
 │    └── Services            # Serviços
 ├── BookStore.Data           # Acesso a Dados
 │    ├── Context             # Contexto do EF Core
 │    ├── Mappings            # Mapeamentos do Banco
 │    ├── Migrations          # Migrações
 │    └── Repository          # Repositórios

```

## Testes

1. Execute os testes unitários usando:
```bash
 dotnet test
```

## Contribuindo

1. Faça um fork do projeto.
2. Crie uma nova branch: `git checkout -b minha-feature`.
3. Commit suas mudanças: `git commit -m 'Adiciona nova feature'`.
4. Envie para o repositório: `git push origin minha-feature`.
5. Crie um Pull Request.

## Licença

Distribuído sob a licença MIT. Veja `LICENSE` para mais informações.

## Contato

**Desenvolvido com ❤️ por [Cleiton Pereira]**
