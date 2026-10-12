// Title: Generate Swiss Post Parcel Additional Service Barcodes and Save as SVG
// Description: Demonstrates how to create barcodes for Swiss Post parcel additional services using Aspose.BarCode and export them as SVG files.
// Category-Description: This example belongs to the barcode generation category of Aspose.BarCode, showcasing the use of BarcodeGenerator with EncodeTypes.SwissPostParcel. It illustrates typical scenarios such as producing shipping label barcodes, customizing captions, and handling licensing constraints when exporting to vector formats. Developers working with postal services, logistics, or custom barcode creation will find these patterns useful.
// Prompt: Generate Swiss Post Parcel additional service code barcodes for multiple service descriptions and save as SVG files.
// Tags: swisspost, parcel, additionalservice, barcode, svg, generation, aspose.barcode, encode-types, barcodgenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of Swiss Post parcel additional service barcodes and saving them as SVG files.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes for a predefined list of service codes, applies visual settings, and writes SVG files to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Define service abbreviations and their corresponding Swiss Post additional service codes
        var services = new (string Abbreviation, string Code)[]
        {
            ("GAS", "0203"),
            ("RMP", "0322"),
            ("AR",  "0327"),
            ("eAR", "0328"),
            ("BLN", "0341"),
            ("ID+RMP", "0470"),
            ("CEC", "0610"),
            ("MIL", "1007"),
            ("SAT", "2512") // abbreviation for Saturday delivery (no official abbreviation in table)
        };

        // Create a unique temporary output folder for the generated SVG files
        string outputFolder = Path.Combine(Path.GetTempPath(), "SwissPostAdditionalServices_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);

        // Iterate over each service definition and generate its barcode
        foreach (var (abbr, code) in services)
        {
            // Build the full file path for the current SVG output
            string filePath = Path.Combine(outputFolder, $"{abbr}_AdditionalService.svg");

            // Initialize the barcode generator with SwissPostParcel symbology and the service code
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                // Set basic size parameters
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;

                // Hide the encoded text; the abbreviation will be shown as a caption instead
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

                // Configure the caption displayed above the barcode
                generator.Parameters.CaptionAbove.Visible = true;
                generator.Parameters.CaptionAbove.Alignment = TextAlignment.Left;
                generator.Parameters.CaptionAbove.Text = abbr;
                generator.Parameters.CaptionAbove.Font.Size.Pixels = 24f;
                generator.Parameters.CaptionAbove.Font.Style = FontStyle.Bold;

                // Attempt to save the barcode as SVG, handling evaluation‑mode licensing limitations
                try
                {
                    generator.Save(filePath, BarCodeImageFormat.Svg);
                    Console.WriteLine($"Saved: {filePath}");
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("evaluation"))
                    {
                        Console.WriteLine("SVG export requires a full license. Skipping file: " + filePath);
                    }
                    else
                    {
                        Console.WriteLine($"Error generating barcode for {abbr}: {ex.Message}");
                    }
                }
            }
        }

        Console.WriteLine("Barcode generation completed.");
    }
}