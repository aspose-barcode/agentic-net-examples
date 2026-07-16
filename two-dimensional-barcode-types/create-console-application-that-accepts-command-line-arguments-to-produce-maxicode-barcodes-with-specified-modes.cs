// Title: Generate MaxiCode barcodes with selectable modes via command‑line
// Description: Demonstrates how to create MaxiCode barcodes in modes 2‑6 using Aspose.BarCode, accepting mode and output path as command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2‑3, and MaxiCodeStandardCodetext classes to encode postal, country, and service data. Developers creating shipping labels, parcel tracking, or logistics solutions often need to generate MaxiCode symbols with specific modes and custom messages.
// Prompt: Create a console application that accepts command‑line arguments to produce MaxiCode barcodes with specified modes.
// Tags: maxicode, barcode generation, command-line, aspnet, aspose.barcode, complex barcode, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console application that generates MaxiCode barcodes based on command‑line input.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts a MaxiCode mode (2‑6) and an optional output file path,
    /// then creates the corresponding barcode image.
    /// </summary>
    /// <param name="args">
    /// Command‑line arguments:
    /// <list type="bullet">
    ///   <item><description>args[0] – Desired MaxiCode mode (integer 2‑6). If omitted, defaults to 2.</description></item>
    ///   <item><description>args[1] – Output file path. If omitted, defaults to "maxicode_mode{mode}.png".</description></item>
    /// </list>
    /// </param>
    static void Main(string[] args)
    {
        // Determine MaxiCode mode (2‑6). Default to Mode2.
        int mode = 2;
        if (args.Length > 0 && int.TryParse(args[0], out int parsedMode))
        {
            if (parsedMode >= 2 && parsedMode <= 6)
                mode = parsedMode;
            else
            {
                Console.WriteLine("Invalid mode specified. Supported modes are 2,3,4,5,6.");
                return;
            }
        }

        // Determine output file path. Default to "maxicode_mode{mode}.png".
        string outputPath = args.Length > 1 ? args[1] : $"maxicode_mode{mode}.png";

        // Ensure the target directory exists.
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        // Build appropriate codetext based on the selected mode.
        IComplexCodetext codetext;
        switch (mode)
        {
            case 2:
                var mode2 = new MaxiCodeCodetextMode2
                {
                    PostalCode = "524032140", // 9‑digit US postal code
                    CountryCode = 56,         // Example country code
                    ServiceCategory = 999     // Example service category
                };
                var secondMsg2 = new MaxiCodeStandardSecondMessage
                {
                    Message = "Sample message for Mode 2"
                };
                mode2.SecondMessage = secondMsg2;
                codetext = mode2;
                break;

            case 3:
                var mode3 = new MaxiCodeCodetextMode3
                {
                    PostalCode = "B1050", // 6‑character alphanumeric postal code
                    CountryCode = 56,
                    ServiceCategory = 999
                };
                var secondMsg3 = new MaxiCodeStandardSecondMessage
                {
                    Message = "Sample message for Mode 3"
                };
                mode3.SecondMessage = secondMsg3;
                codetext = mode3;
                break;

            case 4:
            case 5:
            case 6:
                var standard = new MaxiCodeStandardCodetext
                {
                    Mode = (MaxiCodeMode)mode, // Cast to appropriate enum value
                    Message = $"Sample message for Mode {mode}"
                };
                codetext = standard;
                break;

            default:
                // This should never happen because of earlier validation.
                Console.WriteLine("Unsupported mode.");
                return;
        }

        // Generate the MaxiCode barcode and save it as a PNG image.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            using (Image image = generator.GenerateBarCodeImage())
            {
                image.Save(outputPath, ImageFormat.Png);
            }
        }

        Console.WriteLine($"MaxiCode barcode (Mode {mode}) saved to: {outputPath}");
    }
}