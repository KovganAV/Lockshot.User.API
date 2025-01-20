using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lockshot.User.API.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoUrlToUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Удаляем изменение типа для Id, если оно есть
            // migrationBuilder.AlterColumn<Guid>(
            //     name: "Id",
            //     table: "Users",
            //     type: "uuid",
            //     nullable: false);

            // Добавляем только новый столбец
            migrationBuilder.AddColumn<string>(
                name: "PhotoUrl",
                table: "Users",
                type: "text",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Удаляем только новый столбец
            migrationBuilder.DropColumn(
                name: "PhotoUrl",
                table: "Users");
        }

    }
}
