using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sh_Unit2
{
    class Doctor
    {
        public int Id;
        public string Name;
        public string Spec;
    }
    class Patient
    {
        public string Name;
        public string Diagnose;
        public int Id;
        public DateTime Date;
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            var doctors = new List<Doctor>()
        {
            new Doctor{Id=1,Name="Kaprov",Spec="Surgeon"},
            new Doctor{Id=2,Name="Larin",Spec="Therapist"},
            new Doctor{Id=3,Name="Smith",Spec="Surgeon"},
            new Doctor{Id=4,Name="Lavrov",Spec="Therapist"},
        };
            var patients = new List<Patient>()
        {
            new Patient{Name="Todov",Diagnose="Cancer",Id=1,Date = new DateTime(2017,5,5)},
            new Patient{Name="Dolov",Diagnose="Angina",Id=2,Date = new DateTime(2018,5,2)},
            new Patient{Name="Merry",Diagnose="Cold",Id=2,Date = new DateTime(2018,5,5)},
            new Patient{Name="Merry",Diagnose="Cold",Id=2,Date = new DateTime(2018,5,6)}
        };
            Console.WriteLine($"Task1:");
            var q1 = from doc in doctors
                     join pat in patients on doc.Id equals pat.Id
                     where doc.Spec == "Therapist"
                     group pat.Diagnose by doc.Spec into pats
                     select pats.Distinct().Count();
            PrintCollection(q1);
            Console.WriteLine("Task2:");
            var q2 = from pat in patients
                     where DateTime.Now.Month == pat.Date.Month
                     group pat.Date by pat.Name into dates
                     where dates.Count() >= 2
                     select dates.Key;
            PrintCollection(q2);
            Console.WriteLine("Task3:");
            var sub3 = from pat in patients
                       where pat.Date.Year == DateTime.Now.Year - 1
                       join doc in doctors on pat.Id equals doc.Id
                       group pat.Name by doc.Name into pats
                       select new { doc=pats.Key, pats = pats.Distinct().Count()};
            var max = sub3.Max(x=>x.pats);
            var q3 = from doc in sub3
                     where doc.pats == max
                     select doc.doc;
            PrintCollection(q3);
        }
        static void PrintCollection(IEnumerable collection)
        {
            foreach (var item in collection)
            {
                Console.WriteLine(item);
            }
        }
    }
}
