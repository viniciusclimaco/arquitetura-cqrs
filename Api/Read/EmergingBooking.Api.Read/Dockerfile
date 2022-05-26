#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Api/Read/EmergingBooking.Api.Read/EmergingBooking.Api.Read.csproj", "Api/Read/EmergingBooking.Api.Read/"]
RUN dotnet restore "Api/Read/EmergingBooking.Api.Read/EmergingBooking.Api.Read.csproj"
COPY . .
WORKDIR "/src/Api/Read/EmergingBooking.Api.Read"
RUN dotnet build "EmergingBooking.Api.Read.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmergingBooking.Api.Read.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmergingBooking.Api.Read.dll"]