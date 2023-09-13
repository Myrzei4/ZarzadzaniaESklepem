using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZarzadzaniaESklepem.Services;

namespace ZarzadzaniaESklepem.Data;

public class Customer
{
    private int? customerId;
    private string? name;
    private string? password;
    private Cart cart;

    public int? CustomerId
    {
        get { return customerId; }
        set { customerId = value; }
    }

    public string? Name
    {
        get { return name; }
        set { name = value; }
    }

    
    public string? Password
    {
        get { return password; }
        set { password = value; }
    }

    public Cart Cart
    {
        get { return cart; }
        private set { cart = value; }
    }

    public Customer(int customerId, string name, string password, Cart cart)
    {
        CustomerId = customerId;
        Name = name;
        Password = password;
        Cart = cart;
    }

    public bool LogIn(string enteredPassword)
    {
        UserService userService = new UserService();
        return Password == enteredPassword;
    }

    public void PlaceOrder()
    {
        Console.WriteLine($"Order from a client {Name}:");
        Cart.DisplayCart();
        decimal? total = Cart.CalculateTotal();
        Console.WriteLine($"Total order value: {total:C}");
    }
}
