FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Itis.MyTrainings.Api.Web/Itis.MyTrainings.Api.Web.csproj", "./Itis.MyTrainings.Api.Web/"]
COPY ["./Itis.MyTrainings.Api.PostgreSql/Itis.MyTrainings.Api.PostgreSql.csproj", "./Itis.MyTrainings.Api.PostgreSql/"]
COPY ["./Itis.MyTrainings.Api.Core/Itis.MyTrainings.Api.Core.csproj", "./Itis.MyTrainings.Api.Core/"]
COPY ["./Itis.MyTrainings.Api.Contracts/Itis.MyTrainings.Api.Contracts.csproj", "./Itis.MyTrainings.Api.Contracts/"]
RUN dotnet restore "Itis.MyTrainings.Api.Web/Itis.MyTrainings.Api.Web.csproj"
COPY . .
WORKDIR "/src/Itis.MyTrainings.Api.Web"
RUN dotnet build "Itis.MyTrainings.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itis.MyTrainings.Api.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Itis.MyTrainings.Api.Web.dll"]
