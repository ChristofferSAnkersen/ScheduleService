FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ["ScheduleService/ScheduleService.csproj", "ScheduleService/"]
RUN dotnet restore "ScheduleService/ScheduleService.csproj"
COPY . .
WORKDIR "/src/ScheduleService"
RUN dotnet build "ScheduleService.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "ScheduleService.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ScheduleService.dll"]