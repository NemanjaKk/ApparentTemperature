#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 7245

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["ApparentTemparatureCalculator/ApparentTemparatureCalculator.csproj", "ApparentTemparatureCalculator/"]
RUN dotnet restore "ApparentTemparatureCalculator/ApparentTemparatureCalculator.csproj"
COPY . .
WORKDIR "/src/ApparentTemparatureCalculator"
RUN dotnet build "ApparentTemparatureCalculator.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApparentTemparatureCalculator.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApparentTemparatureCalculator.dll"]