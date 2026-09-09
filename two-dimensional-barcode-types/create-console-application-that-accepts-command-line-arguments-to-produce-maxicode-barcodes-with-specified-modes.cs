// Title: Generate MaxiCode Barcodes (Mode 2/3) via Command Line
// Description: Creates a MaxiCode barcode image using Aspose.BarCode, allowing mode, postal code, country code, service category, secondary message type, and content to be specified via command‑line arguments.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It demonstrates how to use the ComplexBarcodeGenerator with MaxiCodeCodetextMode2 and MaxiCodeCodetextMode3 classes to produce MaxiCode symbols. Typical use cases include shipping labels and logistics applications where MaxiCode is required. Developers often need to customize postal information, service categories, and secondary messages, which this sample shows.
// Prompt: Create a console application that accepts command‑line arguments to produce MaxiCode barcodes with specified modes.
// Tags: maxicode, barcode, generation, command-line, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating MaxiCode barcodes (Mode 2 or Mode 3) using command‑line arguments.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses arguments, builds the appropriate MaxiCode codetext, and saves the barcode image.
    /// </summary>
    /// <param name="args">
    /// Expected arguments:
    /// 0 – mode (2 or 3, default 2)
    /// 1 – output directory (default temporary folder)
    /// 2 – postal code (default depends on mode)
    /// 3 – country code (default 56)
    /// 4 – service category (default 999)
    /// 5 – secondary message type ("structured" or "unstructured", default "unstructured")
    /// 6 – secondary message content (default "Second message")
    /// </param>
    static void Main(string[] args)
    {
        // -------------------- Parse mode (2 or 3) --------------------
        int mode = 2;
        if (args.Length > 0 && int.TryParse(args[0], out int parsedMode) && (parsedMode == 2 || parsedMode == 3))
            mode = parsedMode;

        // -------------------- Determine output directory --------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeOutput");
        if (args.Length > 1 && !string.IsNullOrWhiteSpace(args[1]))
            outputDir = args[1];
        Directory.CreateDirectory(outputDir);

        // -------------------- Postal code (mode‑dependent default) --------------------
        string postalCode = mode == 2 ? "524032140" : "B1050";
        if (args.Length > 2 && !string.IsNullOrWhiteSpace(args[2]))
            postalCode = args[2];

        // -------------------- Country code --------------------
        int countryCode = 56;
        if (args.Length > 3 && int.TryParse(args[3], out int parsedCountry))
            countryCode = parsedCountry;

        // -------------------- Service category --------------------
        int serviceCategory = 999;
        if (args.Length > 4 && int.TryParse(args[4], out int parsedService))
            serviceCategory = parsedService;

        // -------------------- Secondary message type --------------------
        string secondaryType = "unstructured";
        if (args.Length > 5 && !string.IsNullOrWhiteSpace(args[5]))
            secondaryType = args[5].ToLowerInvariant();

        // -------------------- Secondary message content --------------------
        string messageContent = "Second message";
        if (args.Length > 6 && !string.IsNullOrWhiteSpace(args[6]))
            messageContent = args[6];

        // -------------------- Build codetext object based on mode --------------------
        IComplexCodetext codetext;
        if (mode == 2)
        {
            var ct = new MaxiCodeCodetextMode2
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory
            };

            if (secondaryType == "structured")
            {
                var structured = new MaxiCodeStructuredSecondMessage();
                // Split lines by '|', up to three lines
                string[] lines = messageContent.Split('|');
                for (int i = 0; i < Math.Min(lines.Length, 3); i++)
                    structured.Add(lines[i]);
                structured.Year = 99;
                ct.SecondMessage = structured;
            }
            else
            {
                var standard = new MaxiCodeStandardSecondMessage
                {
                    Message = messageContent
                };
                ct.SecondMessage = standard;
            }

            codetext = ct;
        }
        else // mode == 3
        {
            var ct = new MaxiCodeCodetextMode3
            {
                PostalCode = postalCode,
                CountryCode = countryCode,
                ServiceCategory = serviceCategory
            };

            if (secondaryType == "structured")
            {
                var structured = new MaxiCodeStructuredSecondMessage();
                string[] lines = messageContent.Split('|');
                for (int i = 0; i < Math.Min(lines.Length, 3); i++)
                    structured.Add(lines[i]);
                structured.Year = 99;
                ct.SecondMessage = structured;
            }
            else
            {
                var standard = new MaxiCodeStandardSecondMessage
                {
                    Message = messageContent
                };
                ct.SecondMessage = standard;
            }

            codetext = ct;
        }

        // -------------------- Prepare output file path --------------------
        string fileName = $"MaxiCodeMode{mode}_{secondaryType}.png";
        string outputPath = Path.Combine(outputDir, fileName);

        // -------------------- Generate and save barcode --------------------
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"MaxiCode barcode generated: {outputPath}");
    }
}