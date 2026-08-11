// Title: Feature Flag for Multithreaded Barcode Reading
// Description: Demonstrates how to enable or disable multithreaded barcode reading at application startup using a command‑line flag.
// Category-Description: This example belongs to the Aspose.BarCode reading category, illustrating the use of BarCodeReader.ProcessorSettings to control CPU core utilization. Developers often need to toggle multithreading for performance tuning or resource‑constrained environments; the key API classes involved are BarCodeReader, ProcessorSettings, and BarcodeGenerator. The snippet shows typical steps: configure settings, generate a barcode, and read it.
// Prompt: Implement a feature flag that enables or disables multithreaded barcode reading at application startup.
// Tags: barcode symbology, barcode reading, multithreading, processor settings, aspose.barcode, console app, code128

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode;

/// <summary>
/// Sample console application that shows how to toggle multithreaded barcode reading using a feature flag.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Parses a boolean flag to enable or disable multithreading, configures the Aspose.BarCode processor,
    /// generates a sample Code128 barcode, and reads it.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument should be 'true' or 'false' to control multithreading.</param>
    static void Main(string[] args)
    {
        // --------------------------------------------------------------------
        // Parse feature flag from command line (default: true)
        // --------------------------------------------------------------------
        bool enableMultithreading = true;
        if (args.Length > 0)
        {
            if (!bool.TryParse(args[0], out enableMultithreading))
            {
                Console.WriteLine("Invalid flag value. Use 'true' or 'false'. Defaulting to true.");
                enableMultithreading = true;
            }
        }

        // --------------------------------------------------------------------
        // Configure processor settings based on the flag
        // --------------------------------------------------------------------
        BarCodeReader.ProcessorSettings.UseAllCores = enableMultithreading;
        if (!enableMultithreading)
        {
            // Restrict to a single core when multithreading is disabled.
            BarCodeReader.ProcessorSettings.UseAllCores = false;
            BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 1;
        }

        // --------------------------------------------------------------------
        // Generate a sample barcode image
        // --------------------------------------------------------------------
        string imagePath = "sample_barcode.png";
        GenerateSampleBarcode(imagePath);

        // --------------------------------------------------------------------
        // Verify the image exists before attempting to read
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Barcode image not found at '{imagePath}'.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using the configured processor settings
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }

    // ------------------------------------------------------------------------
    // Generates a simple Code128 barcode and saves it to the specified path.
    // ------------------------------------------------------------------------
    private static void GenerateSampleBarcode(string path)
    {
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Save as PNG using the appropriate overload.
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }
}