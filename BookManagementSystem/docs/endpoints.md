# Endpoints da API

A API fornece endpoints para gerenciar livros, autores e gêneros.

## Livros (`/api/v1/Books`)

*   `GET /api/v1/Books/{id}`: Obtém um livro pelo seu ID.
*   `GET /api/v1/Books`: Obtém todos os livros.
*   `GET /api/v1/Books/author/{authorId}`: Obtém todos os livros de um autor específico.
*   `GET /api/v1/Books/genre/{genreId}`: Obtém todos os livros de um gênero específico.
*   `POST /api/v1/Books`: Cria um novo livro.
*   `PUT /api/v1/Books/{id}`: Atualiza um livro existente.
*   `DELETE /api/v1/Books/{id}`: Deleta um livro.