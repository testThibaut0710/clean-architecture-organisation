Utilisez l'image officielle .NET 6 SDK pour construire l'application
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app

Copiez le fichier projet et restaurez les dépendances
COPY *.csproj ./
RUN dotnet restore

Copiez le reste des fichiers et construisez l'application
COPY . ./
RUN dotnet publish -c Release -o out

Utilisez l'image officielle .NET 6 runtime pour exécuter l'application
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

Expose le port sur lequel l'application s'exécute
EXPOSE 5234

Commande pour exécuter l'application
ENTRYPOINT ["dotnet", "HotelInformationAPI.dll"]
