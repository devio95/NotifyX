FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER app
WORKDIR /app
ENV RabbitMQ_HostName='rabbitmq'
ENV NotifyX_ConnectionString='Server=database; Port=5432; Database=NotifyX; User ID=postgres; Password=admin'

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workers/NotificationDispatcher/NotificationDispatcher.csproj", "Workers/NotificationDispatcher/"]
COPY ["Application/Application.Interfaces/Application.Interfaces.csproj", "Application/Application.Interfaces/"]
COPY ["Domain/Domain.csproj", "Domain/"]
COPY ["Application/Application/Application.csproj", "Application/Application/"]
COPY ["Infrastructure/Database/Database.csproj", "Infrastructure/Database/"]
COPY ["Infrastructure/RabbitMq/RabbitMq.csproj", "Infrastructure/RabbitMq/"]
RUN dotnet restore "./Workers/NotificationDispatcher/NotificationDispatcher.csproj"
COPY . .
WORKDIR "/src/Workers/NotificationDispatcher"
RUN dotnet build "./NotificationDispatcher.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./NotificationDispatcher.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENV RabbitMQ_HostName="rabbitmq"
ENTRYPOINT ["dotnet", "NotificationDispatcher.dll"]