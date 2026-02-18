using Medinova.DTOs.AppointmentDtos;
using Medinova.Models;
using Medinova.Repositories.AppointmentRepository;
using Medinova.Services.MailService;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Medinova.Services.AppointmentService
{
    public class AppointmentService : IAppointmentService
    {
        MedinovaContext context = new MedinovaContext();

        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IMailLogService _mailService;

        public AppointmentService()
        {
            _mailService = new MailLogService();
            _appointmentRepository = new AppointmentRepository();
        }
        public async Task CreateAppointmentAsync(CreateAppointmentDto dto)
        {

            var patientActive = await context.Patients.AnyAsync(x => x.PatientId == dto.PatientId && x.User.IsActive == true);

            if (!patientActive)
            {
                throw new Exception("Hasta randveu kaydı için hasta profil durumunun aktifleştirilmesi gerekmektedir.Lütfen gerekli işlemlerden sonra tekrar deneyiniz.");
            }

            var today = DateTime.Today;
            var maxDate = today.AddDays(10);

            if(dto.AppointmentDate < today || dto.AppointmentDate > maxDate )
            {
                throw new Exception("Sadece önümüzdeki 10 gün için randevu oluşturabilirsiniz");
            }

            if(dto.AppointmentDate.DayOfWeek == DayOfWeek.Saturday || dto.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
            {
                throw new Exception("Haftasonu için randevu kaydı oluşturulamaz");
            }

            var isAppointmentFull = await context.Appointments
                .AnyAsync(x => x.AppointmentDate == dto.AppointmentDate 
                            && x.AppointmentTime == dto.AppointmentTime
                            && x.IsActive == true);

            if(!isAppointmentFull)
            {
                throw new Exception("BU randevu kaydı alınamaz seçilen saat doludur");
            }          

           

            var hasSameDay = await context.Appointments
                .AnyAsync(x => x.PatientId == dto.PatientId
                            && x.IsActive == true
                            && x.AppointmentDate == dto.AppointmentDate);

            if(!hasSameDay)
            {
                throw new Exception("Hasta aynı gün ikinci randevu kaydı oluşturamaz");
            }

            var canceledAppointment = await context.Appointments
                .FirstOrDefaultAsync(x => 
                                        DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date
                                        && x.AppointmentTime == dto.AppointmentTime
                                        && !x.IsActive
                                        && !x.IsCompleted);

            if (canceledAppointment != null)
            {
                canceledAppointment.DoctorId = dto.DoctorId;
                canceledAppointment.PatientId = dto.PatientId;
                canceledAppointment.IsActive = true;
                canceledAppointment.CreatedDate = DateTime.Now;

                await context.SaveChangesAsync();
                return;
            }


            var value = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsCompleted = false,

            };
            await _appointmentRepository.CreateAppointmentAsync(value);
           
        }



        public async Task CreateAppointmentByDoctorAsync(CreateAppointmentDto dto, int doctorId)
        {
            var patientActive = await context.Patients
                .AnyAsync(x => x.PatientId == dto.PatientId && x.User.IsActive);

            if (!patientActive)
                throw new Exception("Hasta profil durumu aktif değil.");

            var today = DateTime.Today;
            var maxDate = today.AddDays(10);

            if (dto.AppointmentDate < today || dto.AppointmentDate > maxDate)
                throw new Exception("Sadece önümüzdeki 10 gün için randevu oluşturabilirsiniz.");

            if (dto.AppointmentDate.DayOfWeek == DayOfWeek.Saturday ||
                dto.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
                throw new Exception("Haftasonu için randevu oluşturulamaz.");

           
            var hasSameDay = await context.Appointments
                .AnyAsync(x => x.PatientId == dto.PatientId
                            && x.IsActive
                            && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date);

            if (hasSameDay)
                throw new Exception("Hasta aynı gün ikinci randevu kaydı oluşturamaz.");

           
            var isAppointmentFull = await context.Appointments
                .AnyAsync(x => x.DoctorId == doctorId
                            && x.IsActive
                            && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date
                            && x.AppointmentTime == dto.AppointmentTime);

            if (isAppointmentFull)
                throw new Exception("Bu saat için zaten aktif randevu bulunmaktadır.");

           
            var canceledAppointment = await context.Appointments
                .FirstOrDefaultAsync(x => x.DoctorId == doctorId
                                        && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date
                                        && x.AppointmentTime == dto.AppointmentTime
                                        && !x.IsActive
                                        && !x.IsCompleted);

            if (canceledAppointment != null)
            {
                canceledAppointment.PatientId = dto.PatientId;
                canceledAppointment.IsActive = true;
                canceledAppointment.CreatedDate = DateTime.Now;

                await context.SaveChangesAsync();
                return;
            }

            var newAppointment = new Appointment
            {
                DoctorId = doctorId,
                PatientId = dto.PatientId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsCompleted = false
            };
            
            await _appointmentRepository.CreateAppointmentAsync(newAppointment);

          
            var patient = await context.Patients
                .Where(x => x.PatientId == dto.PatientId)
                .Select(x => new
                {
                    x.User.Email,
                    FullName = x.User.FirstName + " " + x.User.LastName
                })
                .FirstOrDefaultAsync();

           
            var doctor = await context.Doctors
                .Where(x => x.DoctorId == doctorId)
                .Select(x => new
                {
                    FullName = x.User.FirstName + " " + x.User.LastName,
                    DepartmentName = x.Department.Name
                })
                .FirstOrDefaultAsync();


            string subject = "Medinova - Randevunuz Oluşturuldu";

            string mailBody = $@"
<div style='background-color:#f4f6f9; padding:30px; font-family:Segoe UI, Arial, sans-serif;'>

    <div style='max-width:600px; margin:auto; background:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 5px 15px rgba(0,0,0,0.05);'>

        <!-- HEADER -->
        <div style='background-color:#198754; padding:25px; text-align:center;'>

            <img src='https://i.hizliresim.com/nvqbi32.png' 
                 alt='Medinova Hastanesi' 
                 style='height:65px; margin-bottom:10px;' />

            <h2 style='color:white; margin:0;'>Randevunuz Oluşturuldu</h2>
            <p style='color:white; font-size:13px; margin:5px 0 0 0;'>Medinova Hastanesi</p>

        </div>

        <!-- CONTENT -->
        <div style='padding:30px;'>

            <p style='font-size:14px; color:#555;'>
                Sayın <b>{patient.FullName}</b>,
            </p>

            <p style='font-size:14px; color:#555;'>
                Randevunuz başarıyla oluşturulmuştur.
            </p>

            <table style='width:100%; font-size:14px; margin-top:20px; border-collapse:collapse;'>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Tarih</b></td>
                    <td style='padding:8px;'>{dto.AppointmentDate:dd.MM.yyyy}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Saat</b></td>
                    <td style='padding:8px;'>{dto.AppointmentTime}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Doktor</b></td>
                    <td style='padding:8px;'>Dr. {doctor.FullName}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Bölüm</b></td>
                    <td style='padding:8px;'>{doctor.DepartmentName}</td>
                </tr>

            </table>

            <p style='margin-top:25px; font-size:14px; color:#777;'>
                Lütfen randevu saatinden <b>15 dakika önce</b> hastanede olunuz.
            </p>

            <p style='font-size:14px;'>
                Sağlıklı günler dileriz.<br/>
                <b>Medinova Hastanesi</b>
            </p>

        </div>

        <!-- FOOTER -->
        <div style='background:#f1f1f1; padding:15px; text-align:center; font-size:12px; color:#888;'>
            © {DateTime.Now.Year} Medinova Hastanesi – Tüm Hakları Saklıdır
        </div>

    </div>

</div>
";

            await _mailService.SendMailAsync(
                patient.Email,
                subject,
                mailBody
            );


        }

        public async Task CreateAppointmentByDoctorByPatientAsync(CreateAppointmentDto dto, int doctorId, int patientId)
        {
            var patientActive = await context.Patients
                .AnyAsync(x => x.PatientId == patientId && x.User.IsActive);

            if (!patientActive)
                throw new Exception("Hasta profil durumu aktif değil.");

            var today = DateTime.Today;
            var maxDate = today.AddDays(10);

            if (dto.AppointmentDate < today || dto.AppointmentDate > maxDate)
                throw new Exception("Sadece önümüzdeki 10 gün için randevu oluşturabilirsiniz.");

            if (dto.AppointmentDate.DayOfWeek == DayOfWeek.Saturday ||
                dto.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
                throw new Exception("Haftasonu için randevu oluşturulamaz.");


            var hasSameDay = await context.Appointments
                .AnyAsync(x => x.PatientId == patientId
                            && x.IsActive
                            && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date);

            if (hasSameDay)
                throw new Exception("Hasta aynı gün ikinci randevu kaydı oluşturamaz.");


            var isAppointmentFull = await context.Appointments
                .AnyAsync(x => x.DoctorId == doctorId
                            && x.IsActive
                            && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date
                            && x.AppointmentTime == dto.AppointmentTime);

            if (isAppointmentFull)
                throw new Exception("Bu saat için zaten aktif randevu bulunmaktadır.");


            var canceledAppointment = await context.Appointments
                .FirstOrDefaultAsync(x => x.DoctorId == doctorId
                                        && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date
                                        && x.AppointmentTime == dto.AppointmentTime
                                        && !x.IsActive
                                        && !x.IsCompleted);

            if (canceledAppointment != null)
            {
                canceledAppointment.PatientId = patientId;
                canceledAppointment.IsActive = true;
                canceledAppointment.CreatedDate = DateTime.Now;

                await context.SaveChangesAsync();
                return;
            }

            var newAppointment = new Appointment
            {
                DoctorId = doctorId,
                PatientId = patientId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsCompleted = false
            };

            await _appointmentRepository.CreateAppointmentAsync(newAppointment);


            var patient = await context.Patients
                .Where(x => x.PatientId == patientId)
                .Select(x => new
                {
                    x.User.Email,
                    FullName = x.User.FirstName + " " + x.User.LastName
                })
                .FirstOrDefaultAsync();


            var doctor = await context.Doctors
                .Where(x => x.DoctorId == doctorId)
                .Select(x => new
                {
                    FullName = x.User.FirstName + " " + x.User.LastName,
                    DepartmentName = x.Department.Name
                })
                .FirstOrDefaultAsync();


            string subject = "Medinova - Randevunuz Oluşturuldu";

            string mailBody = $@"
<div style='background-color:#f4f6f9; padding:30px; font-family:Segoe UI, Arial, sans-serif;'>

    <div style='max-width:600px; margin:auto; background:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 5px 15px rgba(0,0,0,0.05);'>

        <!-- HEADER -->
        <div style='background-color:#198754; padding:25px; text-align:center;'>

            <img src='https://i.hizliresim.com/nvqbi32.png' 
                 alt='Medinova Hastanesi' 
                 style='height:65px; margin-bottom:10px;' />

            <h2 style='color:white; margin:0;'>Randevunuz Oluşturuldu</h2>
            <p style='color:white; font-size:13px; margin:5px 0 0 0;'>Medinova Hastanesi</p>

        </div>

        <!-- CONTENT -->
        <div style='padding:30px;'>

            <p style='font-size:14px; color:#555;'>
                Sayın <b>{patient.FullName}</b>,
            </p>

            <p style='font-size:14px; color:#555;'>
                Randevunuz başarıyla oluşturulmuştur.
            </p>

            <table style='width:100%; font-size:14px; margin-top:20px; border-collapse:collapse;'>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Tarih</b></td>
                    <td style='padding:8px;'>{dto.AppointmentDate:dd.MM.yyyy}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Saat</b></td>
                    <td style='padding:8px;'>{dto.AppointmentTime}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Doktor</b></td>
                    <td style='padding:8px;'>Dr. {doctor.FullName}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Bölüm</b></td>
                    <td style='padding:8px;'>{doctor.DepartmentName}</td>
                </tr>

            </table>

            <p style='margin-top:25px; font-size:14px; color:#777;'>
                Lütfen randevu saatinden <b>15 dakika önce</b> hastanede olunuz.
            </p>

            <p style='font-size:14px;'>
                Sağlıklı günler dileriz.<br/>
                <b>Medinova Hastanesi</b>
            </p>

        </div>

        <!-- FOOTER -->
        <div style='background:#f1f1f1; padding:15px; text-align:center; font-size:12px; color:#888;'>
            © {DateTime.Now.Year} Medinova Hastanesi – Tüm Hakları Saklıdır
        </div>

    </div>

</div>
";

            await _mailService.SendMailAsync(
                patient.Email,
                subject,
                mailBody
            );

        }

        public async Task CreateAppointmentByPatientAsync(CreateAppointmentDto dto, int patientId)
        {
           

            var today = DateTime.Today;
            var maxDate = today.AddDays(10);

            if (dto.AppointmentDate < today || dto.AppointmentDate > maxDate)
                throw new Exception("Sadece önümüzdeki 10 gün için randevu oluşturabilirsiniz.");

            if (dto.AppointmentDate.DayOfWeek == DayOfWeek.Saturday ||
                dto.AppointmentDate.DayOfWeek == DayOfWeek.Sunday)
                throw new Exception("Haftasonu için randevu oluşturulamaz.");


            var hasSameDay = await context.Appointments
                .AnyAsync(x => x.PatientId == patientId
                            && x.IsActive
                            && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date);

            if (hasSameDay)
                throw new Exception("Hasta aynı gün ikinci randevu kaydı oluşturamaz.");


            var isAppointmentFull = await context.Appointments
                     .AnyAsync(x => x.IsActive
                 && x.DoctorId == dto.DoctorId
                 && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date
                 && x.AppointmentTime == dto.AppointmentTime);


            if (isAppointmentFull)
                throw new Exception("Bu saat için zaten aktif randevu bulunmaktadır.");

            var canceledAppointment = await context.Appointments
                .FirstOrDefaultAsync(x =>
                     x.DoctorId == dto.DoctorId
                    && DbFunctions.TruncateTime(x.AppointmentDate) == dto.AppointmentDate.Date
                    && x.AppointmentTime == dto.AppointmentTime
                    && !x.IsActive
                    && !x.IsCompleted);


            if (canceledAppointment != null)
            {
                canceledAppointment.PatientId = patientId;
                canceledAppointment.IsActive = true;
                canceledAppointment.CreatedDate = DateTime.Now;

                await context.SaveChangesAsync();
                return;
            }


            var newAppointment = new Appointment
            {
                DoctorId = dto.DoctorId,
                PatientId = patientId,
                AppointmentDate = dto.AppointmentDate,
                AppointmentTime = dto.AppointmentTime,
                CreatedDate = DateTime.Now,
                IsActive = true,
                IsCompleted = false
            };

            await _appointmentRepository.CreateAppointmentAsync(newAppointment);

            var patient = await context.Patients
                .Where(x => x.PatientId == patientId)
                .Select(x => new
                {
                    x.User.Email,
                    FullName = x.User.FirstName + " " + x.User.LastName
                })
                .FirstOrDefaultAsync();


            var doctor = await context.Doctors
                .Where(x => x.DoctorId == dto.DoctorId)
                .Select(x => new
                {
                    FullName = x.User.FirstName + " " + x.User.LastName,
                    DepartmentName = x.Department.Name
                })
                .FirstOrDefaultAsync();


            string subject = "Medinova - Randevunuz Oluşturuldu";

            string mailBody = $@"
<div style='background-color:#f4f6f9; padding:30px; font-family:Segoe UI, Arial, sans-serif;'>

    <div style='max-width:600px; margin:auto; background:#ffffff; border-radius:8px; overflow:hidden; box-shadow:0 5px 15px rgba(0,0,0,0.05);'>

        <!-- HEADER -->
        <div style='background-color:#198754; padding:25px; text-align:center;'>

            <img src='https://i.hizliresim.com/nvqbi32.png' 
                 alt='Medinova Hastanesi' 
                 style='height:65px; margin-bottom:10px;' />

            <h2 style='color:white; margin:0;'>Randevunuz Oluşturuldu</h2>
            <p style='color:white; font-size:13px; margin:5px 0 0 0;'>Medinova Hastanesi</p>

        </div>

        <!-- CONTENT -->
        <div style='padding:30px;'>

            <p style='font-size:14px; color:#555;'>
                Sayın <b>{patient.FullName}</b>,
            </p>

            <p style='font-size:14px; color:#555;'>
                Randevunuz başarıyla oluşturulmuştur.
            </p>

            <table style='width:100%; font-size:14px; margin-top:20px; border-collapse:collapse;'>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Tarih</b></td>
                    <td style='padding:8px;'>{dto.AppointmentDate:dd.MM.yyyy}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Saat</b></td>
                    <td style='padding:8px;'>{dto.AppointmentTime}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Doktor</b></td>
                    <td style='padding:8px;'>Dr. {doctor.FullName}</td>
                </tr>

                <tr>
                    <td style='padding:8px; background:#f8f9fa;'><b>Bölüm</b></td>
                    <td style='padding:8px;'>{doctor.DepartmentName}</td>
                </tr>

            </table>

            <p style='margin-top:25px; font-size:14px; color:#777;'>
                Lütfen randevu saatinden <b>15 dakika önce</b> hastanede olunuz.
            </p>

            <p style='font-size:14px;'>
                Sağlıklı günler dileriz.<br/>
                <b>Medinova Hastanesi</b>
            </p>

        </div>

        <!-- FOOTER -->
        <div style='background:#f1f1f1; padding:15px; text-align:center; font-size:12px; color:#888;'>
            © {DateTime.Now.Year} Medinova Hastanesi – Tüm Hakları Saklıdır
        </div>

    </div>

</div>
";

            await _mailService.SendMailAsync(
                patient.Email,
                subject,
                mailBody
            );

        }

        public async Task<List<ResultAppointmentDto>> GetListAppointmentAsync()
        {
            var values = await _appointmentRepository.GetListAppointmentAsync();

            return values.Select(x =>
            {

                var dto = new ResultAppointmentDto
                {


                    AppointmentId = x.AppointmentId,
                    DoctorId = x.DoctorId,
                    DoctorTitle = x.Doctor.Title,
                    DoctorName = x.Doctor.User.FirstName,
                    DoctorLastName = x.Doctor.User.LastName,
                    DoctorBranchName = x.Doctor.Department.Name,
                    PatientId = x.PatientId,
                    PatientName = x.Patient.User.FirstName,
                    PatientLastName = x.Patient.User.LastName,
                    AppointmentDate = x.AppointmentDate,
                    IsActive = x.IsActive,
                    AppointmentTime = x.AppointmentTime,
                    IsCompleted = x.IsCompleted

                };

                if (x.IsActive == true && x.IsCompleted == false)
                {
                    dto.StatusText = "Yaklaşan";
                    dto.BadgeClass = "bg-primary";
                    dto.IconClass = "fa-clock text-warning";
                    dto.IconTitle = "Gelecek Randevu";
                    dto.CompletedText = "Gelecek Randevu";
                }
                else if (x.IsActive == false && x.IsCompleted == true)
                {
                    dto.StatusText = "Tamamlandı";
                    dto.BadgeClass = "bg-secondary";
                    dto.IconClass = "fa-check-circle text-success";
                    dto.IconTitle = "Geçmiş Randevu";
                    dto.CompletedText = "Geçmiş Randevu";
                }
                else if (x.IsActive == false && x.IsCompleted == false)
                {
                    dto.StatusText = "İptal Edildi";
                    dto.BadgeClass = "bg-danger";
                    dto.IconClass = "fa-times-circle text-danger";
                    dto.IconTitle = "İptal Edilen Randevu";
                    dto.CompletedText = "İptal Edilen Randevu";
                }


                return dto;
            }).ToList();
        }

        public  async Task<GetAppointmentInfoDto> GetAppointmentAsync(int appointmentId)
        {
            var values = await _appointmentRepository.GetAppointmentAsync(appointmentId);

            var today = DateTime.Now;
            var birthDate = values.Patient.User.BirthDate.Value;
            var age = today.Year - birthDate.Year;
            return new GetAppointmentInfoDto
            {
                DoctorId = values.DoctorId,
                AppointmentId = appointmentId,
                AppointmentDetailId = values.AppointmentDetails.Select(x => x.AppointmentDetailId).FirstOrDefault(),
                DoctorName = values.Doctor.User.FirstName,
                DoctorLastName = values.Doctor.User.LastName,
                DoctorImage = values.Doctor.User.ImageUrl,

                PatientId = values.PatientId,
                PatientTC = values.Patient.User.IdentityNumber,
                PatientImage = values.Patient.User.ImageUrl,
                PatientName = values.Patient.User.FirstName,
                PatientLastName = values.Patient.User.LastName,
                PatientBrans = values.Doctor.Department.Name,
                PatientGender = values.Patient.User.Gender.GenderName,
                BloodType = values.Doctor.User.BloodType,
                PatientBirhtDate = (DateTime)values.Patient.User.BirthDate,
                PatientAge = age,
                PatientEmail = values.Patient.User.Email,
                PatientPhoneNumber = values.Patient.User.PhoneNumber,

                IsActive = values.IsActive,
                IsCompleted = values.IsCompleted,
                AppointmentDate = values.AppointmentDate,
                AppointmentTime = values.AppointmentTime

            };
        }
    }
}