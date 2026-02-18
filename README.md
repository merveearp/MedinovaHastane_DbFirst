# 🏥 Medinova – Hospital Management System (ASP.NET MVC 5)

> Role-based, layered architecture ile geliştirilmiş, MHRS benzeri kurgulanmış bir Hastane Yönetim Sistemi.

🔗 Repository:  
https://github.com/merveearp/Medinova_DbFirst

---

## 📌 Proje Hakkında

Medinova, ASP.NET MVC 5 kullanılarak geliştirilmiş ve Entity Framework Db-First yaklaşımı ile tasarlanmış bir hastane yönetim sistemidir.

Sistem;
- Admin
- Doctor
- Patient

olmak üzere 3 ayrı rol ve 3 ayrı Area yapısı ile çalışmaktadır.

Proje, Türkiye’deki MHRS (Merkezi Hekim Randevu Sistemi) mantığı örnek alınarak kurgulanmıştır.

⚠️ PROJE DURUMU:  
Bu proje aktif olarak geliştirilmektedir.  
Admin ve Patient panelleri henüz tamamlanmamıştır.

---

# 🏗️ Kullanılan Teknolojiler

- ASP.NET MVC 5
- Entity Framework (DB-First)
- SQL Server
- Katmanlı Mimari (Controller – Service – Repository – DTO)
- Area Based Structure
- Role-Based Authorization
- Session Management
- Bootstrap UI Template

---

# 👥 Roller ve Panel Yapısı

## 1️⃣ Admin Area

Özellikler:
- Doktor yönetimi
- Hasta yönetimi
- Aktif / Pasif kullanıcı kontrolü
- Randevu listeleme
- Sistem yönetimi

Not:
Admin paneli geliştirme aşamasındadır.

---

## 2️⃣ Doctor Area

Özellikler:
- Aktif randevuları listeleme
- Tamamlanan randevuları görüntüleme
- Randevu tamamlama işlemi
- Randevu iptal işlemi
- AppointmentDetail oluşturma
- Hasta geçmiş randevularını görüntüleme

### Randevu İş Akışı

1. Hasta randevu oluşturur (IsActive = true)
2. Doktor randevuyu tamamladığında:
   - IsActive = false
   - IsCompleted = true
3. Sadece tamamlanan randevular için AppointmentDetail oluşturulabilir.

Bu yapı ile veri tutarlılığı sağlanmıştır.

---

## 3️⃣ Patient Area

Özellikler:
- Doktor listeleme
- Randevu oluşturma
- Randevu geçmişi görüntüleme
- Profil düzenleme
- Şifre değiştirme

Not:
Patient paneli geliştirme aşamasındadır.

---

# 🔐 Yetkilendirme & Güvenlik

- Her rol için ayrı Login ekranı
- Role göre yönlendirme
- Yetkisiz erişim engelleme
- Session tabanlı kullanıcı kontrolü
- Şifreler hashlenerek saklanmaktadır
- Aktif / Pasif kullanıcı kontrolü

---

# 🗂️ Veritabanı Yapısı

Temel tablolar:

- Users
- Roles
- UserRoles
- Doctors
- Patients
- Appointments
- AppointmentDetails
- Departments
- Genders

Foreign Key ilişkileri aktif şekilde tasarlanmıştır.

---

# 🚀 Planlanan Geliştirmeler

- Loglama sistemi (Exception & işlem logları)
- Yapay Zeka entegrasyonu (tanı destek & yoğunluk tahmini)
- Dashboard istatistik modülü
- Gelişmiş randevu planlama sistemi
- ML.NET ile veri analizi modülü
- Gelişmiş yetkilendirme katmanı

---

# 📌 Proje Durumu

🟡 Development in Progress

Bu proje aktif olarak geliştirilmektedir.  
Yeni modüller ve geliştirmeler eklenecektir.

---
"Görseller Projeden ekran kaydı alınarak eklenmiştir."
![hasta-register](https://github.com/user-attachments/assets/348df1e4-541c-4ea5-b932-6b82300982ca)
![hasta-login](https://github.com/user-attachments/assets/d5da2601-9454-440e-8161-937ce768ffb1)
![website](https://github.com/user-attachments/assets/68b6c3b5-6499-4fc4-8300-f0dc479e999b)
![doctor-panel](https://github.com/user-attachments/assets/b1d8ca07-8c8f-4bca-ab20-fa00f2e2c2ad)
![doctor-login](https://github.com/user-attachments/assets/95f236f2-7fdc-4d5c-b5ac-e29f4ec2fdab)
![panel-login-islmeleri](https://github.com/user-attachments/assets/27d9bd6c-c7f0-47ac-bd03-c6ad059785a4)
![admin-login](https://github.com/user-attachments/assets/0676a9e8-4b05-420a-9254-0541e2a637e8)


# 👩‍💻 Developer

Merve Arpacıoğlu Türk  
Junior Software Developer  
ASP.NET MVC | SQL Server | Layered Architecture
