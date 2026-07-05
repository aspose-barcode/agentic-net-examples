// Title: Generate Barcodes for All Symbologies with Default Checksum
// Description: The program iterates through every barcode symbology supported by Aspose.BarCode, creates a sample barcode, toggles checksum to its default behavior, saves the image, and logs any errors.
// Prompt: Write a script that generates barcodes for all symbologies, toggles checksum per default behavior, and logs exceptions.
// Tags: barcode, symbology, generation, checksum, exception handling, aspose.barcode

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a PNG barcode image for each supported symbology,
/// applying the default checksum behavior and handling any runtime exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, enumerates all symbologies,
    /// generates sample barcodes, saves them as PNG files, and logs progress or errors.
    /// </summary>
    static void Main()
    {
        // Create (or reuse) the output directory for generated barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Retrieve the list of all symbology names defined in EncodeTypes.
        string[] symNames = EncodeTypes.GetNames();

        // Process each symbology individually.
        foreach (string name in symNames)
        {
            try
            {
                // Resolve the symbology name to its corresponding BaseEncodeType via reflection.
                FieldInfo field = typeof(EncodeTypes).GetField(name, BindingFlags.Public | BindingFlags.Static);
                if (field == null)
                {
                    Console.WriteLine($"[WARN] Symbology field not found: {name}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Generate a sample codetext appropriate for the current symbology.
                string codeText = GetSampleCodeText(name);

                // Initialize the barcode generator with the resolved type and sample text.
                using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Apply the default checksum behavior (enabled/disabled as defined by the symbology).
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Default;

                    // Construct the full file path for the PNG output.
                    string filePath = Path.Combine(outputDir, $"{name}.png");

                    // Save the generated barcode image.
                    generator.Save(filePath);
                    Console.WriteLine($"[INFO] Generated barcode for {name} -> {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any exception that occurs while processing the current symbology.
                Console.WriteLine($"[ERROR] Failed to generate {name}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Provides a generic sample codetext for a given symbology.
    /// Certain symbologies require specific formats, which are handled via a switch expression.
    /// </summary>
    /// <param name="symName">The name of the symbology.</param>
    /// <returns>A sample codetext string suitable for the specified symbology.</returns>
    static string GetSampleCodeText(string symName)
    {
        // Default fallback codetext.
        const string defaultText = "1234567890";

        // Adjust codetext for known symbologies that have strict format requirements.
        return symName switch
        {
            nameof(EncodeTypes.EAN13) => "1234567890128",               // Valid EAN‑13 with checksum
            nameof(EncodeTypes.EAN8) => "12345670",                    // Valid EAN‑8 with checksum
            nameof(EncodeTypes.UPCA) => "123456789012",                // Valid UPC‑A
            nameof(EncodeTypes.UPCE) => "0123456",                     // Valid UPC‑E
            nameof(EncodeTypes.DatabarLimited) => "(01)08888888888888",
            nameof(EncodeTypes.DatabarOmniDirectional) => "(01)12345678901231",
            _ => defaultText
        };
    }
}