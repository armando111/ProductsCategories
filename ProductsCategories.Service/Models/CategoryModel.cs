namespace ProductsCategories.Service.Models
{
    using MongoDB.Bson;
    using ProductsCategories.Models;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq.Expressions;

    public class CategoryModel
    {
        public static Expression<Func<Category, CategoryModel>> FromCategory
        {
            get
            {
                return c => new CategoryModel
                {
                    Id = c.Id,
                    Name = c.Name,
                    Description = c.Description
                };
            }
        }

        public int Id { get; set; }
        
        public string Name { get; set; }

        public string Description { get; set; }
    }
}