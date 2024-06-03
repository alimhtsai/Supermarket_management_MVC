namespace WebApp.Models
{
    public class ProductsRepository
    {
        private static List<Product> _products = new List<Product>()
        {
            new Product { ProductId = 1, CategoryId = 1, Name = "Applaws Mousse Tuna Grain-Free Wet Cat Food", Quantity = 100, Price = 1.99 },
            new Product { ProductId = 2, CategoryId = 1, Name = "Applaws Chicken Breast with Pumpkin Canned Cat Food", Quantity = 200, Price = 1.99 },
            new Product { ProductId = 3, CategoryId = 2, Name = "Temptations MixUps Surfers' Delight Flavor Soft & Crunchy Cat Treats", Quantity = 300, Price = 15.50 },
            new Product { ProductId = 4, CategoryId = 2, Name = "Greenies Feline Oven Roasted Chicken Flavor Adult Dental Cat Treats", Quantity = 100, Price = 4.66 },
            new Product { ProductId = 5, CategoryId = 3, Name = "Frisco Bird with Feathers Teaser Wand Cat Toy with Catnip", Quantity = 100, Price = 4.08 },
            new Product { ProductId = 6, CategoryId = 3, Name = "Quirky Kitty Cute Koi Wand Cat Toy", Quantity = 100, Price = 8.99 }
        };

        public static void AddProduct(Product product)
        {
            if (_products != null && _products.Count > 0)
            {
                var maxId = _products.Max(x => x.ProductId);
                product.ProductId = maxId + 1;
            }
            else
            {
                product.ProductId = 1;
            }

            if (_products == null) _products = new List<Product>();
            _products.Add(product);
        }

        public static List<Product> GetProducts(bool loadCategory = false) 
        {
            if (!loadCategory)
            {
                return _products;
            }
            else
            {
                if (_products != null && _products.Count > 0)
                {
                    _products.ForEach(p => 
                    {
                        if (p.CategoryId.HasValue)
                            p.Category = CategoriesRepository.GetCategoryById(p.CategoryId.Value);
                    });
                }
                return _products??new List<Product>();
            }
        }

        public static Product? GetProductById(int productId, bool loadCategory = false)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                var prod = new Product
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Quantity = product.Quantity,
                    Price = product.Price,
                    CategoryId = product.CategoryId                    
                };
                if (loadCategory && prod.CategoryId.HasValue)
                {
                    prod.Category = CategoriesRepository.GetCategoryById(prod.CategoryId.Value);
                }
                return prod;
            }
            return null;
        }

        public static void UpdateProduct(int productId, Product product)
        {
            if (productId != product.ProductId) return;

            var productToUpdate = _products.FirstOrDefault(x => x.ProductId == productId);
            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Quantity = product.Quantity;
                productToUpdate.Price = product.Price;
                productToUpdate.CategoryId = product.CategoryId;
            }
        }

        public static void DeleteProduct(int productId)
        {
            var product = _products.FirstOrDefault(x => x.ProductId == productId);
            if (product != null)
            {
                _products.Remove(product);
            }
        }

        public static List<Product> GetProductsByCategoryId(int categoryId)
        {
            var products = _products.Where(x => x.CategoryId == categoryId);
            if (products != null)
            {
                return products.ToList();
            }
            else
            {
                return new List<Product>();
            }
        }
    }
}
