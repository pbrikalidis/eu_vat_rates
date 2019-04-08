using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EU_VAT_Rates
{
    class Program
    {
        private const string EuRateUri = "http://jsonvat.com/";
        static void Main(string[] args)
        {
            var vatList = GetVatList();
            // Initialize the list of Rate objects
            List<Rate> rateList = new List<Rate>();
            try
            {
                // Try to sort the VAT rate periods. 
                rateList = SortPeriods(vatList.Result);
            }
            catch (Exception e)
            {
                // Very simple error handling
                // Just displaying the error
                Console.WriteLine(e);
            }
            // Find how many countries there are in the list
            var rateCount = rateList.Count;
            // Check if the list was return empty
            if (rateCount > 0)
            {
                // Using LINQ to order the rate list according to the standard VAT of the 
                // first period for each country. We already sorted the periods, so we are
                // going to be fine.
                rateList = rateList.OrderBy(x => x.periods[0].rates.standard).ToList();

                Console.WriteLine();
                Console.WriteLine("Lowest standart VAT Rate:");
                Console.WriteLine();

                for (var i = 0; i < 3; i++)
                {
                    Console.WriteLine(rateList[i].name + " - Standart VAT: " + rateList[i].periods[0].rates.standard + "%");
                }

                Console.WriteLine();
                Console.WriteLine("Highest standart VAT Rate:");
                Console.WriteLine();

                for (var i = rateCount-1; i >= rateCount-3; i--)
                {
                    Console.WriteLine(rateList[i].name + " - Standart VAT: " + rateList[i].periods[0].rates.standard + "%");
                }
            }
            else
            {
                Console.WriteLine("The VAT rate list could not be retrieved.");
            }

            Console.ReadLine();
        }

        /// <summary>
        /// A task that represents the asynchronous operation.
        /// The task result is a RootObject object and contains
        /// the VAT rates of all the countries in the EU.
        /// </summary>
        static async Task<RootObject> GetVatList()
        {
            Console.WriteLine("Retrieving VAT rates.....");
            var vatList = new RootObject();
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                // HTTP GET
                try
                {
                    HttpResponseMessage response = await client.GetAsync(EuRateUri);
                    if (response.IsSuccessStatusCode)
                    {
                        vatList = await response.Content.ReadAsAsync<RootObject>();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                      
            }
            return vatList;
        }

        /// <summary>
        /// Orders the VAT Rate periods for each country. Even though the
        /// API we are using will probably return the periods sorted, we should
        /// sort them nontheless.
        /// </summary>
        /// <returns>
        /// A list of Rate objects with the rate periods ordered in descending fashion
        /// </returns>
        static List<Rate> SortPeriods(RootObject vatList)
        {
            foreach (var rate in vatList.rates)
            {
                // No need to convert to DateTime. It can be sorted even as a string 
                // since it is formatted as yyyy-mm-dd.
                rate.periods = rate.periods.OrderByDescending(x => x.effective_from).ToList();
            }

            return vatList.rates;
        }

    }
}
