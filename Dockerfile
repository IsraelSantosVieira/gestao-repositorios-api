# Build Api

FROM mcr.microsoft.com/dotnet/sdk:6.0-focal as build_env

WORKDIR /build

COPY ./*.sln ./

COPY ./nuget.config ./

COPY ./src/*/*.csproj ./

RUN for file in $(ls *.csproj); do mkdir -p src/${file%.*}/ && mv $file ./src/${file%.*}/; done

COPY ./tests/*/*.csproj ./

RUN for file in $(ls *.csproj); do mkdir -p tests/${file%.*}/ && mv $file ./tests/${file%.*}/; done

RUN ["dotnet", "restore", "--configfile", "nuget.config", "--ignore-failed-sources"]

COPY . ./

# run tests
WORKDIR /build/tests/QueroTruck.Tests
RUN dotnet build QueroTruck.Tests.csproj -c Release --no-restore
RUN dotnet test --filter Category!=NotRun QueroTruck.Tests.csproj



WORKDIR /build/src/QueroTruck.Api

RUN dotnet publish -c Release -o /build/publish --no-restore

#######################################################################################################################

# Run app

FROM mcr.microsoft.com/dotnet/aspnet:6.0-focal

COPY --from=build_env /build/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "QueroTruck.Api.dll"]
