using Microsoft.AspNetCore.Mvc;
using ModelLibrary;
using Newtonsoft.Json;
using System.Data.SqlClient;

namespace ApiProductCategory.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class CategoryProductController : ControllerBase
    {
        private readonly ILogger<CategoryProductController> _logger;

        public CategoryProductController(ILogger<CategoryProductController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetCategories")]
        public ActionResult GetCat()
        {
            List<Category> categories = new List<Category>();
            using (SqlCommand command = new SqlCommand("SELECT * FROM [Category];", ConnectionSingleton.Instance.connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        categories.Add(new Category() { Id = reader.GetInt32(0), Name = reader.GetString(1) });
                    }
                }
            }
            return Ok(JsonConvert.SerializeObject(categories));
        }
        [HttpGet(Name = "GetProducts")]
        public ActionResult GetProd()
        {
            try
            {
                List<Product> products = new List<Product>();
                using (SqlCommand command = new SqlCommand("SELECT * FROM [Product];", ConnectionSingleton.Instance.connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new Product() { Id = reader.GetInt32(0), Name = reader.GetString(1), Description = reader.GetString(2), CategoryId = reader.GetInt32(3) });
                        }
                    }
                }
                return Ok(JsonConvert.SerializeObject(products));
            }
            catch
            {
            }
            return Problem();
        }

        [HttpPost(Name = "AddCategories")]
        public ActionResult AddCat(string name)
        {
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO [Category] VALUES('{name}');", ConnectionSingleton.Instance.connection);
                return Ok(command.ExecuteScalar());
            }
            catch
            {
            }
            return Problem();
        }
        [HttpPost(Name = "AddProducts")]
        public ActionResult AddProd(string name, int catId, string description)
        {
            try
            {
                SqlCommand command = new SqlCommand($"INSERT INTO [Product] VALUES('{name}',  '{description}', {catId});", ConnectionSingleton.Instance.connection);
                return Ok(command.ExecuteScalar());
            }
            catch
            {
            }
            return Problem();
        }

        [HttpDelete(Name = "DeleteCategory")]
        public ActionResult DelCat(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM [Category] WHERE [id] = {id};", ConnectionSingleton.Instance.connection);
                return Ok(command.ExecuteScalar());
            }
            catch
            {
            }
            return Problem();
        }
        [HttpDelete(Name = "DeleteProduct")]
        public ActionResult DelProd(int id)
        {
            try
            {
                SqlCommand command = new SqlCommand($"DELETE FROM [Product] WHERE [id] = {id};", ConnectionSingleton.Instance.connection);
                return Ok(command.ExecuteScalar());
            }
            catch
            {
            }
            return Problem();
        }
    }
}