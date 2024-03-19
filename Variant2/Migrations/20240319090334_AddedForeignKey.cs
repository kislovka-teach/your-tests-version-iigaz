using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Variant2.Migrations
{
    /// <inheritdoc />
    public partial class AddedForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_revisions_revisions_next_revision_id",
                table: "revisions");

            migrationBuilder.DropIndex(
                name: "ix_revisions_next_revision_id",
                table: "revisions");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "password_hash",
                value: "GplPIyyrD+GRMKuEKXfuAA==.2fs4Z3cj5iVkXKURST4Q97XTUWu/WBKMQ5g63R04gxI=");

            migrationBuilder.CreateIndex(
                name: "ix_revisions_previous_revision_id",
                table: "revisions",
                column: "previous_revision_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_revisions_revisions_previous_revision_id",
                table: "revisions",
                column: "previous_revision_id",
                principalTable: "revisions",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_revisions_revisions_previous_revision_id",
                table: "revisions");

            migrationBuilder.DropIndex(
                name: "ix_revisions_previous_revision_id",
                table: "revisions");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id",
                keyValue: 1,
                column: "password_hash",
                value: "9fyDyuledEvAA/MLckvdUA==.jk6i3/C5yAVuOpO6ArzhSKAQIQD9XISMWvtLQ89f1Xk=");

            migrationBuilder.CreateIndex(
                name: "ix_revisions_next_revision_id",
                table: "revisions",
                column: "next_revision_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_revisions_revisions_next_revision_id",
                table: "revisions",
                column: "next_revision_id",
                principalTable: "revisions",
                principalColumn: "id");
        }
    }
}
