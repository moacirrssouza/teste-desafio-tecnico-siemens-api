# Book Management System API

API REST para gerenciamento de livros, autores e gêneros.

## Descrição

Este projeto consiste em uma API RESTful construída com .NET para gerenciar um sistema de livros. Ele permite realizar operações de CRUD (Criar, Ler, Atualizar e Deletar) para as seguintes entidades:

*   **Livros:** Com informações como título, ISBN, data de publicação, etc.
*   **Autores:** Com informações sobre os autores dos livros.
*   **Gêneros:** Para categorizar os livros.

## Arquitetura

O projeto segue os princípios da Arquitetura Limpa, separando as responsabilidades em diferentes camadas:

*   **BookManagementSystem.Api:** Camada de apresentação, responsável por expor os endpoints da API.
*   **BookManagementSystem.Application:** Camada de aplicação, contendo a lógica de negócio e os casos de uso.
*   **BookManagementSystem.Domain:** Camada de domínio, com as entidades e regras de negócio principais.
*   **BookManagementSystem.Infrastructure:** Camada de infraestrutura, responsável pelo acesso a dados e outros detalhes de implementação.

## Tecnologias Utilizadas

*   **.NET**
*   **Entity Framework Core**
*   **PostgreSQL**
*   **Swagger** para documentação da API.
