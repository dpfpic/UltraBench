/**
* UltraBench – System Benchmark Tool
 * Version : 1.0.0
 * Created by Dpfpic (Fabrice Piscia)
 * Licensed under the MIT License
 * Repository: https://github.com/dpfpic/UltraBench
 *
 * Description:
 * This file is part of the UltraBench project,
 * a tool for benchmarking CPU, RAM, SSD and GPU.
**/

using iText.IO.Font.Constants;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Geom;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Borders;
using iText.Layout.Element;
using iText.Layout.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
// REMOVE: using iText.Kernel.Events; // NE PAS utiliser pour iText 9.x pour cette approche d'événement


namespace UltraBench
{
    public static class PdfReportGenerator
    {
        // Utility method pour ajouter le pied de page à la dernière page du document
        private static void AddPageFooter(PdfDocument pdf, Document document, PdfFont font, Color fontColor, float fontSize, string dedicationText)
        {
            if (pdf.GetNumberOfPages() == 0) return; // Pas de page, pas de pied de page

            PdfPage page = pdf.GetLastPage();
            Rectangle pageSize = page.GetPageSize();

            // CORRECTION ICI : RETRAIT DU "using" pour PdfCanvas
            PdfCanvas pdfCanvas = new PdfCanvas(page);

            // On conserve le "using" pour Canvas car lui implémente IDisposable
            using (Canvas canvas = new Canvas(pdfCanvas, pageSize))
            {
                canvas.ShowTextAligned(
                    new Paragraph(dedicationText)
                        .SetFont(font)
                        .SetFontSize(fontSize)
                        .SetFontColor(fontColor),
                    pageSize.GetWidth() / 2, // Centré horizontalement
                    20, // 20 unités depuis le bas de la page (peut être ajusté)
                    TextAlignment.CENTER
                );
            }
            // Il faut libérer le PdfCanvas après utilisation, comme avant
            pdfCanvas.Release();
        }


        public static void GenerateReport(
            List<BenchmarkResult> benchmarkResults,
            string processorName,
            string totalRamAmount,
            string gpuName,
            string referencePoint)
        {
            string reportsDir = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Reports");
            System.IO.Directory.CreateDirectory(reportsDir);

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

                    document.SetMargins(36, 36, 50, 36);

                    string dedicationText = "This project is dedicated to my mother, born on July 6, with all my love – Fabrice (Dpfpic)";


                    // --- Improved Header and Logo Section ---
                    Table headerTable = new Table(new float[] { 1, 3 });
                    headerTable.SetWidth(UnitValue.CreatePercentValue(100));
                    headerTable.SetMarginTop(0);
                    headerTable.SetMarginBottom(20);

                    string logoPath = "";
                    string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
                    string targetDirectoryName = "UltraBench";

                    bool foundProjectRoot = false;

                    while (currentDirectory != null && System.IO.Directory.GetParent(currentDirectory) != null)
                    {
                        if (System.IO.Path.GetFileName(currentDirectory).Equals(targetDirectoryName, StringComparison.OrdinalIgnoreCase))
                        {
                            logoPath = System.IO.Path.Combine(currentDirectory, "logo_UltraBench.png");
                            foundProjectRoot = true;
                            break;
                        }
                        currentDirectory = System.IO.Directory.GetParent(currentDirectory).FullName;
                    }

                    if (!foundProjectRoot)
                    {
                        logoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo_UltraBench.png");
                        Debug.WriteLine($"Could not find '{targetDirectoryName}' in parent path. Logo will be searched in application base directory: {logoPath}");
                    }

                    if (System.IO.File.Exists(logoPath))
                    {
                        ImageData imageData = ImageDataFactory.Create(logoPath);
                        Image logo = new Image(imageData);
                        logo.SetWidth(80);
                        logo.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.LEFT);
                        headerTable.AddCell(new Cell().Add(logo).SetBorder(Border.NO_BORDER));
                    }
                    else
                    {
                        headerTable.AddCell(new Cell().Add(new Paragraph("UltraBench Logo")).SetBorder(Border.NO_BORDER));
                        Debug.WriteLine($"Logo file '{logoPath}' not found for PDF report generation.");
                    }

                    Paragraph titleParagraph = new Paragraph("UltraBench by Dpfpic – Benchmark Report")
                        .SetFont(boldFont)
                        .SetFontSize(24)
                        .SetFontColor(ColorConstants.DARK_GRAY)
                        .SetTextAlignment(TextAlignment.RIGHT);

                    Paragraph dateRefParagraph = new Paragraph($"Generated on: {DateTime.Now:dd/MM/yyyy HH:mm:ss} | Reference: {referencePoint}")
                        .SetFont(regularFont)
                        .SetFontSize(10)
                        .SetFontColor(ColorConstants.GRAY)
                        .SetTextAlignment(TextAlignment.RIGHT);

                    headerTable.AddCell(new Cell().Add(titleParagraph).Add(dateRefParagraph).SetBorder(Border.NO_BORDER).SetVerticalAlignment(VerticalAlignment.MIDDLE));
                    document.Add(headerTable);

                    // --- AJOUT DE PIED DE PAGE APRÈS LA PREMIÈRE SECTION (SI LA PAGE EST CRÉÉE) ---
                    AddPageFooter(pdf, document, regularFont, ColorConstants.GRAY, 10, dedicationText);

                    // --- Separator Line under the header ---
                    SolidLine lineSeparatorDrawer = new SolidLine(1f);
                    lineSeparatorDrawer.SetColor(ColorConstants.LIGHT_GRAY);
                    document.Add(new LineSeparator(lineSeparatorDrawer).SetMarginBottom(20));

                    // --- Executive Summary Section ---
                    document.Add(new Paragraph("1. Executive Summary")
                        .SetFont(boldFont).SetFontSize(16).SetFontColor(new DeviceRgb(0, 102, 204))
                        .SetMarginBottom(10));

                    string executiveSummaryText = GenerateExecutiveSummary(benchmarkResults, processorName, totalRamAmount, gpuName, referencePoint);
                    document.Add(new Paragraph(executiveSummaryText)
                        .SetFont(regularFont).SetFontSize(11).SetMarginBottom(20));

                    // --- AJOUT DE PIED DE PAGE APRÈS CETTE SECTION ---
                    AddPageFooter(pdf, document, regularFont, ColorConstants.GRAY, 10, dedicationText);


                    // --- System Information Section ---
                    document.Add(new Paragraph("2. System Information")
                        .SetFont(boldFont).SetFontSize(16).SetFontColor(new DeviceRgb(0, 102, 204))
                        .SetMarginBottom(10));

                    document.Add(new Paragraph($"• **Processor:** {processorName}")
                        .SetFont(regularFont).SetFontSize(11).SetMarginBottom(5).SetFontColor(ColorConstants.BLACK));
                    document.Add(new Paragraph($"• **RAM:** {totalRamAmount}")
                        .SetFont(regularFont).SetFontSize(11).SetMarginBottom(5).SetFontColor(ColorConstants.BLACK));
                    document.Add(new Paragraph($"• **Graphics Card:** {gpuName}")
                        .SetFont(regularFont).SetFontSize(11).SetMarginBottom(15).SetFontColor(ColorConstants.BLACK));

                    // --- AJOUT DE PIED DE PAGE APRÈS CETTE SECTION ---
                    AddPageFooter(pdf, document, regularFont, ColorConstants.GRAY, 10, dedicationText);

                    // --- Detailed Benchmark Results Section ---
                    document.Add(new Paragraph("3. Detailed Benchmark Results")
                        .SetFont(boldFont).SetFontSize(16).SetFontColor(new DeviceRgb(0, 102, 204))
                        .SetMarginBottom(10));

                    Table table = new Table(new float[] { 2, 1, 2, 3 });
                    table.SetWidth(UnitValue.CreatePercentValue(95));
                    table.SetTextAlignment(TextAlignment.LEFT);
                    table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);
                    table.SetMarginBottom(20);

                    // Table Headers
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Test")).SetFont(boldFont).SetFontColor(ColorConstants.WHITE).SetBackgroundColor(new DeviceRgb(60, 60, 60)).SetPadding(5));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Score")).SetFont(boldFont).SetFontColor(ColorConstants.WHITE).SetBackgroundColor(new DeviceRgb(60, 60, 60)).SetPadding(5));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Status")).SetFont(boldFont).SetFontColor(ColorConstants.WHITE).SetBackgroundColor(new DeviceRgb(60, 60, 60)).SetPadding(5));
                    table.AddHeaderCell(new Cell().Add(new Paragraph("Detail / Recommendation")).SetFont(boldFont).SetFontColor(ColorConstants.WHITE).SetBackgroundColor(new DeviceRgb(60, 60, 60)).SetPadding(5));

                    if (benchmarkResults != null && benchmarkResults.Any())
                    {
                        foreach (var result in benchmarkResults)
                        {
                            Color bgColor = result.Success ? new DeviceRgb(240, 255, 240) : new DeviceRgb(255, 240, 240);
                            Color textColor = ColorConstants.BLACK;

                            string statusText = result.Success ? "Success" : "Failed";
                            Color statusCellBgColor = result.Success ? new DeviceRgb(144, 238, 144) : new DeviceRgb(255, 99, 71);
                            Color statusTextColor = ColorConstants.WHITE;

                            table.AddCell(new Cell().Add(new Paragraph(result.Title)).SetFont(regularFont).SetFontColor(textColor).SetBackgroundColor(bgColor).SetPadding(5));
                            table.AddCell(new Cell().Add(new Paragraph(Math.Round(result.Score).ToString())).SetFont(regularFont).SetFontColor(textColor).SetBackgroundColor(bgColor).SetPadding(5));

                            table.AddCell(new Cell().Add(new Paragraph(statusText)).SetFont(boldFont).SetFontColor(statusTextColor).SetBackgroundColor(statusCellBgColor).SetTextAlignment(TextAlignment.CENTER).SetPadding(5));

                            string detailOrRecommendation = result.DetailedResult;
                            if (!result.Success)
                            {
                                detailOrRecommendation += "\nRecommendation: Examine logs or components related to this test to understand the root cause of the failure.";
                            }
                            table.AddCell(new Cell().Add(new Paragraph(detailOrRecommendation)).SetFont(regularFont).SetFontColor(textColor).SetBackgroundColor(bgColor).SetPadding(5));
                        }
                    }
                    else
                    {
                        table.AddCell(new Cell(1, 4).Add(new Paragraph("No benchmarks have been executed. Please run the 'Full Stress Test' first.")).SetFont(regularFont).SetFontColor(ColorConstants.RED));
                    }
                    document.Add(table);

                    // --- AJOUT DE PIED DE PAGE APRÈS CETTE SECTION ---
                    AddPageFooter(pdf, document, regularFont, ColorConstants.GRAY, 10, dedicationText);

                    // --- Analysis and Recommendations Section ---
                    document.Add(new Paragraph("4. Analysis and Recommendations")
                        .SetFont(boldFont).SetFontSize(16).SetFontColor(new DeviceRgb(0, 102, 204))
                        .SetMarginBottom(10));

                    if (benchmarkResults != null && benchmarkResults.Any(r => !r.Success))
                    {
                        document.Add(new Paragraph("The results above highlight **specific points of attention**. It is crucial to analyze the root causes of failures to optimize system performance and stability. Suggested actions include:")
                            .SetFont(regularFont).SetFontSize(11).SetMarginBottom(10));

                        foreach (var result in benchmarkResults.Where(r => !r.Success))
                        {
                            document.Add(new Paragraph($"- **Test '{result.Title}': Failed** with a score of {Math.Round(result.Score)}.")
                                .SetFont(boldFont).SetFontSize(11).SetFontColor(ColorConstants.RED));
                            document.Add(new Paragraph($"  Detail: {result.DetailedResult}")
                                .SetFont(regularFont).SetFontSize(10));
                            document.Add(new Paragraph("  **Recommended Action:** Monitor system resources (CPU, RAM, Disk, GPU) during this test. Check drivers and associated software updates. Consider checking component temperatures.")
                                .SetFont(regularFont).SetFontSize(10).SetMarginBottom(5));
                        }
                    }
                    else if (benchmarkResults != null && benchmarkResults.Any())
                    {
                        document.Add(new Paragraph("All tests passed, confirming the **robustness and efficiency** of the hardware configuration compared to our reference point. The system is ready for intensive and high-performance use.")
                            .SetFont(regularFont).SetFontSize(11).SetMarginBottom(10));
                    }
                    else
                    {
                        document.Add(new Paragraph("No analysis available as no benchmarks have been executed.")
                            .SetFont(regularFont).SetFontSize(11).SetMarginBottom(10));
                    }

                    document.Add(new Paragraph("\n"));

                    // --- AJOUT DE PIED DE PAGE À LA FIN DU DOCUMENT ---
                    AddPageFooter(pdf, document, regularFont, ColorConstants.GRAY, 10, dedicationText);

                    document.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during PDF report generation: {ex.Message}");
                throw new Exception($"Error during PDF report generation: {ex.Message}", ex);
            }
        }

        private static string GenerateExecutiveSummary(List<BenchmarkResult> results, string cpu, string ram, string gpu, string referencePoint)
        {
            if (results == null || !results.Any())
            {
                return "No benchmark tests have been executed to generate a summary. Please run the tests to get a complete analysis.";
            }

            int totalTests = results.Count;
            int successfulTests = results.Count(r => r.Success);
            int failedTests = totalTests - successfulTests;

            List<string> keyFindings = new List<string>();

            if (successfulTests == totalTests)
            {
                keyFindings.Add($"All {totalTests} benchmark tests were **successfully passed**, indicating **excellent system stability and performance** compared to the reference point '{referencePoint}'.");
            }
            else if (failedTests > 0)
            {
                keyFindings.Add($"Out of {totalTests} tests executed, {successfulTests} were successful, and **{failedTests} failed**.");
                var failedTestTitles = results.Where(r => !r.Success).Select(r => r.Title).ToList();
                keyFindings.Add($"Failures are primarily observed on the following tests: **{string.Join(", ", failedTestTitles)}**.");
            }

            string conclusion = "";
            if (failedTests == 0)
            {
                conclusion = "The system demonstrates **remarkable robustness and efficiency** for its current configuration.";
            }
            else if (failedTests > 0 && failedTests <= totalTests / 3)
            {
                conclusion = "Despite a few identified weak points, the system maintains **good overall performance**. Targeted investigations are recommended to optimize affected components.";
            }
            else
            {
                conclusion = "Significant **points for improvement** have been highlighted. A thorough analysis of failing components is crucial for maximum performance optimization.";
            }

            return $"This report presents the benchmark results performed on a system equipped with a **{cpu}**, **{ram}** of RAM, and a **{gpu}**. " +
                   $"These results are compared to the reference point **'{referencePoint}'**. {string.Join(" ", keyFindings)} In conclusion, {conclusion}";
        }
    }
}