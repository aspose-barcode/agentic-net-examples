// Title: Micro PDF417 Barcode Generation with Code128 Emulation and Verification
// Description: Demonstrates generating a Micro PDF417 barcode with the Code128 emulation flag enabled, then reads the barcode to confirm the flag state.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating Micro PDF417 symbols, configuring the Pdf417.IsCode128Emulation property, and employing BarCodeReader to decode and inspect extended PDF417 metadata. Developers working with high‑density barcodes or needing to emulate Code128 within Micro PDF417 will find such patterns useful for encoding, validation, and integration scenarios.
// Prompt: Identify Micro PDF417 Code128 emulation flag and handle accordingly in processing logic.
// Tags: barcode, micro-pdf417, code128 emulation, generation, recognition, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Micro PDF417 barcode with Code128 emulation,
/// reads it back to verify the emulation flag, and cleans up temporary files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, verification, and cleanup.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "MicroPdf417Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "MicroPdf417_Code128Emulation.png");

        // ------------------------------------------------------------
        // Generate a Micro PDF417 barcode with the Code128 emulation flag set
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MicroPdf417, "123456789012345678"))
        {
            // Set barcode visual parameters
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Enable Code128 emulation for Micro PDF417
            generator.Parameters.Barcode.Pdf417.IsCode128Emulation = true;

            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify the emulation flag by reading the generated barcode
        // ------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.MicroPdf417))
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("CodeText: " + result.CodeText);

                    // Retrieve the Code128 emulation flag from the extended PDF417 data
                    bool isEmulation = result.Extended.Pdf417.IsCode128Emulation;
                    Console.WriteLine("IsCode128Emulation: " + isEmulation);

                    // Output a friendly message based on the flag state
                    if (isEmulation)
                    {
                        Console.WriteLine("The barcode is in Code128 emulation mode.");
                    }
                    else
                    {
                        Console.WriteLine("The barcode is NOT in Code128 emulation mode.");
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("Failed to generate barcode image.");
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}