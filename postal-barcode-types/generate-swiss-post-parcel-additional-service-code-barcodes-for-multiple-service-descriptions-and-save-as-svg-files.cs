// Title: Generate Swiss Post Parcel additional service barcodes as SVG
// Description: Demonstrates creating Swiss Post Parcel barcodes for various additional services, adding a caption, and saving each as an SVG file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the BarcodeGenerator class with EncodeTypes.SwissPostParcel, configuring visual parameters, and using BarCodeReader for verification. Developers working with postal barcode standards often need to generate service-specific codes and validate them, making this pattern useful for batch processing and automated testing.
// Prompt: Generate Swiss Post Parcel additional service code barcodes for multiple service descriptions and save as SVG files.
// Tags: swisspost, parcel, additional service, barcode generation, svg, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Swiss Post Parcel additional service barcodes and saving them as SVG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for a predefined list of service codes, saves each as SVG,
    /// optionally falls back to PNG, and verifies the generated barcode using <see cref="BarCodeReader"/>.
    /// </summary>
    static void Main()
    {
        // Define service codes together with their abbreviations and descriptions.
        var services = new List<(string Code, string Abbreviation, string Description)>
        {
            ("0203", "GAS", "Business reply label"),
            ("0322", "RMP", "Personal delivery"),
            ("0327", "AR",  "Return receipt"),
            ("0328", "eAR", "Electronic return receipt"),
            ("0340", "COD", "Cash on delivery (obsolete)"),
            ("0341", "BLN", "Electronic cash on delivery"),
            ("0470", "IDR", "ID Check"),
            ("0610", "CEC", "Items for the blind"),
            ("1007", "MIL", "Military mail"),
            ("2512", "SAT", "Second attempted delivery on Saturday")
        };

        // Prepare output directory.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "SwissPostAdditionalService");
        Directory.CreateDirectory(outputDir);

        // Process each service definition.
        foreach (var service in services)
        {
            // Build file name and full path for the SVG output.
            string fileName = $"{service.Abbreviation}_AdditionalService.svg";
            string filePath = Path.Combine(outputDir, fileName);

            // Create a barcode generator for the Swiss Post Parcel symbology using the service code.
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, service.Code))
            {
                // Set basic size parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Hide the encoded text; we will display the abbreviation as a caption.
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Configure the caption that appears above the barcode.
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
                generator.Parameters.CaptionAbove.Text = service.Abbreviation;
                generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
                generator.Parameters.CaptionAbove.Font.Style = Aspose.Drawing.FontStyle.Bold;

                try
                {
                    // Attempt to save the barcode as an SVG file.
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved SVG: {filePath}");
                }
                catch (Exception ex)
                {
                    // If SVG saving fails, fall back to PNG.
                    Console.WriteLine($"Failed to save SVG for {service.Abbreviation}: {ex.Message}");
                    string pngPath = Path.ChangeExtension(filePath, ".png");
                    try
                    {
                        generator.Save(pngPath, BarCodeImageFormat.Png);
                        Console.WriteLine($"Saved fallback PNG: {pngPath}");
                    }
                    catch (Exception fallbackEx)
                    {
                        Console.WriteLine($"Fallback PNG also failed: {fallbackEx.Message}");
                    }
                }
            }

            // Optional verification: read back the generated barcode.
            try
            {
                using (var reader = new BarCodeReader(filePath, DecodeType.SwissPostParcel))
                {
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Read {service.Abbreviation}: Type={result.CodeTypeName}, Text={result.CodeText}");
                    }
                }
            }
            catch (Exception readEx)
            {
                Console.WriteLine($"Reading barcode for {service.Abbreviation} failed: {readEx.Message}");
            }
        }
    }
}