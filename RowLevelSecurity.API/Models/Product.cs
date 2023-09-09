namespace RowLevelSecurity.API.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public Guid TenantId { get; set; }
    }
}
