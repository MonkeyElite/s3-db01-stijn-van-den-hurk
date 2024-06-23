FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

COPY ["./ServerManagerApi/ServerManagerApi.csproj", "ServerManagerApi/"]
COPY ["./ServerManagerCore/ServerManagerCore.csproj", "ServerManagerApi/"]
COPY ["./ServerManagerDAL/ServerManagerDAL.csproj", "ServerManagerApi/"]

RUN dotnet restore "./ServerManagerApi/ServerManagerApi.csproj"
COPY . .

RUN dotnet build "./ServerManagerApi/ServerManagerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./ServerManagerApi/ServerManagerApi.csproj" -c Release -o /app/publish

FROM publish AS migration
WORKDIR /app/publish
RUN dotnet ef database update

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerManagerApi.dll"]
