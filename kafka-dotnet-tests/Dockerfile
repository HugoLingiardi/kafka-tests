FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers
COPY /Kafka.Tests.HostedService/Kafka.Tests.HostedService.csproj Kafka.Tests.HostedService/
COPY /Kafka.Tests.Core/Kafka.Tests.Core.csproj Kafka.Tests.Core/
COPY /Kafka.Tests.Data/Kafka.Tests.Data.csproj Kafka.Tests.Data/
COPY /Kafka.Tests.Tests/Kafka.Tests.Tests.csproj Kafka.Tests.Tests/

RUN dotnet restore ./Kafka.Tests.Tests/Kafka.Tests.Tests.csproj
RUN dotnet restore ./Kafka.Tests.Data/Kafka.Tests.Data.csproj
RUN dotnet restore ./Kafka.Tests.Core/Kafka.Tests.Core.csproj
RUN dotnet restore ./Kafka.Tests.HostedService/Kafka.Tests.HostedService.csproj

# Copy everything else and build
COPY ./ ./

RUN dotnet test ./Kafka.Tests.Tests

# Run Publish
RUN dotnet publish ./Kafka.Tests.HostedService -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "Kafka.Tests.HostedService.dll"]