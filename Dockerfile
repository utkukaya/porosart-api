# FROM mcr.microsoft.com/dotnet/core/sdk:6.0-bionic AS build
# WORKDIR /app

# copy csproj and restore as distinct layers
# COPY *.sln .
# COPY porosartapi/*.csproj ./porosartapi/
# RUN dotnet restore

# # copy everything else and build app
# COPY BennyAPI/. ./BennyAPI/
# WORKDIR /app/porosartapi
# RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-bionic AS runtime
# WORKDIR /app
# COPY --from=build /app/porosartapi/out .
# EXPOSE 80
# CMD ["dotnet", "porosartapi.dll"]


# FROM mcr.microsoft.com/dotnet/sdk:6.0-bionic AS build-env

# # Copy csproj and restore as distinct layers
# COPY ./porosartapi/porosartapi.csproj ./porosartapi/porosartapi.csproj
# COPY *.sln .
# RUN dotnet restore

# # Copy everything else and build
# COPY . ./
# RUN dotnet publish -c Release -o build –no-restore

# FROM mcr.microsoft.com/dotnet/aspnet:6.0
# WORKDIR /app
# # COPY –from=build-env ./build .
# COPY --from=build /app .
# ENV ASPNETCORE_URLS=http://*:8080
# EXPOSE 8080
# ENTRYPOINT [ "dotnet", "porosartapi.dll" ]

# FROM mcr.microsoft.com/dotnet/runtime-deps:6.0-alpine AS base
# WORKDIR /app
# EXPOSE 80
# EXPOSE 443

# RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
# USER appuser

# FROM mcr.microsoft.com/dotnet/sdk:6.0-alpine AS build
# WORKDIR /src
# COPY ["porosartapi.csproj", "./"]
# RUN dotnet restore "porosartapi.csproj"
# COPY . .
# WORKDIR "/src/"
# RUN dotnet build "porosartapi.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "porosartapi.csproj" -c Release -o /app/publish \
#    -r alpine-x64 \
#    --self-contained true \
#    -p:PublishTrimmed=true \
#    -p:PublishSingleFile=true

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["./porosartapi"]


# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 8080


# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["porosartapi.csproj", "./"]
# RUN dotnet restore "porosartapi.csproj"
# COPY . .
# WORKDIR "/src"
# RUN dotnet build "porosartapi.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "porosartapi.csproj" -c Release -o /app/publish


# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "porosartapi.dll"]


# FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
# WORKDIR /app
# EXPOSE 8080

# FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
# WORKDIR /src
# COPY ["porosartapi.csproj", "./"]
# RUN dotnet restore "porosartapi.csproj"
# COPY . .
# WORKDIR "/src/."
# RUN dotnet build "porosartapi.csproj" -c Release -o /app/build

# FROM build AS publish
# RUN dotnet publish "porosartapi.csproj" -c Release -o /app/publish

# FROM base AS final
# WORKDIR /app
# COPY --from=publish /app/publish .
# ENTRYPOINT ["dotnet", "porosartapi.dll"]

# gcr.io/cloudrun/placeholder@sha256:4a1c3386c30f2994092028075a21758e904ea48380b23fefe78327ff52268c0e
# crucial-oven-404213
# docker build -t gcr.io/crucial-oven-404213/porosartapi:v1 .

# docker push gcr.io/crucial-oven-404213/porosartapi:v1


# Cloud builder for the dotnet builder.

# steps:
# - name: 'gcr.io/cloud-builders/docker'
#   args:
#   - 'build'
#   - '--tag=gcr.io/porosart-api-c46jphp5cq-uc/dotnet'
#   - '.'

# # Build the test projects
# - name: 'gcr.io/porosart-api-c46jphp5cq-uc/dotnet'
#   args: ['build']
#   dir: 'porosartapi'

# - name: 'gcr.io/porosart-api-c46jphp5cq-uc/dotnet'
#   args: ['build']
#   dir: 'porosartapi'

# # Specify the --no-source flag to indicate that no source code should be uploaded
# - name: 'gcr.io/cloud-builders/gcloud'
#   args: ['builds', 'submit', '--config=cloudbuild.yaml', '--no-source']

# images:
# - 'gcr.io/porosart-api-c46jphp5cq-uc/dotnet'




FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80


FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["porosartapi.csproj", "./"]
RUN dotnet restore "porosartapi.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "porosartapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "porosartapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "porosartapi.dll"]