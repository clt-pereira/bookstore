# Projeto BookStore - Sistema de Gerenciamento de Livros

## Descrição do Projeto

O projeto **BookStore** é uma aplicação completa para o gerenciamento de livros, incluindo cadastro, atualização e exclusão de registros, com relacionamento entre autores e assuntos. O sistema é composto por uma API desenvolvida em .NET Core 8 para o back-end e uma aplicação Angular para o front-end.

## Objetivo

O objetivo principal do projeto é criar uma solução de gerenciamento de livros que permita operações como:

- Cadastro de livros, autores e assuntos;
- Atualização e exclusão de registros;
- Associação de livros a autores e assuntos;
- Visualização de livros cadastrados com seus relacionamentos.

A aplicação busca demonstrar boas práticas de desenvolvimento, incluindo:

- Arquitetura limpa;
- Clean Code
- Design orientado a domínios;
- Utilização de padrões de projeto como Repository e Service;
- Aplicação de validações e notificações centralizadas;
- Persistência de dados usando Entity Framework Core.
- Mapeamento de Banco de Dados com FluentAPI
- Modelagem baseada em Data Driven

## Tecnologias Utilizadas

### Back-end:
- **.NET Core 8** - Framework principal para desenvolvimento da API.
- **Entity Framework Core** - ORM para persistência de dados.
- **SQLite** - Banco de dados utilizado.
- **FluentValidation** - Biblioteca para validação de modelos.
- **AutoMapper** - Mapeamento entre ViewModels e Entidades.
- **Swagger** - Documentação interativa da API.

### Front-end:
- **Angular 18.2.13** - Framework principal para desenvolvimento do front-end.
- **TypeScript** - Linguagem principal para desenvolvimento Angular.
- **Bootstrap e NgBootstrap** - Para interface responsiva.

## Estrutura do Projeto

A aplicação é dividida em dois projetos principais:

1. **BookStore.Api:** Projeto de back-end responsável pela lógica de negócios, persistência e exposição de endpoints.
2. **BookStore.Front:** Aplicação Angular para interação com os usuários.

## Como Contribuir

Se você deseja contribuir para o projeto:

1. Faça um fork do repositório.
2. Crie uma nova branch:
   ```bash
   git checkout -b minha-feature
   ```
3. Commit suas mudanças:
   ```bash
   git commit -m "Minha nova feature"
   ```
4. Envie para o repositório:
   ```bash
   git push origin minha-feature
   ```
5. Abra um Pull Request.

## Licença

Este projeto é distribuído sob a licença MIT. Consulte o arquivo `LICENSE` para mais detalhes.

## Contato

**Cleiton Pereira** - [GitHub](https://github.com/clt-pereira) - clt-pereira@hotmail.com.com

