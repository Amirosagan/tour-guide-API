﻿FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION
WORKDIR /src
COPY ["src/External/Presentation/Presentation.csproj", "src/External/Presentation/"]
RUN dotnet restore "src/External/Presentation/Presentation.csproj"
COPY . .
WORKDIR "/src/src/External/Presentation"
RUN dotnet build "Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "Presentation.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Presentation.dll"]
