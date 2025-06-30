# Use the official .NET SDK image
FROM mcr.microsoft.com/devcontainers/dotnet:1-9.0-bookworm

# Set the working directory inside the container
WORKDIR /workspace

# Install additional tools if necessary
RUN apt-get update && apt-get install -y git && apt-get clean 
RUN dotnet workload update && dotnet tool restore
