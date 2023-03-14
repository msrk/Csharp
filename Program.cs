using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Dynamic;
using System.Text.Json;

public class Meata{
    public String? charset { get; set; }
    public String? name { get; set; }
    public String? content { get; set; }
}
public class DomObject
    {
        public IList<String>? ehchOnes { get; set; }
        public IList<String>? etchtwos { get; set; }
        public IList<String>? ps { get; set; }
        public IList<Meata>? metas { get; set; }
        public String? title { get; set; }
        
    }
class Program
    {
        static void Main(string[] args)
        {
            
            
            ChromeOptions options = new ChromeOptions();
            // path to chrome driver
            IWebDriver driver = new ChromeDriver("C:/chromedriver.exe", options);
           driver.Navigate().GoToUrl("https://cozy-fairy-5394bc.netlify.app/");
           


         
            
            driver.FindElement(By.Id("username")).SendKeys("gainchanger");
            driver.FindElement(By.Id("password")).SendKeys("justdoit");
            driver.FindElement(By.Id("submit")).Click();

      
            dynamic myObject = new ExpandoObject();
            List<string> hones = new List<string>();

            var h1s=driver.FindElements(By.TagName("h1"));

            foreach (var ht in h1s) {
                var tit= ht.Text;
                hones.Add(tit);
            }
       
            var h2s=driver.FindElements(By.TagName("h2"));
            List<string> htows = new List<string>();
            foreach (var h2 in h2s) {
                var tit= h2.Text;
                htows.Add(tit);
            }

            var paragraphs=driver.FindElements(By.TagName("p"));
            List<string> paras = new List<string>();
            foreach (var pa in paragraphs) {
                var tit= pa.Text;
                paras.Add(tit);
            }

            var meatas=driver.FindElements(By.TagName("meta"));
            List<Meata> mets = new List<Meata>();
            foreach (var mt in meatas) {
                var tit= mt.GetAttribute("charset");
                var tit2= mt.GetAttribute("name");
                var tit3= mt.GetAttribute("content");
                Meata m = new Meata();
                m.charset=tit;
                m.name=tit2;
                m.content= tit3;
                mets.Add(m);
            }

            
  
            var domObject = new DomObject
            {
                ehchOnes =  hones.ToArray() ,
                etchtwos = htows.ToArray(),
                ps = paras.ToArray(),
                metas = mets.ToArray(),
                title = driver.Title
            };

            var options4 = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(domObject, options4);

            Console.WriteLine(jsonString);
            driver.Quit();

           


            File.WriteAllTextAsync("WriteText.txt", jsonString);
        }

       
    }

