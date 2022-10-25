FROM mcr.microsoft.com/dotnet/sdk:6.0 as build-env
WORKDIR /source
COPY ./libraryAPI .
RUN dotnet restore "libraryAPI.csproj"
RUN dotnet publish "libraryAPI.csproj" -c Release -o /app

FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app ./
EXPOSE 443
EXPOSE 80
ENTRYPOINT ["dotnet", "libraryAPI.dll"]