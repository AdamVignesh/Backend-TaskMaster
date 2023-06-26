using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone.Migrations
{
    /// <inheritdoc />
    public partial class userprojectrelationmodeladded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserProjectRelation",
                columns: table => new
                {
                    User_Project_Relation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectsProject_id = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProjectRelation", x => x.User_Project_Relation_id);
                    table.ForeignKey(
                        name: "FK_UserProjectRelation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserProjectRelation_Projects_ProjectsProject_id",
                        column: x => x.ProjectsProject_id,
                        principalTable: "Projects",
                        principalColumn: "Project_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectRelation_ProjectsProject_id",
                table: "UserProjectRelation",
                column: "ProjectsProject_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProjectRelation_UserId",
                table: "UserProjectRelation",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProjectRelation");
        }
    }
}
