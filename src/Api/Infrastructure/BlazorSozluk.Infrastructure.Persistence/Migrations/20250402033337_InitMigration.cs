using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BlazorSozluk.Infrastructure.Persistence.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "emailConfirmation",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    OldEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewEmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_emailConfirmation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "user",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "entry",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Subject = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entry", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entry_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entryComment",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrtyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryComment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryComment_entry_EnrtyId",
                        column: x => x.EnrtyId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entryComment_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryFavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryFavorite_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entryFavorite_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryVote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryVote_entry_EntryId",
                        column: x => x.EntryId,
                        principalSchema: "dbo",
                        principalTable: "entry",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "entryCommentFavorite",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryCommentFavorite", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryCommentFavorite_entryComment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "dbo",
                        principalTable: "entryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_entryCommentFavorite_user_CreatedById",
                        column: x => x.CreatedById,
                        principalSchema: "dbo",
                        principalTable: "user",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "entryCommentVote",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EntryCommentId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_entryCommentVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_entryCommentVote_entryComment_EntryCommentId",
                        column: x => x.EntryCommentId,
                        principalSchema: "dbo",
                        principalTable: "entryComment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_entry_CreatedById",
                schema: "dbo",
                table: "entry",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entryComment_CreatedById",
                schema: "dbo",
                table: "entryComment",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entryComment_EnrtyId",
                schema: "dbo",
                table: "entryComment",
                column: "EnrtyId");

            migrationBuilder.CreateIndex(
                name: "IX_entryCommentFavorite_CreatedById",
                schema: "dbo",
                table: "entryCommentFavorite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entryCommentFavorite_EntryCommentId",
                schema: "dbo",
                table: "entryCommentFavorite",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entryCommentVote_EntryCommentId",
                schema: "dbo",
                table: "entryCommentVote",
                column: "EntryCommentId");

            migrationBuilder.CreateIndex(
                name: "IX_entryFavorite_CreatedById",
                schema: "dbo",
                table: "entryFavorite",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_entryFavorite_EntryId",
                schema: "dbo",
                table: "entryFavorite",
                column: "EntryId");

            migrationBuilder.CreateIndex(
                name: "IX_entryVote_EntryId",
                schema: "dbo",
                table: "entryVote",
                column: "EntryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "emailConfirmation",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entryCommentFavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entryCommentVote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entryFavorite",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entryVote",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entryComment",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "entry",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "user",
                schema: "dbo");
        }
    }
}
