using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Globalization;
using System.Threading;
using DANikitinTZ.Helpers;
using DANikitinTZ.Models;
using System.IO;

namespace DANikitinTZ.Controllers
{
    public class HomeController : BaseController
    {
        private DataContext db = new DataContext();

        public ActionResult Index(string search = null, int count = 10)
        {
            if (search != null)
            {
                ViewBag.Search = true;
                Person p = new Person();
                IEnumerable<Person> result = p.GetPersons(search);
                ViewBag.CountStrings = result.Count();
                ViewBag.CountView = count;
                if (count != 0)
                {
                    result = result.Take(count);
                }
                
                if (Request.IsAjaxRequest())
                {
                    return PartialView("_LiveSearchResult", result);
                }
                else
                {
                    return View(result);
                }
            }
            else
            {
                return View();
            }
        }


        [HttpPost]
        public PartialViewResult Upload(HttpPostedFileBase file)
        {
            CountPersons cp = new CountPersons();
            Person p = new Person();
            
            string path = null;

            List<Person> PersonList = new List<Person> ();
            try
            {
                if (file.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(file.FileName);
                    path = AppDomain.CurrentDomain.BaseDirectory + "upload\\" + fileName;
                    file.SaveAs(path);

                    StreamReader reader = new StreamReader(path);
                    Stream theStream = reader.BaseStream;

                    var parser = new CSVParser();
                    List<string[]> csvData = parser.ParseFile(theStream);


                    foreach (var c in csvData)
                    {
                        Person dm = new Person();

                        dm.Name = c[0];
                        try
                        {
                            dm.Birthday = DateTime.Parse(c[1]);
                            PersonList.Add(dm);
                        }
                        catch (Exception)
                        {

                            throw;
                        }
                    }
                }

            }
            catch (Exception)
            {
                
                throw;
            }

            p.AddPeopleDb(PersonList);
            IEnumerable<CountPersons> NumPersons = cp.GetNumPersons(PersonList);
            return PartialView("_UploadResult",NumPersons);
        }

        
    }
}
