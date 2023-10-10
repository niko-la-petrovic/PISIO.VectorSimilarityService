#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["src/PISIO.VectorSimilarityService.Api/PISIO.VectorSimilarityService.Api.csproj", "src/PISIO.VectorSimilarityService.Api/"]
COPY ["src/PISIO.VectorSimilarityService.Data/PISIO.VectorSimilarityService.Data.csproj", "src/PISIO.VectorSimilarityService.Data/"]
COPY ["src/PISIO.VectorSimilarityService.Models/PISIO.VectorSimilarityService.Models.csproj", "src/PISIO.VectorSimilarityService.Models/"]
COPY ["src/PISIO.VectorSimilarityService.Dtos/PISIO.VectorSimilarityService.Dtos.csproj", "src/PISIO.VectorSimilarityService.Dtos/"]
RUN dotnet restore "src/PISIO.VectorSimilarityService.Api/PISIO.VectorSimilarityService.Api.csproj"
COPY . .
WORKDIR "/src/src/PISIO.VectorSimilarityService.Api"
RUN dotnet build "PISIO.VectorSimilarityService.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "PISIO.VectorSimilarityService.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PISIO.VectorSimilarityService.Api.dll"]