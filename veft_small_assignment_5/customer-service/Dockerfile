FROM mcr.microsoft.com/dotnet/core/sdk:2.2

LABEL maintainer="Hópur 43"

ENV ASPNETCORE_URLS=http://+:80

ENV DBHOST customer-db
ENV DBNAME customer-db

COPY . /app

WORKDIR /app/customer-service

RUN dotnet restore

ENTRYPOINT [ "dotnet", "run" ]
