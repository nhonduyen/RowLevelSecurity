using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RowLevelSecurity.API.Migrations
{
    /// <inheritdoc />
    public partial class initMig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ProductName", "TenantId" },
                values: new object[,]
                {
                    { new Guid("07cb4964-5a23-4dd0-9d4c-94fa5f597d23"), "Product 3", new Guid("8aed0ef4-2f76-4def-bf88-06c6eadfbf15") },
                    { new Guid("12906c21-1359-40d9-afbc-bf3db4ebd2a6"), "Product 5", new Guid("fb0dee6f-86d7-40fc-ac06-5dbdb769551d") },
                    { new Guid("27c3bb5c-695e-41b8-bb55-c9cd3ba8450e"), "Product 2", new Guid("fb0dee6f-86d7-40fc-ac06-5dbdb769551d") },
                    { new Guid("40586ea6-f66d-4633-9a80-17af378ce1f8"), "Product 4", new Guid("06d2940d-ab0c-42dd-8709-5e645e127fc9") },
                    { new Guid("9b2edba5-c748-4380-b8b7-c2ddb7a666d2"), "Product 1", new Guid("06d2940d-ab0c-42dd-8709-5e645e127fc9") },
                    { new Guid("edef1e05-bb1e-40ad-bc8b-686256c42714"), "Product 6", new Guid("8aed0ef4-2f76-4def-bf88-06c6eadfbf15") }
                });

            migrationBuilder.Sql(@"
            CREATE FUNCTION dbo.ProductPredicate(@tenantid uniqueidentifier)  
            RETURNS TABLE  
            WITH SCHEMABINDING  
            AS RETURN SELECT 1 AS fn_securitypredicate_result  
            WHERE @tenantid = CAST(SESSION_CONTEXT(N'tenantid') AS uniqueidentifier);");

            migrationBuilder.Sql(@"
            CREATE SECURITY POLICY dbo.ProductSecPolicy 
            ADD FILTER PREDICATE dbo.ProductPredicate(tenantid) ON dbo.products,
            ADD BLOCK PREDICATE dbo.ProductPredicate(tenantid) ON dbo.products;");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
