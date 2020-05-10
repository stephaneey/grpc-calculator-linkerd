#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["calc.multiply/calc.multiply.csproj", "calc.multiply/"]
RUN dotnet restore "calc.multiply/calc.multiply.csproj"
COPY . .
WORKDIR "/src/calc.multiply"
RUN dotnet build "calc.multiply.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "calc.multiply.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "calc.multiply.dll"]