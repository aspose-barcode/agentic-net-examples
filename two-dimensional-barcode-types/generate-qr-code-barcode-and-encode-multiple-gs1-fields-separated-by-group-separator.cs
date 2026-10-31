// Title: Generate GS1 QR Code with Multiple Application Identifiers
// Description: Demonstrates how to create a GS1 QR Code barcode that encodes several GS1 fields separated by the group separator and then reads it back.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator with EncodeTypes.GS1QR to produce a QR Code containing GS1 Application Identifiers, and how to verify the result using BarCodeReader with DecodeType.GS1QR. Developers working with product identification, inventory management, or any GS1‑based workflows commonly need to generate and decode such barcodes.
/// Prompt: Generate QR Code barcode and encode multiple GS1 fields separated by group separator.
// Tags: qr code, gs1, barcode generation, barcode recognition, aspose.barcode, png, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a GS1 QR Code containing multiple Application Identifiers,
/// saves it as a PNG image, and then reads the barcode to verify the encoded data.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, saves it, and then decodes it for verification.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary output directory for the generated image.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // ---------------------------------------------------------------
        // Define GS1 QR code data containing multiple Application Identifiers.
        // ---------------------------------------------------------------
        // (01) – GTIN, (21) – Serial Number, (30) – Quantity
        string gs1Data = "(01)12345678901231(21)ASPOSE(30)9876";

        // ---------------------------------------------------------------
        // Build the full file path for the barcode image to be saved.
        // ---------------------------------------------------------------
        string barcodePath = Path.Combine(outputDir, "gs1qr.png");

        // ---------------------------------------------------------------
        // Generate the GS1 QR barcode using Aspose.BarCode.
        // ---------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.GS1QR, gs1Data))
        {
            // Set the X-dimension (module size) to 8 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 8;

            // Save the generated barcode as a PNG file.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {barcodePath}");

        // ---------------------------------------------------------------
        // Verify the generated barcode by reading it back.
        // ---------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.GS1QR))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }
    }
}