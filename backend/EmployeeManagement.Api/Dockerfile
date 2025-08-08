# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia apenas o arquivo de projeto
COPY *.csproj ./

# Restaura as dependências
RUN dotnet restore

# Copia o restante do código
COPY . ./

# Publica o projeto
RUN dotnet publish -c Release -o out

# Runtime final
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

ENTRYPOINT ["dotnet", "EmployeeManagement.Api.dll"]
