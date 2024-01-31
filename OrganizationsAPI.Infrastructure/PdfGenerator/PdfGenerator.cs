using OrganizationsAPI.Domain.Entities;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Document = QuestPDF.Fluent.Document;

namespace OrganizationsAPI.Infrastructure.PdfGenerator
{
    public class PdfGenerator : IPdfGenerator
    {
        public byte[] GenerateOrganizationPdf(Organization organization)
        {
            QuestPDF.Settings.License = QuestPDF.Infrastructure.LicenseType.Community;

            var pdf = Document.Create(container =>
            {
                container.Page(page =>
                {
                    page.Size(PageSizes.A4);
                    page.Margin(2, QuestPDF.Infrastructure.Unit.Centimetre);
                    page.PageColor(Colors.White);
                    page.DefaultTextStyle(x => x.FontSize(20));

                    page.Header()
                        .Text($"Information for {organization.Name}")
                        .SemiBold()
                        .FontSize(30)
                        .FontColor(Colors.Blue.Medium);

                    page.Content()
                        .Padding(1, QuestPDF.Infrastructure.Unit.Centimetre)
                        .Column(x =>
                        {
                            x.Spacing(20);
                            x.Item().Text(organization.Description);
                            x.Spacing(30);
                            x.Item().Text($"Founded in the year {organization.Founded} in {organization.Country}");
                            x.Spacing(30);
                            x.Item().Text($"An organization dealing with {organization.Industry}");
                            x.Spacing(20);
                            x.Item().Text($"Current employees: {organization.NumberOfEmployees}");
                            x.Item().Text($"Website : {organization.Website}");
                        });
                });

            }).GeneratePdf();

            return pdf;
        }
    }
}
