# Dotnet Core Api for kubernetes

Dotnet Core 6 api template with the focus on Kubernetes. This template takes care of the repeatable boring stuff.
Besides the Helm charts, this template is also a good starting point for other deployments.

## Features

- Health checks, including readyness and liveness probes.
- OpenApi, Swagger support
- Api Versioning
- Prometheus metrics
- Docker : small and more secure Alpine image.
  - Running as a non root user
  - Using Container Tools in Visual Studio for debugging the App in Docker
- Helm charts to deploy the app on kubernetes
  - Build in tight security context to run the app in read only, non root and own service account.
- Integration test project setup

## How to use

Install the template. Reference the installation guide in the repository.
After installing generate the template:

```ps
dotnet new k8sapi --name <YourProjectName>
```

## Options

| Option                   |                                   Description                                   | Default |
| ------------------------ | :-----------------------------------------------------------------------------: | ------: |
| --disable-open-api       |                       Disable OpenAPI (Swagger) support.                        |   false |
| --disable-health-check   | Disable health checks endpoints and health probes in the kubernetes deployment. |   false |
| --use-prometheus         |   Use prometheus to collect app metrics and exposes the '/metrics' endpoint.    |   false |
| --disable-api-versioning |                          Disable api version support.                           |   false |
| --no-restore             |    Do not restore the solution as post action after generating the template     |   false |