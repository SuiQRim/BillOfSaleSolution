using BillSale.DAL.Contracts;
using BillSale.Entities;
using Microsoft.EntityFrameworkCore;

namespace BillSale.Context
{
    /// <summary>
    /// Начальные данные для базы данных. Полностью сгенерированный класс
    /// </summary>
    internal static class SeedData
    {
        private static readonly DateTimeOffset createdAt =
            new(2026, 1, 1, 10, 0, 0, TimeSpan.Zero);

        private static readonly DateTimeOffset updatedAt =
            new(2026, 1, 1, 10, 0, 0, TimeSpan.Zero);

        private const string CreatedBy = "Admin";
        private const string UpdatedBy = "Admin";

        public static void Seed(ModelBuilder modelBuilder)
        {
            var company1Id = Guid.Parse("10000000-0000-0000-0000-000000000001");
            var company2Id = Guid.Parse("10000000-0000-0000-0000-000000000002");
            var company3Id = Guid.Parse("10000000-0000-0000-0000-000000000003");
            var company4Id = Guid.Parse("20000000-0000-0000-0000-000000000001");
            var company5Id = Guid.Parse("20000000-0000-0000-0000-000000000002");
            var company6Id = Guid.Parse("20000000-0000-0000-0000-000000000003");

            var product1Id = Guid.Parse("30000000-0000-0000-0000-000000000001");
            var product2Id = Guid.Parse("30000000-0000-0000-0000-000000000002");
            var product3Id = Guid.Parse("30000000-0000-0000-0000-000000000003");
            var product4Id = Guid.Parse("30000000-0000-0000-0000-000000000004");
            var product5Id = Guid.Parse("30000000-0000-0000-0000-000000000005");
            var product6Id = Guid.Parse("30000000-0000-0000-0000-000000000006");
            var product7Id = Guid.Parse("30000000-0000-0000-0000-000000000007");
            var product8Id = Guid.Parse("30000000-0000-0000-0000-000000000008");
            var product9Id = Guid.Parse("30000000-0000-0000-0000-000000000009");
            var product10Id = Guid.Parse("30000000-0000-0000-0000-000000000010");

            var certificate1Id = Guid.Parse("40000000-0000-0000-0000-000000000001");
            var certificate2Id = Guid.Parse("40000000-0000-0000-0000-000000000002");
            var certificate3Id = Guid.Parse("40000000-0000-0000-0000-000000000003");
            var certificate4Id = Guid.Parse("40000000-0000-0000-0000-000000000004");
            var certificate5Id = Guid.Parse("40000000-0000-0000-0000-000000000005");

            var item1Id = Guid.Parse("50000000-0000-0000-0000-000000000001");
            var item2Id = Guid.Parse("50000000-0000-0000-0000-000000000002");
            var item3Id = Guid.Parse("50000000-0000-0000-0000-000000000003");
            var item4Id = Guid.Parse("50000000-0000-0000-0000-000000000004");
            var item5Id = Guid.Parse("50000000-0000-0000-0000-000000000005");
            var item6Id = Guid.Parse("50000000-0000-0000-0000-000000000006");
            var item7Id = Guid.Parse("50000000-0000-0000-0000-000000000007");
            var item8Id = Guid.Parse("50000000-0000-0000-0000-000000000008");
            var item9Id = Guid.Parse("50000000-0000-0000-0000-000000000009");
            var item10Id = Guid.Parse("50000000-0000-0000-0000-000000000010");
            var item11Id = Guid.Parse("50000000-0000-0000-0000-000000000011");
            var item12Id = Guid.Parse("50000000-0000-0000-0000-000000000012");
            var item13Id = Guid.Parse("50000000-0000-0000-0000-000000000013");
            var item14Id = Guid.Parse("50000000-0000-0000-0000-000000000014");
            var item15Id = Guid.Parse("50000000-0000-0000-0000-000000000015");
            var item16Id = Guid.Parse("50000000-0000-0000-0000-000000000016");

            SeedCompanies(
                modelBuilder,
                company1Id,
                company2Id,
                company3Id,
                company4Id,
                company5Id,
                company6Id);

            SeedProducts(
                modelBuilder,
                product1Id,
                product2Id,
                product3Id,
                product4Id,
                product5Id,
                product6Id,
                product7Id,
                product8Id,
                product9Id,
                product10Id);

            SeedCertificates(
                modelBuilder,
                certificate1Id,
                certificate2Id,
                certificate3Id,
                certificate4Id,
                certificate5Id,
                company1Id,
                company2Id,
                company3Id,
                company4Id,
                company5Id,
                company6Id);

            SeedCertificateProducts(
                modelBuilder,
                item1Id,
                item2Id,
                item3Id,
                item4Id,
                item5Id,
                item6Id,
                item7Id,
                item8Id,
                item9Id,
                item10Id,
                item11Id,
                item12Id,
                item13Id,
                item14Id,
                item15Id,
                item16Id,
                certificate1Id,
                certificate2Id,
                certificate3Id,
                certificate4Id,
                certificate5Id,
                product1Id,
                product2Id,
                product3Id,
                product4Id,
                product5Id,
                product6Id,
                product7Id,
                product8Id,
                product9Id,
                product10Id);
        }

        private static void SeedCompanies(
            ModelBuilder modelBuilder,
            Guid company1Id,
            Guid company2Id,
            Guid company3Id,
            Guid company4Id,
            Guid company5Id,
            Guid company6Id)
        {
            modelBuilder.Entity<Company>().HasData(
                new Company
                {
                    Id = company1Id,
                    OrganizationName = "ООО «ТехноСнаб»",
                    Post = "Генеральный директор",
                    FirstName = "Алексей",
                    LastName = "Смирнов",
                    MiddleName = "Игоревич",
                    DocumentName = "Устав",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Company
                {
                    Id = company2Id,
                    OrganizationName = "ООО «СеверТорг»",
                    Post = "Директор",
                    FirstName = "Дмитрий",
                    LastName = "Волков",
                    MiddleName = "Александрович",
                    DocumentName = "Доверенность №15",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Company
                {
                    Id = company3Id,
                    OrganizationName = "АО «ПромКомплект»",
                    Post = "Коммерческий директор",
                    FirstName = "Максим",
                    LastName = "Кузнецов",
                    MiddleName = "Сергеевич",
                    DocumentName = "Доверенность №27",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Company
                {
                    Id = company4Id,
                    OrganizationName = "ООО «Вектор»",
                    Post = "Генеральный директор",
                    FirstName = "Иван",
                    LastName = "Петров",
                    MiddleName = "Андреевич",
                    DocumentName = "Устав",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Company
                {
                    Id = company5Id,
                    OrganizationName = "ООО «Балтика»",
                    Post = "Директор по закупкам",
                    FirstName = "Николай",
                    LastName = "Орлов",
                    MiddleName = "Викторович",
                    DocumentName = "Доверенность №8",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Company
                {
                    Id = company6Id,
                    OrganizationName = "ИП «Морозов»",
                    Post = "Индивидуальный предприниматель",
                    FirstName = "Евгений",
                    LastName = "Морозов",
                    MiddleName = "Олегович",
                    DocumentName = "Паспорт",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                });
        }

        private static void SeedProducts(
            ModelBuilder modelBuilder,
            Guid product1Id,
            Guid product2Id,
            Guid product3Id,
            Guid product4Id,
            Guid product5Id,
            Guid product6Id,
            Guid product7Id,
            Guid product8Id,
            Guid product9Id,
            Guid product10Id)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = product1Id,
                    Name = "Ноутбук Lenovo ThinkPad",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product2Id,
                    Name = "Монитор Samsung 27",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product3Id,
                    Name = "Клавиатура Logitech",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product4Id,
                    Name = "Мышь Logitech MX",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product5Id,
                    Name = "USB-C кабель 2 м",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product6Id,
                    Name = "Док-станция USB-C",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product7Id,
                    Name = "Офисное кресло",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product8Id,
                    Name = "Стол офисный",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product9Id,
                    Name = "Бумага офисная А4",
                    MeasureUnit = "уп.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Product
                {
                    Id = product10Id,
                    Name = "Картридж лазерный",
                    MeasureUnit = "шт.",
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                });
        }

        private static void SeedCertificates(
            ModelBuilder modelBuilder,
            Guid certificate1Id,
            Guid certificate2Id,
            Guid certificate3Id,
            Guid certificate4Id,
            Guid certificate5Id,
            Guid company1Id,
            Guid company2Id,
            Guid company3Id,
            Guid company4Id,
            Guid company5Id,
            Guid company6Id)
        {
            modelBuilder.Entity<Certificate>().HasData(
                new Certificate
                {
                    Id = certificate1Id,
                    ArticulNumber = 1001,
                    SellerId = company1Id,
                    PurchaserId = company4Id,
                    City = "Санкт-Петербург",
                    PreparationDate = new DateTimeOffset(
                        2026, 1, 15, 11, 30, 0, TimeSpan.Zero),
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Certificate
                {
                    Id = certificate2Id,
                    ArticulNumber = 1002,
                    SellerId = company2Id,
                    PurchaserId = company5Id,
                    City = "Москва",
                    PreparationDate = new DateTimeOffset(
                        2026, 2, 3, 14, 0, 0, TimeSpan.Zero),
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Certificate
                {
                    Id = certificate3Id,
                    ArticulNumber = 1003,
                    SellerId = company3Id,
                    PurchaserId = company4Id,
                    City = "Псков",
                    PreparationDate = new DateTimeOffset(
                        2026, 3, 20, 10, 15, 0, TimeSpan.Zero),
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Certificate
                {
                    Id = certificate4Id,
                    ArticulNumber = 1004,
                    SellerId = company1Id,
                    PurchaserId = company6Id,
                    City = "Великий Новгород",
                    PreparationDate = new DateTimeOffset(
                        2026, 4, 8, 13, 45, 0, TimeSpan.Zero),
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new Certificate
                {
                    Id = certificate5Id,
                    ArticulNumber = 1005,
                    SellerId = company2Id,
                    PurchaserId = company6Id,
                    City = "Тверь",
                    PreparationDate = new DateTimeOffset(
                        2026, 5, 12, 16, 20, 0, TimeSpan.Zero),
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                });
        }

        private static void SeedCertificateProducts(
            ModelBuilder modelBuilder,
            Guid item1Id,
            Guid item2Id,
            Guid item3Id,
            Guid item4Id,
            Guid item5Id,
            Guid item6Id,
            Guid item7Id,
            Guid item8Id,
            Guid item9Id,
            Guid item10Id,
            Guid item11Id,
            Guid item12Id,
            Guid item13Id,
            Guid item14Id,
            Guid item15Id,
            Guid item16Id,
            Guid certificate1Id,
            Guid certificate2Id,
            Guid certificate3Id,
            Guid certificate4Id,
            Guid certificate5Id,
            Guid product1Id,
            Guid product2Id,
            Guid product3Id,
            Guid product4Id,
            Guid product5Id,
            Guid product6Id,
            Guid product7Id,
            Guid product8Id,
            Guid product9Id,
            Guid product10Id)
        {
            modelBuilder.Entity<CertificateProduct>().HasData(
                new CertificateProduct
                {
                    Id = item1Id,
                    CertificateId = certificate1Id,
                    ProductId = product1Id,
                    Count = 5,
                    Price = 85000m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item2Id,
                    CertificateId = certificate1Id,
                    ProductId = product2Id,
                    Count = 5,
                    Price = 32000m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item3Id,
                    CertificateId = certificate1Id,
                    ProductId = product3Id,
                    Count = 5,
                    Price = 7500m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item4Id,
                    CertificateId = certificate2Id,
                    ProductId = product4Id,
                    Count = 10,
                    Price = 6500m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item5Id,
                    CertificateId = certificate2Id,
                    ProductId = product5Id,
                    Count = 20,
                    Price = 1200m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item6Id,
                    CertificateId = certificate2Id,
                    ProductId = product6Id,
                    Count = 4,
                    Price = 14500m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item7Id,
                    CertificateId = certificate2Id,
                    ProductId = product9Id,
                    Count = 30,
                    Price = 650m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item8Id,
                    CertificateId = certificate3Id,
                    ProductId = product7Id,
                    Count = 12,
                    Price = 18500m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item9Id,
                    CertificateId = certificate3Id,
                    ProductId = product8Id,
                    Count = 8,
                    Price = 22000m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item10Id,
                    CertificateId = certificate3Id,
                    ProductId = product10Id,
                    Count = 6,
                    Price = 9800m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item11Id,
                    CertificateId = certificate4Id,
                    ProductId = product1Id,
                    Count = 2,
                    Price = 83000m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item12Id,
                    CertificateId = certificate4Id,
                    ProductId = product6Id,
                    Count = 2,
                    Price = 14000m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item13Id,
                    CertificateId = certificate4Id,
                    ProductId = product4Id,
                    Count = 3,
                    Price = 6200m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item14Id,
                    CertificateId = certificate5Id,
                    ProductId = product2Id,
                    Count = 3,
                    Price = 31500m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item15Id,
                    CertificateId = certificate5Id,
                    ProductId = product3Id,
                    Count = 3,
                    Price = 7200m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                },
                new CertificateProduct
                {
                    Id = item16Id,
                    CertificateId = certificate5Id,
                    ProductId = product9Id,
                    Count = 50,
                    Price = 620m,
                    CreatedAt = createdAt,
                    CreatedBy = CreatedBy,
                    UpdatedAt = updatedAt,
                    UpdatedBy = UpdatedBy,
                    DeletedAt = null
                });
        }
    }
}
