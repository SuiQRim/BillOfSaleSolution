using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using BillSale.BLL.Services.Contracts;
using BillSale.BLL.Services.Contracts.Models.Certificate;
using BillSale.BLL.Services.Contracts.Models.Certificate.ProductItem;
using BillSale.BLL.Services.Contracts.Models.Company;

namespace BillSale.BLL.Services
{
    /// <inheritdoc cref="ICertificateExcelService"/>
    public class CertificateExcelService : ICertificateExcelService
    {
        private const uint NormalStyle = 0;
        private const uint BoldStyle = 1;
        private const uint BorderStyle = 2;
        private const uint BoldBorderStyle = 3;

        /// <inheritdoc/>
        public Task<Stream> GenerateCertificateExcelAsync(
            CertificateDetailModel certificate,
            CancellationToken cancellationToken)
        {
            var stream = new MemoryStream();

            using (var document = SpreadsheetDocument.Create(
                stream,
                SpreadsheetDocumentType.Workbook))
            {
                var workbookPart = CreateWorkbook(document);
                var worksheetPart = CreateWorksheet(workbookPart);

                var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>()!;

                AddCertificateInfo(sheetData, certificate);
                AddCompanyInfo(sheetData, certificate.Seller, "Продавец");
                AddCompanyInfo(sheetData, certificate.Purchaser, "Покупатель");
                AddProducts(sheetData, certificate.Products);

                workbookPart.Workbook.Save();
                worksheetPart.Worksheet.Save();
            }

            stream.Position = 0;

            return Task.FromResult<Stream>(stream);
        }

        private WorkbookPart CreateWorkbook(SpreadsheetDocument document)
        {
            var workbookPart = document.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            var stylesPart = workbookPart.AddNewPart<WorkbookStylesPart>();
            stylesPart.Stylesheet = CreateStylesheet();
            stylesPart.Stylesheet.Save();

            return workbookPart;
        }

        private WorksheetPart CreateWorksheet(WorkbookPart workbookPart)
        {
            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();

            var sheetData = new SheetData();

            worksheetPart.Worksheet = new Worksheet(sheetData);

            var sheets = workbookPart.Workbook.AppendChild(new Sheets());

            var sheet = new Sheet
            {
                Id = workbookPart.GetIdOfPart(worksheetPart),
                SheetId = 1,
                Name = "Сертификат"
            };

            sheets.Append(sheet);

            return worksheetPart;
        }

        private void AddCertificateInfo(
            SheetData sheetData,
            CertificateDetailModel certificate)
        {
            AddPropertyRow(
                sheetData,
                "Id",
                certificate.Id.ToString());

            AddPropertyRow(
                sheetData,
                "Артикул",
                certificate.ArticulNumber.ToString());

            AddPropertyRow(
                sheetData,
                "Город",
                certificate.City);

            AddPropertyRow(
                sheetData,
                "Дата подготовки",
                certificate.PreparationDate.ToString());

            AddEmptyRow(sheetData);
        }

        private void AddCompanyInfo(
            SheetData sheetData,
            CompanyModel seller,
            string role)
        {
            AddTitleRow(sheetData, role);

            AddPropertyRow(
                sheetData,
                "Наименование организации",
                seller.OrganizationName);

            AddPropertyRow(
                sheetData,
                "Название документа",
                seller.DocumentName);

            AddPropertyRow(
                sheetData,
                "Ф.И.О",
                $"{seller.LastName} {seller.FirstName} {seller.MiddleName}");

            AddPropertyRow(
                sheetData,
                "Должность",
                seller.Post);

            AddEmptyRow(sheetData);
        }

        private void AddProducts(
            SheetData sheetData,
            IReadOnlyCollection<CertificateProductDetailsModel> products)
        {
            AddTitleRow(sheetData, "Товары");

            AddRow(
                sheetData,
                BoldBorderStyle,
                "№",
                "Наименование",
                "Ед. изм.",
                "Количество",
                "Цена",
                "Итоговая цена");

            var number = 1;

            foreach (var product in products)
            {
                AddRow(
                    sheetData,
                    BorderStyle,
                    number.ToString(),
                    product.ProductName,
                    product.MeasureUnit,
                    product.Count.ToString(),
                    product.Price.ToString(),
                    product.TotalPrice.ToString());

                number++;
            }
        }

        private void AddPropertyRow(
            SheetData sheetData,
            string propertyName,
            string value)
        {
            AddRow(
                sheetData,
                NormalStyle,
                propertyName,
                value);
        }

        private void AddTitleRow(
            SheetData sheetData,
            string title)
        {
            AddRow(
                sheetData,
                BoldStyle,
                title);
        }

        private void AddEmptyRow(SheetData sheetData)
        {
            sheetData.AppendChild(new Row());
        }

        private void AddRow(
            SheetData sheetData,
            uint styleIndex,
            params string[] values)
        {
            var row = new Row();

            foreach (var value in values)
            {
                var cell = new Cell
                {
                    StyleIndex = styleIndex,
                    DataType = CellValues.String,
                    CellValue = new CellValue(value)
                };

                row.AppendChild(cell);
            }

            sheetData.AppendChild(row);
        }

        private Stylesheet CreateStylesheet()
        {
            var fonts = new Fonts(
                new Font(
                    new FontName
                    {
                        Val = "Calibri"
                    },
                    new FontSize
                    {
                        Val = 11
                    }),

                new Font(
                    new Bold(),
                    new FontName
                    {
                        Val = "Calibri"
                    },
                    new FontSize
                    {
                        Val = 11
                    }));

            var fills = new Fills(
                new Fill(
                    new PatternFill
                    {
                        PatternType = PatternValues.None
                    }),

                new Fill(
                    new PatternFill
                    {
                        PatternType = PatternValues.Gray125
                    }));

            var borders = new Borders(
                new Border(),

                new Border(
                    new LeftBorder
                    {
                        Style = BorderStyleValues.Thin
                    },
                    new RightBorder
                    {
                        Style = BorderStyleValues.Thin
                    },
                    new TopBorder
                    {
                        Style = BorderStyleValues.Thin
                    },
                    new BottomBorder
                    {
                        Style = BorderStyleValues.Thin
                    }));

            var cellFormats = new CellFormats(
                new CellFormat
                {
                    FontId = 0,
                    FillId = 0,
                    BorderId = 0
                },

                new CellFormat
                {
                    FontId = 1,
                    FillId = 0,
                    BorderId = 0
                },

                new CellFormat
                {
                    FontId = 0,
                    FillId = 0,
                    BorderId = 1
                },

                new CellFormat
                {
                    FontId = 1,
                    FillId = 0,
                    BorderId = 1
                });

            return new Stylesheet(
                fonts,
                fills,
                borders,
                cellFormats);
        }
    }
}
