using ZarzadzaniaESklepem.Data;
using System.Xml;
using Newtonsoft.Json;

namespace ZarzadzaniaESklepem.Services;

public class DataService
{
    private List<DiscountProduct> products;
    private string productsFilePath = "products.json";

    public DataService()
    {
        LoadProducts();
    }

    public void AddProduct(DiscountProduct product)
    {
        products.Add(product);
        SaveProducts();
    }

    public List<DiscountProduct> GetProducts()
    {
        return products;
    }

    private void LoadProducts()
    {
        if (File.Exists("products.json"))
        {
            string json = File.ReadAllText("products.json");
            var deserializedProducts = JsonConvert.DeserializeObject<List<DiscountProduct>>(json);
            if (deserializedProducts != null) 
            {
                products = deserializedProducts;               
            }
        }
        else
        {
            products = new List<DiscountProduct>();
        }
    }

    private void SaveProducts()
    {
        string json = JsonConvert.SerializeObject(products, Newtonsoft.Json.Formatting.Indented);
        File.WriteAllText(productsFilePath, json);
    }
}

