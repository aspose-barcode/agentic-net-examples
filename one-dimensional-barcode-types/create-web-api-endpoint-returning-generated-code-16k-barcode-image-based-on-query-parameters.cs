// Title: Generate Code 16K Barcode Image via Console Parameters
// Description: Demonstrates generating a Code 16K barcode image using Aspose.BarCode and saving it to a file. The example shows how to configure X dimension, aspect ratio, and quiet‑zone coefficients based on input parameters.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It illustrates the use of the EncodeTypes, BarcodeGenerator, and related parameter classes to create high‑density linear barcodes. Typical scenarios include creating shipping labels, inventory tags, or any application that requires Code 16K symbology. Developers often need to adjust dimensions, aspect ratios, and quiet zones to meet printer or scanner specifications.
// Prompt: Create web API endpoint returning generated Code 16K barcode image based on query parameters.
// Tags: code16k, barcode, generation, png, aspose.barcode, console, parameters

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Console application that generates a Code 16K barcode image based on supplied parameters
/// and saves the result to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Parses command‑line arguments, configures the barcode generator,
    /// and writes the barcode image to the specified output path.
    /// </summary>
    /// <param name="args">Key‑value pairs in the form key=value (e.g., codetext=Hello).</param>
    static void Main(string[] args)
    {
        // Default values for simulated query parameters
        string codeText = "Aspose.BarCode";
        float xDimensionPixels = 2f;
        int aspectRatio = 10;
        int quietZoneLeftCoef = 10;
        int quietZoneRightCoef = 10;
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "Code16K.png");

        // --------------------------------------------------------------------
        // Parse command‑line arguments (key=value). Invalid values abort execution.
        // --------------------------------------------------------------------
        foreach (string arg in args)
        {
            if (string.IsNullOrWhiteSpace(arg) || !arg.Contains("="))
                continue;

            var parts = arg.Split(new[] { '=' }, 2);
            var key = parts[0].Trim().ToLowerInvariant();
            var value = parts[1].Trim();

            try
            {
                switch (key)
                {
                    case "codetext":
                        codeText = value;
                        break;
                    case "xdimension":
                        xDimensionPixels = float.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "aspectratio":
                        aspectRatio = int.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "quietzoneleft":
                        quietZoneLeftCoef = int.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "quietzoneright":
                        quietZoneRightCoef = int.Parse(value, System.Globalization.CultureInfo.InvariantCulture);
                        break;
                    case "output":
                        outputPath = value;
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Invalid value for '{key}': {ex.Message}");
                return;
            }
        }

        // --------------------------------------------------------------------
        // Ensure the output directory exists before attempting to save the file.
        // --------------------------------------------------------------------
        string outDir = Path.GetDirectoryName(outputPath);
        if (!string.IsNullOrEmpty(outDir) && !Directory.Exists(outDir))
        {
            Directory.CreateDirectory(outDir);
        }

        // --------------------------------------------------------------------
        // Create and configure the Code 16K barcode generator.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code16K, codeText))
        {
            // Set the X dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels;

            // Set the aspect ratio; values greater than 8 are recommended for readability.
            generator.Parameters.Barcode.Code16K.AspectRatio = aspectRatio;

            // Configure quiet zone coefficients (minimum 10 for left, 1 for right).
            generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = Math.Max(10, quietZoneLeftCoef);
            generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = Math.Max(1, quietZoneRightCoef);

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Code 16K barcode generated: {outputPath}");
    }
}