# Inventory Order Management Sistemi

Bu proje, envanter ve sipariş yönetimi işlemlerini gerçekleştirmek için oluşturulmuş bir API tabanlı uygulamadır.

## Gereksinimler

- .NET 8.0 SDK
- SQLite (veya SQL Server)

## Kurulum ve Çalıştırma

### 1. Projeyi Klonlama

```bash
git clone <repo-url>
cd InventoryOrderManagement
```

### 2. Veritabanı Ayarları

`InventoryOrderManagement.Presentation/appsettings.Development.json` dosyasında veritabanı ayarlarını yapılandırın:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=InventoryOrderManagement.db"
  },
  "DatabaseProvider": "Sqlite",
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "Jwt": {
    "Key": "InventoryOrderManagementSecurityKey1234567890",
    "Issuer": "InventoryOrderManagement",
    "Audience": "InventoryOrderManagementApp",
    "ExpireInMinute": 60
  }
}
```

Desteklenen veritabanı sağlayıcıları:
- `Sqlite` (varsayılan)
- `SqlServer`

SQL Server kullanmak için:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=InventoryOrderManagement;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
  },
  "DatabaseProvider": "SqlServer",
  ...
}
```

### 3. Projeyi Derleme

```bash
dotnet build
```

### 4. Projeyi Çalıştırma

```bash
dotnet run --project InventoryOrderManagement.Presentation
```

Uygulama varsayılan olarak aşağıdaki adreslerde çalışacaktır:
- http://localhost:5232
- https://localhost:7132

### 5. API Dokümantasyonu

Swagger UI'a aşağıdaki URL üzerinden erişebilirsiniz:
```
http://localhost:5232/swagger
```

## Kimlik Doğrulama

Uygulama JWT tabanlı kimlik doğrulama kullanmaktadır. Korumalı API'lere erişmek için önce kullanıcı kaydı oluşturmalı ve giriş yaparak token almanız gerekir.

### Kullanıcı Rolleri
Uygulamada iki temel rol bulunmaktadır:
- `Admin`: Tam yetkili kullanıcı
- `User`: Sınırlı yetkili kullanıcı

Uygulama ilk çalıştığında `SeedRoles` metodu sayesinde roller otomatik olarak oluşturulur. Yeni kaydolan kullanıcılara varsayılan olarak `User` rolü atanır.

### Kullanıcı Kaydı
```bash
curl -X POST -H "Content-Type: application/json" -d '{
  "email": "user@example.com",
  "password": "Test123!",
  "confirmPassword": "Test123!",
  "firstName": "Test",
  "lastName": "User",
  "companyName": "Test Company"
}' http://localhost:5232/api/Security/Register
```

### Giriş Yapma ve Token Alma
```bash
curl -X POST -H "Content-Type: application/json" -d '{
  "email": "user@example.com",
  "password": "Test123!"
}' http://localhost:5232/api/Security/Login
```

Başarılı bir giriş işleminden sonra, bir JWT token alacaksınız. Yanıtta şöyle bir payload göreceksiniz:

```json
{
  "code": 200,
  "message": "Giriş başarılı",
  "content": {
    "message": "...",
    "userName": "user@example.com",
    "email": "user@example.com",
    "userId": "...",
    "firstName": "Test",
    "lastName": "User",
    "companyName": "Test Company",
    "accessToken": "eyJhbGciOiJIUzI1NiIsInR5...",
    "refreshToken": "...",
    "menuNavigation": [...],
    "roles": [...],
    "avatar": null
  }
}
```

Bu yanıttaki `accessToken` değerini alıp API isteklerinizde kullanmanız gerekir.

### Token Kullanımı

Aldığınız token ile API endpointlerini kullanırken `Authorization` başlığını şu şekilde eklemeniz gerekir:

```bash
curl -X GET -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5..." http://localhost:5232/api/Company/GetCompanyList
```

Swagger UI üzerinden token kullanmak için:

1. Swagger sayfasının sağ üstündeki "Authorize" butonuna tıklayın
2. Açılan pencerede "Bearer" alanına aldığınız token'ı "Bearer " öneki olmadan girin
3. "Authorize" butonuna tıklayın
4. Artık tüm API endpointlerini kullanabilirsiniz

### Çıkış Yapma
```bash
curl -X POST -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5..." http://localhost:5232/api/Security/Logout
```

Çıkış işlemi, kullanıcının refresh token'ını veritabanından kaldırır ve tarayıcı cookie'sini temizler. Başarılı bir çıkış işlemi sonrası kullanıcı yeniden giriş yapmalıdır.

Çıkış işlemi için geçerli bir JWT token gereklidir. Token, `Authorization` başlığında "Bearer " öneki ile birlikte gönderilmelidir.

## API Endpoint'leri

### Güvenlik İşlemleri (Security)

#### Kullanıcı Kaydı (Register)
```bash
curl -X POST -H "Content-Type: application/json" -d '{
  "email": "user@example.com",
  "password": "Test123!",
  "confirmPassword": "Test123!",
  "firstName": "Test",
  "lastName": "User",
  "companyName": "Test Company"
}' http://localhost:5232/api/Security/Register
```

#### Giriş Yapma (Login)
```bash
curl -X POST -H "Content-Type: application/json" -d '{
  "email": "user@example.com",
  "password": "Test123!"
}' http://localhost:5232/api/Security/Login
```

#### Çıkış Yapma (Logout)
```bash
curl -X POST -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5..." http://localhost:5232/api/Security/Logout
```

### Şirket Yönetimi (Company)

#### Şirket Listesini Alma
```bash
curl -X GET -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5..." "http://localhost:5232/api/Company/GetCompanyList"
```

#### Tek Bir Şirket Bilgisi Alma
```bash
curl -X GET -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5..." "http://localhost:5232/api/Company/GetCompanySingle?id=<guid>"
```

#### Şirket Ekleme
```bash
curl -X POST -H "Content-Type: application/json" -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5..." -d '{
  "name": "Yeni Şirket",
  "description": "Açıklama",
  "currency": "TRY",
  "street": "Cadde",
  "city": "Şehir",
  "state": "Eyalet",
  "zipCode": "34000",
  "country": "Ülke",
  "phoneNumber": "1234567890",
  "email": "ornek@yenisirket.com",
  "website": "https://www.yenisirket.com"
}' http://localhost:5232/api/Company/AddCompany
```

#### Şirket Güncelleme
```bash
curl -X POST -H "Content-Type: application/json" -H "Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5..." -d '{
  "id": "<guid>",
  "name": "Şirket Adı",
  "description": "Açıklama",
  "currency": "TRY",
  "street": "Cadde",
  "city": "Şehir",
  "state": "Eyalet",
  "zipCode": "34000",
  "country": "Ülke",
  "phoneNumber": "1234567890",
  "email": "ornek@sirket.com",
  "website": "https://www.sirket.com"
}' http://localhost:5232/api/Company/UpdateCompany
```

**Not:** Güncelleme işlemi için veritabanında ilgili ID'ye sahip bir şirket kaydı bulunmalıdır.

## Veritabanı Yapılandırması

Uygulama ilk çalıştığında SQLite veritabanı otomatik olarak oluşturulur ve örnek verilerle doldurulur. Varsayılan olarak aşağıdaki kayıtlar oluşturulacaktır:

- Örnek bir şirket kaydı (Acme Corp)
- Temel sistem ayarları
- Gerekli depo (warehouse) kayıtları

## Unit Testler

Bu proje kapsamlı birim testleri içermektedir. Testleri çalıştırmak için:

```bash
dotnet test
```

### Kod Kapsama Raporu Oluşturma

Kod kapsama raporunu HTML formatında oluşturmak için:

```bash
cd InventoryOrderManagement.UnitTests
./generate-coverage-report.sh
```

Bu script, testleri çalıştırır ve HTML formatında kod kapsama raporu oluşturur. Rapor otomatik olarak varsayılan tarayıcınızda açılır.

## Hata Çözümleri

### "No such file or directory" Hatası
Eğer veritabanı dosyası bulunamazsa, uygulamayı yeniden başlatmak genellikle sorunu çözecektir. Uygulama başlangıçta veritabanını ve gerekli tabloları oluşturacaktır.

### API İstek Süresi Dolması
Sunucu yanıt vermiyorsa veya istek süresi doluyorsa, uygulamanın çalıştığından emin olun. Sunucu Swagger arayüzünü görüntüleyebiliyorsanız ancak API istekleri başarısız oluyorsa, token'ın geçerli olduğundan emin olun.

### Token İle İlgili Sorunlar
Token süreleri 60 dakika olarak ayarlanmıştır. Eğer "401 Unauthorized" hatası alırsanız, tekrar Login endpoint'ini kullanarak yeni bir token alın.

Token format hatası alırsanız, Authorization header'ını tam olarak şu formatta kullandığınızdan emin olun:
```
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5...
```

### Doğrulama Hataları
API isteklerini gönderirken tüm zorunlu alanların doldurulduğundan emin olun. Örneğin, şirket güncellemesi yaparken `name`, `currency`, `street`, `city`, `state`, `zipCode`, `phoneNumber` ve `email` alanları zorunludur.

## Sonuç

Bu sistem, şirket, envanter, sipariş ve diğer ilgili verileri yönetmek için Clean Architecture ilkelerine dayalı bir API sağlar. Swagger UI üzerinden tüm API'leri test edebilir ve belgeleri inceleyebilirsiniz. 