# Arquitetura do Projeto

O projeto adota a **Arquitetura Limpa** (Clean Architecture), que promove a separação de interesses e a independência de frameworks, UI e banco de dados. A estrutura é dividida nas seguintes camadas:

## 1. Domain

Esta é a camada mais interna e o coração da aplicação. Ela contém as entidades de negócio e as regras de negócio mais críticas.

*   **Entidades:** Classes que representam os objetos de negócio (ex: `Book`, `Author`, `Genre`).
*   **Regras de Negócio:** Lógica que é independente de qualquer outra camada.

## 2. Application

A camada de aplicação orquestra o fluxo de dados e executa os casos de uso do sistema. Ela depende da camada de Domínio, mas não de camadas mais externas.

*   **Services:** Contêm a lógica de negócio da aplicação (casos de uso).
*   **DTOs (Data Transfer Objects):** Objetos para transferir dados entre as camadas.
*   **Interfaces de Repositório:** Contratos para a camada de infraestrutura implementar.

## 3. Infrastructure

A camada de infraestrutura é responsável por detalhes de implementação, como acesso a banco de dados, comunicação com serviços externos, etc.

*   **Data:** Contém o `DbContext` do Entity Framework e as configurações do banco de dados.
*   **Repositories:** Implementações das interfaces de repositório definidas na camada de Aplicação.
*   **Migrations:** Scripts para gerenciar as alterações no esquema do banco de dados.

## 4. Api

A camada mais externa, responsável pela interface com o usuário (neste caso, uma API REST).

*   **Controllers:** Expor os endpoints da API.
*   **Program.cs:** Configuração da aplicação, injeção de dependência, etc.
