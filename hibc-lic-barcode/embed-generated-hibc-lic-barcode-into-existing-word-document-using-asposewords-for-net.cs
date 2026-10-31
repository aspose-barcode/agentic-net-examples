// Title: Embed HIBC LIC Barcode into a Word Document using Aspose
// Description: Demonstrates generating a HIBC LIC barcode with Aspose.BarCode and inserting it into a Word document via Aspose.Words.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Words integration category, illustrating how to create complex barcodes (HIBC LIC) and embed them into Microsoft Word files. It showcases key API classes such as ComplexBarcodeGenerator, HIBCLICPrimaryDataCodetext, Document, and DocumentBuilder. Developers working on document automation, reporting, or labeling solutions often need to generate barcodes programmatically and place them into office documents for printing or distribution.
// Prompt: Embed a generated HIBC LIC barcode into an existing Word document using Aspose.Words for .NET.
// Tags: barcode, hibc, lic, aspose.barcode, aspose.words, image, word, generation, embedding, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Words;
using Aspose.Drawing;

/// <summary>
/// Generates a HIBC LIC barcode and embeds it into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary Word file if needed,
    /// generates a HIBC LIC barcode, and inserts the barcode image at the end of the document.
    /// </summary>
    static void Main()
    {
        const int resolution = 300; // DPI for high‑resolution barcode rendering

        // Define the path for the temporary Word document
        string docPath = Path.Combine(Path.GetTempPath(), "HIBCLIC_Document.docx");

        // Ensure the document exists; create a simple one if it does not
        if (!File.Exists(docPath))
        {
            Document newDoc = new Document();
            DocumentBuilder newBuilder = new DocumentBuilder(newDoc);
            newBuilder.Writeln("Sample document with HIBC LIC barcode.");
            newDoc.Save(docPath);
        }

        // Configure primary data for the HIBC LIC barcode
        HIBCLICPrimaryDataCodetext primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image and embed it into the existing Word document
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            // Generate a bitmap to obtain the image dimensions in pixels
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the barcode image to a memory stream in PNG format
                using (MemoryStream imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    byte[] imageBytes = imageStream.ToArray();

                    // Load the existing Word document
                    Document doc = new Document(docPath);
                    DocumentBuilder builder = new DocumentBuilder(doc);

                    // Move the cursor to the end of the document and insert the barcode
                    builder.MoveToDocumentEnd();
                    builder.Writeln("HIBC LIC Barcode:");
                    builder.InsertImage(
                        imageBytes,
                        (bitmap.Width * 72) / resolution,   // Convert width from pixels to points
                        (bitmap.Height * 72) / resolution); // Convert height from pixels to points

                    // Save the updated document back to the same path
                    doc.Save(docPath);
                }
            }
        }

        Console.WriteLine($"Barcode embedded successfully into: {docPath}");
    }
}