using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using HtmlAgilityPack;
using WebScrapperMVC.Models;

namespace WebScrapperMVC.Controllers
{
    public class NewsController : Controller
    {
        // GET: News
        public ActionResult Index()
        {
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            List<News> news = new List<News>();

            var web = new HtmlWeb();
            var doc = web.Load("https://www.thenews.com.pk/latest/category/world");
            foreach (var item in doc.DocumentNode.SelectNodes("//div[@class='writter-list-item-story']"))
            {
                string title = item.SelectSingleNode(".//h2").InnerText.Trim();
                string details = item.SelectSingleNode(".//a").GetAttributeValue("href", null).Trim();
                string img = item.SelectSingleNode(".//img").GetAttributeValue("src", null).Trim();

                news.Add(new News()
                {
                    title = title,
                    details = details,
                    img = img,
                });

            }
           
            return View(news);
        }
    }
}