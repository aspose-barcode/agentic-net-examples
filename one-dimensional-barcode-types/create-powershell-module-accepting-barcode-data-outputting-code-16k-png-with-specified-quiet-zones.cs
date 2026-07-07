// Title: Generate Code 16K barcode PNG with custom quiet zones
// Description: Demonstrates creating a Code 16K barcode image using Aspose.BarCode, allowing input data and quiet‑zone coefficients via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the BarcodeGenerator class with EncodeTypes.Code16K. It illustrates typical use cases such as customizing quiet zones and exporting to PNG, which developers often need when integrating barcode creation into scripts or CI pipelines.
// Prompt: Create PowerShell module accepting barcode data, outputting Code 16K PNG with specified quiet zones.
// Tags: code16k, barcode, generation, png, quietzone, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace Code16KGenerator
{
    /// <summary>
    /// Generates a Code 16K barcode image (PNG) using Aspose.BarCode.
    /// Accepts optional command‑line arguments for the barcode data and quiet‑zone coefficients.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// Parses arguments, configures the barcode generator, and saves the PNG file.
        /// </summary>
        /// <param name="args">
        /// args[0] – barcode data (default: "1234567890123456")
        /// args[1] – left quiet‑zone coefficient (default: 10)
        /// args[2] – right quiet‑zone coefficient (default: 1)
        /// </param>
        static void Main(string[] args)
        {
            // Default barcode data and quiet‑zone coefficients
            string codeText = "1234567890123456";
            int leftQuietZone = 10;   // default left coefficient
            int rightQuietZone = 1;   // default right coefficient

            // Override defaults with command‑line arguments, if provided
            if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
                codeText = args[0];

            if (args.Length > 1 && int.TryParse(args[1], out int left))
                leftQuietZone = left;

            if (args.Length > 2 && int.TryParse(args[2], out int right))
                rightQuietZone = right;

            // Output file name for the generated PNG image
            string outputPath = "code16k.png";

            // Initialize the barcode generator for Code 16K symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
            {
                // Apply the specified quiet‑zone coefficients
                generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = leftQuietZone;
                generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = rightQuietZone;

                // Save the barcode image as PNG
                generator.Save(outputPath);
            }

            // Inform the user where the file was saved
            Console.WriteLine($"Code 16K barcode saved to '{Path.GetFullPath(outputPath)}'.");
        }
    }
}