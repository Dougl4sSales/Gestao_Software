FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

COPY . .
RUN dotnet restore GestaoDeSofware.Frontend/GestaoDeSofware.Frontend.csproj
RUN dotnet publish GestaoDeSofware.Frontend/GestaoDeSofware.Frontend.csproj \
    -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS final
WORKDIR /app

COPY --from=build /app/publish .
ENV ASPNETCORE_HTTP_PORTS=8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "GestaoDeSofware.Frontend.dll"]