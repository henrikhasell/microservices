FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /app
COPY ./ .
RUN dotnet publish -c Release -o out

FROM microsoft/aspnetcore:2.0
WORKDIR /app
COPY --from=build /app/out .
ENV LANG=en_GB.UTF-8
ENTRYPOINT ["dotnet", "Paris.dll"]
