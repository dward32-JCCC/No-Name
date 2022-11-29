using Microsoft.EntityFrameworkCore.Migrations;

namespace GameStuff.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Developers",
                columns: table => new
                {
                    DeveloperId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DevName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Developers", x => x.DeveloperId);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    GenreId = table.Column<string>(maxLength: 25, nullable: false),
                    Name = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.GenreId);
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(maxLength: 200, nullable: false),
                    Price = table.Column<double>(nullable: false),
                    GenreId = table.Column<string>(maxLength: 25, nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.GameId);
                    table.ForeignKey(
                        name: "FK_Games_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "GenreId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GameDevelopers",
                columns: table => new
                {
                    GameId = table.Column<int>(nullable: false),
                    DeveloperId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameDevelopers", x => new { x.GameId, x.DeveloperId });
                    table.ForeignKey(
                        name: "FK_GameDevelopers_Developers_DeveloperId",
                        column: x => x.DeveloperId,
                        principalTable: "Developers",
                        principalColumn: "DeveloperId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameDevelopers_Games_GamesId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "GameId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Developers",
                columns: new[] { "DeveloperId", "DevName" },
                values: new object[,]
                {
                    { 1, "InfinityWard" },
                    { 2, "Creative Assembly" },
                    { 3, "Santa Monica Studio" },
                    { 4, "WB Games Montréal" },
                    { 5, "Electronic Arts" },
                    { 6, "Team Cherry" },
                    { 7, "Extremely OK Games" },
                    { 8, "Visual Concepts" },
                    { 9, "Insomniac Games" },
                    { 10, "Square Enix" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "Name" },
                values: new object[,]
                {
                    { "FPS", "FPS" },
                    { "RTS", "RTS" },
                    { "MMO", "MMO" },
                    { "Action", "Action" },
                    { "Sports", "Sports" },
                    { "Metroidvania", "Metroidvania" },
                    { "Platformer", "Platformer" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "GameId", "GenreId", "Price", "Title", "Description" , "Image" },
                values: new object[,]
                {
                    { 1, "FPS", 69.99, "Call of Duty: Modern Warfare II ", "Call of Duty: Modern Warfare II drops players into an unprecedented global conflict that features the return of the iconic operators of Task Force 141. Modern Warfare® II will launch with a globe-trotting single-player campaign, immersive multiplayer combat, and an evolved special ops game mode featuring tactical co-op gameplay", "mw22.png" },
                    { 2, "RTS", 59.99, "Halo Wars 2", "Halo Wars 2 delivers real-time strategy at the speed of Halo combat. Get ready to lead armies against a terrifying new foe on the biggest Halo battlefield ever", "Halo.jpg"},
                    { 3, "Action", 69.99, "God of War Ragnarok" , "Embark on a mythic journey for answers and allies before Ragnarök arrives.", "godofwar.jpg"},
                    { 4, "Action", 69.99, "Gotham Knights" , "Gotham Knights is a brand-new open world, third-person action RPG featuring the Batman Family as players step into the roles of Batgirl, Nightwing, Red Hood and Robin, a new guard of trained DC Super Heroes who must rise up as the protectors of Gotham City in the wake of Batman’s death. An expansive, criminal underworld has swept through the streets of Gotham, and it is now up to these new heroes to protect the city, bring hope to its citizens, discipline to its cops and fear to its criminals. Players must save Gotham from descent into chaos and reinvent themselves into their own version of the Dark Knight.", "gothamKnights.jpg"},
                    { 5, "Sports", 19.99, "Madden NFL 22" , "Madden NFL 22 is where gameday happens. It's everything you love about the NFL injected into every mode via ALL-NEW Dynamic Gameday.\r\nBrought to life by Next Gen Stats Star-Driven AI and Gameday Atmosphere elements, Dynamic Gameday delivers the smartest gameplay experience ever, all powered by real life data.", "madden22.jpg"},
                    { 6, "RTS", 59.99, "Total War: Warhammer III", "Total War: Warhammer III is a turn-based strategy and real-time tactics video game developed by Creative Assembly and published by Sega. It is part of the Total War series, and the third to be set in Games Workshop's Warhammer Fantasy fictional universe.", "warhammeriii.jpg" },
                    { 7, "Metroidvania", 14.99, "Hollow Knight", "Hollow Knight is a classically styled 2D action adventure across a vast interconnected world. Explore twisting caverns, ancient cities and deadly wastes; battle tainted creatures and befriend bizarre bugs; and solve ancient mysteries at the kingdom's heart. Includes free content packs!", "hollowknight.jpg" },
                    { 8, "Platformer", 19.99, "Celeste", "Celeste is a 2018 platform game designed, directed and written by Maddy Thorson and programmed by Thorson and Noel Berry. It is a remake of a PICO-8 game of the same name made by Thorson and Berry during a game jam in 2016.", "celeste.png" },
                    { 9,"Sports", 69.99, "NBA 2K23", "Become the MVP of the league with NBA 2K23 on PS5! Play as some of the biggest names in the NBA or write your own legacy in MyCAREER. Redefine the game as you put together your dream team in MyTEAM and develop your skills on the court with authentic gameplay in this iconic basketball video game." , "NBA2K23.jpg"},
                    { 10, "Action", 19.99, "Marvel's Spider-Man", "Starring the world's most iconic Super Hero, Marvel's Spider-Man features the acrobatic abilities, improvisation and web-slinging that the wall-crawler is famous for. This isn’t the Spider-Man you’ve met or ever seen before. This is an experienced Peter Parker who’s more masterful at fighting big crime in New York. At the same time, he’s struggling to balance his chaotic personal life and career while the fate of millions of New Yorkers rest upon his shoulders.Marvel's Spider-Man features your favorite web-slinger in a story unlike any before it. Now a seasoned Super Hero, Peter Parker has been busy keeping crime off the streets as Spider-Man. Just as he's ready to focus on life as Peter, a new villain threatens New York City. Faced with overwhelming odds and higher stakes, Spider-Man must rise up and be greater.", "spiderman.jpg" },
                    { 11, "Action", 19.99, "Marvel's Guardians of the Galaxy", "Set out on an adventure of a lifetime with Marvel’s mightiest band of misfits on PS5. In Marvel’s Guardians of the Galaxy, it’s your turn to lead as a star-lord and save the universe. In this third-person action-adventure game, Some jerk (surely not you) has set off a chain of catastrophic events, and only you can hold the unpredictable Guardians together long enough to fight off total interplanetary meltdown.Use Element Blasters, tag-team beat downs, jet boot-powered dropkicks, nothing's off-limits. If you think it's all going to plan, you're in for a world of surprises, with the consequences of your actions guaranteed to keep the Guardians on their toes. In this original Marvel's Guardians of the Galaxy story, you'll cross paths with powerful new beings and unique takes on iconic characters, all caught in a struggle for the galaxy's fate. It's time to show the universe what you're made of. You got this. Probably.","galaxy.jpg" },
                    {12,"Sports", 39.99, "FIFA 23", "EA SPORTS™ FIFA 23 brings The World’s Game to the pitch, with HyperMotion2 Technology that delivers even more gameplay realism, both the men’s and women’s FIFA World Cup™, the addition of women’s club teams, cross-play features, and more.", "Fifa23.jpg"}
                });

            migrationBuilder.InsertData(
                table: "GameDevelopers",
                columns: new[] { "GameId", "DeveloperId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 2 },
                    { 3, 3 },
                    { 4, 4 },
                    { 5, 5 },
                    { 6, 2 },
                    { 7, 6 },
                    { 8, 7 },
                    { 9, 8 },
                    { 10, 9},
                    { 11, 10},
                    { 12, 5}
                });

            migrationBuilder.CreateIndex(
                name: "IX_GameDevelopers_DeveloperId",
                table: "GameDevelopers",
                column: "DeveloperId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_GenreId",
                table: "Games",
                column: "GenreId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GameDevelopers");

            migrationBuilder.DropTable(
                name: "Developers");

            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Genres");
        }
    }
}
