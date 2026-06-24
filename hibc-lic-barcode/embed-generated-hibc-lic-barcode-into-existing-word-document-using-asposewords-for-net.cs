using System;
using System.IO;
using Aspose.Words;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a HIBC LIC secondary‑data‑only barcode,
/// embed it into a Word document, and save the result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Creates a sample Word document if needed, generates a barcode,
    /// inserts the barcode image at the end of the document, and saves it.
    /// </summary>
    static void Main()
    {
        // Paths for the input and output Word documents
        string inputPath = "input.docx";
        string outputPath = "output.docx";

        // Ensure an input document exists; create a simple one if missing
        if (!File.Exists(inputPath))
        {
            // Create a new empty Word document
            var newDoc = new Document();

            // Use DocumentBuilder to add introductory text
            var builder = new DocumentBuilder(newDoc);
            builder.Writeln("Document with embedded HIBC LIC barcode:");

            // Save the newly created document to the input path
            newDoc.Save(inputPath);
        }

        // Prepare HIBC LIC secondary‑data‑only codetext
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = '+',
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SN98765",
                ExpiryDate = DateTime.Today.AddMonths(6),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 10,
                DateOfManufacture = DateTime.Today.AddMonths(-1)
            }
        };

        // Generate the barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        using (var ms = new MemoryStream())
        {
            // Save the barcode as PNG into the memory stream
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for reading

            // Load the existing Word document
            var doc = new Document(inputPath);
            var builder = new DocumentBuilder(doc);

            // Move cursor to the end of the document and insert the barcode image
            builder.MoveToDocumentEnd();
            builder.InsertImage(ms);

            // Save the modified document to the output path
            doc.Save(outputPath);
        }

        // Inform the user that the process completed successfully
        Console.WriteLine($"Barcode embedded and document saved to '{outputPath}'.");
    }
}