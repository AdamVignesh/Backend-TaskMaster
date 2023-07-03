using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone.Migrations
{
    /// <inheritdoc />
    public partial class taskProjectRelationtableadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskProjectRelation",
                columns: table => new
                {
                    Task_Project_Relation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectsProject_id = table.Column<int>(type: "int", nullable: false),
                    Taskstask_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProjectRelation", x => x.Task_Project_Relation_id);
                    table.ForeignKey(
                        name: "FK_TaskProjectRelation_Projects_ProjectsProject_id",
                        column: x => x.ProjectsProject_id,
                        principalTable: "Projects",
                        principalColumn: "Project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskProjectRelation_Tasks_Taskstask_id",
                        column: x => x.Taskstask_id,
                        principalTable: "Tasks",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskProjectRelation_ProjectsProject_id",
                table: "TaskProjectRelation",
                column: "ProjectsProject_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskProjectRelation_Taskstask_id",
                table: "TaskProjectRelation",
                column: "Taskstask_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskProjectRelation");
        }
    }
}
