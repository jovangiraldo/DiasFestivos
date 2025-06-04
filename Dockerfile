# Etapa 1: Imagen base para tiempo de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Configurar la URL en la que la app escuchará
ENV ASPNETCORE_URLS=http://+:5235

# Exponer el puerto (debe coincidir con ASPNETCORE_URLS)
EXPOSE 5235

# Etapa 2: Imagen para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar todos los archivos del proyecto
COPY . .

# Restaurar paquetes NuGet
RUN dotnet restore

# Publicar el proyecto en modo Release
RUN dotnet publish -c Release -o /app

# Etapa 3: Imagen final, solo con lo necesario para ejecutar
FROM base AS final
WORKDIR /app

# Copiar los archivos publicados desde la etapa build
COPY --from=build /app .

# Definir el comando de inicio de la aplicación
ENTRYPOINT ["dotnet", "apiFestivos.Presentacion.dll"]
