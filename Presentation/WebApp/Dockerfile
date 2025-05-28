FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .

RUN dotnet restore "VibeNote.sln"
RUN dotnet publish "Presentation/WebApp/WebApp.csproj" -c Release -o /app/publish

# Финальный образ
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY .env /app/.env
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "WebApp.dll"]