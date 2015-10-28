namespace ProductsCategories.Service.Models
{
    using ProductsCategories.Models;
    using ProductsCategories.Repository.Interfaces;
    using ProductsCategories.Repository.Repositories;
    using System;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    public class ProductModel
    {
        public static Expression<Func<Product, ProductModel>> FromProduct
        {
            get
            {
                return p => new ProductModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    CategoryId = p.CategoryId,
                    CategoryName = GetCategoryName(p.CategoryId),
                    ImageUrl = p.ImageUrl
                };
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        private static string GetCategoryName(int id)
        {
            IRepository<Category> categoryRepo = new MongoRepository<Category>();

            Category category = categoryRepo.GetOne(x => x.Id == id).Result;

            if (category != null)
            {
                return string.Empty;
            }

            return category.Name;
        }
    }
}
    