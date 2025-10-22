# 📧 SendingMail.API

Bu proje, ASP.NET Core tabanlı bir **e-posta gönderim servisi**dir.  
Kullanıcıların tekli veya çoklu alıcılara e-posta göndermesini sağlar.  
SMTP protokolü aracılığıyla e-posta gönderimi yapılır ve gönderilen e-postalar HTML formatında biçimlendirilmiştir.

---

## 🚀 Kullanılan Teknolojiler

| Teknoloji | Açıklama |
|------------|-----------|
| **.NET 8.0** | Uygulamanın geliştirildiği framework |
| **ASP.NET Core Web API** | RESTful API mimarisi |
| **SMTP (System.Net.Mail)** | E-posta gönderimi için kullanılan .NET kütüphanesi |
| **Dependency Injection (DI)** | `IConfiguration` üzerinden SMTP ayarlarının yönetimi |

---

## ⚙️ Proje Kurulumu

### 1️⃣ Klonlama
```bash
git clone https://github.com/berkaycelik66/SendingMail.API.git
cd SendingMail.API
```

### 2️⃣ Gerekli Paketlerin Kurulumu
```bash
dotnet restore
```

### 3️⃣ SMTP Ayarlarının Yapılandırılması

`appsettings.json` dosyasında aşağıdaki ayarları düzenleyin:

```json
{
  "SmtpSettings": {
    "Host": "smtp.gmail.com",
    "Port": "587",
    "Username": "youremail@gmail.com",
    "Password": "yourpassword",
    "FromEmail": "youremail@gmail.com",
    "BusinessName": "Your Company Name"
  }
}
```

> ⚠️ **Not:** Gmail kullanıyorsanız, uygulama şifresi oluşturmanız gerekebilir.  
> (Google hesabı → Güvenlik → Uygulama şifreleri → "Mail" için bir şifre oluşturun.)

### 4️⃣ Projeyi Çalıştırma
```bash
dotnet run
```

Uygulama varsayılan olarak şu adreste çalışır:
```
https://localhost:5001
http://localhost:5000
```

---

## 📬 API Dokümantasyonu

### 🔹 1. Tekli Mail Gönderimi
**Endpoint:**
```
POST /api/send-single-mail
```

**İstek Gövdesi (JSON):**
```json
{
  "mail": "example@domain.com",
  "subject": "Welcome to Our Service",
  "message": "Thank you for joining us!"
}
```

**Başarılı Yanıt:**
```json
{
  "message": "Mail sent to the example@domain.com"
}
```

**Hatalı Yanıt (örnek):**
```json
{
  "message": "The SMTP server requires a secure connection..."
}
```

---

### 🔹 2. Çoklu (Toplu) Mail Gönderimi
**Endpoint:**
```
POST /api/send-bulk-mail
```

**İstek Gövdesi (JSON):**
```json
{
  "mails": [
    "user1@domain.com",
    "user2@domain.com",
    "user3@domain.com"
  ],
  "subject": "New Product Launch",
  "message": "We are excited to announce our new product line!"
}
```

**Başarılı Yanıt:**
```json
{
  "message": "3 mail sent succesfully."
}
```

---

## 💡 HTML Mail Formatı

Gönderilen her e-posta aşağıdaki HTML yapısını kullanır:

```html
<div class='email-container'>
  <div class='email-header'>Subject</div>
  <div class='email-body'>
    <p>Message Content</p>
  </div>
</div>
```

Dilerseniz `GetHtmlBody()` metodunda HTML Mail Template'ini ve CSS stilini özelleştirebilirsiniz.

---

## 🧩 Proje Yapısı

```
SendingMail.API/
│
├── Controllers/
│   └── MailController.cs         # E-posta gönderimi için API uç noktaları
│
├── Models/
│   ├── SingleMailRequest.cs      # Tekli mail isteği modeli
│   └── BulkMailRequest.cs        # Çoklu mail isteği modeli
│
├── appsettings.json              # SMTP yapılandırması
│
└── Program.cs                    # Uygulama başlangıç noktası
```

---

## 🧪 Test Etme

Postman veya cURL kullanarak test edebilirsiniz.

**Postman Örneği (Tekli Mail):**
- Method: `POST`
- URL: `https://localhost:5000/api/mail/send-single-mail`
- Body → raw → JSON:

```json
{
  "Mail": "example@domain.com",
  "Subject": "Test Email",
  "Message": "This is a test mail!"
}
```


