using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RowLevelSecurity.API.Data;

namespace RowLevelSecurity.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ProductContext _productContext;

        public ProductsController(ILogger<ProductsController> logger, ProductContext productContext)
        {
            _logger = logger;
            _productContext = productContext;
        }

        [HttpGet]
        public async Task<ActionResult> Get(Guid tenantId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Current tenant id: {tenantId}");
            _productContext.TenantId = tenantId;
            var product = await _productContext.Products.AsNoTracking().ToListAsync(cancellationToken);

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult> Product(Guid tenantId, Guid id, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Current tenant id: {tenantId}");
            _productContext.TenantId = tenantId;
            var product = await _productContext.Products.AsNoTracking().FirstOrDefaultAsync(_ => _.Id.Equals(id), cancellationToken);

            if (product  == null)
            {
                return new NoContentResult();
            }

            return Ok(product);
        }

        [HttpGet]
        public async Task<ActionResult> Count(Guid tenantId, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Current tenant id: {tenantId}");
            _productContext.TenantId = tenantId;
            var product = await _productContext.Products.CountAsync(cancellationToken);

            return Ok(product);
        }

        [HttpPatch]
        public async Task<ActionResult> Update(Guid tenantId, Guid id, string productName, CancellationToken cancellationToken)
        {
            _logger.LogInformation($"Current tenant id: {tenantId}");
            _productContext.TenantId = tenantId;
            var product = await _productContext.Products.FirstOrDefaultAsync(_ => _.Id.Equals(id), cancellationToken);

            if (product == null)
            {
                return new NoContentResult();
            }

            product.ProductName = productName.Trim();
            await _productContext.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
    }
}
