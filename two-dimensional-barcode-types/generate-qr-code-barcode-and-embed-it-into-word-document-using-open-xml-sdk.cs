// Title: Generate QR Code barcode and embed into Word document
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to an image, and inserting it into a Word document using Aspose.Words/Open XML SDK.
// Category-Description: This example belongs to the Aspose.BarCode for .NET barcode generation and Aspose.Words document manipulation category. It shows how to use BarcodeGenerator (EncodeTypes.QR) to produce high‑resolution QR Code images, retrieve their dimensions, and embed them into a DOCX file via DocumentBuilder. Developers working with automated report generation, document templating, or any scenario that requires embedding barcodes into Office documents will find this pattern useful.
// Prompt: Generate QR Code barcode and embed it into a Word document using Open XML SDK.
// Tags: qr code, barcode generation, document embedding, docx, aspose.barcode, aspose.words, openxml

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR Code barcode and embedding it into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code image and inserts it into a DOCX file.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated Word document.
        string outputDocPath = Path.Combine(Environment.CurrentDirectory, "QRCodeInWord.docx");

        // Initialize a new Word document and a DocumentBuilder for content insertion.
        Document wordDoc = new Document();
        DocumentBuilder builder = new DocumentBuilder(wordDoc);

        // Add introductory text to the document.
        builder.Writeln("Document with an embedded QR Code barcode:");

        // Generate a QR Code barcode using Aspose.BarCode.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high resolution to ensure the barcode image is sharp.
            generator.Parameters.Resolution = 300;

            // Produce a bitmap to obtain the image dimensions.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the barcode image to a memory stream in PNG format.
                using (MemoryStream imageStream = new MemoryStream())
                {
                    generator.Save(imageStream, BarCodeImageFormat.Png);
                    imageStream.Position = 0;

                    // Convert pixel dimensions to points (1 inch = 72 points) for Word insertion.
                    float widthPoints = (float)(bitmap.Width * 72) / generator.Parameters.Resolution;
                    float heightPoints = (float)(bitmap.Height * 72) / generator.Parameters.Resolution;

                    // Insert the barcode image into the document with the calculated size.
                    builder.InsertImage(imageStream.ToArray(), widthPoints, heightPoints);
                }
            }
        }

        // Add trailing text to the document.
        builder.Writeln();
        builder.Writeln("End of document.");

        // Save the Word document to the specified path in DOCX format.
        wordDoc.Save(outputDocPath, SaveFormat.Docx);

        // Inform the user where the document was created.
        Console.WriteLine($"Word document created at: {outputDocPath}");
    }
}