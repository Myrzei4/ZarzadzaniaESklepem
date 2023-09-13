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
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="productId">Id of Product</param>
    /// <param name="name">Name of product</param>
    /// <param name="price">Product's price</param>
    public Product(int productId, string name, decimal price)
    {
        ProductId = productId;
        Name = name;
        Price = price;
    }

}
/// <summary>
/// Discount product
/// </summary>
public class DiscountProduct : Product
{
    public decimal Discount { get; set; }
    /// <summary>
    /// Ctor
    /// </summary>
    /// <param name="productId"></param>
    /// <param name="name"></param>
    /// <param name="price"></param>
    /// <param name="discount"></param>
    public DiscountProduct(int productId, string name, decimal price, decimal discount) : base(productId, name, price)
    {
        Discount = discount;
    }

}
