// Title: Scanning High‑Resolution TIFF Barcode with MaxQuality Setting
// Description: Demonstrates generating a QR barcode saved as a high‑resolution TIFF file and reading it using the MaxQuality preset for optimal scanning accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader for decoding, and QualitySettings to control scanning precision. Typical use cases include processing high‑resolution scanned documents where accurate barcode detection is critical. Developers often need to adjust quality presets, handle multiple symbologies, and manage temporary files when integrating barcode workflows.
// Prompt: Switch QualitySettings.Preset to MaxQuality to prioritize accuracy when scanning high‑resolution TIFF files.
// Tags: qr, tiff, barcode generation, barcode recognition, maxquality, qualitysettings, aspnet, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a QR barcode, saves it as a TIFF image,
/// and reads it back using the MaxQuality preset for high‑accuracy scanning.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with high quality settings,
    /// and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeTiffDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated TIFF file
        string tiffPath = Path.Combine(tempFolder, "sample.tiff");

        // Generate a high‑resolution QR barcode and save it as a TIFF image
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // The generator uses default resolution; adjust size parameters here if needed
            generator.Save(tiffPath, BarCodeImageFormat.Tiff);
        }

        // Verify that the TIFF file was successfully created
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine("Failed to create the TIFF file.");
            return;
        }

        // Initialize a barcode reader for multiple symbologies, targeting the generated TIFF
        using (BarCodeReader reader = new BarCodeReader(
            tiffPath,
            DecodeType.QR,
            DecodeType.Code128,
            DecodeType.DataMatrix,
            DecodeType.Aztec,
            DecodeType.Pdf417))
        {
            // Switch to MaxQuality for the highest detection accuracy
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output detection results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(tiffPath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}