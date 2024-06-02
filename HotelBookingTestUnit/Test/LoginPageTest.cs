using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;

namespace LoginPageTestGraphique;

[TestClass]
public class LoginPageTest
{
    private IWebDriver driver;
    private string baseUrl;
    private Process apiProcess;


    [TestInitialize]
    public async Task Setup()
    {
        // Initialise le pilote Chrome

        ChromeOptions options = new ChromeOptions();
        options.BinaryLocation = @"C:\Program Files (x86)\BraveSoftware\Brave-Browser\Application\brave.exe"; // Chemin vers l'exécutable Brave
        driver = new ChromeDriver();
        baseUrl = "http://localhost:5136/login";
        driver.Navigate().GoToUrl(baseUrl);
        await StartApi();
    }

    [TestMethod]
    public void TestLogin()
    {
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(500000);
        IWebElement revealed = driver.FindElement(By.Id("login"));
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));
        // Ouvrir la page de connexion
        wait.Until(d => revealed.Displayed);
        wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));

        // Saisir les informations d'identification
        IWebElement usernameField = driver.FindElement(By.Id("userName"));
        usernameField.SendKeys("Thibaut");

        IWebElement passwordField = driver.FindElement(By.Id("password"));
        passwordField.SendKeys("azertyuiop");

        // Cliquer sur le bouton de connexion
        IWebElement loginButton = driver.FindElement(By.CssSelector("button[type='submit']")); // Sélectionnez le bouton de soumission par son type
        loginButton.Click();
        wait.Until(driver => driver.Url.Contains("/counter"));
        Debug.WriteLine("Navigated to /counter.");

        WebDriverWait wait2 = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));
        wait2.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));
        WebDriverWait wait3 = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));
        WebDriverWait wait4 = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));

        // Ajouter un wait après le clic sur le bouton de connexion
        // Vérifier si l'utilisateur est connecté

        // Vérifier si l'utilisateur est connecté
        Debug.WriteLine(driver.Url);
        WebDriverWait wait5 = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));

        Assert.IsTrue(driver.Url.Contains("/counter"));
        // OU Assert.IsTrue(driver.FindElement(By.Id("element_sur_la_page_de_destination")).Displayed);
    }
    

    [TestCleanup]
    public async Task Teardown()
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