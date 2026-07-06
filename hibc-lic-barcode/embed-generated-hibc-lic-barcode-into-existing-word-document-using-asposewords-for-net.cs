// Title: Embed HIBC LIC barcode into a Word document
// Description: Demonstrates generating a HIBC LIC barcode with secondary data and embedding it into an existing or new Word document using Aspose.Words.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Words integration category, showing how to create complex barcodes (HIBC LIC) with the ComplexBarcodeGenerator and insert the resulting image into a Word file via DocumentBuilder. Developers often need to automate document generation with embedded barcodes for labeling, tracking, and compliance purposes.
// Prompt: Embed a generated HIBC LIC barcode into an existing Word document using Aspose.Words for .NET.
// Tags: hibc lic barcode generation, image insertion, aspnet, aspose.words, aspose.barcode, document automation

using System;
using System.IO;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates embedding a generated HIBC LIC barcode into a Word document using Aspose.Words.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a HIBC LIC barcode with secondary data, creates or loads a Word document,
    /// inserts the barcode image, and saves the file.
    /// </summary>
    static void Main()
    {
        // Define the path to the target Word document.
        const string wordFilePath = "SampleDocument.docx";

        // Prepare HIBC LIC secondary-data-only codetext.
        var secondaryCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = '+',
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SN456",
                ExpiryDate = DateTime.Today.AddMonths(6),
                ExpiryDateFormat = HIBCLICDateFormat.MMDDYY,
                Quantity = 10,
                DateOfManufacture = DateTime.Today.AddMonths(-1)
            }
        };

        // Generate the barcode image and store it in a memory stream.
        using (var generator = new ComplexBarcodeGenerator(secondaryCodetext))
        using (Bitmap bitmap = generator.GenerateBarCodeImage())
        using (var imageStream = new MemoryStream())
        {
            // Save the bitmap as PNG into the stream.
            bitmap.Save(imageStream, ImageFormat.Png);
            imageStream.Position = 0; // Reset stream position for reading.

            // Load the existing Word document or create a new one if it does not exist.
            Document doc;
            if (File.Exists(wordFilePath))
            {
                doc = new Document(wordFilePath);
            }
            else
            {
                doc = new Document();
                // Add an initial paragraph so the document is not empty.
                var builderInit = new DocumentBuilder(doc);
                builderInit.Writeln("Document created by Aspose.Words.");
            }

            // Insert the barcode image at the end of the document.
            var builder = new DocumentBuilder(doc);
            builder.MoveToDocumentEnd();
            builder.InsertParagraph();
            builder.InsertImage(imageStream);

            // Save the modified document back to the same file.
            doc.Save(wordFilePath);
        }

        Console.WriteLine("HIBC LIC barcode embedded into Word document successfully.");
    }
}