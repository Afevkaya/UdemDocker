FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app
COPY ./bin/Release/net9.0/publish/ /app/
ENTRYPOINT ["dotnet", "UdemyDockerConsoleExamples.dll"]