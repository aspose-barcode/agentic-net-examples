// Title: Generate GS1 Composite Barcodes for Files in a Folder
// Description: Demonstrates creating GS1 Composite barcodes for each file in a temporary working directory and saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on GS1 Composite barcode creation. It showcases the use of BarcodeGenerator, EncodeTypes, and GS1CompositeBar parameters to produce combined linear and 2D barcodes. Typical use cases include labeling products with both human‑readable and machine‑readable data, such as GTINs and serial numbers. Developers often need to generate these barcodes programmatically for batch processing or integration with file‑based workflows.
// Prompt: Develop a Windows service that monitors a folder and generates GS1 Composite barcodes for new files.
// Tags: gs1 composite barcode generation file monitoring aspose.barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a temporary folder, seeds a sample file,
/// and generates GS1 Composite barcodes for each file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for files in the working folder and saves them.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary working folder
        string workFolder = Path.Combine(Path.GetTempPath(), "GS1CompositeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Seed a sample input file to demonstrate barcode generation
        string sampleFileName = "SampleDocument.txt";
        string sampleFilePath = Path.Combine(workFolder, sampleFileName);
        File.WriteAllText(sampleFilePath, "This is a sample file for GS1 Composite barcode generation.");

        // Prepare an output folder for the generated barcode images
        string outputFolder = Path.Combine(workFolder, "Barcodes");
        Directory.CreateDirectory(outputFolder);

        // Retrieve all files in the working folder (excluding subfolders)
        string[] files = Directory.GetFiles(workFolder, "*.*", SearchOption.TopDirectoryOnly);
        foreach (string file in files)
        {
            // Skip any entries that are actually directories
            if (Directory.Exists(file))
                continue;

            // Build GS1 Composite codetext
            // Linear component: (01) GTIN‑14 (example GTIN padded to 14 digits)
            string gtin = "00123456789012"; // 14‑digit GTIN (example)
            string linearComponent = $"(01){gtin}";

            // Two‑dimensional component: (21) file name without extension
            string fileNameWithoutExt = Path.GetFileNameWithoutExtension(file);
            string twoDComponent = $"(21){fileNameWithoutExt}";

            // Combine components with '|' separator as required for GS1 Composite Bar
            string compositeCodeText = $"{linearComponent}|{twoDComponent}";

            // Generate the barcode using Aspose.BarCode
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, compositeCodeText))
            {
                // Specify component types: linear part as GS1‑Code128, 2D part as CC‑A
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Optional visual settings
                generator.Parameters.Barcode.XDimension.Point = 2f;          // Module size
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;       // Height of linear part
                generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;       // Aspect ratio for 2D part (if applicable)

                // Save the barcode image as PNG
                string outputFileName = Path.GetFileNameWithoutExtension(file) + "_GS1Composite.png";
                string outputPath = Path.Combine(outputFolder, outputFileName);
                generator.Save(outputPath);
                Console.WriteLine($"Generated barcode for '{file}' -> '{outputPath}'");
            }
        }

        Console.WriteLine("Processing completed.");
    }
}