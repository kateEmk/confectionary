#build container
FROM mcr.microsoft.com/dotnet/sdk:6.0 as build

WORKDIR /build
COPY . .
RUN dotnet run -p Confectionery.csproj

#runtime container
FROM mcr.microsoft.com/dotnet/aspnet:7.0
#RUN apk add --no-cache tzdata

COPY --from=build /build/publish /app
WORKDIR /app

EXPOSE 5000

CMD ["ls"]