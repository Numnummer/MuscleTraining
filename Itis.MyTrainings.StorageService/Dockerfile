FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Itis.MyTrainings.StorageService.Web/Itis.MyTrainings.StorageService.Web.csproj", "./Itis.MyTrainings.StorageService.Web/"]
COPY ["./Itis.MyTrainings.StorageService.Core/Itis.MyTrainings.StorageService.Core.csproj", "./Itis.MyTrainings.StorageService.Core/"]

RUN dotnet restore "Itis.MyTrainings.StorageService.Web/Itis.MyTrainings.StorageService.Web.csproj"
COPY . .
WORKDIR "/src/Itis.MyTrainings.StorageService.Web"
RUN dotnet build "Itis.MyTrainings.StorageService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itis.MyTrainings.StorageService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Itis.MyTrainings.StorageService.Web.dll"]
