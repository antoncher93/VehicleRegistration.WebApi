﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["VehicleRegistration.WebApi/VehicleRegistration.WebApi.csproj", "VehicleRegistration.WebApi/"]
RUN dotnet restore "VehicleRegistration.WebApi/VehicleRegistration.WebApi.csproj"
COPY . .
WORKDIR "/src/VehicleRegistration.WebApi"
RUN dotnet build "VehicleRegistration.WebApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "VehicleRegistration.WebApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "VehicleRegistration.WebApi.dll"]
