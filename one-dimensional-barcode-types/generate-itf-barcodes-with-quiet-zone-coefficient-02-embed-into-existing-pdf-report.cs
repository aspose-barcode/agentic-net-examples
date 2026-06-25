using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Pdf;

/// <summary>
/// Demonstrates embedding an ITF14 barcode into an existing PDF document using Aspose libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates an ITF14 barcode and inserts it into the first page of a PDF.
    /// </summary>
    static void Main()
    {
        // Define file paths for the source PDF and the resulting PDF with the barcode.
        const string sourcePdfPath = "report.pdf";
        const string outputPdfPath = "report_with_barcode.pdf";

        // Ensure the source PDF exists before proceeding.
        if (!File.Exists(sourcePdfPath))
        {
            Console.WriteLine($"Source PDF not found: {sourcePdfPath}");
            return;
        }

        // Sample ITF14 barcode text (must be 14 numeric characters).
        const string itfCodeText = "12345678901234";

        // Desired quiet zone coefficient (the API requires an integer >= 10).
        const float desiredQuietZoneCoef = 0.2f;

        // MemoryStream will hold the generated barcode image in PNG format.
        using (var barcodeStream = new MemoryStream())
        {
            // Create a barcode generator for ITF14 using the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, itfCodeText))
            {
                // The QuietZoneCoef property expects an integer >= 10.
                // If the requested coefficient is invalid, inform the user and keep the default.
                if (desiredQuietZoneCoef < 10f)
                {
                    Console.WriteLine("Quiet zone coefficient 0.2 is below the minimum allowed value (10). Using default quiet zone.");
                }
                else
                {
                    // This block would set a valid quiet zone coefficient if needed.
                    generator.Parameters.Barcode.ITF.QuietZoneCoef = (int)desiredQuietZoneCoef;
                }

                // Enable automatic sizing of the barcode image using interpolation.
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

                // Save the generated barcode image to the memory stream in PNG format.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading it for PDF insertion.
            barcodeStream.Position = 0;

            // Load the existing PDF document.
            var pdfDocument = new Document(sourcePdfPath);

            // Access the first page of the PDF where the barcode will be placed.
            var page = pdfDocument.Pages[1];

            // Create an Aspose.Pdf.Image object using the barcode stream.
            var pdfImage = new Aspose.Pdf.Image
            {
                ImageStream = barcodeStream,
                // Set fixed dimensions for the barcode image (adjust as needed).
                FixWidth = 200.0,
                FixHeight = 100.0
            };

            // Add the image to the page's paragraph collection.
            page.Paragraphs.Add(pdfImage);

            // Save the modified PDF to the specified output path.
            pdfDocument.Save(outputPdfPath);
        }

        // Inform the user that the operation completed successfully.
        Console.WriteLine($"Barcode embedded successfully into '{outputPdfPath}'.");
    }
}