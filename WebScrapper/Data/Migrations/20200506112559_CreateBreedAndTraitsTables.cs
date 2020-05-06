using Microsoft.EntityFrameworkCore.Migrations;

namespace WebScrapper.Data.Migrations
{
    public partial class CreateBreedAndTraitsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Breeds",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: true),
                    Height = table.Column<string>(nullable: true),
                    Weight = table.Column<string>(nullable: true),
                    LifeSpan = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Traits",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreedId = table.Column<int>(nullable: false),
                    Adaptability = table.Column<int>(nullable: false),
                    DogFriendly = table.Column<int>(nullable: false),
                    SheddingLevel = table.Column<int>(nullable: false),
                    AffectionLevel = table.Column<int>(nullable: false),
                    ExerciseNeeds = table.Column<int>(nullable: false),
                    SocialNeeds = table.Column<int>(nullable: false),
                    AppartmentFriendly = table.Column<int>(nullable: false),
                    Grooming = table.Column<int>(nullable: false),
                    StrangerFriendly = table.Column<int>(nullable: false),
                    BarkingTendencies = table.Column<int>(nullable: false),
                    HealthIssues = table.Column<int>(nullable: false),
                    Territorial = table.Column<int>(nullable: false),
                    CatFriendly = table.Column<int>(nullable: false),
                    Intelligence = table.Column<int>(nullable: false),
                    Trainability = table.Column<int>(nullable: false),
                    ChildFriendly = table.Column<int>(nullable: false),
                    Playfulness = table.Column<int>(nullable: false),
                    WatchdogAbility = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Traits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Traits_Breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Traits_BreedId",
                table: "Traits",
                column: "BreedId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Traits");

            migrationBuilder.DropTable(
                name: "Breeds");
        }
    }
}
