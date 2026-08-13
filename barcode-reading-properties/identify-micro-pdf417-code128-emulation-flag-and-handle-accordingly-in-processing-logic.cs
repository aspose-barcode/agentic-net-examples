// Title: Micro PDF417 Barcode Generation with Code128 Emulation
// Description: Demonstrates generating a Micro PDF417 barcode with Code128 emulation enabled and reading back the emulation flag.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a MicroPdf417 symbol, configure the Pdf417.IsCode128Emulation property, and then employ BarCodeReader to decode the image and inspect the Extended.Pdf417.IsCode128Emulation flag. Developers working with compact PDF417 variants or needing Code128 emulation for legacy systems can reference this pattern.
// Prompt: Identify Micro PDF417 Code128 emulation flag and handle accordingly in processing logic.
// Tags: barcode symbology, generation, recognition, micropdf417, code128, emulation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Micro PDF417 barcode with Code128 emulation enabled,
/// saves it to an image file, and then reads the barcode back to
/// verify the emulation flag.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation,
    /// image saving, and decoding with flag inspection.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        string outputPath = "micropdf417.png";

        // Sample codetext: Application Indicator "a" followed by FNC1 (group separator) and data
        string codeText = "a\u001d1222322323";

        // Create a MicroPdf417 barcode generator with the sample codetext
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, codeText))
        {
            // Enable Code128 emulation mode (required for MicroPdf417)
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;

            // Save the generated barcode image to a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode from the saved image and inspect the emulation flag
        using (BarCodeReader reader = new BarCodeReader(outputPath, DecodeType.MicroPdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text
                Console.WriteLine("Decoded CodeText: " + result.CodeText);

                // The extended PDF417 information contains the IsCode128Emulation flag
                bool isEmulation = result.Extended.Pdf417.IsCode128Emulation;
                Console.WriteLine("IsCode128Emulation flag: " + isEmulation);
            }
        }
    }
}