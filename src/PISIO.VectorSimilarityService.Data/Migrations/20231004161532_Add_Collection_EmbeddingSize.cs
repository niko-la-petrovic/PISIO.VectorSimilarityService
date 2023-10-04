using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISIO.VectorSimilarityService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Collection_EmbeddingSize : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmbeddingSize",
                table: "Collections",
                type: "integer",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmbeddingSize",
                table: "Collections");
        }
    }
}
