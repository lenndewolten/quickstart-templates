# Quick start templates

This repository contains starter templates that can be initialized with the `dotnet new` template engine.

## Installation

Clone, fork or download this repository or the specific template. Navigate with your CLI to the specific template and run the template installation command:

```ps
dotnet new --install .
```

## Using a template

After installing the template, you can generate the template using the following command:

```ps
dotnet new <shortname> --name <projectname>
```

## Templates

- [.NET Core API for Kubernetes](.\templates\dotnet-api-for-k8s-template\README.md)
- [.NET Core Minimal API - .NET 7 Preview](.\templates\dotnet-minimal-api-template\README.md)

## Roadmap
- [ ] NuGet installation support
