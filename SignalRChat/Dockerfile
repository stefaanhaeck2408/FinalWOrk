FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 44330

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["SignalRChat.csproj", "./"]
RUN dotnet restore "SignalRChat.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "SignalRChat.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SignalRChat.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SignalRChat.dll"]
