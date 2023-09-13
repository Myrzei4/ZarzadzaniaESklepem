using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZarzadzaniaESklepem.Data;

public class Product
{
    private int? productId;
    private string? name;
    private decimal? price;

    public int? ProductId
    {
        get { return productId; }
        set { productId = value; }
    }

    public string? Name
    {
        get { return name; }
        set { name = value; }
    }

    public decimal? Price
    {
        get { return price; }
        set { price = value; }
    }

    public Product(int productId, string name, decimal price)
    {
        ProductId = productId;
        Name = name;
        Price = price;
    }

}
public class DiscountProduct : Product
{
    public decimal Discount { get; set; }
    public DiscountProduct(int productId, string name, decimal price, decimal discount) : base(productId, name, price)
    {
        Discount = discount;
    }

}
