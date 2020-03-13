using Microsoft.EntityFrameworkCore.Migrations;

namespace QAForumAPI.Migrations
{
    public partial class AddedVoteStatusPropertyToAnswerClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VoteStatus",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteStatus",
                table: "Answers");
        }
    }
}
