FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal AS base
WORKDIR /app
EXPOSE 80

ENV ASPNETCORE_URLS=http://+:80

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal AS build
WORKDIR /src
COPY ["Src/TFG.API/TFG.API.csproj", "Src/TFG.API/"]
RUN dotnet restore "Src/TFG.API/TFG.API.csproj"
COPY . .
WORKDIR "/src/Src/TFG.API"
RUN dotnet build "TFG.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TFG.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TFG.API.dll"]
