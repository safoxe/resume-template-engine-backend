using engine_plugin_backend.Models;
using engine_plugin_backend.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace engine_plugin_backend.Services
{
    public class CompanySiteWebScrapper : IWebScrapper
    {
        public ScaffoldModel GetParsedData(string url)
        {
            var resume = new ScaffoldModel();
            // settings to avoid opening of the browser
            // actually, Selenium is used for testing purposes
            // but in this case it's possible to re-use it for dev-purposes 
            // to avoid the re-inventing of the wheel

            // TO-DO: Think of better solution for parsing dynamic website(written using Angular)
            var options = new ChromeOptions();
            options.AddArgument("headless");
            var service = ChromeDriverService.CreateDefaultService();
            service.HideCommandPromptWindow = true;

            using (IWebDriver driver = new ChromeDriver(service, options))
            {
                WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
                driver.Navigate().GoToUrl(url);
                // wait until main element is ready
                wait.Until(webDriver => webDriver.FindElement(By.ClassName("body")).Displayed);

                // each field with data is marked with an id
                // refernce to mock-company-site repo
                resume.Name = driver.FindElement(By.Id("project_name")).Text;
                resume.Domain = driver.FindElement(By.Id("domain")).Text;
                resume.MainTechnology = driver.FindElement(By.Id("main_technology")).Text;
                resume.Description = driver.FindElement(By.Id("description")).Text;
                resume.AdditionalTechnologies = new System.Collections.Generic.List<string>();
                foreach (var technology in driver.FindElement(By.Id("additional_technologies")).FindElements(By.CssSelector("ul>li")))
                {
                    resume.AdditionalTechnologies.Add(technology.Text);
                }
            }
            return resume;
        }
    }
}