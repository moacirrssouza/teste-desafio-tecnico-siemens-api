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

## Autores (`/api/v1/Authors`)

*   `GET /api/v1/Authors/{id}`: Obtém um autor pelo seu ID.
*   `GET /api/v1/Authors`: Obtém todos os autores.
*   `POST /api/v1/Authors`: Cria um novo autor.
*   `PUT /api/v1/Authors/{id}`: Atualiza um autor existente.
*   `DELETE /api/v1/Authors/{id}`: Deleta um autor.

## Gêneros (`/api/v1/Genres`)

*   `GET /api/v1/Genres/{id}`: Obtém um gênero pelo seu ID.
*   `GET /api/v1/Genres`: Obtém todos os gêneros.
*   `POST /api/v1/Genres`: Cria um novo gênero.
*   `PUT /api/v1/Genres/{id}`: Atualiza um gênero existente.
*   `DELETE /api/v1/Genres/{id}`: Deleta um gênero.
