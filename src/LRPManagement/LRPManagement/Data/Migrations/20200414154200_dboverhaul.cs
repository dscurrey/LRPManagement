using Microsoft.EntityFrameworkCore.Migrations;

namespace LRPManagement.Data.Migrations
{
    public partial class dboverhaul : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey
            (
                "FK_Characters_Players_PlayerId",
                "Characters"
            );

            migrationBuilder.DropForeignKey
            (
                "FK_CharacterSkills_Characters_CharId",
                "CharacterSkills"
            );

            migrationBuilder.DropForeignKey
            (
                "FK_CharacterSkills_Skills_SkillId",
                "CharacterSkills"
            );

            migrationBuilder.DropPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills"
            );

            migrationBuilder.DeleteData
            (
                "Players",
                "Id",
                1
            );

            migrationBuilder.DropColumn
            (
                "Name",
                "Players"
            );

            migrationBuilder.DropColumn
            (
                "CharId",
                "CharacterSkills"
            );

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Skills",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int"
                )
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Players",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int"
                )
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>
            (
                "FirstName",
                "Players",
                nullable: true
            );

            migrationBuilder.AddColumn<string>
            (
                "LastName",
                "Players",
                nullable: true
            );

            migrationBuilder.AddColumn<int>
            (
                "CharacterId",
                "CharacterSkills",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AlterColumn<string>
            (
                "Name",
                "Characters",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true
            );

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Characters",
                    nullable: false,
                    oldClrType: typeof(int),
                    oldType: "int"
                )
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>
            (
                "CharacterRef",
                "Characters",
                fixedLength: true,
                maxLength: 10,
                nullable: true
            );

            migrationBuilder.AddPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills",
                new[] {"CharacterId", "SkillId"}
            );

            migrationBuilder.CreateTable
            (
                "Craftables",
                table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Craftables", x => x.Id); }
            );

            migrationBuilder.CreateTable
            (
                "Bond",
                table => new
                {
                    CharacterId = table.Column<int>(nullable: false),
                    ItemId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bond", x => new {x.CharacterId, x.ItemId});
                    table.ForeignKey
                    (
                        "FK_Bond_Characters",
                        x => x.CharacterId,
                        "Characters",
                        "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                    table.ForeignKey
                    (
                        "FK_Bond_Craftables",
                        x => x.ItemId,
                        "Craftables",
                        "Id",
                        onDelete: ReferentialAction.Restrict
                    );
                }
            );

            migrationBuilder.CreateIndex
            (
                "IX_Bond_ItemId",
                "Bond",
                "ItemId"
            );

            migrationBuilder.AddForeignKey
            (
                "FK_Characters_Players",
                "Characters",
                "PlayerId",
                "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey
            (
                "FK_CharacterSkills_Characters",
                "CharacterSkills",
                "CharacterId",
                "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );

            migrationBuilder.AddForeignKey
            (
                "FK_CharacterSkills_Skills",
                "CharacterSkills",
                "SkillId",
                "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict
            );
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey
            (
                "FK_Characters_Players",
                "Characters"
            );

            migrationBuilder.DropForeignKey
            (
                "FK_CharacterSkills_Characters",
                "CharacterSkills"
            );

            migrationBuilder.DropForeignKey
            (
                "FK_CharacterSkills_Skills",
                "CharacterSkills"
            );

            migrationBuilder.DropTable
            (
                "Bond"
            );

            migrationBuilder.DropTable
            (
                "Craftables"
            );

            migrationBuilder.DropPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills"
            );

            migrationBuilder.DropColumn
            (
                "FirstName",
                "Players"
            );

            migrationBuilder.DropColumn
            (
                "LastName",
                "Players"
            );

            migrationBuilder.DropColumn
            (
                "CharacterId",
                "CharacterSkills"
            );

            migrationBuilder.DropColumn
            (
                "CharacterRef",
                "Characters"
            );

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Skills",
                    "int",
                    nullable: false,
                    oldClrType: typeof(int)
                )
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Players",
                    "int",
                    nullable: false,
                    oldClrType: typeof(int)
                )
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>
            (
                "Name",
                "Players",
                "nvarchar(max)",
                nullable: true
            );

            migrationBuilder.AddColumn<int>
            (
                "CharId",
                "CharacterSkills",
                "int",
                nullable: false,
                defaultValue: 0
            );

            migrationBuilder.AlterColumn<string>
            (
                "Name",
                "Characters",
                "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string)
            );

            migrationBuilder.AlterColumn<int>
                (
                    "Id",
                    "Characters",
                    "int",
                    nullable: false,
                    oldClrType: typeof(int)
                )
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey
            (
                "PK_CharacterSkills",
                "CharacterSkills",
                new[] {"CharId", "SkillId"}
            );

            migrationBuilder.InsertData
            (
                "Players",
                new[] {"Id", "Name"},
                new object[] {1, "Test Character"}
            );

            migrationBuilder.AddForeignKey
            (
                "FK_Characters_Players_PlayerId",
                "Characters",
                "PlayerId",
                "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey
            (
                "FK_CharacterSkills_Characters_CharId",
                "CharacterSkills",
                "CharId",
                "Characters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );

            migrationBuilder.AddForeignKey
            (
                "FK_CharacterSkills_Skills_SkillId",
                "CharacterSkills",
                "SkillId",
                "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade
            );
        }
    }
}