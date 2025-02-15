# Use the .NET 8 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Set the working directory
WORKDIR /src

# Copy the project file and restore the dependencies
COPY Event-Management-System-Backend.csproj ./
RUN dotnet restore "Event-Management-System-Backend.csproj"

# Copy the rest of the files and build the app
COPY . .
WORKDIR "/src"
RUN dotnet build "Event-Management-System-Backend.csproj" -c Release -o /app/build

# Publish the app to a folder
RUN dotnet publish "Event-Management-System-Backend.csproj" -c Release -o /app/publish

# Use the NGINX image for the reverse proxy
FROM nginx:alpine AS nginx

# Copy the default nginx configuration file
COPY ./nginx.conf /etc/nginx/nginx.conf

# Expose ports for both NGINX and the .NET app
EXPOSE 80 443

# Use .NET runtime image to run the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS final

WORKDIR /app

# Copy the published app from the build stage
COPY --from=build /app/publish .

# Expose the port the app will run on
EXPOSE 5000

# Start the .NET app
ENTRYPOINT ["dotnet", "Event-Management-System-Backend.dll"]
