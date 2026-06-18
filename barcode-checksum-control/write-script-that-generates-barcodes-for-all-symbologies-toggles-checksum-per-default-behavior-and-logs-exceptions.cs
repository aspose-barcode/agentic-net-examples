using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of barcode images for all supported symbologies using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a PNG barcode for each symbology defined in <see cref="EncodeTypes"/>.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        // Ensure the directory exists.
        Directory.CreateDirectory(outputDir);

        // Retrieve all symbology names defined in EncodeTypes.
        string[] symbologyNames = EncodeTypes.GetNames();

        // Iterate over each symbology name.
        foreach (string name in symbologyNames)
        {
            try
            {
                // Attempt to parse the symbology name into a BaseEncodeType instance.
                if (!EncodeTypes.TryParse(name, out BaseEncodeType encodeType))
                {
                    Console.WriteLine($"Unable to parse symbology name: {name}");
                    continue; // Skip to the next symbology if parsing fails.
                }

                // Sample code text to encode; invalid formats will trigger an exception.
                string sampleCodeText = "1234567890";

                // Create a barcode generator for the current symbology and sample text.
                using (var generator = new BarcodeGenerator(encodeType, sampleCodeText))
                {
                    // Set checksum behavior to the default for the symbology.
                    generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Default;

                    // Build the full file path for the PNG image.
                    string filePath = Path.Combine(outputDir, $"{name}.png");
                    // Save the generated barcode as a PNG file.
                    generator.Save(filePath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode for {name} at {filePath}");
                }
            }
            catch (Exception ex)
            {
                // Log any errors that occur during barcode generation.
                Console.WriteLine($"Error generating barcode for {name}: {ex.Message}");
            }
        }

        // Indicate that the process has finished.
        Console.WriteLine("Barcode generation completed.");
    }
}