FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
LABEL maintainer="kabanovrr"
WORKDIR /src
COPY . .
RUN dotnet restore ./DigitalTrade.Catalog.Host/DigitalTrade.Catalog.Host.csproj && \
    dotnet publish ./DigitalTrade.Catalog.Host/DigitalTrade.Catalog.Host.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /src
COPY --from=build-env /src/out .
ENTRYPOINT ["dotnet", "DigitalTrade.Catalog.Host.dll"]