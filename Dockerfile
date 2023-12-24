FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["QuickJob.Users.Api/QuickJob.Users.Api.csproj", "QuickJob.Users.Api/"]
RUN dotnet restore "QuickJob.Users.Api/QuickJob.Users.Api.csproj"
COPY . .
WORKDIR "/src/QuickJob.Users.Api"
RUN dotnet build "QuickJob.Users.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "QuickJob.Users.Api.csproj" -c Release -o /app/publish 

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "QuickJob.Users.Api.dll"]
