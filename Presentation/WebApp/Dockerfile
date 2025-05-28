FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY . .
ENV ASPNETCORE_ENVIRONMENT=Development

RUN dotnet restore "VibeNote.sln"
RUN dotnet publish "Presentation/WebApp/WebApp.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

EXPOSE 8510

ENTRYPOINT ["dotnet", "WebApp.dll"]