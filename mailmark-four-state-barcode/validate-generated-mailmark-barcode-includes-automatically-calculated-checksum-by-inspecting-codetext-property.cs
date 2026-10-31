// Title: Validate Mailmark barcode checksum via Codetext inspection
// Description: Demonstrates generating a Mailmark barcode, retrieving its automatically calculated checksum from the Codetext, and verifying the checksum by decoding the Codetext.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of MailmarkCodetext, ComplexBarcodeGenerator, and ComplexCodetextReader to create, inspect, and decode Mailmark barcodes—common tasks for developers handling postal and logistics barcodes that require checksum validation.
// Prompt: Validate generated Mailmark barcode includes automatically calculated checksum by inspecting Codetext property.
// Tags: mailmark, barcode, checksum, codetext, generation, decoding, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Mailmark barcode, inspecting its Codetext for the automatically calculated checksum,
/// and verifying the checksum by decoding the Codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, saves it as an image,
    /// prints the constructed Codetext (including checksum), and validates it by decoding.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "mailmark.png");

        // Create Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "1",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Get the constructed codetext, which includes the automatically calculated checksum
        string constructedCode = mailmark.GetConstructedCodetext();
        Console.WriteLine("Constructed Mailmark Codetext (includes checksum):");
        Console.WriteLine(constructedCode);

        // Generate the barcode image using ComplexBarcodeGenerator and save it to a file
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            using (var stream = new MemoryStream())
            {
                generator.Save(stream, BarCodeImageFormat.Png);
                File.WriteAllBytes(imagePath, stream.ToArray());
            }
        }

        Console.WriteLine($"Mailmark barcode image saved to: {imagePath}");

        // Decode the codetext back to verify that the checksum is present and correct
        var decoded = ComplexCodetextReader.TryDecodeMailmark(constructedCode);
        if (decoded != null)
        {
            Console.WriteLine("Decoded Mailmark codetext successfully.");
            Console.WriteLine($"VersionID: {decoded.VersionID}, Class: {decoded.Class}, ItemID: {decoded.ItemID}");
        }
        else
        {
            Console.WriteLine("Failed to decode Mailmark codetext.");
        }
    }
}