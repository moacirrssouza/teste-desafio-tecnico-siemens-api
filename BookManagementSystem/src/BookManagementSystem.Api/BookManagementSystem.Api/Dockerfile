FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080


FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/BookManagementSystem.Api/BookManagementSystem.Api/BookManagementSystem.Api.csproj", "src/BookManagementSystem.Api/BookManagementSystem.Api/"]
COPY ["src/BookManagementSystem.Domain/BookManagementSystem.Domain/BookManagementSystem.Domain.csproj", "src/BookManagementSystem.Domain/BookManagementSystem.Domain/"]
COPY ["src/BookManagementSystem.Application/BookManagementSystem.Application/BookManagementSystem.Application.csproj", "src/BookManagementSystem.Application/BookManagementSystem.Application/"]
COPY ["src/BookManagementSystem.Infrastructure/BookManagementSystem.Infrastructure/BookManagementSystem.Infrastructure.csproj", "src/BookManagementSystem.Infrastructure/BookManagementSystem.Infrastructure/"]
RUN dotnet restore "./src/BookManagementSystem.Api/BookManagementSystem.Api/BookManagementSystem.Api.csproj"
COPY . .
WORKDIR "/src/src/BookManagementSystem.Api/BookManagementSystem.Api"
RUN dotnet build "./BookManagementSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./BookManagementSystem.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BookManagementSystem.Api.dll"]
