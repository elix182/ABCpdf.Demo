FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
WORKDIR /App

COPY ../* ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
FROM abcpdf/mcr-aspnet:8.0-jammy AS runtime
WORKDIR /App
COPY --from=builder /App/out .

ENV ASPNETCORE_URLS=http://+:8085
EXPOSE 8085
ENTRYPOINT ["dotnet", "Elixware.ABCpdfDemo.API.dll"]