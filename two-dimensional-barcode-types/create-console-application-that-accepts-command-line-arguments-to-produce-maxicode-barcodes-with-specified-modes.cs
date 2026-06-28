using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of MaxiCode barcodes in various modes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Parses command‑line arguments to select a MaxiCode mode and output file,
    /// then generates the corresponding barcode.
    /// </summary>
    /// <param name="args">
    /// args[0] (optional) – integer mode number (2‑6). Defaults to 2 if omitted or invalid.<br/>
    /// args[1] (optional) – output file path. Defaults to "maxicode.png" if omitted.
    /// </param>
    static void Main(string[] args)
    {
        // Default mode is 2 (Mode2) and default output file name.
        int modeNumber = 2;
        string outputPath = "maxicode.png";

        // Attempt to parse the first argument as a mode number (2‑6).
        if (args.Length > 0 && int.TryParse(args[0], out int parsedMode) && parsedMode >= 2 && parsedMode <= 6)
        {
            modeNumber = parsedMode;
        }
        else if (args.Length > 0)
        {
            // Invalid mode argument supplied – inform the user and keep default.
            Console.WriteLine("Invalid mode argument. Using default Mode2.");
        }

        // If a second argument is provided, use it as the output file path.
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
        {
            outputPath = args[1];
        }

        try
        {
            // Generate the barcode according to the selected mode.
            switch (modeNumber)
            {
                case 2:
                    GenerateMode2(outputPath);
                    break;
                case 3:
                    GenerateMode3(outputPath);
                    break;
                case 4:
                    GenerateStandardMode(outputPath, MaxiCodeMode.Mode4);
                    break;
                case 5:
                    GenerateStandardMode(outputPath, MaxiCodeMode.Mode5);
                    break;
                case 6:
                    GenerateStandardMode(outputPath, MaxiCodeMode.Mode6);
                    break;
                default:
                    // This case should never be reached because of earlier validation.
                    Console.WriteLine("Unsupported mode. No barcode generated.");
                    break;
            }

            Console.WriteLine($"MaxiCode barcode (Mode{modeNumber}) saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Report any errors that occur during barcode generation.
            Console.WriteLine($"Error generating barcode: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode in Mode 2 and saves it to the specified path.
    /// </summary>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    private static void GenerateMode2(string outputPath)
    {
        // Prepare codetext for Mode 2 (postal code, country code, service category).
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9‑digit US postal code
            CountryCode = 56,         // Example country code
            ServiceCategory = 999     // Example service category
        };

        // Optional second message (standard for Mode 2/3).
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        codetext.SecondMessage = secondMessage;

        // Generate and save the barcode.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode in Mode 3 and saves it to the specified path.
    /// </summary>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    private static void GenerateMode3(string outputPath)
    {
        // Prepare codetext for Mode 3 (alphanumeric postal code, country code, service category).
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050", // 6‑character alphanumeric postal code
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Optional second message (standard for Mode 2/3).
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        codetext.SecondMessage = secondMessage;

        // Generate and save the barcode.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode in a standard mode (4, 5, or 6) and saves it.
    /// </summary>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    /// <param name="mode">The MaxiCode mode to use (Mode4, Mode5, or Mode6).</param>
    private static void GenerateStandardMode(string outputPath, MaxiCodeMode mode)
    {
        // Prepare codetext for the selected standard mode.
        var codetext = new MaxiCodeStandardCodetext
        {
            Mode = mode,
            Message = "Sample message"
        };

        // Generate and save the barcode.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath);
        }
    }
}