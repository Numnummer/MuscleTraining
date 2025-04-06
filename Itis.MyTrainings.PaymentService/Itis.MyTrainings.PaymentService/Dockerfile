FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Itis.MyTrainings.PaymentService/Itis.MyTrainings.PaymentService.csproj", "Itis.MyTrainings.PaymentService/"]
RUN dotnet restore "Itis.MyTrainings.PaymentService/Itis.MyTrainings.PaymentService.csproj"
COPY . .
WORKDIR "/src/Itis.MyTrainings.PaymentService"
RUN dotnet build "Itis.MyTrainings.PaymentService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Itis.MyTrainings.PaymentService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Itis.MyTrainings.PaymentService.dll"]
