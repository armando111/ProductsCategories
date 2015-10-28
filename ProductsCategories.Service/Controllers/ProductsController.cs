namespace ProductsCategories.Service.Controllers
{
    using ProductsCategories.Models;
    using ProductsCategories.Repository.Interfaces;
    using ProductsCategories.Repository.Repositories;
    using Models;
    using System.Linq;
    using System.Web.Http;
    using System.Threading.Tasks;

    //[EnableCors(origins: "http://localhost:57757", headers: "*", methods: "*")]
    public class ProductsController : ApiController
    {
        IRepository<Product> productsRepo;
        IRepository<Category> categoiesRepo;

        public ProductsController() : this(new MongoRepository<Product>(), new MongoRepository<Category>())
        {

        }

        public ProductsController(IRepository<Product> repoProducts, IRepository<Category> repoCategories)
        {
            this.productsRepo = repoProducts;
            this.categoiesRepo = repoCategories;
        }

        /// GET: api/Products
        [HttpGet]
        public async Task<IHttpActionResult> GetAllProducts()
        {
            var products = await productsRepo.GetAll();
            
            products.AsQueryable().Select(ProductModel.FromProduct);

            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(products);
        }
        
        /// GET: api/Products
        [HttpGet]
        public IHttpActionResult GetProductById([FromUri]int id)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = productsRepo.GetOne(p => p.Id == id);
            
            if (product == null)
            {
                return BadRequest("The Product does not exist!");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IHttpActionResult> CreateProduct([FromBody]Product product)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            product.Id = await productsRepo.GetNextId();
            var productTmp = await productsRepo
                .GetOne(p => p.Id == product.Id);
            
            if (productTmp != null)
            {
                return BadRequest("Such product already exists!");
            }
            
            productsRepo.Insert(product);
            return Ok(product);
        }

        [HttpPut]
        public async Task<IHttpActionResult> UpdateProduct([FromBody]Product product)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingProduct = await productsRepo.GetOne(p => p.Id == product.Id);

            if (existingProduct == null)
            {
                return BadRequest("Such product does not exists!");
            }

            product._id = existingProduct._id;
            productsRepo.Update(x => x.Id == product.Id, product);
            
            return Ok(product);
        }

        [HttpDelete]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            if (!this.ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = await productsRepo.GetOne(p => p.Id == id);
            if (product == null)
            {
                return BadRequest("Such product does not exists!");
            }

            productsRepo.Delete(product.Id);
            
            return Ok();
        }

        private async Task<string> GetCategoryName(int id)
        {
            if(id == 0)
            {
                return string.Empty;
            }
            IRepository<Category> categoryRepo = new MongoRepository<Category>();

            Category category = await categoryRepo.GetOne(x => x.Id == id);
            
            return category.Name;
        }
    }
}
