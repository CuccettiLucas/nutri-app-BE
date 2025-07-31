# Usa la imagen oficial de .NET SDK para compilar la aplicación.
# 'AS build' le da un nombre a esta etapa para que podamos referenciarla más tarde.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copia los archivos de proyecto (.csproj) y restaura las dependencias.
# Esto mejora la velocidad del build, ya que Docker puede cachear este paso.
COPY *.csproj ./
RUN dotnet restore

# Copia el resto de los archivos del proyecto y compila la aplicación.
COPY . ./
RUN dotnet publish -c Release -o out

# Usa una imagen oficial de .NET ASP.NET para el tiempo de ejecución.
# Esta imagen es más ligera y solo contiene lo necesario para ejecutar la app.
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
# Copia los archivos de salida compilados desde la etapa 'build' a la imagen final.
COPY --from=build /app/out ./

# Define el puerto en el que la aplicación escuchará.
EXPOSE 80

# Este es el comando que se ejecutará cuando se inicie el contenedor.
ENTRYPOINT ["dotnet", "App-Nutri.dll"]
