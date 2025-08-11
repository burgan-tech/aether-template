FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

RUN adduser MyProjectNameuser --disabled-password --gecos "" && chown -R MyProjectNameuser:MyProjectNameuser /app
USER MyProjectName

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build

COPY ["common.props", "/common.props"]
COPY ["Directory.Build.props", "/Directory.Build.props"]
COPY ["NuGet.Config", "/NuGet.Config"]

WORKDIR /src
ENV DOTNET_NUGET_SIGNATURE_VERIFICATION=false

COPY src/ .
RUN dotnet restore "BBT.MyProjectName.HttpApi.Host/BBT.MyProjectName.HttpApi.Host.csproj"
COPY . .
WORKDIR "/src/BBT.MyProjectName.HttpApi.Host"

ARG configuration=Release

RUN dotnet build "BBT.MyProjectName.HttpApi.Host.csproj" -c $configuration -o /app/build

FROM build AS publish
ARG configuration=Release
RUN dotnet publish "BBT.MyProjectName.HttpApi.Host.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 5000
ENV ASPNETCORE_URLS=http://+:5000
ENTRYPOINT ["dotnet", "BBT.MyProjectNameuser.HttpApi.Host.dll"]
