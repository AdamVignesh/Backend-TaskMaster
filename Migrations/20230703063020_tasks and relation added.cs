using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace capstone.Migrations
{
    /// <inheritdoc />
    public partial class tasksandrelationadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    task_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    task_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    task_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    file_url = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    deadline = table.Column<DateTime>(type: "datetime2", nullable: false),
                    start_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    end_date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    weightage = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasks", x => x.task_id);
                    table.ForeignKey(
                        name: "FK_UserTasks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TaskProjectRelationTable",
                columns: table => new
                {
                    Task_Project_Relation_id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectsProject_id = table.Column<int>(type: "int", nullable: false),
                    Taskstask_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskProjectRelationTable", x => x.Task_Project_Relation_id);
                    table.ForeignKey(
                        name: "FK_TaskProjectRelationTable_Projects_ProjectsProject_id",
                        column: x => x.ProjectsProject_id,
                        principalTable: "Projects",
                        principalColumn: "Project_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TaskProjectRelationTable_UserTasks_Taskstask_id",
                        column: x => x.Taskstask_id,
                        principalTable: "UserTasks",
                        principalColumn: "task_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TaskProjectRelationTable_ProjectsProject_id",
                table: "TaskProjectRelationTable",
                column: "ProjectsProject_id");

            migrationBuilder.CreateIndex(
                name: "IX_TaskProjectRelationTable_Taskstask_id",
                table: "TaskProjectRelationTable",
                column: "Taskstask_id");

            migrationBuilder.CreateIndex(
                name: "IX_UserTasks_UserId",
                table: "UserTasks",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskProjectRelationTable");

            migrationBuilder.DropTable(
                name: "UserTasks");
        }
    }
}
