// Title: Generate GS1 QR Code with Product Identifier (GTIN-14)
// Description: Demonstrates how to create a QR Code barcode that encodes GS1 data using the Application Identifier (01) for a product's GTIN-14 and saves it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.GS1QR. It shows how to format GS1 data, configure QR error correction, and output the barcode as an image. Developers working on retail, logistics, or inventory systems often need to generate GS1-compliant QR codes for product identification and tracking.
// Prompt: Generate QR Code barcode and encode GS1 data with Application Identifier for product code.
// Tags: qr code,gs1,gtin-14,barcode generation,aspose.barcode,encode types,output png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a GS1 QR Code containing a GTIN‑14 product identifier
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code with GS1 formatting and writes it to disk.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define a sample GTIN‑14 value for the Application Identifier (01). Must be exactly 14 digits.
        string gtin = "00123456789012";

        // Build the GS1 formatted string: (01) followed by the GTIN‑14.
        string gs1CodeText = $"(01){gtin}";

        // Initialize a barcode generator for the GS1 QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1QR))
        {
            // Assign the GS1 formatted text to the generator.
            generator.CodeText = gs1CodeText;

            // Optional: increase error correction to the highest level (Level H) for better resilience.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Define the output file path and save the barcode as a PNG image.
            string outputPath = "gs1qr.png";
            generator.Save(outputPath);

            // Inform the user where the image was saved.
            Console.WriteLine($"GS1 QR Code saved to: {outputPath}");
        }
    }
}