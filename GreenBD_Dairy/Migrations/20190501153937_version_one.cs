using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace GreenBD_Dairy.Migrations
{
    public partial class version_one : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AmountSign",
                columns: table => new
                {
                    AmountSignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountSignName = table.Column<string>(maxLength: 10, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AmountSign", x => x.AmountSignId);
                });

            migrationBuilder.CreateTable(
                name: "CowCollection",
                columns: table => new
                {
                    CowCollectionId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CowCollectionName = table.Column<string>(maxLength: 100, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CowCollection", x => x.CowCollectionId);
                });

            migrationBuilder.CreateTable(
                name: "CowGroup",
                columns: table => new
                {
                    CowGroupId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CowGroupName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CowGroup", x => x.CowGroupId);
                });

            migrationBuilder.CreateTable(
                name: "CowPurpose",
                columns: table => new
                {
                    CowPurposeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CowPurposeName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CowPurpose", x => x.CowPurposeId);
                });

            migrationBuilder.CreateTable(
                name: "Days",
                columns: table => new
                {
                    DayId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayName = table.Column<string>(maxLength: 50, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Days", x => x.DayId);
                });

            migrationBuilder.CreateTable(
                name: "FoodType",
                columns: table => new
                {
                    FoodTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    FoodTypeName = table.Column<string>(maxLength: 100, nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodType", x => x.FoodTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Gender",
                columns: table => new
                {
                    GenderId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    GenderName = table.Column<string>(maxLength: 6, nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gender", x => x.GenderId);
                });

            migrationBuilder.CreateTable(
                name: "Investment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<double>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Investment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Month",
                columns: table => new
                {
                    MonthId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    MonthName = table.Column<string>(maxLength: 100, nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Month", x => x.MonthId);
                });

            migrationBuilder.CreateTable(
                name: "NationOfCow",
                columns: table => new
                {
                    CowNationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CowNationName = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NationOfCow", x => x.CowNationId);
                });

            migrationBuilder.CreateTable(
                name: "NumberSign",
                columns: table => new
                {
                    NumberSignId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    NumberSignName = table.Column<string>(maxLength: 20, nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NumberSign", x => x.NumberSignId);
                });

            migrationBuilder.CreateTable(
                name: "PaymentMethod",
                columns: table => new
                {
                    PaymentMethodId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true),
                    PaymentMethodName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethod", x => x.PaymentMethodId);
                });

            migrationBuilder.CreateTable(
                name: "ProductQuality",
                columns: table => new
                {
                    ProductQualityId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true),
                    ProductQualityName = table.Column<string>(maxLength: 25, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductQuality", x => x.ProductQualityId);
                });

            migrationBuilder.CreateTable(
                name: "ProductType",
                columns: table => new
                {
                    ProductTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true),
                    ProductTypeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductType", x => x.ProductTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Rank",
                columns: table => new
                {
                    RankId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true),
                    RankName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rank", x => x.RankId);
                });

            migrationBuilder.CreateTable(
                name: "SpecialistType",
                columns: table => new
                {
                    SpecialistTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true),
                    SpecialistTypeName = table.Column<string>(maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpecialistType", x => x.SpecialistTypeId);
                });

            migrationBuilder.CreateTable(
                name: "TransportType",
                columns: table => new
                {
                    TransportTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    OwnerSignature = table.Column<string>(nullable: true),
                    TransportTypeName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransportType", x => x.TransportTypeId);
                });

            migrationBuilder.CreateTable(
                name: "CowManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CowCollectionId = table.Column<int>(nullable: false),
                    CowGroupId = table.Column<int>(nullable: false),
                    CowName = table.Column<string>(nullable: false),
                    CowNationId = table.Column<int>(nullable: false),
                    CowPurposeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    InitialPrice = table.Column<float>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    OurCodeNo = table.Column<string>(nullable: false),
                    PreCodeNo = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CowManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CowManagement_CowCollection_CowCollectionId",
                        column: x => x.CowCollectionId,
                        principalTable: "CowCollection",
                        principalColumn: "CowCollectionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CowManagement_CowGroup_CowGroupId",
                        column: x => x.CowGroupId,
                        principalTable: "CowGroup",
                        principalColumn: "CowGroupId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CowManagement_NationOfCow_CowNationId",
                        column: x => x.CowNationId,
                        principalTable: "NationOfCow",
                        principalColumn: "CowNationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CowManagement_CowPurpose_CowPurposeId",
                        column: x => x.CowPurposeId,
                        principalTable: "CowPurpose",
                        principalColumn: "CowPurposeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CowManagement_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LandManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountSignId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    LandArea = table.Column<string>(maxLength: 200, nullable: false),
                    Location = table.Column<string>(maxLength: 100, nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    NumberSignId = table.Column<int>(nullable: false),
                    Price = table.Column<float>(maxLength: 100, nullable: false),
                    SellerAddress = table.Column<string>(maxLength: 200, nullable: false),
                    SellerName = table.Column<string>(maxLength: 100, nullable: false),
                    TotalPrice = table.Column<float>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LandManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LandManagement_AmountSign_AmountSignId",
                        column: x => x.AmountSignId,
                        principalTable: "AmountSign",
                        principalColumn: "AmountSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandManagement_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LandManagement_NumberSign_NumberSignId",
                        column: x => x.NumberSignId,
                        principalTable: "NumberSign",
                        principalColumn: "NumberSignId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FoodManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountSignId = table.Column<int>(nullable: false),
                    ComOrBrndName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    FoodForm = table.Column<string>(nullable: false),
                    FoodName = table.Column<string>(nullable: false),
                    FoodTypeId = table.Column<int>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    NumberOfProduct = table.Column<float>(nullable: false),
                    NumberSignId = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: true),
                    ShopName = table.Column<string>(nullable: false),
                    TotalPrice = table.Column<float>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FoodManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FoodManagement_AmountSign_AmountSignId",
                        column: x => x.AmountSignId,
                        principalTable: "AmountSign",
                        principalColumn: "AmountSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodManagement_FoodType_FoodTypeId",
                        column: x => x.FoodTypeId,
                        principalTable: "FoodType",
                        principalColumn: "FoodTypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodManagement_NumberSign_NumberSignId",
                        column: x => x.NumberSignId,
                        principalTable: "NumberSign",
                        principalColumn: "NumberSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FoodManagement_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductBuy",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountSignId = table.Column<int>(nullable: false),
                    Condition = table.Column<string>(maxLength: 1000, nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    NumberOfProduct = table.Column<double>(nullable: false),
                    NumberSignId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false),
                    Price = table.Column<double>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 200, nullable: false),
                    ProductQualityId = table.Column<int>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductBuy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductBuy_AmountSign_AmountSignId",
                        column: x => x.AmountSignId,
                        principalTable: "AmountSign",
                        principalColumn: "AmountSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBuy_NumberSign_NumberSignId",
                        column: x => x.NumberSignId,
                        principalTable: "NumberSign",
                        principalColumn: "NumberSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBuy_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBuy_ProductQuality_ProductQualityId",
                        column: x => x.ProductQualityId,
                        principalTable: "ProductQuality",
                        principalColumn: "ProductQualityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductBuy_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductReadyToDeliver",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    NumberOfProduct = table.Column<float>(nullable: false),
                    NumberSignId = table.Column<int>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 200, nullable: false),
                    ProductQualityId = table.Column<int>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    WorkerSignature = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReadyToDeliver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReadyToDeliver_NumberSign_NumberSignId",
                        column: x => x.NumberSignId,
                        principalTable: "NumberSign",
                        principalColumn: "NumberSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReadyToDeliver_ProductQuality_ProductQualityId",
                        column: x => x.ProductQualityId,
                        principalTable: "ProductQuality",
                        principalColumn: "ProductQualityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductReadyToDeliver_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSell",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountSignId = table.Column<int>(nullable: false),
                    Condition = table.Column<string>(maxLength: 1000, nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    NumberOfProduct = table.Column<float>(nullable: false),
                    NumberSignId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false),
                    Price = table.Column<float>(nullable: false),
                    ProductName = table.Column<string>(maxLength: 200, nullable: false),
                    ProductQualityId = table.Column<int>(nullable: false),
                    ProductTypeId = table.Column<int>(nullable: false),
                    TotalPrice = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSell", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductSell_AmountSign_AmountSignId",
                        column: x => x.AmountSignId,
                        principalTable: "AmountSign",
                        principalColumn: "AmountSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSell_NumberSign_NumberSignId",
                        column: x => x.NumberSignId,
                        principalTable: "NumberSign",
                        principalColumn: "NumberSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSell_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSell_ProductQuality_ProductQualityId",
                        column: x => x.ProductQualityId,
                        principalTable: "ProductQuality",
                        principalColumn: "ProductQualityId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductSell_ProductType_ProductTypeId",
                        column: x => x.ProductTypeId,
                        principalTable: "ProductType",
                        principalColumn: "ProductTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OthersPayment",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Amount = table.Column<float>(nullable: false),
                    AmountSignId = table.Column<int>(nullable: false),
                    BithCertificate = table.Column<byte>(nullable: false),
                    Country = table.Column<string>(maxLength: 100, nullable: false),
                    Description = table.Column<string>(maxLength: 500, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    Image = table.Column<byte>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    NID = table.Column<byte>(nullable: false),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Passport = table.Column<byte>(nullable: false),
                    RankId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OthersPayment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OthersPayment_AmountSign_AmountSignId",
                        column: x => x.AmountSignId,
                        principalTable: "AmountSign",
                        principalColumn: "AmountSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OthersPayment_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OthersPayment_Rank_RankId",
                        column: x => x.RankId,
                        principalTable: "Rank",
                        principalColumn: "RankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerManagement",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountSignId = table.Column<int>(nullable: false),
                    BithCertificate = table.Column<byte[]>(nullable: true),
                    Country = table.Column<string>(maxLength: 100, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: false),
                    JoinDate = table.Column<DateTime>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    NID = table.Column<byte[]>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    Passport = table.Column<byte[]>(nullable: true),
                    Profile = table.Column<byte[]>(nullable: false),
                    Qualification = table.Column<string>(maxLength: 200, nullable: false),
                    RankId = table.Column<int>(nullable: false),
                    Salary = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerManagement", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerManagement_AmountSign_AmountSignId",
                        column: x => x.AmountSignId,
                        principalTable: "AmountSign",
                        principalColumn: "AmountSignId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerManagement_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerManagement_Rank_RankId",
                        column: x => x.RankId,
                        principalTable: "Rank",
                        principalColumn: "RankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkerSalary",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AmountSignId = table.Column<int>(nullable: true),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    GenderId = table.Column<int>(nullable: true),
                    IdCardNumber = table.Column<int>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    MonthId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: false),
                    RankId = table.Column<int>(nullable: false),
                    Salary = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkerSalary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkerSalary_AmountSign_AmountSignId",
                        column: x => x.AmountSignId,
                        principalTable: "AmountSign",
                        principalColumn: "AmountSignId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkerSalary_Gender_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Gender",
                        principalColumn: "GenderId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_WorkerSalary_Month_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Month",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerSalary_PaymentMethod_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethod",
                        principalColumn: "PaymentMethodId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkerSalary_Rank_RankId",
                        column: x => x.RankId,
                        principalTable: "Rank",
                        principalColumn: "RankId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    DoctorId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    DoctorName = table.Column<string>(maxLength: 100, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    Profile = table.Column<byte[]>(nullable: false),
                    SpecialistTypeId = table.Column<int>(nullable: false),
                    VisitFee = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.DoctorId);
                    table.ForeignKey(
                        name: "FK_Doctors_SpecialistType_SpecialistTypeId",
                        column: x => x.SpecialistTypeId,
                        principalTable: "SpecialistType",
                        principalColumn: "SpecialistTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Transport",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    TransportTypeId = table.Column<int>(nullable: false),
                    UsesFor = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transport_TransportType_TransportTypeId",
                        column: x => x.TransportTypeId,
                        principalTable: "TransportType",
                        principalColumn: "TransportTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DoctorsSchedule",
                columns: table => new
                {
                    ScheduleTypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DayId = table.Column<int>(nullable: false),
                    DoctorId = table.Column<int>(nullable: false),
                    EntryDate = table.Column<DateTime>(nullable: false),
                    ManagerSignature = table.Column<string>(nullable: true),
                    Time = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorsSchedule", x => x.ScheduleTypeId);
                    table.ForeignKey(
                        name: "FK_DoctorsSchedule_Days_DayId",
                        column: x => x.DayId,
                        principalTable: "Days",
                        principalColumn: "DayId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DoctorsSchedule_Doctors_DoctorId",
                        column: x => x.DoctorId,
                        principalTable: "Doctors",
                        principalColumn: "DoctorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CowManagement_CowCollectionId",
                table: "CowManagement",
                column: "CowCollectionId");

            migrationBuilder.CreateIndex(
                name: "IX_CowManagement_CowGroupId",
                table: "CowManagement",
                column: "CowGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_CowManagement_CowNationId",
                table: "CowManagement",
                column: "CowNationId");

            migrationBuilder.CreateIndex(
                name: "IX_CowManagement_CowPurposeId",
                table: "CowManagement",
                column: "CowPurposeId");

            migrationBuilder.CreateIndex(
                name: "IX_CowManagement_GenderId",
                table: "CowManagement",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_Doctors_SpecialistTypeId",
                table: "Doctors",
                column: "SpecialistTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsSchedule_DayId",
                table: "DoctorsSchedule",
                column: "DayId");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorsSchedule_DoctorId",
                table: "DoctorsSchedule",
                column: "DoctorId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodManagement_AmountSignId",
                table: "FoodManagement",
                column: "AmountSignId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodManagement_FoodTypeId",
                table: "FoodManagement",
                column: "FoodTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodManagement_NumberSignId",
                table: "FoodManagement",
                column: "NumberSignId");

            migrationBuilder.CreateIndex(
                name: "IX_FoodManagement_ProductTypeId",
                table: "FoodManagement",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LandManagement_AmountSignId",
                table: "LandManagement",
                column: "AmountSignId");

            migrationBuilder.CreateIndex(
                name: "IX_LandManagement_GenderId",
                table: "LandManagement",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_LandManagement_NumberSignId",
                table: "LandManagement",
                column: "NumberSignId");

            migrationBuilder.CreateIndex(
                name: "IX_OthersPayment_AmountSignId",
                table: "OthersPayment",
                column: "AmountSignId");

            migrationBuilder.CreateIndex(
                name: "IX_OthersPayment_GenderId",
                table: "OthersPayment",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_OthersPayment_RankId",
                table: "OthersPayment",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBuy_AmountSignId",
                table: "ProductBuy",
                column: "AmountSignId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBuy_NumberSignId",
                table: "ProductBuy",
                column: "NumberSignId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBuy_PaymentMethodId",
                table: "ProductBuy",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBuy_ProductQualityId",
                table: "ProductBuy",
                column: "ProductQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductBuy_ProductTypeId",
                table: "ProductBuy",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReadyToDeliver_NumberSignId",
                table: "ProductReadyToDeliver",
                column: "NumberSignId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReadyToDeliver_ProductQualityId",
                table: "ProductReadyToDeliver",
                column: "ProductQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductReadyToDeliver_ProductTypeId",
                table: "ProductReadyToDeliver",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_AmountSignId",
                table: "ProductSell",
                column: "AmountSignId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_NumberSignId",
                table: "ProductSell",
                column: "NumberSignId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_PaymentMethodId",
                table: "ProductSell",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_ProductQualityId",
                table: "ProductSell",
                column: "ProductQualityId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductSell_ProductTypeId",
                table: "ProductSell",
                column: "ProductTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Transport_TransportTypeId",
                table: "Transport",
                column: "TransportTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerManagement_AmountSignId",
                table: "WorkerManagement",
                column: "AmountSignId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerManagement_GenderId",
                table: "WorkerManagement",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerManagement_RankId",
                table: "WorkerManagement",
                column: "RankId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSalary_AmountSignId",
                table: "WorkerSalary",
                column: "AmountSignId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSalary_GenderId",
                table: "WorkerSalary",
                column: "GenderId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSalary_MonthId",
                table: "WorkerSalary",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSalary_PaymentMethodId",
                table: "WorkerSalary",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkerSalary_RankId",
                table: "WorkerSalary",
                column: "RankId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CowManagement");

            migrationBuilder.DropTable(
                name: "DoctorsSchedule");

            migrationBuilder.DropTable(
                name: "FoodManagement");

            migrationBuilder.DropTable(
                name: "Investment");

            migrationBuilder.DropTable(
                name: "LandManagement");

            migrationBuilder.DropTable(
                name: "OthersPayment");

            migrationBuilder.DropTable(
                name: "ProductBuy");

            migrationBuilder.DropTable(
                name: "ProductReadyToDeliver");

            migrationBuilder.DropTable(
                name: "ProductSell");

            migrationBuilder.DropTable(
                name: "Transport");

            migrationBuilder.DropTable(
                name: "WorkerManagement");

            migrationBuilder.DropTable(
                name: "WorkerSalary");

            migrationBuilder.DropTable(
                name: "CowCollection");

            migrationBuilder.DropTable(
                name: "CowGroup");

            migrationBuilder.DropTable(
                name: "NationOfCow");

            migrationBuilder.DropTable(
                name: "CowPurpose");

            migrationBuilder.DropTable(
                name: "Days");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "FoodType");

            migrationBuilder.DropTable(
                name: "NumberSign");

            migrationBuilder.DropTable(
                name: "ProductQuality");

            migrationBuilder.DropTable(
                name: "ProductType");

            migrationBuilder.DropTable(
                name: "TransportType");

            migrationBuilder.DropTable(
                name: "AmountSign");

            migrationBuilder.DropTable(
                name: "Gender");

            migrationBuilder.DropTable(
                name: "Month");

            migrationBuilder.DropTable(
                name: "PaymentMethod");

            migrationBuilder.DropTable(
                name: "Rank");

            migrationBuilder.DropTable(
                name: "SpecialistType");
        }
    }
}
