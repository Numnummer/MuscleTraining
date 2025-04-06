FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Itis.MyTrainings.PaymentService.Web/Itis.MyTrainings.PaymentService.Web.csproj", "Itis.MyTrainings.PaymentService.Web/"]
RUN dotnet restore "Itis.MyTrainings.PaymentService.Web/Itis.MyTrainings.PaymentService.Web.csproj"
COPY . .
WORKDIR "/src/Itis.MyTrainings.PaymentService.Web"
RUN dotnet build "Itis.MyTrainings.PaymentService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itis.MyTrainings.PaymentService.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Itis.MyTrainings.PaymentService.Web.dll"]
