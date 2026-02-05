# ---------- STAGE 1: Build ----------
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

COPY DemoApi.sln .
COPY DemoApi/DemoApi.csproj DemoApi/

RUN dotnet restore DemoApi/DemoApi.csproj

COPY . .

RUN dotnet publish DemoApi/DemoApi.csproj \
    -c Release \
    -o /app/publish

# ---------- STAGE 2: Runtime ----------
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app

COPY --from=build /app/publish .

EXPOSE 8080
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "DemoApi.dll"]

