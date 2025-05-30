# Usar imagem oficial do .NET 7 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar csproj e restaurar dependências
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o código e buildar a aplicação
COPY . ./
RUN dotnet publish -c Release -o out

# Imagem runtime para rodar a aplicação
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expor porta 80 para HTTP
EXPOSE 80

# Comando para rodar a aplicação
ENTRYPOINT ["dotnet", "EmbalagemLojaApi.dll"]
