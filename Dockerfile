# .NET SDK görüntüsünü kullanarak projeyi derleyin
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build-env
WORKDIR /app

# Dosyaları kopyalayın ve restore edin
COPY *.csproj ./
RUN dotnet restore

# Tüm dosyaları kopyalayın ve yayınlayın
COPY . ./
RUN dotnet publish -c Release -o out

# Çalışma zamanı görüntüsünü oluşturun
FROM mcr.microsoft.com/dotnet/aspnet:10.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "KutuphaneYonetim.dll"]