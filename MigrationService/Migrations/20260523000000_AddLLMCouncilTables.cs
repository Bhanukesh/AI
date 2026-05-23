using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MigrationService.Migrations
{
    /// <inheritdoc />
    public partial class AddLLMCouncilTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    WhatsAppNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsNewUser = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    HistoryBiteIndex = table.Column<int>(type: "int", nullable: false, defaultValue: 0),
                    DateJoined = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    LastSentAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Email",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateTable(
                name: "HistoryBites",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Index = table.Column<int>(type: "int", nullable: false),
                    Era = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoryBites", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeliveryLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Channel = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BriefSummary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeliveryLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DeliveryLogs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DeliveryLogs_UserId",
                table: "DeliveryLogs",
                column: "UserId");

            // Seed HistoryBites
            migrationBuilder.InsertData(
                table: "HistoryBites",
                columns: new[] { "Id", "Index", "Era", "Content" },
                values: new object[,]
                {
                    {
                        Guid.NewGuid(), 1, "1950s",
                        "Alan Turing published 'Computing Machinery and Intelligence' in 1950, introducing the Turing Test — the idea that if a machine could hold a conversation indistinguishable from a human, it could be considered intelligent. This single paper planted the seed for an entirely new field: artificial intelligence."
                    },
                    {
                        Guid.NewGuid(), 2, "1956",
                        "The Dartmouth Conference of 1956, organized by John McCarthy, Marvin Minsky, and others, formally coined the term 'artificial intelligence.' Researchers gathered with the optimistic belief that every aspect of human intelligence could, in principle, be simulated by a machine — a bold claim that would drive (and haunt) the field for decades."
                    },
                    {
                        Guid.NewGuid(), 3, "1970s - First AI Winter",
                        "By the early 1970s, AI research had promised too much and delivered too little. Funding dried up as government agencies grew frustrated with the lack of practical results. This became known as the First AI Winter — a period of reduced funding, skepticism, and stalled progress that lasted nearly a decade."
                    },
                    {
                        Guid.NewGuid(), 4, "1980s - Expert Systems",
                        "The 1980s saw AI's revival through expert systems — programs that encoded human knowledge as rules to solve specific domain problems. Companies invested billions, and the LISP machine industry boomed. But expert systems were brittle: they couldn't learn, couldn't generalize, and required enormous manual effort to maintain."
                    },
                    {
                        Guid.NewGuid(), 5, "Late 1980s - Second AI Winter",
                        "When expert systems collapsed under their own weight in the late 1980s, the Second AI Winter set in. The LISP machine market evaporated, DARPA slashed AI funding, and the term 'AI' became toxic in research circles. Progress quietly continued in academia, but commercial AI had lost credibility entirely."
                    },
                    {
                        Guid.NewGuid(), 6, "1986-2000 - Neural Network Revival",
                        "Geoffrey Hinton's 1986 paper on backpropagation gave neural networks a way to actually learn from data by propagating errors backward through layers. Through the 1990s, Yann LeCun applied this to recognize handwritten digits for the US postal service — the first practical deep learning deployment, reading millions of ZIP codes daily."
                    },
                    {
                        Guid.NewGuid(), 7, "2012 - ImageNet Breakthrough",
                        "In 2012, Alex Krizhevsky, Ilya Sutskever, and Geoffrey Hinton entered AlexNet into the ImageNet competition and won by a margin that shocked the computer vision world. Their deep convolutional neural network cut the error rate nearly in half. It was the moment the research community understood: deep learning was not a curiosity — it was the future."
                    },
                    {
                        Guid.NewGuid(), 8, "2017 - Attention Is All You Need",
                        "Google researchers published 'Attention Is All You Need' in 2017, introducing the Transformer architecture. By replacing recurrence with self-attention mechanisms, Transformers could process entire sequences in parallel, training faster and scaling further than anything before them. Every modern LLM — GPT, Claude, Gemini — descends directly from this paper."
                    },
                    {
                        Guid.NewGuid(), 9, "2020s - The LLM Era",
                        "GPT-3 in 2020 demonstrated that scale alone could produce remarkably capable language models. ChatGPT's launch in November 2022 brought AI to 100 million users in two months — the fastest consumer product adoption in history. Today, AI writes code, diagnoses diseases, composes music, and reasons through complex problems, raising fundamental questions about intelligence, creativity, and what comes next."
                    }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "DeliveryLogs");
            migrationBuilder.DropTable(name: "HistoryBites");
            migrationBuilder.DropTable(name: "Users");
        }
    }
}
