FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

COPY *.csproj ./
RUN ["dotnet", "restore"]

COPY . ./
RUN ["dotnet", "build"]
EXPOSE 5000

HEALTHCHECK CMD curl --fail http://localhost:5000/health || exit

CMD [ "dotnet", "run" ]
