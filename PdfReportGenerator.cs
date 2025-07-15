using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
// using System.Management; // Not needed here if you're already passing the strings
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace UltraBench
{
    // DO NOT put BenchmarkResult definition here, it's already in your other file!

    public static class PdfReportGenerator
    {
        // The method signature must include ALL necessary parameters
        public static void GenerateReport(
            List<BenchmarkResult> benchmarkResults, // Takes the list of results
            string processorName,                   // Takes the CPU name
            string totalRamAmount,                  // Takes the RAM amount
            string gpuName)                         // Takes the GPU name
        {
            string reportsDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            Directory.CreateDirectory(reportsDir);

            string fileName = $"Benchmark_Report_{DateTime.Now:yyyyMMdd_HHmmss}.pdf";
            string filePath = System.IO.Path.Combine(reportsDir, fileName);

            try
            {
                using (PdfWriter writer = new PdfWriter(filePath))
                {
                    PdfDocument pdf = new PdfDocument(writer);
                    Document document = new Document(pdf, PageSize.A4);
                    PdfFont boldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
                    PdfFont regularFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);

                    // --- FOR THE LOGO ---
                    string logoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo_UltraBench.png"); // CHANGE "YourLogo.png" to the REAL name of your logo file

                    if (System.IO.File.Exists(logoPath))
                    {
                        ImageData imageData = ImageDataFactory.Create(logoPath);
                        Image logo = new Image(imageData);

                        logo.SetWidth(100);
                        logo.SetFixedPosition(400, 650); // Adjust X, Y according to your preferences
                        document.Add(logo);
                    }
                    else
                    {
                        Debug.WriteLine($"The logo file '{logoPath}' was not found for the PDF report.");
                    }
                    // --- END LOGO ---

                    // --- Report Header ---
                    document.Add(new Paragraph("UltraBench by Dpfpic – Benchmark Report").SetFont(boldFont).SetFontSize(18).SetTextAlignment(TextAlignment.CENTER));
                    document.Add(new Paragraph($"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm:ss}").SetFont(regularFont).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER));
                    document.Add(new Paragraph("\n"));

                    // --- System Information ---
                    document.Add(new Paragraph("System Information").SetFont(boldFont).SetFontSize(14).SetFontColor(ColorConstants.BLUE));
                    // Use the parameters here, DO NOT CALL GetProcessorName(), etc.
                    document.Add(new Paragraph($"Processor: {processorName}").SetFont(regularFont).SetFontSize(11));
                    document.Add(new Paragraph($"RAM: {totalRamAmount}").SetFont(regularFont).SetFontSize(11));
                    document.Add(new Paragraph($"Graphics Card: {gpuName}").SetFont(regularFont).SetFontSize(11));
                    document.Add(new Paragraph("\n"));

                    // --- Benchmark Results Table ---
                    document.Add(new Paragraph("Benchmark Results").SetFont(boldFont).SetFontSize(14).SetFontColor(ColorConstants.BLUE));
                    document.Add(new Paragraph("\n"));

                    Table table = new Table(UnitValue.CreatePercentArray(new float[] { 2, 1, 4 }));
                    table.SetWidth(UnitValue.CreatePercentValue(90));
                    table.SetTextAlignment(TextAlignment.LEFT);
                    table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

                    table.AddHeaderCell(new Cell().Add(new Paragraph("Test")).SetFont(boldFont).SetFontColor(ColorConstants.WHITE).SetBackgroundColor(ColorConstants.DARK_GRAY));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Score")).SetFont(boldFont).SetFontColor(ColorConstants.WHITE).SetBackgroundColor(ColorConstants.DARK_GRAY));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Detail")).SetFont(boldFont).SetFontColor(ColorConstants.WHITE).SetBackgroundColor(ColorConstants.DARK_GRAY));

                    // Use the 'benchmarkResults' parameter here, DO NOT use _benchmarkResults
                    if (benchmarkResults != null && benchmarkResults.Any())
                    {
                        foreach (var result in benchmarkResults)
                        {
                            Color rowColor = result.Success ? ColorConstants.BLACK : ColorConstants.RED;
                            Color bgColor = (table.GetNumberOfRows() % 2 == 0) ? ColorConstants.LIGHT_GRAY : ColorConstants.WHITE;

                            table.AddCell(new Cell().Add(new Paragraph(result.Title)).SetFont(regularFont).SetFontColor(rowColor).SetBackgroundColor(bgColor));
                            table.AddCell(new Cell().Add(new Paragraph(result.Score.ToString())).SetFont(regularFont).SetFontColor(rowColor).SetBackgroundColor(bgColor));
                            table.AddCell(new Cell().Add(new Paragraph(result.DetailedResult)).SetFont(regularFont).SetFontColor(rowColor).SetBackgroundColor(bgColor));
                        }
                    }
                    else
                    {
                        table.AddCell(new Cell(1, 3).Add(new Paragraph("No benchmarks have been executed. Please run the 'Full Stress Test' first.")).SetFont(regularFont).SetFontColor(ColorConstants.RED));
                    }

                    document.Add(table);
                    document.Add(new Paragraph("\n"));

                    // --- Dedication ---
                    document.Add(new Paragraph("This project is dedicated to my mother, born on July 6, with all my love – Fabrice (Dpfpic)")
                        .SetFont(regularFont).SetFontSize(10).SetTextAlignment(TextAlignment.CENTER));

                    document.Close();
                }

                // These MessageBoxes are okay here because System.Windows.Forms has been added
                //MessageBox.Show($"PDF Report '{fileName}' successfully generated in: {reportsDir}", "UltraBench", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //Process.Start("explorer.exe", reportsDir);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PDF generation error: {ex.Message}");
                // Rethrow the exception so that Form1 handles it and displays the MessageBox to the user
                throw new Exception($"Error during PDF report generation: {ex.Message}", ex);
            }
        }
    }
}