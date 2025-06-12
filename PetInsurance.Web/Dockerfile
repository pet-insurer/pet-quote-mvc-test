FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install curl for debugging
RUN apt-get update && apt-get install -y curl

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PetInsurance.Web/PetInsurance.Web.csproj", "PetInsurance.Web/"]
COPY ["PetInsurance.Shared/PetInsurance.Shared.csproj", "PetInsurance.Shared/"]
RUN dotnet restore "PetInsurance.Web/PetInsurance.Web.csproj"
COPY . .
WORKDIR "/src/PetInsurance.Web"
RUN dotnet build "PetInsurance.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetInsurance.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetInsurance.Web.dll"] 