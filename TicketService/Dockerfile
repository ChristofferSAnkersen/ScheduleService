FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY TicketService/TicketService.csproj TicketService/
RUN dotnet restore TicketService/TicketService.csproj
COPY . .
WORKDIR /src/TicketService
RUN dotnet build TicketService.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish TicketService.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "TicketService.dll"]
