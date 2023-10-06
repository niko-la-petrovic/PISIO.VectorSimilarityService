using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PISIO.VectorSimilarityService.Data.Migrations
{
    /// <inheritdoc />
    public partial class Add_Collection_VectorCount : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "VectorCount",
                table: "Collections",
                type: "bigint",
                nullable: true,
                defaultValue: 0L);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VectorCount",
                table: "Collections");
        }
    }
}
