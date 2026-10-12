// Title: Generate GS1 Composite barcodes for newly created files
// Description: Demonstrates creating sample files, then generating GS1 Composite (GS1‑128 + PDF417) barcodes for each file using Aspose.BarCode and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator with EncodeTypes.GS1CompositeBar, configure linear and 2D components, and export barcode images. Typical use cases include encoding product identifiers and serial numbers in a composite format for inventory or logistics applications. Developers often need to combine GS1 linear symbologies with PDF417 for rich data representation.
// Prompt: Develop a Windows service that monitors a folder and generates GS1 Composite barcodes for new files.
// Tags: gs1 composite, barcode generation, png output, aspose.barcode, encode types, pdf417, gs1code128, file monitoring

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample console application that creates temporary text files and generates
/// GS1 Composite barcodes (GS1‑128 linear component + PDF417 2D component) for each file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample files, creates barcodes,
    /// and saves them as PNG images in a temporary output folder.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary input folder and generate sample text files
        // --------------------------------------------------------------------
        string inputFolder = Path.Combine(Path.GetTempPath(), "Input_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(inputFolder);
        List<string> sampleFiles = new List<string>();
        for (int i = 1; i <= 3; i++)
        {
            string filePath = Path.Combine(inputFolder, $"File{i}.txt");
            File.WriteAllText(filePath, $"Sample content {i}");
            sampleFiles.Add(filePath);
        }

        // ---------------------------------------------------------------
        // Create a unique temporary output folder for the barcode images
        // ---------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "Barcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Fixed linear component (valid GTIN‑14) used for all barcodes
        string linearComponent = "(01)01234567890128";

        // ---------------------------------------------------------------
        // Process each sample file and generate a corresponding barcode
        // ---------------------------------------------------------------
        foreach (string filePath in sampleFiles)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                continue;
            }

            // Extract the file name without extension to use as the serial number
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(filePath);
            // Build the 2D component using the serial number (Application Identifier 21)
            string twoDComponent = $"(21){fileNameWithoutExt}";
            // Combine linear and 2D components with the required separator
            string codeText = $"{linearComponent}|{twoDComponent}";

            // Determine the full path for the output PNG image
            string outputPath = Path.Combine(outputFolder, $"{fileNameWithoutExt}.png");

            try
            {
                // Initialize the barcode generator for GS1 Composite symbology
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
                {
                    // Configure the linear component to use GS1 Code128
                    generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                    // Configure the 2D component to use CC_C (PDF417)
                    generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;
                    // Set PDF417 column count for the 2D component
                    generator.Parameters.Barcode.Pdf417.Columns = 30;
                    // Allow non‑GS1 data in the 2D component (required for custom serial numbers)
                    generator.Parameters.Barcode.GS1CompositeBar.AllowOnlyGS1Encoding = false;

                    // Save the generated barcode as a PNG image
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }

                Console.WriteLine($"Generated barcode for '{filePath}' -> '{outputPath}'");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{filePath}': {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Optional cleanup: remove temporary folders (uncomment if desired)
        // --------------------------------------------------------------------
        // Directory.Delete(inputFolder, true);
        // Directory.Delete(outputFolder, true);
    }
}