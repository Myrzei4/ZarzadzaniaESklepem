using System;
using System.Linq;
using ZarzadzaniaESklepem.Data;
using ZarzadzaniaESklepem.Services;
using ZarzadzaniaESklepem.Enum;
using System.Globalization;

namespace ZarzadzaniaESklepem.App
{
    class Program
    {
        static Cart cart = new Cart();
        static UserService userService = new UserService();
        /// <summary>
        /// Application LogIn screen.
        /// </summary>
        static void Main()
        {
            while (true)
            {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Enter profile");
                Console.WriteLine("2. Register new profile");
                Console.WriteLine("0. Exit");
                Console.Write("Choose an option: ");

                string? choiceAsString = Console.ReadLine();
                try
                {
                    if (choiceAsString == null)
                    {
                        throw new ArgumentNullException(nameof(choiceAsString));
                    }

                    LogInOption choice = (LogInOption)Int32.Parse(choiceAsString);
                    switch (choice)
                    {
                        case LogInOption.LogIn:
                            // LogIn profile
                            Console.Clear();
                            LogIn();
                            Console.WriteLine("Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case LogInOption.Register:
                            // Register new profile
                            Console.Clear();
                            Register();
                            Console.Write("Press any key to continue");
                            Console.ReadKey();
                            Console.Clear();
                            break;

                        case LogInOption.Exit:
                            // Exit app
                            return;
                    }

                }
                catch
                {
                    Console.WriteLine("Invalid choice. Try again.");
                }
            }

            /// <summary>
            /// Application main screen.
            /// </summary>

            static void MainMenu(Customer customer)
            {
                CultureInfo.CurrentCulture = new CultureInfo("pl-PL");
                Cart cart = new Cart();
                Console.WriteLine("Welcome, {0}",customer.Name);
                while (true)
                {
                    Console.WriteLine();
                    Console.WriteLine("Main menu:");
                    Console.WriteLine("1. Show available products");
                    Console.WriteLine("2. Add to cart");
                    Console.WriteLine("3. Place Order");
                    Console.WriteLine("4. Sign out");
                    Console.WriteLine("0. Exit");
                    Console.Write("Choose an option: ");

                    string? choiceAsString = Console.ReadLine();
                    try {
                        if (choiceAsString == null)
                        {
                            throw new ArgumentNullException(nameof(choiceAsString));
                        }
                        MenuOption choice = (MenuOption)Int32.Parse(choiceAsString);

                        switch (choice)
                        {
                            case MenuOption.ShowAvailableProduct:
                                // Show available products                                   
                                DisplayAvailableProducts();
                                break;

                            case MenuOption.AddToCart:
                                // Add product to cart
                                Console.Write("Enter product ID you want to add to your cart: ");
                                string? IDAsString = Console.ReadLine();
                                if (IDAsString != null)
                                {
                                    int ID = Int32.Parse(IDAsString);
                                GetProductByID(ID, customer);
                                userService.SaveCustomers();
                                }
                                break;

                            case MenuOption.PlaceOrder:
                                // Make an order
                                Console.Clear();
                                PlaceOrder(customer);
                                break;

                            case MenuOption.LogOut:
                                // Exit profile                                
                                Console.WriteLine("Exit profile.");
                                return;

                            case MenuOption.Exit:
                                // Exiting the application                               
                                Environment.Exit(0);
                                break;

                            default:
                                Console.WriteLine("Invalid choice. Try again.");
                                break;
                        }
                    }
                    catch
                    {
                        Console.WriteLine("Invalid choice. Try again.");
                    }
                }
            }

            /// <summary>
            /// Loging in account method
            /// </summary>
            static void LogIn()
            {
                Console.Write("Enter username: ");
                string? username = Console.ReadLine();
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();

                if (username != null && password != null) {
                    // Search for a customer by username and password
                    var loggedInCustomer = FindCustomer(username, password);

                    if (loggedInCustomer != null)
                    {
                        // Login Successful
                        Console.WriteLine($"Logged in as {loggedInCustomer.Name}");
                        Console.Write("Press any key to continue");
                        Console.ReadKey();
                        Console.Clear();
                        // Switching to the main screen
                        MainMenu(loggedInCustomer);
                    }
                    else
                    {
                        Console.WriteLine("Invalid credentials.");
                    }
                }
            }
            /// <summary>
            /// Registering new account method
            /// </summary>
            static void Register()
            {
                Console.Write("Enter username: ");
                string? username = Console.ReadLine();
                Console.Write("Enter password: ");
                string? password = Console.ReadLine();
                if (username == null) {
                    throw new ArgumentNullException(nameof(username));
                }
                if (password == null)
                {
                    throw new ArgumentNullException(nameof(password));
                }
                // Create ID for new user
                Random random = new Random();
                int ID = random.Next(0, 99999);
                // Create a new customer and add it to the customer list
                Customer newCustomer = new Customer(ID, username, password, cart);
                if (userService.AddCustomer(newCustomer, username) == true)
                {
                    Console.WriteLine($"Account {newCustomer.Name} registered and logged in.");
                    Console.Write("Press any key to continue");
                    Console.ReadKey();
                    Console.Clear();
                    MainMenu(newCustomer);
                }
            }


            /// <summary>
            /// Display list of available products.
            /// </summary>
            static void DisplayAvailableProducts()
            {
                Console.Clear();
                Console.WriteLine("Available products:");
                // Get product list from DataService
                var productService = new DataService();
                var productsList = productService.GetProducts();

                foreach (var product in productsList)
                {
                    Console.WriteLine($"\tID: {product.ProductId, -2} |\tName: {product.Name, -14} | \tPrice: {product.Price,10:C} | \tDiscount: {product.Discount*100}%");
                }
            }

            
            static void GetProductByID(int productId, Customer customer)
            {
                var productService = new DataService();
                var products = productService.GetProducts();
                
                DiscountProduct? productToAdd = products.Find(p => p.ProductId == productId);
                if (productToAdd != null && customer != null)
                {
                    customer.Cart.AddProduct(productToAdd);
                }

            }

            static Customer? FindCustomer(string username, string password)
            {
                // Finding client by username and password
                return userService.GetCustomers().FirstOrDefault(c => c.Name == username && c.LogIn(password));
                
            }


            static void PlaceOrder(Customer customer)
            {
                customer.PlaceOrder();
            }

        }
    }
}