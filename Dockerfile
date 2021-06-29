FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-env
WORKDIR /app

COPY . ./

COPY *.sln ./
RUN dotnet restore "./Api_Example.sln"

COPY . ./
RUN dotnet publish -c release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
EXPOSE 80
COPY --from=build-env /app/out .
ENTRYPOINT [ "dotnet", "Dev.Api.dll" ]



