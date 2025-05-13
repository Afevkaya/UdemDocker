FROM mcr.microsoft.com/dotnet/runtime:9.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["UdemyDockerConsoleExamples/UdemyDockerConsoleExamples.csproj", "UdemyDockerConsoleExamples/"]
RUN dotnet restore "UdemyDockerConsoleExamples/UdemyDockerConsoleExamples.csproj"
COPY . .
WORKDIR "/src/UdemyDockerConsoleExamples"
RUN dotnet build "./UdemyDockerConsoleExamples.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./UdemyDockerConsoleExamples.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UdemyDockerConsoleExamples.dll"]
