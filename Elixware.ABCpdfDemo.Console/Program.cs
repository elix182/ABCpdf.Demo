// See https://aka.ms/new-console-template for more information
using Demo.Common.Models.Request;
using Demo.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System.Text;

namespace ABCpdfDemo.ConsoleApp
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            const string OutputParam = "-o";
            try
            {
                var input = ReadInputData();
                if (string.IsNullOrWhiteSpace(input)) 
                {
                    //"No input to parse";
                    return;
                }
                var inputData = ParseInput(input);
                if (inputData == null)
                {
                    Console.Error.WriteLine("Error when parsing input data");
                    return;
                }
                bool outputToFile = args.Length > 0 && args.Contains(OutputParam);
                var builder = Host.CreateApplicationBuilder(args);
                builder.Services.AddDemoServices(builder.Configuration);
                using var host = builder.Build();
                var service = host.Services.GetRequiredService<IDocumentCreatorService>();
                var result = await service.CreateDocumentAsync(inputData);
                if (result.IsError || result.IsDataNull)
                {
                    Console.Error.WriteLine("Error generating document: " + result.Message);
                    foreach(var note in result.Notes)
                    {
                        Console.Error.WriteLine(note);
                    }
                    return;
                }
                var outputData = result.Data!;
                if(outputToFile)
                {
                    string outputFileName = outputData.FileName;
                    var file = File.Create(outputFileName);
                    await file.WriteAsync(outputData.FileData);
                }
                else
                {
                    using var stdout = Console.OpenStandardOutput();
                    stdout.Write(outputData.FileData);
                    stdout.Flush();
                }
                //await host.RunAsync();
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine(ex.ToString());
                Console.Error.WriteLine(ex.StackTrace);
            }
        }

        private static string ReadInputData()
        {
            var builder = new StringBuilder();
            string? s;
            while ((s = Console.ReadLine()) != null)
            {
                builder.Append(s);
            }
            return builder.ToString();
        }

        private static InputDataRequest? ParseInput(string json)
        {
            var inputData = JsonConvert.DeserializeObject<InputDataRequest>(json);
            return inputData;
        }
    }
}