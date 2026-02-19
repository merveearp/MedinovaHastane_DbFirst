# 🏥 Medinova Hospital Management System (DB-First)

Modern, rol bazlı ve yapay zekâ destekli bir Hastane Yönetim Sistemi.  
Proje mimarisi MHRS sistemi örnek alınarak geliştirilmiştir.

📍 Proje Başlangıcı: Ağustos 2025  
📊 Toplam Veri: 30.000+ kayıt  
👩‍⚕️ 56 Doktor | 🏢 16 Departman | 👨‍👩‍👧 81 Hasta | 🔐 Rol Bazlı Kullanım

---

## 🚀 Proje Amacı

Medinova, hastane süreçlerini dijitalleştirmek, randevu akışını yönetmek ve
AI destekli hasta yönlendirme sistemi ile sağlık hizmetlerini modernize etmek amacıyla geliştirilmiştir.

Sistem, Admin – Doctor – Patient olmak üzere 3 farklı rol üzerinden çalışmaktadır.

---

# 🔐 Rol Bazlı Giriş Sistemi

Kullanıcı rolüne göre otomatik yönlendirme yapılır:

- 👑 **Admin Paneli**
- 👨‍⚕️ **Doctor Paneli**
- 👤 **Patient Paneli**

Her rolün ayrı dashboard ve yetkileri bulunmaktadır.

---

# 👑 Admin Panel Özellikleri

- 📊 Dashboard (Widget & Chart yapıları)
- 👨‍⚕️ Doktor Yönetimi (58 aktif doktor)
- 🏢 Departman Yönetimi (15 aktif departman)
- 👤 Kullanıcı & Rol Yönetimi
- 📅 Randevu Durum Analizi (Aktif / İptal / Tamamlanan)
- 📈 Grafiksel Raporlama
- 📰 Blog Yönetimi
- 📝 Loglama Sistemi
- 📧 Mail Servisi Yönetimi

Admin panelinde 50.000+ veri üzerinden analiz yapılmaktadır.

---

# 👨‍⚕️ Doctor Panel Özellikleri

- 📅 Kendi randevularını görüntüleme
- 🧾 Tamamlanan randevulara muayene kaydı oluşturma
- 🩺 Hastaya teşhis & tedavi girişi
- 👤 Sadece kendi hastalarını görüntüleme
- 📊 Günlük / Aylık randevu istatistikleri

Doktor yalnızca kendisine ait randevular üzerinde işlem yapabilir.

---

# 👤 Patient Panel Özellikleri

- 📅 Randevu oluşturma
- ❌ Randevu iptal etme
- 📧 Randevu oluşturma & iptal mail bildirimi
- 🧾 Muayene sonuçlarını görüntüleme
- 🏢 Departmana göre doktor filtreleme
- ⛔ Doluluk kontrolü (aynı saat için tekrar randevu engellenir)

---

# 🤖 AI Destekli Hasta Yönlendirme

Sistem içerisinde:

- 💬 “Neyim Var?” AI modülü
- 🧠 Semptom bazlı departman önerisi
- 📍 Hastayı doğru branşa yönlendirme

OpenAI API kullanılarak geliştirilmiştir.

---

# 📈 ML.NET Entegrasyonu

- 📊 Randevu yoğunluk analizi
- 🔮 Gelecek dönem randevu tahmini
- 📅 Departman bazlı tahminleme

ML.NET ile zaman serisi tahmin modeli uygulanmıştır.

---

# 📧 Mail & Bildirim Sistemi

- Randevu oluşturulduğunda mail
- Randevu iptal edildiğinde mail
- Sistem içi SweetAlert bildirimleri
- Doluluk uyarı sistemi

---

# 📝 Loglama Sistemi

- Randevu oluşturma logu
- Randevu iptal logu
- Admin işlem logları

---

# 📰 Blog Modülü

- Sağlık bilgilendirme yazıları
- AI destekli yönlendirme entegrasyonu
- Hastaya rehber içerik

---

# 🗄 Veritabanı Yapısı

- Database First yaklaşımı
- SQL Server
- Rol bazlı kullanıcı yapısı
- Appointment iş kuralları:
    - IsActive
    - IsCompleted
    - Durum kombinasyon kontrolleri

---

# 🛠 Kullanılan Teknolojiler

- ASP.NET MVC 5
- Entity Framework (DB-First)
- SQL Server
- ML.NET
- OpenAI API
- Bootstrap
- Chart.js
- SweetAlert
- SMTP Mail Service

---

# 🔐 İş Kuralları

- Aynı hastaya aynı saat için randevu engellenir
- 7 gün kuralı uygulanır
- Hafta sonu randevu kısıtı
- Rol bazlı yetkilendirme

---

# 📊 Proje İstatistikleri

- 👨‍⚕️ 56 Doktor
- 🏢 16 Departman
- 👤 81 Hasta
- 📅 30.000+ veri kaydı
- 🔐 3 Rol (Admin / Doctor / Patient)

---

# 🎯 Mimari

- Katmanlı yapı
- DTO kullanımı
- Service Layer
- Area bazlı mimari
- Session bazlı rol yönetimi

---

# 📌 Not

Bu proje, MHRS sistemi referans alınarak geliştirilmiştir.
Eğitim ve portföy amaçlıdır.

---

# 👩‍💻 Geliştirici

Merve Arpacıoğlu Türk  
Backend Developer | Data & AI Enthusiast  

Projeden görüntüler :

![YeniRandveu_LOG](https://github.com/user-attachments/assets/f2c9b6c6-b152-4684-85a7-d66772a2c614)
<img width="1212" height="882" alt="UI-7" src="https://github.com/user-attachments/assets/96b28c1d-5f41-46d0-ad00-8d61b22189ca" />
<img width="1095" height="907" alt="UI-6" src="https://github.com/user-attachments/assets/db6500b5-ff1e-482e-bd5e-dd6523dd9fc4" />
![uı-5](https://github.com/user-attachments/assets/18c2a173-7724-4355-8fb4-da45da0cc01e)
![UI-4](https://github.com/user-attachments/assets/9cb504ee-ff14-49ea-b3a3-b929c9870f60)
![UI-3](https://github.com/user-attachments/assets/56b5f9d0-4431-4cca-ad4f-ec252205d0c0)
![UI-2](https://github.com/user-attachments/assets/c0004d1e-df40-494e-a0fc-b504cc491ad0)
![UI-1-6](https://github.com/user-attachments/assets/2eff6d90-37e9-4c61-8057-011e6e952dfd)
![UI-1](https://github.com/user-attachments/assets/922f2248-6057-40fd-ae19-2bf1c72b6518)
![REGISTER](https://github.com/user-attachments/assets/dc07cb8a-96ae-494f-a368-473b1a207386)
![panel](https://github.com/user-attachments/assets/8dafbea7-07a5-4ecc-8e25-28d967c653f9)
![LOGİN-2](https://github.com/user-attachments/assets/d5154a94-ca16-4ea4-8308-1047e38432d7)
![LOGİN-1](https://github.com/user-attachments/assets/78da936a-6846-4632-8835-73e63cb42cb4)
![İptalRandevu_LOG](https://github.com/user-attachments/assets/3892c394-8c84-4d8a-bf31-6e3ceaa93545)
![HastaPanel-8](https://github.com/user-attachments/assets/f175cb91-bfc5-47b3-8fd1-4ee171771b37)
![HastaPanel-7](https://github.com/user-attachments/assets/cf6004da-df35-4459-b091-1acf589eb4ac)
![HastaPanel-6](https://github.com/user-attachments/assets/99c8f2e1-7971-4443-82cc-6b864204cb3b)
![HastaPanel-5](https://github.com/user-attachments/assets/9178e1ea-b069-4664-a74f-d31880253a1e)
![HastaPanel-4](https://github.com/user-attachments/assets/e9791243-0df2-4b65-9075-106090947a5a)
![HastaPanel-3](https://github.com/user-attachments/assets/ad3c94b7-7943-40fc-b79b-254d7b7cd230)
![HastaPanel-2](https://github.com/user-attachments/assets/28333069-7e67-499c-a168-8060042c3428)
![HastaPanel-1](https://github.com/user-attachments/assets/3e3ea091-8295-4551-94e0-ca86a7d3bd1c)
![DoktorPanel-9](https://github.com/user-attachments/assets/c6ab5152-bc58-44d6-aaae-c176a55a8cf2)
![DoktorPanel-8](https://github.com/user-attachments/assets/f220ada4-0937-4bfd-8d6c-dae8e97a26af)
![DoktorPanel-7](https://github.com/user-attachments/assets/f5d0f181-f49f-476c-8f78-a5804b623cd3)
![DoktorPanel-6](https://github.com/user-attachments/assets/69c02b29-b8a1-421c-82d6-377525757b27)
![DoktorPanel-5](https://github.com/user-attachments/assets/b41f3424-446f-4449-9515-bb39a7a94ea3)
![DoktorPanel-4](https://github.com/user-attachments/assets/90533d03-abec-495c-aebc-5ccc614d2936)
![DoktorPanel-3](https://github.com/user-attachments/assets/d49f68fb-b987-4f84-951e-07426840e195)
![DoktorPanel-2](https://github.com/user-attachments/assets/5f76c5be-134b-432f-a1bf-0081a7cb3001)
![DoktorPanel-1](https://github.com/user-attachments/assets/f38b12a9-ce71-4816-92f7-42ebc0d6764c)
![Admin13](https://github.com/user-attachments/assets/fc3e7e17-6896-486c-8572-0efaaff5b47f)
![Admin12](https://github.com/user-attachments/assets/338e6287-6df5-4562-8c11-733b3839ab7c)
![Admin11](https://github.com/user-attachments/assets/12b7c628-3625-419b-b6ce-7ff615e57e1f)
![Admin10](https://github.com/user-attachments/assets/7a7dd811-f884-42b4-95a1-5c87c1348d1e)
![Admin9](https://github.com/user-attachments/assets/240d2832-09ea-476f-9837-0404b9b1e636)
![Admin8](https://github.com/user-attachments/assets/091ecb59-36b6-42a1-a286-7517c6471767)
![Admin7](https://github.com/user-attachments/assets/6214fc11-84c3-46e4-93d4-4c612304c33b)
![Admin6](https://github.com/user-attachments/assets/2570ab0c-a9c4-4e5a-95f6-07ece38c1bab)
![Admin5](https://github.com/user-attachments/assets/c843bc22-e795-46cc-8e4e-17ae6b3d77ef)
![Admin4](https://github.com/user-attachments/assets/930edc68-6a71-40b3-90e5-9fd003122c32)
![Admin3](https://github.com/user-attachments/assets/2114ff85-be90-4f3d-9e42-f0592264d59e)
![Admin2](https://github.com/user-attachments/assets/faff5af6-52ce-4c53-bc79-815eb091a96c)
![Admin1](https://github.com/user-attachments/assets/8672a11e-786f-4ea3-90c1-2f7ed578483f)

