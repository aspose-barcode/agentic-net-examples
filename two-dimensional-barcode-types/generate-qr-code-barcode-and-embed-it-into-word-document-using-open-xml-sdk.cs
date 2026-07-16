// Title: Generate QR Code and embed into Word document
// Description: Demonstrates creating a QR Code barcode and inserting it into a Word document using Aspose.BarCode and Aspose.Words.
// Category-Description: This example belongs to the Aspose.BarCode generation and Aspose.Words document manipulation category. It showcases how to use BarcodeGenerator (Aspose.BarCode) to produce QR Code images and DocumentBuilder (Aspose.Words) to embed those images into a Word file. Developers often need to automate barcode creation and integrate them into office documents for reporting, labeling, or data sharing scenarios.
// Prompt: Generate QR Code barcode and embed it into a Word document using Open XML SDK.
// Tags: qr code, barcode generation, word document, aspose.barcode, aspose.words, openxml

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Words;
using Aspose.Words.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode and embeds it into a Word document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated Word document.
        string outputDocPath = "QRCodeDocument.docx";

        // Initialize a QR code generator with the desired text/content.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Configure the QR code to use the highest error correction level (Level H).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the generated barcode image to a memory stream in PNG format.
            using (MemoryStream ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset the stream position to the beginning for reading.

                // Create a new Word document and obtain a DocumentBuilder for content insertion.
                Document doc = new Document();
                DocumentBuilder builder = new DocumentBuilder(doc);

                // Insert the barcode image from the memory stream into the document.
                builder.InsertImage(ms);

                // Persist the Word document to the specified file path.
                doc.Save(outputDocPath);
            }
        }

        // Output the full path of the saved document for user reference.
        Console.WriteLine($"Word document with QR code saved to: {Path.GetFullPath(outputDocPath)}");
    }
}