# Minimal Api - Preview

Dotnet Core 7 **(preview)** minimal api template. This template takes care of the repeatable boring stuff.
This template is ideal for microservices and apps that want to include the minimal amount of dependencies.

## Features
- Minimal Api
  - Using .Net 7 for the latests [minimal Api improvements](https://devblogs.microsoft.com/dotnet/asp-net-core-updates-in-dotnet-7-preview-4/).
- Health checks, including readyness and liveness probes.
- OpenApi, Swagger support
- Prometheus metrics
- Docker : small and more secure Alpine image.
  - Running as a non root user
  - Using Container Tools in Visual Studio for debugging the App in Docker
- Helm charts to deploy the app on kubernetes
  - Build in tight security context to run the app in read only, non root and own service account.
- Integration test project setup
- Unit test project setup

## How to use

Install the template. Reference the installation guide in the repository.
After installing generate the template:

```ps
dotnet new minimalapi --name <YourProjectName>
```

## Options

| Option                   |                                   Description                                   | Default |
| ------------------------ | :-----------------------------------------------------------------------------: | ------: |
| --disable-open-api       |                       Disable OpenAPI (Swagger) support.                        |   false |
| --disable-health-check   | Disable health checks endpoints and health probes in the kubernetes deployment. |   false |
| --use-prometheus         |   Use prometheus to collect app metrics and exposes the '/metrics' endpoint.    |   true  |
| --deploy-on-kubernetes   |   Adds Helm charts en deployment files for kubernetes                           |   false |
| --no-restore             |    Do not restore the solution as post action after generating the template     |   false |
