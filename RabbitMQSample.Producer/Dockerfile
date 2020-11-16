#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:5.0-buster-slim AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["RabbitMQSample.Producer/RabbitMQSample.Producer.csproj", "RabbitMQSample.Producer/"]
RUN dotnet restore "RabbitMQSample.Producer/RabbitMQSample.Producer.csproj"
COPY . .
WORKDIR "/src/RabbitMQSample.Producer"
RUN dotnet build "RabbitMQSample.Producer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "RabbitMQSample.Producer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RabbitMQSample.Producer.dll"]