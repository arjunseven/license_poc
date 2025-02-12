# Use the official .NET 8 runtime image as a base
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Use the official .NET 8 SDK image for building
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Ai_Inside.csproj", "./"]
RUN dotnet restore "./Ai_Inside.csproj"

# Copy the entire project and publish it
COPY . .
WORKDIR "/src"
RUN dotnet publish "./Ai_Inside.csproj" -c Release -o /app/publish

# Build the runtime image
FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Ai_Inside.dll"]
