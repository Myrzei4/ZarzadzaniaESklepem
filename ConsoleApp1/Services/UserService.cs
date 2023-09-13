using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZarzadzaniaESklepem.Data;

namespace ZarzadzaniaESklepem.Services
{
    public class UserService
    {
        private List<Customer> customers;
        private string customersFilePath = "customers.json";

        public UserService()
        {
            LoadCustomers();
        }

        public bool AddCustomer(Customer customer, string username)
        {
            if (customers == null)
            {
                customers = new List<Customer>(); // Initializing a list if it hasn't been initialized
            }
            if (!customers.Any(c => c.Name == username))
            {
                customers.Add(customer);
                SaveCustomers();
                return true;
            }
            else 
            {
                Console.WriteLine("User with this name already exists.");
                return false;
            }
           
        }

        /// <summary>
        /// Get customers list
        /// </summary>
        /// <returns></returns>
        public List<Customer> GetCustomers()
        {
            return customers;
        }

        /// <summary>
        /// Load customers from file
        /// </summary>
        private void LoadCustomers()
        {
            if (File.Exists(customersFilePath))
            {
                string json = File.ReadAllText(customersFilePath);
                var deserializedCustomers = JsonConvert.DeserializeObject<List<Customer>>(json);
                if(deserializedCustomers != null)
                {
                    customers = deserializedCustomers;
                }
            }
            else
            {
                customers = new List<Customer>();
            }
        }

        /// <summary>
        /// Save customers to file
        /// </summary>
        public void SaveCustomers()
        {
            string json = JsonConvert.SerializeObject(customers, Formatting.Indented);
            File.WriteAllText(customersFilePath, json);
        }
    }

}
