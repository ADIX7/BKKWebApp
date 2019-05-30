FROM mcr.microsoft.com/dotnet/core/sdk:2.2 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY ./BKKWebApp/*.csproj ./BKKWebApp/
COPY ./BKKWebApp.Data/*.csproj ./BKKWebApp.Data/
RUN dotnet restore ./BKKWebApp/BKKWebApp.csproj

# Copy everything else and build
COPY . ./
RUN dotnet publish -c Release -o ../out BKKWebApp/BKKWebApp.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:2.2
WORKDIR /app
COPY ./BKKWebApp/app.db .
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "BKKWebApp.dll"]