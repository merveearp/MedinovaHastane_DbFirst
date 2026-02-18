using System.Linq;

namespace Medinova.Services.AIService
{
    using Medinova.DTOs.AIDtos;
    using Medinova.Models;
    using Newtonsoft.Json;
    using System;
    using System.Configuration;
    using System.Net.Http;
    using System.Text;
    using System.Threading.Tasks;

    public class AIService : IAIService
    {
        private readonly MedinovaContext _context;

        private readonly string _apiKey =
     Environment.GetEnvironmentVariable("OpenAIKey")
     ?? ConfigurationManager.AppSettings["OpenAIKey"];



        private readonly string _apiUrl =
            "https://api.openai.com/v1/chat/completions";

        public AIService()
        {
            _context = new MedinovaContext();
        }
        public async Task<AIResponseDto> AskAIAsync(string question)
        {

            var departments = _context.Departments.Where(x => x.IsActive).Select(x => x.Name).ToList();
            var departmentList = string.Join(",", departments);

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");

                var requestBody = new
                {
                    model = "gpt-4o-mini",
                    messages = new[]
                    {
                        new
                        {
                            role = "system",
                           content =
                            "Sen Medinova Hastanesi'nin dijital sağlık danışmanısın. " +
                            "Görevin, hastalara yalnızca genel sağlık bilgilendirmesi yapmak ve uygun uzmanlık alanına yönlendirmektir. " +

                            "Tıbbi teşhis koymazsın, kesin tanı vermezsin, ilaç ismi veya doz önermezsin. " +
                            "Cevapların bilimsel, sade, resmi ve güven verici bir üslupta olmalıdır. " +
                            "Gereksiz sohbet dili kullanma. " +

                          "Cevabını SADECE aşağıdaki formatta üret: " +
                            "1. ... \n" +
                            "2. ... \n" +
                            "3. ... \n" +
                            "4. ... \n" +
                            "5. ... \n" +
                            "6. ... \n" +

                            "Maddeler numara ile başlasın ve her madde yeni satırda olsun. " +
                            "Paragraf formatı kullanma. " +
                            "Maddeler kısa, net ve açıklayıcı olsun. " +

                            "Yanıt şu yapıda ilerlemelidir: " +
                            "1) Olası genel nedenler, " +
                            "2) Evde uygulanabilecek güvenli öneriler, " +
                            "3) Hangi durumda mutlaka doktora başvurulmalı. " +

                            "Şikayetin niteliğine göre en uygun uzmanlık alanını belirle. " +
                            "\"Kas, eklem, boyun, sırt, bel ve uzuv ağrılarında\r\nilk tercih Ortopedi olmalıdır.\r\n\r\nSadece uyuşma, karıncalanma, güç kaybı,\r\ndenge problemi veya nörolojik belirti varsa\r\nNöroloji seçilmelidir.\"\r\n"+
                            "Dahiliye bölümünü yalnızca gerçekten sistemik veya genel durumlarda seç. " +
                            "Kas-iskelet sistemi şikayetlerinde Ortopedi veya Fizik Tedavi, " +
                            "Sinir sistemi şikayetlerinde Nöroloji tercih edilmelidir. " +

                            "Cevabının en sonunda boş bir satır bırak ve yalnızca şu formatta yaz: " +
                            "Department: {yalnızca şu listeden bir isim seç -> " + departmentList + "} " +

                            "Eğer durum aciliyet içeriyorsa bunu net şekilde belirt. " +
                            "Yanıtın kurumsal ve profesyonel bir sağlık danışmanı seviyesinde olmalıdır."


                        },
                        new
                        {
                            role = "user",
                            content = question

                        }
                    },
                    temperature = 0.6
                };

                var content = new StringContent(JsonConvert.SerializeObject(requestBody),Encoding.UTF8,"application/json");
                var response = await client.PostAsync(_apiUrl, content);

                if(!response.IsSuccessStatusCode)
                {
                    return new AIResponseDto
                    {
                        Answer="Tedavi üretilemedi",
                        Department=null
                    };

                }
                var responseString = await response.Content.ReadAsStringAsync();

                dynamic json = JsonConvert.DeserializeObject(responseString);

                
                string aiText = json.choices[0].message.content.ToString();

              
                string matchedDepartment = departments
                    .FirstOrDefault(d => aiText.Contains(d));

               
                return new AIResponseDto
                {
                    Answer = aiText,
                    Department = matchedDepartment
                };
            }
        }
    }

}