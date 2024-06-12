FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["ServerManagerApi.csproj", "ServerManagerApi/"]

RUN dotnet restore "./ServerManagerApi/ServerManagerApi.csproj"
WORKDIR "/src/ServerManagerApi"
COPY . .

RUN dotnet build "./ServerManagerApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./ServerManagerApi.csproj" -c Release -o /app/publish

FROM publish AS migration
WORKDIR /app/publish
RUN dotnet ef database update

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServerManagerApi.dll"]
