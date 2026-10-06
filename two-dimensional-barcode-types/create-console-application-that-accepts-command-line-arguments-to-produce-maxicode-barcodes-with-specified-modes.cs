// Title: Generate MaxiCode barcodes with selectable modes via command‑line
// Description: Demonstrates how to create MaxiCode barcodes in modes 2‑6 using Aspose.BarCode, accepting mode and output path as command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of BarcodeGenerator, ComplexBarcodeGenerator, and related MaxiCode codetext classes (MaxiCodeCodetextMode2, MaxiCodeCodetextMode3, etc.). Developers often need to generate shipping labels or logistics tags where different MaxiCode modes encode varying data structures; this snippet provides a ready‑to‑run console app for those scenarios.
// Prompt: Create a console application that accepts command‑line arguments to produce MaxiCode barcodes with specified modes.
// Tags: maxicode, barcode generation, command-line, aspose.barcode, complexbarcode, encode types, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Console application that generates MaxiCode barcodes based on command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, creates output directory, selects barcode mode, and saves the image.
    /// </summary>
    /// <param name="args">First argument: mode (2‑6). Second argument: output file path.</param>
    static void Main(string[] args)
    {
        // Determine mode argument; default to mode 4 if none provided.
        string modeArg = args.Length > 0 ? args[0] : "4";

        // Determine output file path; default to "MaxiCodeMode{mode}.png" in current directory.
        string outputPath = args.Length > 1 ? args[1] : $"MaxiCodeMode{modeArg}.png";

        try
        {
            // Ensure the output directory exists.
            Directory.CreateDirectory(Path.GetDirectoryName(outputPath) ?? ".");

            // Choose generation routine based on requested mode.
            switch (modeArg)
            {
                case "2":
                    GenerateMode2(outputPath);
                    break;
                case "3":
                    GenerateMode3(outputPath);
                    break;
                case "4":
                case "5":
                case "6":
                    GenerateSimpleMode(outputPath, modeArg);
                    break;
                default:
                    Console.WriteLine($"Unsupported mode '{modeArg}'. Generating default mode 4.");
                    GenerateSimpleMode(outputPath, "4");
                    break;
            }

            Console.WriteLine($"MaxiCode barcode generated at: {Path.GetFullPath(outputPath)}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode in Mode 2, which includes structured secondary message data.
    /// </summary>
    /// <param name="path">File path where the barcode image will be saved.</param>
    static void GenerateMode2(string path)
    {
        // Populate codetext for Mode 2 with postal, country, and service information.
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build a structured second message (address lines, state, year).
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;

        codetext.SecondMessage = structuredMessage;

        // Use ComplexBarcodeGenerator to create and save the barcode.
        using (var complexGenerator = new ComplexBarcodeGenerator(codetext))
        {
            complexGenerator.Save(path);
        }
    }

    /// <summary>
    /// Generates a MaxiCode barcode in Mode 3, which includes a standard second message.
    /// </summary>
    /// <param name="path">File path where the barcode image will be saved.</param>
    static void GenerateMode3(string path)
    {
        // Populate codetext for Mode 3 with postal, country, and service information.
        var codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Create a simple standard second message.
        var standardMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Second message"
        };

        codetext.SecondMessage = standardMessage;

        // Use ComplexBarcodeGenerator to create and save the barcode.
        using (var complexGenerator = new ComplexBarcodeGenerator(codetext))
        {
            complexGenerator.Save(path);
        }
    }

    /// <summary>
    /// Generates a simple MaxiCode barcode for modes 4, 5, or 6 using the basic BarcodeGenerator.
    /// </summary>
    /// <param name="path">File path where the barcode image will be saved.</param>
    /// <param name="mode">String representation of the mode (\"4\", \"5\", or \"6\").</param>
    static void GenerateSimpleMode(string path, string mode)
    {
        // Initialize generator with EncodeTypes.MaxiCode and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, $"Sample text for mode {mode}"))
        {
            // Set the specific MaxiCode mode based on the input argument.
            switch (mode)
            {
                case "4":
                    generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;
                    break;
                case "5":
                    generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode5;
                    break;
                case "6":
                    generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode6;
                    break;
                default:
                    generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;
                    break;
            }

            // Save the generated barcode as a PNG image.
            generator.Save(path, BarCodeImageFormat.Png);
        }
    }
}