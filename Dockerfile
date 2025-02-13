# Use the official .NET SDK image as the build environment
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Ai_Inside/Ai_Inside.csproj", "Ai_Inside/"]
WORKDIR /src/Ai_Inside
RUN dotnet restore "Ai_Inside.csproj"

# Copy the entire project and build it
COPY . .
WORKDIR /src/Ai_Inside
RUN dotnet build "Ai_Inside.csproj" -c Release -o /app/build

# Publish the app
RUN dotnet publish "Ai_Inside.csproj" -c Release -o /app/publish

# Use the official .NET runtime as the runtime environment
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Start the application
ENTRYPOINT ["dotnet", "Ai_Inside.dll"]
