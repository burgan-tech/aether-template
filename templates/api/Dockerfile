FROM mcr.microsoft.com/dotnet/aspnet:10.0-alpine AS base
WORKDIR /app

RUN adduser MyProjectNameuser --disabled-password --gecos "" && chown -R MyProjectNameuser:MyProjectNameuser /app
USER MyProjectName

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:10.0-alpine AS build

# Install NETStandard.Library.Ref targeting pack for PostSharp compatibility
RUN wget -q https://www.nuget.org/api/v2/package/NETStandard.Library.Ref/2.1.0 -O /tmp/netstandard.nupkg && \
    mkdir -p /root/.nuget/packages/netstandard.library.ref/2.1.0 && \
    mkdir -p /var/tmp/postsharp/NuGetFallback/NETStandard.Library.Ref/2.1.0 && \
    unzip -q /tmp/netstandard.nupkg -d /root/.nuget/packages/netstandard.library.ref/2.1.0 && \
    unzip -q /tmp/netstandard.nupkg -d /var/tmp/postsharp/NuGetFallback/NETStandard.Library.Ref/2.1.0 && \
    rm /tmp/netstandard.nupkg

COPY ["common.props", "/common.props"]
COPY ["Directory.Build.props", "/Directory.Build.props"]
COPY ["global.json", "/global.json"]
COPY ["nuget.config", "/nuget.config"]

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
