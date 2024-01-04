using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Music_Store.Migrations
{
    /// <inheritdoc />
    public partial class musicdatamigrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AlbumTbl",
                columns: table => new
                {
                    albumId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    albumTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumTbl", x => x.albumId);
                });

            migrationBuilder.CreateTable(
                name: "GenreTbl",
                columns: table => new
                {
                    genreId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    genreName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreTbl", x => x.genreId);
                });

            migrationBuilder.CreateTable(
                name: "LanguageTbl",
                columns: table => new
                {
                    languageId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    languageName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LanguageTbl", x => x.languageId);
                });

            migrationBuilder.CreateTable(
                name: "SingerTbl",
                columns: table => new
                {
                    singerId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    singerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    singerBio = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    profilePicture = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SingerTbl", x => x.singerId);
                });

            migrationBuilder.CreateTable(
                name: "SongTbl",
                columns: table => new
                {
                    songId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    songName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    songDescription = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    songRelease = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    songPicture = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    songFilepath = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    genreId = table.Column<int>(type: "int", nullable: false),
                    albumId = table.Column<int>(type: "int", nullable: false),
                    singerId = table.Column<int>(type: "int", nullable: false),
                    languageId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SongTbl", x => x.songId);
                    table.ForeignKey(
                        name: "FK_SongTbl_AlbumTbl",
                        column: x => x.albumId,
                        principalTable: "AlbumTbl",
                        principalColumn: "albumId");
                    table.ForeignKey(
                        name: "FK_SongTbl_GenreTbl",
                        column: x => x.genreId,
                        principalTable: "GenreTbl",
                        principalColumn: "genreId");
                    table.ForeignKey(
                        name: "FK_SongTbl_LanguageTbl",
                        column: x => x.languageId,
                        principalTable: "LanguageTbl",
                        principalColumn: "languageId");
                    table.ForeignKey(
                        name: "FK_SongTbl_SingerTbl",
                        column: x => x.singerId,
                        principalTable: "SingerTbl",
                        principalColumn: "singerId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SongTbl_albumId",
                table: "SongTbl",
                column: "albumId");

            migrationBuilder.CreateIndex(
                name: "IX_SongTbl_genreId",
                table: "SongTbl",
                column: "genreId");

            migrationBuilder.CreateIndex(
                name: "IX_SongTbl_languageId",
                table: "SongTbl",
                column: "languageId");

            migrationBuilder.CreateIndex(
                name: "IX_SongTbl_singerId",
                table: "SongTbl",
                column: "singerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SongTbl");

            migrationBuilder.DropTable(
                name: "AlbumTbl");

            migrationBuilder.DropTable(
                name: "GenreTbl");

            migrationBuilder.DropTable(
                name: "LanguageTbl");

            migrationBuilder.DropTable(
                name: "SingerTbl");
        }
    }
}
