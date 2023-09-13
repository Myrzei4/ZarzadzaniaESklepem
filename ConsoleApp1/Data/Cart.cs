using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZarzadzaniaESklepem.Data;

public class Cart
{

    private List<DiscountProduct> cart = new List<DiscountProduct>();


    // Adding a Product to the Cart
    public void AddProduct(DiscountProduct product)
    {
        cart.Add(product);
    }

    // Displaying the contents of the shopping cart
    public void DisplayCart()
    {
        Console.WriteLine("Cart contents:");
        foreach (var product in cart)
        {
            
            Console.WriteLine($"\tProduct: {product.Name, -14} | \tPrice: {product.Price,5:C} | \tDiscount: {product.Discount*100}%");
        }
    }

    // Calculating the total cost of products in the cart
    public decimal? CalculateTotal()
    {
        decimal? total = 0;
        foreach (var product in cart)
        {
            total += product.Price-(product.Price*product.Discount);
        }
        return total;
    }
}
