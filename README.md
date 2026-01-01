# TurkishValidators 🇹🇷

<img src="icon.png" align="right" width="128" height="128" />

[![.NET](https://img.shields.io/badge/.NET-6.0%20%7C%207.0%20%7C%208.0-512bd4)](https://dotnet.microsoft.com/download)
[![License: MIT](https://img.shields.io/badge/License-MIT-yellow.svg)](LICENSE)
[![NuGet](https://img.shields.io/nuget/v/TurkishValidators.svg)](https://www.nuget.org/packages/TurkishValidators/)

**TurkishValidators**, Türkiye'ye özgü veri tipleri (TCKN, Vergi No, IBAN, Telefon, Plaka, Posta Kodu) için geliştirilmiş; performanslı, hafif, API bağımlılığı olmayan ve genişletilebilir bir .NET doğrulama kütüphanesidir.

## 🚀 Özellikler

*   **Tamamen Offline:** Hiçbir dış servise veya API'ye istek atmaz. Matematiksel algoritmalarla çalışır.
*   **Yüksek Performans:** Allocation-free (tahsisatsız) algoritmalar ve optimize edilmiş string işlemleri.
*   **Geniş Kapsam:**
    *   ✅ **TC Kimlik Numarası** (Algoritma + Test Numarası Desteği)
    *   ✅ **Vergi Kimlik Numarası** (VKN)
    *   ✅ **IBAN** (TR IBAN Formatı + Checksum)
    *   ✅ **Telefon Numarası** (GSM ve Sabit Hat, Operör Kontrolü)
    *   ✅ **Araç Plakası** (81 İl Kodu, Resmi/Özel Plaka Formatları)
    *   ✅ **Posta Kodu** (81 İl ve İlçe Validasyonu)
*   **Çoklu Dil Desteği (Localization):** Türkçe (Varsayılan) ve İngilizce hata mesajları.
*   **Veri Maskeleme:** KVKK/GDPR uyumlu veri maskeleme yardımcıları.
*   **Entegrasyonlar:**
    *   ASPNET Core `ValidationAttribute` desteği.
    *   `FluentValidation` extension metodları.
*   **Test Verisi Üretimi:** Testleriniz için geçerli rastgele veri üreten `TurkishValidators.TestData` paketi.

## 📦 Kurulum

Projenize NuGet üzerinden ekleyebilirsiniz:

```bash
# Core Kütüphane (Temel Doğrulayıcılar)
dotnet add package TurkishValidators

# ASP.NET Core Entegrasyonu (Attributes)
dotnet add package TurkishValidators.AspNetCore

# FluentValidation Entegrasyonu
dotnet add package TurkishValidators.FluentValidation

# Test Verisi Üreticisi (Sadece Test Projeleri İçin)
dotnet add package TurkishValidators.TestData
```

## 💻 Kullanım

### 1. Temel Kullanım (Core)

```csharp
using TurkishValidators.Validators;

// TC Kimlik No Doğrulama
var tcknValidator = new TcKimlikNoValidator();
var result = tcknValidator.Validate("10000000146");
if (result.IsValid)
{
    Console.WriteLine("Geçerli TCKN!");
}
else
{
    Console.WriteLine(result.ErrorMessage); // "TC Kimlik Numarası geçersiz."
}

// Statik Kullanım
bool isValid = TcKimlikNoValidator.IsValid("10000000146");
```

### 2. ASP.NET Core Entegrasyonu (Attributes)

Model sınıflarınızda doğrudan kullanabilirsiniz:

```csharp
using TurkishValidators.AspNetCore.Attributes;

public class UserDto
{
    [TcKimlikNo(ErrorMessage = "Lütfen geçerli bir TC giriniz.")]
    public string NationalId { get; set; }

    [TurkishPhone]
    public string PhoneNumber { get; set; }

    [VehiclePlate]
    public string CarPlate { get; set; }
}
```

### 3. FluentValidation Entegrasyonu

Mevcut validator sınıflarınızda zincirleme metodlar (chaining) ile kullanın:

```csharp
using FluentValidation;
using TurkishValidators.FluentValidation.Extensions;

public class CustomerValidator : AbstractValidator<Customer>
{
    public CustomerValidator()
    {
        RuleFor(x => x.TcNo).MustBeTurkishIdentity();
        RuleFor(x => x.Iban).MustBeTurkishIban();
        RuleFor(x => x.Plate).MustBeVehiclePlate();
        
        // Opsiyonel Ayarlar
        RuleFor(x => x.TaxNo).MustBeTurkishTaxNumber(new VergiNoValidationOptions 
        { 
             // Ayarlar...
        });
    }
}
```

### 4. Veri Maskeleme (Masking)

Hassas verileri loglarken veya gösterirken maskeleyin:

```csharp
using TurkishValidators.Masking;

string maskedTc = TcKimlikNoMasker.Mask("12345678901"); 
// Çıktı: 123******01 (Varsayılan: İlk 3, Son 2 açık)

string maskedIban = IbanMasker.Mask("TR330006100519786457841326");
// Çıktı: TR********************1326

// Özel Ayarlar
var options = new MaskingOptions { VisibleStart = 0, VisibleEnd = 4, MaskChar = 'X' };
string customMask = TcKimlikNoMasker.Mask("12345678901", options);
// Çıktı: XXXXXXX8901
```

### 5. Test Verisi Üretimi (TestData)

Testlerinizde kullanmak üzere geçerli rastgele veriler üretin:

```csharp
using TurkishValidators.TestData.Services;

var provider = new TurkishDataProvider();

string randomTc = provider.GenerateTcKimlikNo();
string randomIban = provider.GenerateTurkishIban();
string istPlate = provider.GenerateVehiclePlate("İstanbul"); // 34 ... ...

// Toplu Veri Üretimi
var bulkData = provider.GenerateBulk(100);
```

## ⚙️ Yapılandırma (Configuration)

Uygulama genelinde hata mesajı dilini veya formatını değiştirebilirsiniz:

```csharp
using TurkishValidators.Config;

// Uygulama başlangıcında (Program.cs / Startup.cs)
TurkishValidatorConfig.Culture = new System.Globalization.CultureInfo("en-US");
// Artık hata mesajları İngilizce dönecektir.
```

## 🌍 Gelişmiş Dil Desteği (Advanced Localization)

Varsayılan Türkçe ve İngilizce mesajların yanı sıra, yeni diller ekleyebilir veya mevcut mesajları ezebilirsiniz:

```csharp
using TurkishValidators.Config;
using TurkishValidators.Resources;

// Almanca için özel mesaj seti tanımlama
var germanMessages = new ValidationMessages
{
    TcKimlikNoEmpty = "Die TC-Identitätsnummer darf nicht leer sein.",
    TcKimlikNoLength = "Die TC-Identitätsnummer muss 11 Ziffern lang sein.",
    // Diğer mesajlar...
};

// "de-DE" kültürü için kaydet
TurkishValidatorConfig.RegisterMessages("de-DE", germanMessages);

// Veya mevcut Türkçe mesajı değiştirme
var customTr = ValidationMessages.CreateDefault();
customTr.TcKimlikNoEmpty = "Lütfen TCKN alanını boş bırakmayınız!";
TurkishValidatorConfig.RegisterMessages("tr-TR", customTr);
```

## 🔌 Uyumluluk (Compatibility)

Proje **.NET Standard 2.0** hedeflemektedir, bu sayede aşağıdaki platformların tamamında sorunsuz çalışır:

*   .NET 5, .NET 6, .NET 7, .NET 8+
*   .NET Core 2.0+
*   .NET Framework 4.6.1+

## 🏗️ Proje Yapısı

*   `src/TurkishValidators`: Çekirdek kütüphane.
*   `src/TurkishValidators.AspNetCore`: ASP.NET Core attribute'ları.
*   `src/TurkishValidators.FluentValidation`: FluentValidation eklentileri.
*   `src/TurkishValidators.TestData`: Test verisi üretim kütüphanesi.

## 🤝 Katkıda Bulunma

PR'lar kabul edilir! Lütfen önce bir issue açarak değişikliği tartışın.

## 📄 Lisans

Bu proje [MIT](LICENSE) lisansı ile lisanslanmıştır.