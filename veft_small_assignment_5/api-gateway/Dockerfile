FROM mcr.microsoft.com/dotnet/core/sdk:2.2

LABEL maintainer="Hópur 43"

ENV ASPNETCORE_URLS=http://+:5000

COPY . /app

WORKDIR /app

RUN dotnet restore

ENTRYPOINT [ "dotnet", "run" ]
