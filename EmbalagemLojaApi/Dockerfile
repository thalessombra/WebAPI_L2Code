# Usar imagem oficial do .NET 7 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copiar csproj e restaurar depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copiar todo o c�digo e buildar a aplica��o
COPY . ./
RUN dotnet publish -c Release -o out

# Imagem runtime para rodar a aplica��o
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Expor porta 80 para HTTP
EXPOSE 80

# Comando para rodar a aplica��o
ENTRYPOINT ["dotnet", "EmbalagemLojaApi.dll"]
