# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Restaurar dependencias con el archivo de proyecto
COPY src/MathHammer.Api/MathHammer.Api.csproj .
RUN dotnet restore MathHammer.Api.csproj

# Copiar el código fuente y publicar
COPY src/MathHammer.Api/ .
RUN dotnet publish MathHammer.Api.csproj --configuration Release --output /app/publish --no-restore

# Etapa final
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "MathHammer.Api.dll"]