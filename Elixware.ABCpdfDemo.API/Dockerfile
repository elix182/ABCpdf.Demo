FROM mcr.microsoft.com/dotnet/sdk:8.0 AS builder
# For ABCChrome123
RUN apt-get update -y
RUN apt update --fix-missing 
RUN apt-get upgrade -y
RUN apt install -y curl
# === 
WORKDIR /App

COPY . ./
RUN dotnet restore
RUN dotnet publish -c Release -o out

# FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
FROM abcpdf/mcr-aspnet:8.0-jammy AS runtime
WORKDIR /App
COPY --from=builder /App/out .

ENV ASPNETCORE_URLS=http://+:8085
EXPOSE 8085
ENTRYPOINT ["dotnet", "Elixware.ABCpdfDemo.API.dll"]