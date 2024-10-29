using System;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using static System.Console;

namespace ConsumerAdviceApi
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string url = $"https://api.adviceslip.com/advice";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    WriteLine("Iniciando requisição...");
                    HttpResponseMessage response = await client.GetAsync(url);

                    response.EnsureSuccessStatusCode();

                    string responseBody = await response.Content.ReadAsStringAsync();

                    JObject json = JObject.Parse(responseBody);
                    if (json.ContainsKey("slip") && json["slip"]["advice"] != null)
                    {
                        WriteLine($"Dica do dia: {json["slip"]["advice"]}");
                    }
                    else
                    {
                        WriteLine("O formato da resposta não é o esperado.");
                    }
                }
                catch (HttpRequestException e)
                {
                    WriteLine("Erro na requisição: " + e.Message);
                }
                catch (Exception ex)
                {
                    // Exibe a mensagem completa da exceção para facilitar a depuração
                    WriteLine("Erro: " + ex.ToString());
                }
            }
            ReadKey();
        }
    }
}