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
            for (var i = 1; i < 32; i++)
            {
                await page.GoToAsync($"https://localhost:5001/Contact?id={i}");
                await page.TypeAsync("form textarea", "Lorem ipsum dolor sit amet");
                await Task.Delay(1000);
                var navigationTask = page.WaitForNavigationAsync();
                await page.ClickAsync("form button");
                await navigationTask;
                await Task.Delay(1000);
            }
            Console.ReadLine();
        }
    }
}
