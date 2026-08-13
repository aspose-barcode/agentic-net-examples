// Title: Generate and read a Mailmark complex barcode using Aspose.BarCode
// Description: Demonstrates creating a Mailmark complex barcode, saving it to a PNG image in a memory stream, and reading it back with BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator for barcode creation and BarCodeReader for decoding, common tasks for developers integrating barcode workflows into applications that require high‑density data encoding and verification.
// Prompt: Dispose of BarCodeReader and ComplexBarcodeGenerator objects in a finally block to ensure resource cleanup.
// Tags: mailmark, complex barcode, generation, recognition, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Mailmark complex barcode, saves it to a memory stream,
/// and then reads the barcode back to display its type and text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation and reading of a Mailmark barcode.
    /// </summary>
    static void Main()
    {
        // Prepare a simple Mailmark codetext (valid sample)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,               // 4-state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required
        };

        BarCodeReader reader = null;
        ComplexBarcodeGenerator generator = null;
        MemoryStream barcodeStream = null;

        try
        {
            // Generate the complex barcode and save it to a memory stream as PNG
            generator = new ComplexBarcodeGenerator(mailmark);
            barcodeStream = new MemoryStream();
            generator.Save(barcodeStream, BarCodeImageFormat.Png);
            barcodeStream.Position = 0; // Reset stream position for reading

            // Read the barcode from the generated image
            reader = new BarCodeReader(barcodeStream, DecodeType.Mailmark);
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected type: {result.CodeTypeName}");
                Console.WriteLine($"Code text: {result.CodeText}");
            }
        }
        finally
        {
            // Ensure resources are released even if an exception occurs
            if (reader != null)
                reader.Dispose();

            if (generator != null)
                generator.Dispose();

            if (barcodeStream != null)
                barcodeStream.Dispose();
        }
    }
}