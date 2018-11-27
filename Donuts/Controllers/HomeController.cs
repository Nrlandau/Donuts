using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Donuts.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            List<KeyValuePair<int, string>> donuts = new List<KeyValuePair<int, string>>();

            HttpWebRequest request = WebRequest.CreateHttp("https://grandcircusco.github.io/demo-apis/donuts.json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string data = sr.ReadToEnd();
            sr.Close();

            JObject jDonuts = JObject.Parse(data);

            foreach(JToken dount in jDonuts["results"])
            {
                donuts.Add(new KeyValuePair<int,string>(dount["id"].Value<int>(), dount["name"].Value<string>()));
            }
            
            //donuts.Add(new KeyValuePair<int, string>(1, "TEST"));
            return View(donuts);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Donut(int donutID)
        {
            HttpWebRequest request = WebRequest.CreateHttp($"https://grandcircusco.github.io/demo-apis/donuts/{donutID}.json");
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            StreamReader sr = new StreamReader(response.GetResponseStream());
            string data = sr.ReadToEnd();
            sr.Close();

            JObject donut = JObject.Parse(data);
            ViewBag.Name = donut["name"];
            ViewBag.Calories = donut["calories"];
            ViewBag.Photo = donut["photo"];
            List<string> extras = new List<string>();
            foreach(JToken extra in donut["extras"])
            {
                extras.Add(extra.Value<string>());
            }
            if(extras.Count ==0)
            {
                extras.Add("NONE");
            }
            ViewBag.Extras = extras;
            return View();
        }
    }
}