FROM microsoft/dotnet:2.1-runtime AS base
WORKDIR /app

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY src/Microsoft.Azure.IIoT.OpcUa.Servers/cli/Microsoft.Azure.IIoT.OpcUa.Servers.Cli.csproj src/Microsoft.Azure.IIoT.OpcUa.Servers/cli/
COPY src/Microsoft.Azure.IIoT.OpcUa.Servers/src/Microsoft.Azure.IIoT.OpcUa.Servers.csproj src/Microsoft.Azure.IIoT.OpcUa.Servers/src/
COPY src/Microsoft.Azure.IIoT.OpcUa/src/Microsoft.Azure.IIoT.OpcUa.csproj src/Microsoft.Azure.IIoT.OpcUa/src/
COPY NuGet.Config NuGet.Config
RUN dotnet restore --configfile NuGet.Config -nowarn:msb3202,nu1503 src/Microsoft.Azure.IIoT.OpcUa.Servers/cli/Microsoft.Azure.IIoT.OpcUa.Servers.Cli.csproj
COPY . .
WORKDIR /src/src/Microsoft.Azure.IIoT.OpcUa.Servers/cli
RUN dotnet build Microsoft.Azure.IIoT.OpcUa.Servers.Cli.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish Microsoft.Azure.IIoT.OpcUa.Servers.Cli.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Microsoft.Azure.IIoT.OpcUa.Servers.Cli.dll"]
