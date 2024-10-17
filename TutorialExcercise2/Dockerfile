FROM mcr.microsoft.com/dotnet/runtime:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["TutorialExcercise2/TutorialExcercise2.csproj", "TutorialExcercise2/"]
RUN dotnet restore "TutorialExcercise2/TutorialExcercise2.csproj"
COPY . .
WORKDIR "/src/TutorialExcercise2"
RUN dotnet build "TutorialExcercise2.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "TutorialExcercise2.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TutorialExcercise2.dll"]
