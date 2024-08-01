# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Install missing libraries
USER root
RUN apt-get update && \
    apt-get install -y libgssapi-krb5-2

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy the csproj file and restore dependencies
COPY ["DataAcquisitionService.csproj", "DataAcquisitionService/"]
RUN dotnet restore "DataAcquisitionService/DataAcquisitionService.csproj"

# Copy the rest of the application code
COPY . /src/DataAcquisitionService/

# Set the working directory to the project folder
WORKDIR "/src/DataAcquisitionService"

# Build the project
RUN dotnet build "DataAcquisitionService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "DataAcquisitionService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DataAcquisitionService.dll"]
