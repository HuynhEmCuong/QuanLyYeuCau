#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["Manager_Request/Manager_Request.csproj", "Manager_Request/"]
RUN dotnet restore "Manager_Request/Manager_Request.csproj"
COPY . .
WORKDIR "/src/Manager_Request"
RUN dotnet build "Manager_Request.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Manager_Request.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Manager_Request.dll"]