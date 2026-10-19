FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/publish .

# Preview: SQLite (xogtu si toos ah ayay u abuurmaa marka app-ku furo).
# Production SQL Server: u gudub DB_PROVIDER=sqlserver + ConnectionStrings__DefaultConnection env.
ENV ASPNETCORE_HTTP_PORTS=8080 \
    DB_PROVIDER=sqlite \
    SQLITE_PATH=/app/bootnews.db

EXPOSE 8080
ENTRYPOINT ["dotnet", "BootNews.dll"]
