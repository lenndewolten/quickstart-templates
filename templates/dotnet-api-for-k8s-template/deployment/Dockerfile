FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine as build
WORKDIR /app

#COPY . /src

COPY ./src /src
COPY ./Directory.Build.props /src

WORKDIR /src
RUN dotnet restore ./Kubernetes.Api.Template/Kubernetes.Api.Template.csproj --runtime alpine-x64
RUN dotnet publish ./Kubernetes.Api.Template/Kubernetes.Api.Template.csproj -c Release -o /app/publish \
  --no-restore \
  --runtime alpine-x64 \
  --self-contained true \
  /p:PublishSingleFile=true 

FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS runtime
RUN adduser --disabled-password \
  --home /app \
  --gecos '' dotnetuser && chown -R dotnetuser /app

# upgrade musl to remove potential vulnerability
RUN apk upgrade musl
USER dotnetuser

WORKDIR /app
COPY --from=build /app/publish .

# needed when running from a read-only filesystem
ENV DOTNET_CLI_TELEMETRY_OPTOUT=1 \
  COMPlus_EnableDiagnostics=0 

ENV DOTNET_RUNNING_IN_CONTAINER=true \
  ASPNETCORE_URLS=http://*:5000
EXPOSE 5000
ENTRYPOINT ["./Kubernetes.Api.Template"]