#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Copy Projects
COPY ["Short/Short.csproj", "Short/"]
COPY ["CQRS/CQRS.csproj", "CQRS/"]

# Restore Short
RUN dotnet restore "Short/Short.csproj"
COPY . .
WORKDIR "/src/Short"

# Build Short
RUN dotnet build "Short.csproj" -c Release -o /app/build

# Publish Short
FROM build AS publish
RUN dotnet publish "Short.csproj" -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:6.0-alpine3.17 AS base
WORKDIR /app

# Define entrypoint
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Short.dll"]
