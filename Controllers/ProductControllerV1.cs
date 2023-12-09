using DemoApp.Models;
using DemoApp.Models.Request;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductControllerV1 : Controller
    {

        private readonly InvoiceContext invoiceContext;

        public ProductControllerV1(InvoiceContext invoiceContext)
        {
            this.invoiceContext = invoiceContext;
        }

        [HttpPost]
        public Product IsertProducts(ProductV1 productData)
        {

            var product = new Product
            {

                Name = productData.Name,
                Price = productData.Price,
                Active = 1,

            };

            invoiceContext.Products.Add(product);

            invoiceContext.SaveChanges();

            return product;

        }

        [HttpDelete]
        public Product DeleteProduct(int id)
        {
            Product product = invoiceContext.Products.Where(x => x.ProductId == id).FirstOrDefault();
            product.Active = 0;
            invoiceContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            invoiceContext.SaveChanges();
            return product;
        }

        [HttpPut]
        public Product updateProduct(ProductV2 productData)
        {
            Product product = invoiceContext.Products.Where(x => x.ProductId == productData.Id).FirstOrDefault();
            product.Price = productData.Precio;
            invoiceContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;

            invoiceContext.SaveChanges();
            return product;
        }

        [HttpDelete]
        public List<Product> deleteListProducts(List<Product> products)
        {

            List<Product> productsList = products.ToList();

            foreach (var product in products)
            {
                if (invoiceContext.Products.Find(product) != null)
                {
                    product.Active = 0;
                    invoiceContext.Entry(product).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    invoiceContext.SaveChanges();
                    productsList.Add(product);
                }
                
            }

            return productsList;

        }

       
    }
}
