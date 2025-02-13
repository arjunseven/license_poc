# Use the .NET SDK image for building the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the project file and restore dependencies
COPY ["Ai_Inside/Ai_Inside.csproj", "Ai_Inside/"]
RUN dotnet restore "Ai_Inside/Ai_Inside.csproj"

# Copy everything else and build
COPY . .
WORKDIR /src/Ai_Inside

# 🔥 Add this line to clean before building
RUN rm -rf /src/Ai_Inside/bin /src/Ai_Inside/obj

RUN dotnet build "Ai_Inside.csproj" -c Release -o /app/build

# Publish the app
RUN dotnet publish "Ai_Inside.csproj" -c Release -o /app/publish

# Use the ASP.NET runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

ENTRYPOINT ["dotnet", "Ai_Inside.dll"]
