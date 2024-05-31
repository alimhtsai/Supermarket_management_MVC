namespace WebApp.Models
{
    public static class CategoriesRepository
    {
        // data that seeds in memory storage
        private static List<Category> _categories = new List<Category>()
        {
            new Category { CategoryId = 1, Name = "Cans", Description = "Contains the essential nutrients your kitty needs to support a strong immune system." },
            new Category { CategoryId = 2, Name = "Treats", Description = "Your kitty will love the crunchy outside and soft and creamy inside." },
            new Category { CategoryId = 3, Name = "Toys", Description = "Stimulates your kitty’s natural hunting instincts to spark up play and exercise." }
        };

        public static void AddCategory(Category category)
        {
            var maxId = _categories.Max(x => x.CategoryId);
            category.CategoryId = maxId + 1;
            _categories.Add(category);
        }

        public static List<Category> GetCategories() => _categories;

        public static Category? GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId); // LINQ

            // return a copy, instead of the real data from the database
            if (category != null) 
            { 
                return new Category
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description,                    
                };
            }

            return null;
        }

        public static void UpdateCategory(int categoryId, Category category)
        {
            if (categoryId != category.CategoryId) return;

            var categoryToUpdate = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
            }
        }

        public static void DeleteCategory(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);
            if (category != null)
            {
                _categories.Remove(category);
            }
        }

    }
}