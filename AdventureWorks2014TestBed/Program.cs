using System;
using System.Data.Entity;
using System.Linq;
using AdventureWorks2014TestBed.Models;

namespace AdventureWorks2014TestBed
{
    class Program
    {
        static void Main(string[] args)
        {
            // Largest & slowest
            LargeObjectWay("Griffin", "Abigail");

            // Middle of the road (nice mix)
            SmallQueryWay("Griffin", "Abigail");

            // Quickest (your mileage may vary depending on database & application landscapes so keep that in mind)
            SeparateQueryWay("Griffin", "Abigail");

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Get back all the data about the object that you may want regardless of if you actually need or use it.
        /// </summary>
        /// <param name="lastName">The last name of the person we are looking for.</param>
        /// <param name="firstName">The first name of the person we are looking for.</param>
        private static void LargeObjectWay(string lastName, string firstName)
        {
            Person person;

            using (var context = new AdventureWorks2014Context())
            {
                // Write out the query & parameters so we can view everything if the debugger is attached
                if (System.Diagnostics.Debugger.IsAttached) context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                // Include all the possible user information we could ever need
                person = context.People
                    .Include(p => p.Employee)
                    .Include(p => p.BusinessEntity)
                    .Include(p => p.BusinessEntityContacts)
                    .Include(p => p.EmailAddresses)
                    .Include(p => p.Password)
                    .Include(p => p.Customers)
                    .Include(p => p.PersonCreditCards)
                    .Include(p => p.PersonPhones)
                    .Single(p => p.LastName == lastName && p.FirstName == firstName);
            }

            Console.WriteLine($"Person: {person.LastName}, {person.FirstName}");
            Console.WriteLine("Phone Numbers:");
            foreach (var phone in person.PersonPhones)
                Console.WriteLine($"....{phone.PhoneNumber}");
        }

        /// <summary>
        /// Create a single query and only include the data that you need for the object.
        /// </summary>
        /// <param name="lastName">The last name of the person we are looking for.</param>
        /// <param name="firstName">The first name of the person we are looking for.</param>
        private static void SmallQueryWay(string lastName, string firstName)
        {
            Person person;

            using (var context = new AdventureWorks2014Context())
            {
                // Write out the query & parameters so we can view everything if the debugger is attached
                if (System.Diagnostics.Debugger.IsAttached) context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                person = context.People
                    .Include(p => p.PersonPhones)
                    .Single(p => p.LastName == lastName && p.FirstName == firstName);
            }

            Console.WriteLine($"Person: {person.LastName}, {person.FirstName}");
            Console.WriteLine("Phone Numbers:");
            foreach (var phone in person.PersonPhones)
                Console.WriteLine($"....{phone.PhoneNumber}");
        }

        /// <summary>
        /// Create multiple separate queries for the data needed but they will be smaller & simplier.
        /// </summary>
        /// <param name="lastName">The last name of the person we are looking for.</param>
        /// <param name="firstName">The first name of the person we are looking for.</param>
        private static void SeparateQueryWay(string lastName, string firstName)
        {
            using (var context = new AdventureWorks2014Context())
            {
                // Write out the query & parameters so we can view everything if the debugger is attached
                if (System.Diagnostics.Debugger.IsAttached) context.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);

                // Note that we aren't including any tables in this query
                var person = context.People
                    .Single(p => p.LastName == lastName && p.FirstName == firstName);

                // As a result of not including the PersonPhones table, we need to run this part of the logic within the context so that it will not fail since it will make a repeat trip to the database.
                Console.WriteLine($"Person: {person.LastName}, {person.FirstName}");
                Console.WriteLine("Phone Numbers:");
                foreach (var phone in person.PersonPhones)
                    Console.WriteLine($"....{phone.PhoneNumber}");
            }
        }
    }
}
