FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["PetInsurance.API/PetInsurance.API.csproj", "PetInsurance.API/"]
COPY ["PetInsurance.Shared/PetInsurance.Shared.csproj", "PetInsurance.Shared/"]
RUN dotnet restore "PetInsurance.API/PetInsurance.API.csproj"
COPY . .
WORKDIR "/src/PetInsurance.API"
RUN dotnet build "PetInsurance.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PetInsurance.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PetInsurance.API.dll"] 