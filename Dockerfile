# Imagen base con ASP.NET runtime para .NET 8
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

# Imagen para compilar con .NET 8 SDK
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY . .
WORKDIR "/src/App-Nutri"
RUN dotnet publish "App-Nutri.csproj" -c Release -o /app/out

# Imagen final
FROM base AS final
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT ["dotnet", "App-Nutri.dll"]