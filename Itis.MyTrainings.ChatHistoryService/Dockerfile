FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./Itis.MyTrainings.ChatHistoryService.Web/Itis.MyTrainings.ChatHistoryService.Web.csproj", "./Itis.MyTrainings.ChatHistoryService.Web/"]
COPY ["./Itis.MyTrainings.ChatHistoryService.PostgreSql/Itis.MyTrainings.ChatHistoryService.PostgreSql.csproj", "./Itis.MyTrainings.ChatHistoryService.PostgreSql/"]
COPY ["./Itis.MyTrainings.ChatHistoryService.Core/Itis.MyTrainings.ChatHistoryService.Core.csproj", "./Itis.MyTrainings.ChatHistoryService.Core/"]

RUN dotnet restore "Itis.MyTrainings.ChatHistoryService.Web/Itis.MyTrainings.ChatHistoryService.Web.csproj"
COPY . .
WORKDIR "/src/Itis.MyTrainings.ChatHistoryService.Web"
RUN dotnet build "Itis.MyTrainings.ChatHistoryService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itis.MyTrainings.ChatHistoryService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Itis.MyTrainings.ChatHistoryService.Web.dll"]
