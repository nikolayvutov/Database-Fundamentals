using System;
using System.Globalization;
using System.IO;
using System.Linq;
using DatabaseFirst2.Data;
using Remotion.Linq.Clauses;

namespace DatabaseFirst2
{
    public class Program
    {
        public static void Main(string[] args)
        {   
            using (SoftUniDbContext context = new SoftUniDbContext())
            {
                var projects = context.Projects
                    .Select(p => new
                    {
                        p.Name,
                        p.Description,
                        p.StartDate
                    }).Take(10)
                    .OrderBy(p => p.Name).ToArray();



                foreach (var project in projects)
                {
                    Console.WriteLine(project.Name);
                    Console.WriteLine(project.Description);
                    Console.WriteLine(project.StartDate.ToString("M/d/yyyy h:mm:ss tt", CultureInfo.InvariantCulture));
                }
            }    
        }
    }
}
