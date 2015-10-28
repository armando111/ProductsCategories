
namespace ProductsCategories.Service.Controllers
{
    using ProductsCategories.Models;
    using System.Web.Http;
    using Repository.Interfaces;
    using Repository.Repositories;
    using System.Threading.Tasks;
    
    public class CategoriesController : ApiController
    {
        IRepository<Category> categoriesRepo;
        
        public CategoriesController() : this(new MongoRepository<Category>())
        {

        }

        public CategoriesController(IRepository<Category> repository)
        {
            this.categoriesRepo = repository;
        }

        /// GET: api/Categories
        [HttpGet]
        public async Task<IHttpActionResult> GetAllCategories()
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var categories = await categoriesRepo.GetAll();

            return Ok(categories);
        }

        /// GET: api/Categories
        [HttpGet]
        public async Task<IHttpActionResult> GetCategoryById([FromUri]int id)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
                        
            var category = await categoriesRepo.GetOne(c => c.Id == id);

            if ( category == null)
            {
                return BadRequest();
            }
            
            return Ok(category);
        }

        /// GET: api/Categories
        [HttpGet]
        public async Task<IHttpActionResult> GetCategoryByName([FromUri]string name)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var category = await categoriesRepo.GetOne(c => c.Name == name);

            if (category == null)
            {
                return BadRequest();
            }

            return Ok(category);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateCategory([FromBody]Category category)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            category.Id = await categoriesRepo.GetNextId();
            var categoryTmp = await categoriesRepo.GetOne(p => p.Id == category.Id);
            
            if (categoryTmp != null)
            {
                return BadRequest("Such category already exists!");
            }
                        
            categoriesRepo.Insert(category);

            return Ok(category);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateCategory([FromBody]Category category)
        {
            
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = await categoriesRepo.GetOne(c => c.Id == category.Id);

            if (existingCategory == null)
            {
                return BadRequest("Such category does not exists!");
            }

            category._id = existingCategory._id;
            categoriesRepo.Update(c => c.Id == category.Id, category);
            
            return Ok(category);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteCategory([FromUri]int id)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingCategory = await categoriesRepo.GetOne(c => c.Id == id);
            if (existingCategory == null)
            {
                return BadRequest("Such category does not exists!");
            }
            
            categoriesRepo.Delete(existingCategory.Id);
            return Ok("Successfully deleted category !");
        }
    }
}