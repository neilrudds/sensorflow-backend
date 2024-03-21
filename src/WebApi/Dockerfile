#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app

EXPOSE 4000
EXPOSE 4001

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/WebApi/SensorFlow.WebApi.csproj", "src/WebApi/"]
COPY ["src/Application/SensorFlow.Application.csproj", "src/Application/"]
COPY ["src/Domain/SensorFlow.Domain.csproj", "src/Domain/"]
COPY ["src/Infrastructure/SensorFlow.Infrastructure.csproj", "src/Infrastructure/"]
RUN dotnet restore "src/WebApi/SensorFlow.WebApi.csproj"
COPY . .
WORKDIR "/src/src/WebApi"
RUN dotnet build "SensorFlow.WebApi.csproj" -c Release -o /app/build

# run the unit tests
FROM build AS test
# set the directory to be within the unit test project
WORKDIR /tests/SensorFlow.Domain.Tests
# run the unit tests
RUN dotnet test --logger:trx

FROM build AS publish
RUN dotnet publish "SensorFlow.WebApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
ENV ASPNETCORE_URLS=https://*:4001;http://*:4000
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SensorFlow.WebApi.dll"]
