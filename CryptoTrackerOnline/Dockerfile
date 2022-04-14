#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["CryptoTrackerOnline/CryptoTrackerOnline.csproj", "CryptoTrackerOnline/"]
RUN dotnet restore "CryptoTrackerOnline/CryptoTrackerOnline.csproj"
COPY . .
WORKDIR "/src/CryptoTrackerOnline"
RUN dotnet build "CryptoTrackerOnline.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CryptoTrackerOnline.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CryptoTrackerOnline.dll"]