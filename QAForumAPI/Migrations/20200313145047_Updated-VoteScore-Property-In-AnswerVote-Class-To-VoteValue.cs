using Microsoft.EntityFrameworkCore.Migrations;

namespace QAForumAPI.Migrations
{
    public partial class UpdatedVoteScorePropertyInAnswerVoteClassToVoteValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteScore",
                table: "AnswerVotes");

            migrationBuilder.AddColumn<short>(
                name: "VoteValue",
                table: "AnswerVotes",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteValue",
                table: "AnswerVotes");

            migrationBuilder.AddColumn<short>(
                name: "VoteScore",
                table: "AnswerVotes",
                type: "smallint",
                nullable: false,
                defaultValue: (short)0);
        }
    }
}
