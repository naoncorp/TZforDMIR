using DANikitinTZ.App_LocalResources;
using DANikitinTZ.Helpers;
using DANikitinTZ.Utils;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DANikitinTZ.Models
{
    public class Person
    {
        DataContext dc = new DataContext();

        [Key]
        [HiddenInput(DisplayValue = false)]
        public virtual int id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "EnterName")]
        [LocalizedDisplayName("Name", NameResourceType = typeof(GlobalRes))]
        public virtual string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(GlobalRes), ErrorMessageResourceName = "Birthday")]
        [LocalizedDisplayName("Birthday", NameResourceType = typeof(GlobalRes))]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString="{0:dd/MM/yyyy}")]
        public virtual DateTime Birthday { get; set; }

        /// <summary>
        /// Добавляем людей в базу данных
        /// </summary>
        /// <param name="PersonList"></param>
        public void AddPeopleDb(List<Person> PersonList) 
        {
            PersonList.ForEach(x => dc.People.Add(x));
            dc.SaveChanges();
        }

        /// <summary>
        /// Получает список пользователей подходящих по буквам из имени
        /// </summary>
        /// <param name="title"></param>
        /// <returns></returns>
        public IEnumerable<Person> GetPersons(string title)
        {
            IEnumerable<Person> result = null;
                result = dc.People.ToList().Where(x => x.Name.StartsWith(title, true, null)).OrderBy(z => z.Name);
            return result;
        }

    }

    public class CountPersons
    {
        public virtual int Count { get; set; }
        public virtual int Age { get; set; }

        /// <summary>
        /// Получает количество человек по возрастам
        /// </summary>
        /// <param name="PersonList"></param>
        /// <returns></returns>
        public IEnumerable<CountPersons> GetNumPersons(List<Person> PersonList)
        {
            var NumPersons =
                     (from person in PersonList
                      let Age = DateTime.Now.Year - person.Birthday.Year
                      group person by Age into PersonsGroupAge
                      orderby PersonsGroupAge.Key
                      select new CountPersons
                      {
                          Age = PersonsGroupAge.Key,
                          Count =
                          (from person2 in PersonsGroupAge
                           select person2.Name).Count()
                      }).ToList();
            
            return NumPersons;
        }
    }


}