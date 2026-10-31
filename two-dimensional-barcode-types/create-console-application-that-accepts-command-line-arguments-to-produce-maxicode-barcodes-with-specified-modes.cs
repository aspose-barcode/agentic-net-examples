// Title: Generate MaxiCode barcodes with selectable mode via command line
// Description: Demonstrates how to create MaxiCode barcodes (mode 2 or 3) using Aspose.BarCode, allowing the mode and output file path to be supplied as command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, showcasing the use of ComplexBarcodeGenerator with MaxiCodeCodetextMode2 and MaxiCodeCodetextMode3 classes. Typical use cases include shipping label creation and logistics applications where MaxiCode is required. Developers often need to customize postal information, service categories, and secondary messages, and this snippet illustrates those common tasks.
// Prompt: Create a console application that accepts command‑line arguments to produce MaxiCode barcodes with specified modes.
// Tags: maxicode, barcode generation, complex barcode, command-line, aspnet.barcode, png output

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Console application that generates a MaxiCode barcode (mode 2 or 3) based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, builds the appropriate MaxiCode codetext, and saves the barcode image.
    /// </summary>
    /// <param name="args">Command‑line arguments: optional mode (2 or 3) and optional output file path.</param>
    static void Main(string[] args)
    {
        // Default mode is 2; default output path is a temporary PNG file.
        int mode = 2;
        string outputPath = Path.Combine(Path.GetTempPath(), "MaxiCode.png");

        // Parse first argument as mode if provided and valid (2 or 3).
        if (args.Length >= 1 && int.TryParse(args[0], out int parsedMode) && (parsedMode == 2 || parsedMode == 3))
        {
            mode = parsedMode;
        }

        // Parse second argument as output file path if provided.
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }

        try
        {
            // Ensure the output directory exists.
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath));

            if (mode == 2)
            {
                // Build codetext for MaxiCode mode 2 (structured message).
                var codetext = new MaxiCodeCodetextMode2
                {
                    PostalCode = "524032140",
                    CountryCode = 56,
                    ServiceCategory = 999
                };

                // Populate the second message with address lines and year.
                var secondMessage = new MaxiCodeStructuredSecondMessage();
                secondMessage.Add("634 ALPHA DRIVE");
                secondMessage.Add("PITTSBURGH");
                secondMessage.Add("PA");
                secondMessage.Year = 99;

                codetext.SecondMessage = secondMessage;

                // Generate and save the barcode image.
                using (var generator = new ComplexBarcodeGenerator(codetext))
                {
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }
            }
            else // mode == 3
            {
                // Build codetext for MaxiCode mode 3 (standard message).
                var codetext = new MaxiCodeCodetextMode3
                {
                    PostalCode = "B1050",
                    CountryCode = 56,
                    ServiceCategory = 999
                };

                // Set a simple second message.
                var secondMessage = new MaxiCodeStandardSecondMessage
                {
                    Message = "Second message"
                };

                codetext.SecondMessage = secondMessage;

                // Generate and save the barcode image.
                using (var generator = new ComplexBarcodeGenerator(codetext))
                {
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                }
            }

            Console.WriteLine($"MaxiCode barcode (mode {mode}) saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation.
            Console.WriteLine($"Error generating MaxiCode barcode: {ex.Message}");
        }
    }
}