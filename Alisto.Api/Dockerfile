# Stage 1: Runtime environment (minimal image for running the app)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 5070

# Stage 2: Build environment (includes SDK and build tools)
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src
COPY ["Alisto.Api.csproj", "./"]
RUN dotnet restore "Alisto.Api.csproj"
COPY . .
RUN dotnet build "Alisto.Api.csproj" -c Release -o /app/build

# Stage 3: Publishing (creates optimized, self-contained application)
FROM build AS publish
WORKDIR /src
RUN dotnet publish "Alisto.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 4: Final runtime image (minimal image with only the published app)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

# Create uploads directory and set permissions
RUN mkdir -p /app/uploads && chmod 755 /app/uploads

ENTRYPOINT ["dotnet", "Alisto.Api.dll"] 