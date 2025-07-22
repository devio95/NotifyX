FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workers/NotificationExecutioner/NotificationExecutioner.csproj", "Workers/NotificationExecutioner/"]
COPY ["Application/Application.Interfaces/Application.Interfaces.csproj", "Application/Application.Interfaces/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application/Application.csproj", "Application/Application/"]
COPY ["Infrastructure/Database/Database.csproj", "Infrastructure/Database/"]
COPY ["Infrastructure/RabbitMq/RabbitMq.csproj", "Infrastructure/RabbitMq/"]
RUN dotnet restore "./Workers/NotificationExecutioner/NotificationExecutioner.csproj"
COPY . .
WORKDIR "/src/Workers/NotificationExecutioner"
RUN dotnet build "./NotificationExecutioner.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotificationExecutioner.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NotificationExecutioner.dll"]