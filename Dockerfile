FROM microsoft/dotnet AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/dotnet AS build
WORKDIR /src
COPY ["Shipping.EndPoint/Shipping.EndPoint.csproj", "Shipping.EndPoint/"]
RUN dotnet restore "Shipping.EndPoint/Shipping.EndPoint.csproj"
COPY . .
WORKDIR "/src/Shipping.EndPoint"
RUN dotnet build "Shipping.EndPoint.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Shipping.EndPoint.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Shipping.EndPoint.dll"]