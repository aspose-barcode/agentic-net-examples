// Title: Detect Barcodes in Password‑Protected PDFs and Images
// Description: Demonstrates loading a password‑protected PDF (or regular image), converting it to a bitmap, and reading barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating how to handle secure image sources. It shows using Aspose.Pdf to open password‑protected PDFs, converting pages to images with Aspose.Drawing, and reading barcodes with BarCodeReader. Developers often need to process protected documents in automated pipelines, requiring credential handling and robust detection.
// Prompt: Handle password‑protected image files by supplying credentials before barcode detection in secure pipelines.
// Tags: barcode detection, pdf password, aspose.barcode, aspose.pdf, image processing, barcodereader, decodeall

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Pdf;
using Aspose.Pdf.Facades;

/// <summary>
/// Example program that loads a password‑protected PDF (or a regular image),
/// converts it to a bitmap, and detects barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the input file (could be a password‑protected PDF or a regular image)
        string inputPath = "protected.pdf";
        // Password for the protected PDF (if applicable)
        string pdfPassword = "secret";

        // If the file does not exist, create a simple barcode image to demonstrate the flow.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"File '{inputPath}' not found. Generating a sample barcode image.");

            // Generate a sample Code128 barcode image.
            using (var generator = new Aspose.BarCode.Generation.BarcodeGenerator(
                Aspose.BarCode.Generation.EncodeTypes.Code128, "Sample123"))
            {
                string sampleImagePath = "sample.png";
                generator.Save(sampleImagePath);
                inputPath = sampleImagePath; // Use the generated image for reading.
            }
        }

        // Determine processing based on file extension.
        string extension = Path.GetExtension(inputPath).ToLowerInvariant();

        // Bitmap that will hold the image to be scanned.
        Aspose.Drawing.Bitmap barcodeBitmap = null;

        if (extension == ".pdf")
        {
            // Handle password‑protected PDF.
            try
            {
                // Load the PDF with the supplied password.
                var pdfDocument = new Document(inputPath, pdfPassword);

                // Convert the first page to an image.
                var pdfConverter = new PdfConverter(pdfDocument);
                pdfConverter.RenderingOptions.BarcodeOptimization = true;
                pdfConverter.StartPage = 1;
                pdfConverter.EndPage = 1;
                pdfConverter.DoConvert();

                using (var imageStream = new MemoryStream())
                {
                    pdfConverter.GetNextImage(imageStream);
                    imageStream.Position = 0;
                    barcodeBitmap = new Aspose.Drawing.Bitmap(imageStream);
                }

                pdfConverter.Close();
                pdfDocument.Dispose();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to open PDF: {ex.Message}");
                return;
            }
        }
        else
        {
            // Assume a regular image file.
            try
            {
                barcodeBitmap = new Aspose.Drawing.Bitmap(inputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load image: {ex.Message}");
                return;
            }
        }

        // Ensure the bitmap was created.
        if (barcodeBitmap == null)
        {
            Console.WriteLine("No image available for barcode detection.");
            return;
        }

        // Perform barcode detection.
        using (barcodeBitmap)
        using (var reader = new BarCodeReader(barcodeBitmap, DecodeType.AllSupportedTypes))
        {
            // Optional: improve detection of damaged barcodes.
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            int count = 0;
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                count++;

                // Limit to first 5 barcodes for safety.
                if (count >= 5)
                    break;
            }

            if (count == 0)
                Console.WriteLine("No barcodes were detected in the image.");
        }
    }
}