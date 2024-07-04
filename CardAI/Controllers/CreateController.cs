using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Experimental.FileAccess;
using OpenAI_API.Completions;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace CardAI.Controllers
{
    public class CreateController : Controller
    {

        public IActionResult Index()
        {
            // Sayfaya Dinamik Yazdırma

            ViewBag.KahramanTurleri = new List<string> { "Savaşçı", "Büyücü", "Hırsız", "Şifacı", "Avcı", "Necromancer", "Paladin", "Barbar", "Druid", "Samuray", "Ninja", "Bard", "Monk", "Ranger", "Warlock", "Sorcerer", "Cleric", "Knight", "Berserker", "Alchemist", "Assassin", "Elementalist", "Shaman", "Vampire", "Werewolf", "Psionic", "Enchanter", "Illusionist", "Templar", "Beastmaster", "Gunslinger", "Swashbuckler", "Pirate", "Witch", "Summoner", "Shadowblade", "Mystic", "Seer", "Artificer", "Oracle" };
            ViewBag.KahramanTurleriENG = new List<string> { "Warrior", "Wizard", "Thief", "Healer", "Hunter", "Necromancer", "Paladin", "Barbarian", "Druid", "Samurai", "Ninja", "Bard", "Monk", "Ranger", "Warlock", "Sorcerer", "Cleric", "Knight", "Berserker", "Alchemist", "Assassin", "Elementalist", "Shaman", "Vampire", "Werewolf", "Psionic", "Enchanter", "Illusionist", "Templar", "Beastmaster", "Gunslinger", "Swashbuckler", "Pirate", "Witch", "Summoner", "Shadowblade", "Mystic", "Seer", "Artificer", "Oracle" };

            ViewBag.DovusTipleri = new List<string> { "Yakın Dövüş", "Uzun Menzilli Dövüş", "Büyüsel Dövüş", "Hileli Dövüş", "Savunma Dövüşü", "Hızlı Saldırı", "Güçlü Vuruş", "Tekniğe Dayalı Dövüş", "Çift Silah Dövüşü", "Ağır Zırh Dövüşü", "Hafif Zırh Dövüşü", "Büyü Kalkanı", "Tuzak Kurma", "Psikolojik Savaş", "Hayalet Dövüşü", "Canavar Dövüşü", "Ruhani Dövüş", "Zehirli Saldırı", "Karanlık Sanatlar", "Işık Sanatları", "Dondurucu Saldırı", "Yanan Saldırı", "Elektrik Saldırısı", "Toprak Saldırısı", "Su Saldırısı", "Hava Saldırısı", "Psişik Saldırı", "Kimyasal Saldırı", "Mekanik Dövüş", "Gölgelerden Saldırı", "Fırlatma Silahları", "Dövüş Sanatları", "Telekinezi Dövüşü", "Çığlık Saldırısı", "Kan Saldırısı", "Zihin Kontrolü", "Asit Saldırısı", "Mistik Dövüş", "İllüzyon Dövüşü", "Rünik Dövüş" };
            ViewBag.DovusTipleriENG = new List<string> { "Melee Combat", "Ranged Combat", "Magic Combat", "Deceptive Combat", "Defense Combat", "Swift Attack", "Powerful Strike", "Technique-Based Combat", "Dual Wielding", "Heavy Armor Combat", "Light Armor Combat", "Magic Shielding", "Trap Setting", "Psychological Warfare", "Ghostly Combat", "Monster Combat", "Spiritual Combat", "Poisonous Attack", "Dark Arts", "Light Arts", "Freezing Attack", "Burning Attack", "Electric Attack", "Earth Attack", "Water Attack", "Air Attack", "Psychic Attack", "Chemical Attack", "Mechanical Combat", "Shadowy Assault", "Projectile Weapons", "Martial Arts", "Telekinetic Combat", "Scream Attack", "Blood Attack", "Mind Control", "Acid Attack", "Mystical Combat", "Illusory Combat", "Runic Combat" };

            ViewBag.EkipmanTipleri = new List<string> { "Kılıç", "Yay", "Büyü Asası", "Mızrak", "Balta", "Hançer", "Çekiç", "Topuz", "Tırpan", "Tüfek", "Tabanca", "Sapan", "Savaş Baltası", "Çift El Kılıç", "Tırpan", "Nunchaku", "Şövalye Kılıcı", "Dikenli Top", "Kancalı Halat", "Enerji Kılıcı", "Lazer Silahı", "Plazma Tüfeği", "Büyülü Kalkan", "Işık Kılıcı", "İki El Topuzu", "Elektrikli Çomak", "Büyü Kitabı", "Rünik Taş", "Kristal Küre", "El Bombası", "Roket", "Telsiz", "Zırhlı Eldiven", "Mühürlü Mektup", "Zehirli Ok", "Kutsal Amulet", "İksir Şişesi", "Şifa Taşı", "Gölge Pelerini", "Kanatlı Ayakkabı" };
            ViewBag.EkipmanTipleriENG = new List<string> { "Sword", "Bow", "Magic Wand", "Spear", "Axe", "Dagger", "Hammer", "Mace", "Scythe", "Rifle", "Pistol", "Slingshot", "War Hammer", "Two-Handed Sword", "Scythe", "Nunchaku", "Knight Sword", "Spiked Ball", "Hooked Rope", "Energy Sword", "Laser Gun", "Plasma Rifle", "Magic Shield", "Light Sword", "Double Mace", "Electric Stick", "Spell Book", "Runic Stone", "Crystal Sphere", "Hand Bomb", "Rocket", "Walkie-Talkie", "Armored Gloves", "Sealed Letter", "Poisonous Arrow", "Holy Amulet", "Elixir Bottle", "Healing Stone", "Shadow Cloak", "Winged Shoes" };

            ViewBag.BuyuTipleri = new List<string> { "Ateş Topu", "Buz Kalkanı", "Yıldırım Çarpması", "Telekinezi", "Zihin Kontrolü", "Hayalet Çağırma", "Şifa Dalgası", "Mana Patlaması", "Görünmezlik", "Zaman Durdurma", "Element Kontrolü", "Ruhani Koruma", "Zehirli Sis", "Işık Işını", "Karanlık Perde", "Psişik Dalga", "Fırtına Çağırma", "Deprem", "Canlı Gölge", "Ruh Bağlama", "Enerji Patlaması", "Şimşek Zinciri", "Su Duvarı", "Alev Dalgası", "Çöl Fırtınası", "Doğa Gücü", "Metal Bükme", "Rünik Patlama", "Ruhani Arınma", "İllüzyon Yaratma", "Gece Görüşü", "Gök Gürültüsü", "Astral Seyahat", "Büyüsel Kopya" };
            ViewBag.BuyuTipleriENG = new List<string> { "Fireball", "Ice Shield", "Lightning Strike", "Telekinesis", "Mind Control", "Ghost Summoning", "Healing Wave", "Mana Explosion", "Invisibility", "Time Stop", "Element Control", "Spiritual Protection", "Toxic Mist", "Light Beam", "Dark Curtain", "Psychic Wave", "Storm Summoning", "Earthquake", "Living Shadow", "Spirit Bond", "Energy Burst", "Lightning Chain", "Water Wall", "Wave of Flame", "Desert Storm", "Nature Power", "Metal Bending", "Runic Explosion", "Spiritual Purification", "Illusion Creation", "Night Vision", "Thunderstorm", "Astral Travel", "Magical Copy" };

            ViewBag.MekanTipleri = new List<string> {"Orman","Dağ","Çöl","Şehir Meydanı","Kale","Mağara","Denizaltı Şehri","Bataklık","Buzul","Antik Harabeler","Göl Kenarı","Gizli Geçit","Kanyon","Volkan","Kule","Tapınak","Sihirli Orman","Gök Ada","Yeraltı Şehri","Bozkır","Rıhtım","Gizemli Ada","Çayır","Harabe Şehir","Laboratuvar","Mezarlık","Sirk","Hava Gemisi","Karavan Kampı","Vahşi Orman","Kum Tepesi","Fırtına Denizi","Tuz Gölü","Büyülü Bahçe","Antik Kütüphane","Uzay İstasyonu","Rüzgar Değirmeni","Tapınak Bahçesi","Düşler Diyarı","Ayna Labirenti"};
            ViewBag.MekanTipleriENG = new List<string> { "Forest", "Mountain", "Desert", "City Square", "Castle", "Cave", "Underwater City", "Swamp", "Glacier", "Ancient Ruins", "Lakeside", "Hidden Passage", "Canyon", "Volcano", "Tower", "Temple", "Enchanted Forest", "Sky Island", "Underground City", "Grassland", "Dock", "Mysterious Island", "Meadow", "Ruined City", "Laboratory", "Graveyard", "Circus", "Airship", "Caravan Camp", "Wilderness", "Sand Dune", "Stormy Sea", "Salt Lake", "Enchanted Garden", "Ancient Library", "Space Station", "Windmill", "Temple Garden", "Realm of Dreams", "Mirror Maze" };

            ViewBag.AracVeBinekTipleri = new List<string> {"At","Ejderha","Unicorn","Dev Kartal","Savaş Arabası","Denizaltı","Hava Gemisi","Sihirli Halı","Kurt","Panter","Gryphon","Dev Kaplumbağa","Mekanik Golem","Büyülü Kayık","Kanatlı Aslan","Direk Beygir","Rüzgar Gemi","Şahin","Antik Otomobil","Roket","Dev Yılan","Dönüşüm Küresi","Hayalet At","Dev Örümcek","Deniz Yılanı","Buz Arabası","Alev Fırtınası","Karanlık Gölge","Bulut Atı","Volkanik Kirpi","Parlayan Yol","Işık Koşucusu","Ay Arabası","Su Arabası","Gizemli Rüzgar","Altın Ejderha","Yıldırım Kartalı","Büyülü Kaplumbağa","Sırtlan","Zümrüdü Anka Kuşu"};
            ViewBag.AracVeBinekTipleriENG = new List<string> { "Horse", "Dragon", "Unicorn", "Giant Eagle", "War Chariot", "Submarine", "Airship", "Magic Carpet", "Wolf", "Panther", "Gryphon", "Giant Tortoise", "Mechanical Golem", "Magic Boat", "Winged Lion", "Dire Horse", "Wind Ship", "Falcon", "Ancient Car", "Rocket", "Giant Snake", "Transformation Sphere", "Ghostly Horse", "Giant Spider", "Sea Serpent", "Ice Carriage", "Fire Storm", "Dark Shadow", "Cloud Horse", "Volcanic Hedgehog", "Shining Path", "Light Runner", "Moon Chariot", "Water Chariot", "Mysterious Wind", "Golden Dragon", "Lightning Eagle", "Magical Tortoise", "Hyena", "Emerald Phoenix" };

            ViewBag.CevreselEtkiler = new List<string> {"Fırtına","Deprem","Hortum","Tsunami","Güneş Tutulması","Ay Tutulması","Şiddetli Rüzgar","Şimşek Fırtınası","Buz Yağmuru","Kum Fırtınası","Çamur Kayması","Kar Fırtınası","Volkan Patlaması","Gelgit","Sis","Aşırı Sıcak","Aşırı Soğuk","Zehirli Sis","Çölleşme","Buzlanma","Yangın","Rüzgar Hortumu","Deniz Kabarması","Gökkuşağı","Ateş Yağmuru","Taş Yağmuru","Büyülü Fırtına","Enerji Dalgası","Gece-Gündüz Döngüsü","Auroralar","Yıldız Yağmuru","Kara Delik","Meteor Yağmuru","Işınlanma","Parlak Gün","Gürleyen Gök","Rüzgar Koridoru","Sonsuz Gece","Ebedi Gün","Hayaletli Rüzgar"};
            ViewBag.CevreselEtkilerENG = new List<string> { "Storm", "Earthquake", "Tornado", "Tsunami", "Solar Eclipse", "Lunar Eclipse", "Strong Wind", "Lightning Storm", "Hailstorm", "Sandstorm", "Mudslide", "Snowstorm", "Volcanic Eruption", "Tide", "Fog", "Extreme Heat", "Extreme Cold", "Toxic Fog", "Desertification", "Icing", "Fire", "Whirlwind", "Surge", "Rainbow", "Fire Rain", "Stone Rain", "Magical Storm", "Energy Wave", "Day-Night Cycle", "Auroras", "Meteor Shower", "Black Hole", "Meteor Rain", "Teleportation", "Bright Day", "Roaring Sky", "Wind Corridor", "Eternal Night", "Eternal Day", "Ghostly Wind" };

            ViewBag.ElementKontrol = new List<string> {"Ateş Kontrolü","Su Kontrolü","Hava Kontrolü","Toprak Kontrolü","Buz Kontrolü","Yıldırım Kontrolü","Bitki Kontrolü","Hayvan Kontrolü","Metal Kontrolü","Gölge Kontrolü","Işık Kontrolü","Ses Kontrolü","Rüzgar Kontrolü","Kum Kontrolü","Zehir Kontrolü","Asit Kontrolü","Lav Kontrolü","Taş Kontrolü","Kristal Kontrolü","Çamur Kontrolü","Ruh Kontrolü","Manyetizma","Psişik Güçler","Kan Kontrolü","Kül Kontrolü","Kar Kontrolü","Sis Kontrolü","Elektrik Kontrolü","Buhar Kontrolü","Alev Kontrolü","Buzul Kontrolü","Titreşim Kontrolü","Yerçekimi Kontrolü","Plazma Kontrolü","Duman Kontrolü","Çiçek Kontrolü","Kök Kontrolü","Fırtına Kontrolü","Zaman Kontrolü","Uzay Kontrolü"};
            ViewBag.ElementKontrolENG = new List<string> { "Fire Control", "Water Control", "Air Control", "Earth Control", "Ice Control", "Lightning Control", "Plant Control", "Animal Control", "Metal Control", "Shadow Control", "Light Control", "Sound Control", "Wind Control", "Sand Control", "Poison Control", "Acid Control", "Lava Control", "Stone Control", "Crystal Control", "Mud Control", "Spirit Control", "Magnetism", "Psychic Powers", "Blood Control", "Ash Control", "Snow Control", "Mist Control", "Electricity Control", "Steam Control", "Fire Control", "Ice Control", "Vibration Control", "Gravity Control", "Plasma Control", "Smoke Control", "Flower Control", "Root Control", "Storm Control", "Time Control", "Space Control" };

            return View();
        }

        [HttpPost]
        public IActionResult GetLore(string allSpecs)
        {
            string APIKey = "sk-proj-XzMbEGGIVryQ1PI0rpd7T3BlbkFJZevBymVhYSIRWfn9XKSI";
            string answer = string.Empty;

            var openAi = new OpenAI_API.OpenAIAPI(APIKey);
            CompletionRequest completion = new CompletionRequest();
            completion.Prompt = "Create a game hero lore using all these features: " + allSpecs;
            completion.Model = OpenAI_API.Models.Model.Davinci;
            completion.MaxTokens = 200;

            var result = openAi.Completions.CreateCompletionAsync(completion);
            foreach (var item in result.Result.Completions)
            {
                answer += item.ToString();
            }

            Console.WriteLine(answer);

            return RedirectToAction("Index");
        }

        
        [HttpPost]
        public async Task<ContentResult> MakeImageRequests(string allSpecs)
        {
            // SSE için event-stream tipi yapma
            Response.ContentType = "text/event-stream";
            Response.Headers.Append("Cache-Control", "no-cache");


            using (var client = new HttpClient())
            {
                // resim için random hash oluşturma
                const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
                Random random = new Random();
                string session_hash = new string(Enumerable.Repeat(chars, 10)
                                            .Select(s => s[random.Next(s.Length)]).ToArray());

                // resim için random seed oluşturma
                const string numbers = "123456789";
                int maxValue = 2147483647;
                int seed;
                do
                {
                    string tempSeed = new string(Enumerable.Repeat(numbers, 10)
                                                .Select(s => s[random.Next(s.Length)]).ToArray());

                    if (int.TryParse(tempSeed, out seed) && seed <= maxValue)
                    {
                        break;
                    }
                } while (true);

                // istek için body oluşturma
                var postUrl = "https://artificialguybr-juggernaut-xl-free-demo.hf.space/queue/join?__theme=light";
                var postBody = new
                {
                    data = new object[]
                    {
                        allSpecs,
                        "(worst quality, low quality, normal quality, lowres, low details, oversaturated, undersaturated, overexposed, underexposed, grayscale, bw, bad photo, bad photography, bad art:1.4), (watermark, signature, text font, username, error, logo, words, letters, digits, autograph, trademark, name:1.2), (blur, blurry, grainy), morbid, ugly, asymmetrical, mutated malformed, mutilated, poorly lit, bad shadow, draft, cropped, out of frame, cut off, censored, jpeg artifacts, out of focus, glitch, duplicate, (airbrushed, cartoon, anime, semi-realistic, cgi, render, blender, digital art, manga, amateur:1.3), (3D ,3D Game, 3D Game Scene, 3D Character:1.1), (bad hands, bad anatomy, bad body, bad face, bad teeth, bad arms, bad legs, deformities:1.3)",
                        seed,
                        1024,
                        1024,
                        7,
                        28,
                        "DPM++ 2M SDE Karras",
                        "1024 x 1024",
                        false,
                        0.55,
                        1.5
                    },
                    event_data = (object)null,
                    fn_index = 9,
                    trigger_id = 7,
                    session_hash = session_hash
                };

                // c# nesnesi body'i json'a çevir
                var postJson = Newtonsoft.Json.JsonConvert.SerializeObject(postBody);

                // json'u http post isteği için paketle
                var postContent = new StringContent(postJson, Encoding.UTF8, "application/json");

                // istek için başlıklar ekle
                client.DefaultRequestHeaders.Add("accept", "*/*");
                client.DefaultRequestHeaders.Add("accept-language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");
                client.DefaultRequestHeaders.Add("priority", "u=1, i");
                client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"124\", \"Opera\";v=\"110\", \"Not-A.Brand\";v=\"99\"");
                client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");

                postContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

                var postResponse = await client.PostAsync(postUrl, postContent);
                var postResponseBody = await postResponse.Content.ReadAsStringAsync();
                Console.WriteLine(session_hash);

                return Content(session_hash);

            }
        }

        [HttpGet]
        public async Task GetImageRequests(string session_hash)
        {
            Response.ContentType = "text/event-stream";
            Response.Headers.Append("Cache-Control", "no-cache");

            try
            {
                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("accept", "*/*");
                    client.DefaultRequestHeaders.Add("accept-language", "tr-TR,tr;q=0.9,en-US;q=0.8,en;q=0.7");
                    client.DefaultRequestHeaders.Add("priority", "u=1, i");
                    client.DefaultRequestHeaders.Add("sec-ch-ua", "\"Chromium\";v=\"124\", \"Opera\";v=\"110\", \"Not-A.Brand\";v=\"99\"");
                    client.DefaultRequestHeaders.Add("sec-ch-ua-mobile", "?0");
                    client.DefaultRequestHeaders.Add("sec-ch-ua-platform", "\"Windows\"");
                    client.DefaultRequestHeaders.Add("sec-fetch-dest", "empty");
                    client.DefaultRequestHeaders.Add("sec-fetch-mode", "cors");
                    client.DefaultRequestHeaders.Add("sec-fetch-site", "same-origin");

                    var getUrl = $"https://artificialguybr-juggernaut-xl-free-demo.hf.space/queue/data?session_hash={session_hash}";

                    var getResponse = await client.GetAsync(getUrl, HttpCompletionOption.ResponseHeadersRead);
                    var stream = await getResponse.Content.ReadAsStreamAsync();

                    using (var reader = new StreamReader(stream))
                    {
                        while (!reader.EndOfStream)
                        {
                            var line = await reader.ReadLineAsync();
                            if (!string.IsNullOrEmpty(line) && line.StartsWith("data: "))
                            {
                                string json = line.Substring(6); // "data: " kısmını çıkar
                                try
                                {
                                    using (JsonDocument document = JsonDocument.Parse(json))
                                    {
                                        JsonElement root = document.RootElement;

                                        if (root.TryGetProperty("progress_data", out JsonElement progressDataElement))
                                        {
                                            foreach (JsonElement progressData in progressDataElement.EnumerateArray())
                                            {
                                                if (progressData.TryGetProperty("index", out JsonElement indexElement))
                                                {
                                                    Console.WriteLine($"Index: {indexElement.GetInt32()}");
                                                    await Response.WriteAsync($"data: {indexElement.GetInt32()}\n\n");
                                                }
                                            }
                                        }

                                        if (root.TryGetProperty("output", out JsonElement outputElement) &&
                                            outputElement.TryGetProperty("data", out JsonElement dataElement))
                                        {
                                            foreach (JsonElement dataItem in dataElement.EnumerateArray())
                                            {
                                                foreach (JsonElement subItem in dataItem.EnumerateArray())
                                                {
                                                    if (subItem.TryGetProperty("image", out JsonElement imageElement) &&
                                                        imageElement.TryGetProperty("url", out JsonElement urlElement))
                                                    {
                                                        Console.WriteLine($"URL: {urlElement.GetString()}");
                                                        await Response.WriteAsync($"data: {urlElement.GetString()}\n\n");
                                                    }
                                                }
                                            }
                                        }

                                        await Response.Body.FlushAsync();
                                    }
                                }
                                catch (JsonException ex)
                                {
                                    Console.WriteLine($"JSON parse error: {ex.Message}");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await Response.WriteAsync($"data: Error: {ex.Message}\n\n");
                await Response.Body.FlushAsync();
            }
            finally
            {
                Response.Body.Close();
            }
        }



        // resim response içerik tipi
        public class Root
        {
            [JsonPropertyName("msg")]
            public string Msg { get; set; }

            [JsonPropertyName("event_id")]
            public string EventId { get; set; }

            [JsonPropertyName("progress_data")]
            public List<ProgressData> ProgressData { get; set; }

            [JsonPropertyName("output")]
            public Output Output { get; set; }
        }

        public class ProgressData
        {
            [JsonPropertyName("index")]
            public int Index { get; set; }

            [JsonPropertyName("length")]
            public int Length { get; set; }

            [JsonPropertyName("unit")]
            public string Unit { get; set; }

            [JsonPropertyName("progress")]
            public object Progress { get; set; }

            [JsonPropertyName("desc")]
            public object Desc { get; set; }
        }

        public class Output
        {
            [JsonPropertyName("data")]
            public List<List<JsonElement>> Data { get; set; }
        }
    }    
}