FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Ing_pr_web_api/Ing_pr_web_api.csproj", "Ing_pr_web_api/"]
RUN dotnet restore "Ing_pr_web_api/Ing_pr_web_api.csproj"
COPY . .
WORKDIR "/src/Ing_pr_web_api"
RUN dotnet build "Ing_pr_web_api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ing_pr_web_api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ing_pr_web_api.dll"]
