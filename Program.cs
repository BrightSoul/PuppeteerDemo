using System;
using System.Threading.Tasks;
using PuppeteerSharp;

namespace PuppeteerDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);
            using var browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = false,
                IgnoreHTTPSErrors = true,
                Devtools = true
            });
            
            using var page = await browser.NewPageAsync();
            await Task.Delay(8000);
            await page.GoToAsync("https://localhost:5001/Identity/Account/Login");
            await page.TypeAsync("#Input_Email", "severino.padovano@example.com");
            await page.TypeAsync("#Input_Password", "Teacher1!");
            await Task.Delay(3000);
            var navigationTask = page.WaitForNavigationAsync();
            await page.ClickAsync(".container button[type=submit]");
            await navigationTask;

            for (var i = 1; i < 32; i++)
            {
                await page.GoToAsync($"https://localhost:5001/Contact?id={i}");
                await page.TypeAsync("#Question", "Lorem ipsum dolor sit amet");
                await Task.Delay(1000);
                navigationTask = page.WaitForNavigationAsync();
                await page.ClickAsync(".container button[type=submit]");
                await navigationTask;
                await Task.Delay(1000);
            }
            Console.ReadLine();
        }
    }
}
