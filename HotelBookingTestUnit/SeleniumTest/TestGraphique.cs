using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.IO;

namespace TestGraphique;

[TestClass]
public class TestGraphique
{
    private IWebDriver driver;
    private string baseUrl;
    private Process apiProcess;
    private Process blazorProcess;


    [TestInitialize]
    public async Task Setup()
    {
        // Initialise le pilote Chrome
        await StartBlazorApp();
        driver = new ChromeDriver();
        await StartApi();
    }

    [TestMethod]
    [TestCategory("UITest")]
    public void TestLogin()
    {
        baseUrl = "http://localhost:5000/login";
        driver.Navigate().GoToUrl(baseUrl);
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5000));
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(500000);
        IWebElement revealed = driver.FindElement(By.Id("login"));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500));
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

        wait.Until(driver => ((IJavaScriptExecutor)driver).ExecuteScript("return document.readyState").Equals("complete"));


        // Vérifier si l'utilisateur est connecté
        Debug.WriteLine(driver.Url);

        Assert.IsTrue(driver.Url.Contains("/counter"));
        // OU Assert.IsTrue(driver.FindElement(By.Id("element_sur_la_page_de_destination")).Displayed);
    }

    [TestMethod]
    [TestCategory("UITest")]
    public void TestRegister()
    {
        baseUrl = "http://localhost:5000/register";
        driver.Navigate().GoToUrl(baseUrl);
        // Saisir les informations de registration
        WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5000));
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(500000);
        IWebElement usernameField = driver.FindElement(By.Id("userName"));
        wait = new WebDriverWait(driver, TimeSpan.FromSeconds(500000));
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
    public async Task Teardown()
    {
        // Fermer le navigateur
        driver.Quit();
        Process[] processesAPI = Process.GetProcessesByName("UserRegistrationAPI");
        foreach (Process process in processesAPI)
        {
            process.Kill();
        }

        Process[] processesBlazor = Process.GetProcessesByName("dotnet");
        foreach (Process process in processesBlazor)
        {
            Debug.WriteLine(process.Id);
            process.Kill();
            process.WaitForExit();
            process.Dispose();
        }

    }
    
    private async Task StartApi()
    {
        string workingDirectory = Directory.GetCurrentDirectory();

        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
        string apiProjectDirectory = Path.Combine(projectDirectory, "UserRegistrationAPI");
        Debug.WriteLine(apiProjectDirectory);

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
    private async Task StartBlazorApp()
    {
        string workingDirectory = Directory.GetCurrentDirectory();
        
        string projectDirectory = Directory.GetParent(workingDirectory).Parent.Parent.Parent.FullName;
        string blazorAppDirectory = Path.Combine(projectDirectory, "BlazorAppFrontend");
        Debug.WriteLine(blazorAppDirectory);

        var startInfo = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            RedirectStandardInput = true,
            WorkingDirectory = blazorAppDirectory,
            UseShellExecute = false
        };

        blazorProcess = new Process { StartInfo = startInfo };
        blazorProcess.Start();

        using (var sw = blazorProcess.StandardInput)
        {
            if (sw.BaseStream.CanWrite)
            {
                sw.WriteLine("dotnet run");
            }
        }

        await Task.Delay(5000); // Attendre que l'application Blazor démarre
        int processId = blazorProcess.Id;
        Debug.WriteLine("Processus Blazor démarré avec succès. PID : " + blazorProcess);
    }
}