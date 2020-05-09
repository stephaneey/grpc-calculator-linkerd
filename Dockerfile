#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["calc.add/calc.add.csproj", "calc.add/"]
RUN dotnet restore "calc.add/calc.add.csproj"
COPY . .
WORKDIR "/src/calc.add"
RUN dotnet build "calc.add.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "calc.add.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "calc.add.dll"]