// Title: Generate MaxiCode barcode with command‑line parameters
// Description: Demonstrates creating a MaxiCode barcode image using Aspose.BarCode, allowing mode, output path, and message to be supplied via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, IComplexCodetext implementations (MaxiCodeCodetextMode2, MaxiCodeCodetextMode3, MaxiCodeStandardCodetext) and related message classes to produce PNG images. Developers needing to integrate shipping or logistics barcodes into .NET applications can adapt this pattern for various MaxiCode modes and custom data.
// Prompt: Create a console application that accepts command‑line arguments to produce MaxiCode barcodes with specified modes.
// Tags: maxicode, barcode, generation, command-line, aspnet, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console application that generates a MaxiCode barcode image based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments for mode, output file path, and message, then creates and saves the barcode.
    /// </summary>
    /// <param name="args">Command‑line arguments: [mode] [outputPath] [message].</param>
    static void Main(string[] args)
    {
        // Default values for optional parameters
        int mode = 2; // default MaxiCode mode
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode.png");
        string message = "Sample message";

        // Parse mode argument (first argument)
        if (args.Length > 0 && int.TryParse(args[0], out int parsedMode))
        {
            mode = parsedMode;
        }

        // Parse output path argument (second argument)
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }

        // Parse message argument (third argument)
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
        {
            message = args[2];
        }

        // Validate that the requested mode is supported (2‑6)
        if (mode < 2 || mode > 6)
        {
            Console.WriteLine("Supported MaxiCode modes are 2, 3, 4, 5, and 6.");
            return;
        }

        try
        {
            // Build the appropriate codetext object based on the selected mode
            IComplexCodetext codetext;

            if (mode == 2)
            {
                // Mode 2 requires postal code, country code, and service category
                var ct = new MaxiCodeCodetextMode2
                {
                    PostalCode = "524032140",
                    CountryCode = 56,
                    ServiceCategory = 999
                };
                // Attach a secondary message
                var second = new MaxiCodeStandardSecondMessage { Message = message };
                ct.SecondMessage = second;
                codetext = ct;
            }
            else if (mode == 3)
            {
                // Mode 3 uses alphanumeric postal code
                var ct = new MaxiCodeCodetextMode3
                {
                    PostalCode = "B1050",
                    CountryCode = 56,
                    ServiceCategory = 999
                };
                var second = new MaxiCodeStandardSecondMessage { Message = message };
                ct.SecondMessage = second;
                codetext = ct;
            }
            else // Modes 4, 5, 6 share the standard codetext structure
            {
                var ct = new MaxiCodeStandardCodetext
                {
                    Mode = (MaxiCodeMode)mode,
                    Message = message
                };
                codetext = ct;
            }

            // Generate the barcode image using the complex barcode generator
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Ensure the target directory exists before saving
                    string dir = Path.GetDirectoryName(outputPath);
                    if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
                    {
                        Directory.CreateDirectory(dir);
                    }

                    // Save the generated image as PNG
                    bitmap.Save(outputPath, ImageFormat.Png);
                }
            }

            Console.WriteLine($"MaxiCode barcode (mode {mode}) saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during generation
            Console.WriteLine($"Error generating MaxiCode barcode: {ex.Message}");
        }
    }
}