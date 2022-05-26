#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["BackgroundService/EmergingBooking.Message.Consumer/EmergingBooking.Message.Consumer.csproj", "BackgroundService/EmergingBooking.Message.Consumer/"]
RUN dotnet restore "BackgroundService/EmergingBooking.Message.Consumer/EmergingBooking.Message.Consumer.csproj"
COPY . .
WORKDIR "/src/BackgroundService/EmergingBooking.Message.Consumer"
RUN dotnet build "EmergingBooking.Message.Consumer.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "EmergingBooking.Message.Consumer.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "EmergingBooking.Message.Consumer.dll"]