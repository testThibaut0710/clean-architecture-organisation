using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using UserRegistrationAPI;

namespace RegisterPageTestGraphique
{
    [TestClass]
    public class RegisterPageTest
    {
        //private static CustomWebApplicationFactoryUserRegistration<Program> factory;
        private IWebDriver driver;
        private string baseUrl;
        private Process apiProcess;


        [AssemblyInitialize]
        public static void AssemblyInit(TestContext context)
        {
            /*factory = new CustomWebApplicationFactoryUserRegistration<Program>();
            var client = factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Configure the base URL to use the client address
            var clientBaseAddress = client.BaseAddress.ToString();
            context.Properties["BaseUrl"] = clientBaseAddress;*/

        }


        [TestInitialize]
        public async Task Setup()
        {
            // Initialise le pilote Chrome
            ChromeOptions options = new ChromeOptions();
            options.BinaryLocation = @"C:\Program Files (x86)\BraveSoftware\Brave-Browser\Application\brave.exe"; // Chemin vers l'exécutable Brave
            driver = new ChromeDriver(options);
            baseUrl = "http://localhost:5136/register";
            driver.Navigate().GoToUrl(baseUrl);
            await StartApi();
        }

        [TestMethod]
        public void TestRegister()
        {
            // Saisir les informations de registration
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(500000);
            IWebElement usernameField = driver.FindElement(By.Id("userName"));
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));
            usernameField.SendKeys("NewTestUser");

            IWebElement passwordField = driver.FindElement(By.Id("passwordClear"));
            passwordField.SendKeys("newpassword");

            IWebElement emailField = driver.FindElement(By.Id("email"));
            emailField.SendKeys("test@example.com");

            IWebElement phoneNumberField = driver.FindElement(By.Id("phoneNumber"));
            phoneNumberField.SendKeys("0673642996");

            IWebElement dateOfBirthField = driver.FindElement(By.Id("dateOfBirth"));
            dateOfBirthField.SendKeys(DateTime.Now.ToString());

            IWebElement roleField = driver.FindElement(By.Id("role"));
            roleField.SendKeys("user");

            // Cliquer sur le bouton de registration
            IWebElement registerButton = driver.FindElement(By.CssSelector("button[type='submit']")); // Sélectionnez le bouton de registration par son texte
            registerButton.Click();

            

            // Attendre que la page se charge
            WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));
            wait2.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
            // Vérifier si la redirection vers la page de login a été effectuée
            Debug.WriteLine(driver.Url);
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(500000);
            IWebElement revealed = driver.FindElement(By.Id("login"));
            WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));
            Debug.WriteLine($"Revealed: {revealed}");
            Assert.IsTrue(driver.Url.Contains("/login"));
        }

        [TestCleanup]
        public void Teardown()
        {
            // Fermer le navigateur
            driver.Quit();
            Process[] processes = Process.GetProcessesByName("UserRegistrationAPI");
            foreach (Process process in processes)
            {
                process.Kill();
            }
        }
        private async Task StartApi()
        {
            var apiProjectDirectory = @"C:\Users\jlol0\source\repos\clean-architecture-v4\UserRegistrationAPI";

            var startInfo = new ProcessStartInfo
            {
                FileName = "cmd.exe",
                RedirectStandardInput = true,
                WorkingDirectory = apiProjectDirectory,
                UseShellExecute = false
            };

            apiProcess = new Process { StartInfo = startInfo };
            apiProcess.Start();

            using (var sw = apiProcess.StandardInput)
            {
                if (sw.BaseStream.CanWrite)
                {
                    sw.WriteLine("dotnet run");
                }
            }
            await Task.Delay(5000);
            int processId = apiProcess.Id;

            Debug.WriteLine("Processus API démarré avec succès" + processId);
        }

    }
}
