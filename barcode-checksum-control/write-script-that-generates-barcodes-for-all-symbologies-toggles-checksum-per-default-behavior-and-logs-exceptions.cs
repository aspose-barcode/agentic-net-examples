// Title: Generate Barcodes for All Symbologies with Checksum Toggle
// Description: The example iterates through every supported barcode symbology, creates PNG images with checksum enabled and disabled, and logs any errors encountered.
// Category-Description: This sample belongs to the Aspose.BarCode generation category, demonstrating how to enumerate EncodeTypes, configure checksum behavior via the BarcodeGenerator.Parameters.Barcode.IsChecksumEnabled property, and save images using BarCodeImageFormat. Developers often need to produce barcode assets in bulk for testing, documentation, or batch processing, and this pattern shows the typical workflow for handling multiple symbologies and handling exceptions.
// Prompt: Write a script that generates barcodes for all symbologies, toggles checksum per default behavior, and logs exceptions.
// Tags: barcode, symbology, generation, checksum, aspose.barcode, png, exception-handling

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates bulk generation of barcode images for every supported symbology,
/// toggling the checksum setting on and off, and logging any exceptions that occur.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a temporary output folder,
    /// iterates over all EncodeTypes, generates barcode images with checksum
    /// enabled and disabled, and writes status messages to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated barcode images.
        string outputDir = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        Console.WriteLine("Output directory: " + outputDir);

        // Retrieve all public static fields of the EncodeTypes class (each represents a symbology).
        FieldInfo[] fields = typeof(EncodeTypes).GetFields(BindingFlags.Public | BindingFlags.Static);
        foreach (FieldInfo field in fields)
        {
            // Skip fields that are not barcode symbology types.
            if (!typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
                continue;

            // Get the actual EncodeType instance and its name.
            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            string symbologyName = field.Name;
            string baseFileName = Path.Combine(outputDir, $"{symbologyName}");

            // Sample code text; using a static numeric string for simplicity.
            string codeText = "123456";

            // ------------------------------------------------------------
            // Generate barcode with checksum enabled.
            // ------------------------------------------------------------
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Enable checksum calculation for this symbology.
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                    string filePath = baseFileName + "_ChecksumOn.png";

                    // Save the generated barcode as a PNG image.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated (checksum on): {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while generating with checksum enabled.
                Console.WriteLine($"Error generating {symbologyName} with checksum enabled: {ex.Message}");
            }

            // ------------------------------------------------------------
            // Generate barcode with checksum disabled.
            // ------------------------------------------------------------
            try
            {
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Disable checksum calculation for this symbology.
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;
                    string filePath = baseFileName + "_ChecksumOff.png";

                    // Save the generated barcode as a PNG image.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated (checksum off): {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur while generating with checksum disabled.
                Console.WriteLine($"Error generating {symbologyName} with checksum disabled: {ex.Message}");
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}