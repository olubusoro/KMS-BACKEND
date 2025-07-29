FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 8081

# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["CsKmsBackend.Domain/CsKmsBackend.Domain.csproj", "CsKmsBackend.Domain/"]
COPY ["CsKmsBackend.Application/CsKmsBackend.Application.csproj", "CsKmsBackend.Application/"]
COPY ["CsKmsBackend.Infrastructure/CsKmsBackend.Infrastructure.csproj", "CsKmsBackend.Infrastructure/"]
COPY ["CsKmsBackend.Presentation/CsKmsBackend.Presentation.csproj", "CsKmsBackend.Presentation/"]
COPY .config/dotnet-tools.json .config/
RUN dotnet tool restore
RUN dotnet restore "./CsKmsBackend.Presentation/CsKmsBackend.Presentation.csproj"
RUN dotnet tool run dotnet-ef database update -p "./CsKmsBackend.Infrastructure/" -s "./CsKmsBackend.Presentation/" --verbose
COPY . .
WORKDIR "/src/CsKmsBackend.Presentation"
RUN dotnet build "./CsKmsBackend.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish Stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./CsKmsBackend.Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CsKmsBackend.Presentation.dll"]
