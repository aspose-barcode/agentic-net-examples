// Title: Batch Generation of GS1 Composite Barcodes from CSV
// Description: Demonstrates reading a CSV file and creating a GS1 Composite barcode for each record, saving them as PNG images.
// Category-Description: This example belongs to the Aspose.BarCode batch processing category. It showcases how to use the BarcodeGenerator class with EncodeTypes.GS1CompositeBar, configure linear and 2D components, and output images. Typical use cases include generating product barcodes in bulk for inventory, shipping, or retail labeling. Developers often need to read data sources, build GS1‑compliant codetext, and automate image creation.
// Prompt: Create a batch job that reads a CSV file and produces GS1 Composite barcodes for each record.
// Tags: gs1 composite, barcode generation, csv processing, aspose barcode, png output, batch processing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates batch creation of GS1 Composite barcodes from a CSV file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary workspace, generates a sample CSV,
    /// reads each record, builds the GS1 Composite codetext, and saves the barcode images.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a unique temporary working directory
        string workDir = Path.Combine(Path.GetTempPath(), "Gs1CompositeBatch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Paths for the sample CSV input and the output folder for barcodes
        string csvPath = Path.Combine(workDir, "input.csv");
        string outputDir = Path.Combine(workDir, "Barcodes");
        Directory.CreateDirectory(outputDir);

        // Generate a small sample CSV file (GTIN,Serial) with 5 records
        using (var writer = new StreamWriter(csvPath))
        {
            writer.WriteLine("00123456789012,ABC001");
            writer.WriteLine("00123456789013,ABC002");
            writer.WriteLine("00123456789014,ABC003");
            writer.WriteLine("00123456789015,ABC004");
            writer.WriteLine("00123456789016,ABC005");
        }

        // Verify the CSV file exists
        if (!File.Exists(csvPath))
        {
            Console.WriteLine("CSV file not found: " + csvPath);
            return;
        }

        // Process each record and generate a GS1 Composite barcode
        int index = 1;
        using (var reader = new StreamReader(csvPath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                if (string.IsNullOrWhiteSpace(line))
                    continue; // Skip empty lines

                string[] parts = line.Split(',');
                if (parts.Length < 2)
                {
                    Console.WriteLine($"Skipping malformed line {index}: {line}");
                    continue;
                }

                string gtin = parts[0].Trim();   // Must be 14 digits for AI (01)
                string serial = parts[1].Trim(); // Example serial number for AI (21)

                // Basic validation of GTIN length; pad with leading zeros if necessary
                if (gtin.Length < 14)
                    gtin = gtin.PadLeft(14, '0');
                else if (gtin.Length > 14)
                {
                    Console.WriteLine($"GTIN length exceeds 14 digits on line {index}: {gtin}");
                    continue;
                }

                // Build the GS1 Composite codetext using '|' as the separator
                string codeText = $"(01){gtin}|(21){serial}";

                // Define output file name
                string outputPath = Path.Combine(outputDir, $"barcode_{index:D3}.png");

                // Generate the barcode
                using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
                {
                    // Set linear component to GS1 Code128
                    generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                    // Set 2D component type (CC_A is a MicroPDF417 variant)
                    generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                    // Optional visual parameters
                    generator.Parameters.Barcode.XDimension.Pixels = 3f;
                    generator.Parameters.Barcode.BarHeight.Pixels = 100f;
                    generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;

                    // Save the barcode image as PNG
                    generator.Save(outputPath);
                }

                Console.WriteLine($"Generated barcode {index}: {outputPath}");
                index++;
            }
        }

        Console.WriteLine("Barcode generation completed.");
        Console.WriteLine("All files are located in: " + workDir);
    }
}