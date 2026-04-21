using System;
using System.IO;
using Aspose.Words;
using Aspose.Words.Drawing;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Paths for the input and output Word documents
        string inputPath = "InputDocument.docx";
        string outputPath = "OutputDocument.docx";

        // Ensure the input document exists; if not, create a simple placeholder document
        Document doc;
        if (File.Exists(inputPath))
        {
            doc = new Document(inputPath);
        }
        else
        {
            doc = new Document();
            var placeholderBuilder = new DocumentBuilder(doc);
            placeholderBuilder.Writeln("Placeholder document created because the input file was not found.");
        }

        // Prepare a DocumentBuilder for inserting the barcode image
        DocumentBuilder builder = new DocumentBuilder(doc);
        builder.MoveToDocumentEnd();

        // Create HIBC LIC secondary‑and‑additional‑data codetext
        var hibcCodetext = new HIBCLICSecondaryAndAdditionalDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            LinkCharacter = 'L',
            Data = new SecondaryAndAdditionalData
            {
                LotNumber = "LOT123",
                SerialNumber = "SERIAL123"
                // Additional fields (e.g., ExpiryDate) can be set here if needed
            }
        };

        // Generate the barcode image and insert it into the Word document
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            using (var imageStream = new MemoryStream())
            {
                // Save the barcode as PNG into the memory stream
                generator.Save(imageStream, BarCodeImageFormat.Png);
                // Insert the image bytes at the current builder position
                builder.InsertImage(imageStream.ToArray());
            }
        }

        // Save the modified document
        doc.Save(outputPath);
        Console.WriteLine($"Barcode embedded and document saved to '{outputPath}'.");
    }
}