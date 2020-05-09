#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["calc.substract/calc.substract.csproj", "calc.substract/"]
RUN dotnet restore "calc.substract/calc.substract.csproj"
COPY . .
WORKDIR "/src/calc.substract"
RUN dotnet build "calc.substract.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "calc.substract.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "calc.substract.dll"]