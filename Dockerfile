# Use official .NET SDK image for build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj and restore dependencies
COPY *.sln .
COPY CleanShop.API/*.csproj ./CleanShop.API/
COPY CleanShop.Application/*.csproj ./CleanShop.Application/
COPY CleanShop.Domain/*.csproj ./CleanShop.Domain/
COPY CleanShop.Infrastructure/*.csproj ./CleanShop.Infrastructure/

RUN dotnet restore

# Copy everything else and build
COPY . .
WORKDIR /app/CleanShop.API
RUN dotnet publish -c Release -o /app/publish

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 7194
EXPOSE 5031
ENTRYPOINT ["dotnet", "CleanShop.API.dll"]
