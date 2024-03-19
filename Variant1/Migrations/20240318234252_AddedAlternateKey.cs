using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Variant1.Migrations
{
    /// <inheritdoc />
    public partial class AddedAlternateKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reviews");

            migrationBuilder.AddUniqueConstraint(
                name: "ak_users_login",
                table: "users",
                column: "login");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "password_hash",
                value: "344Du4Glpd7jea42pglZ3w==.gn0Y9+DjRSbtM/GXJjAhzoDzbhVKd7snv8iXSZAcPis=");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "ak_users_login",
                table: "users");

            migrationBuilder.CreateTable(
                name: "reviews",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    display_id = table.Column<int>(type: "integer", nullable: true),
                    rating = table.Column<int>(type: "integer", nullable: false),
                    text = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reviews", x => x.id);
                    table.ForeignKey(
                        name: "fk_reviews_displays_display_id",
                        column: x => x.display_id,
                        principalTable: "displays",
                        principalColumn: "id");
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "password_hash",
                value: "2UfAU+9jPtWKPu9esgC2MQ==.AZLXTe1DRj0lCQ6MW220DGzfviqASXErKzBhGUSTs7Q=");

            migrationBuilder.CreateIndex(
                name: "ix_reviews_display_id",
                table: "reviews",
                column: "display_id");
        }
    }
}
