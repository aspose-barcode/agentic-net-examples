// Title: Batch barcode generation from XML with unit conversion
// Description: Demonstrates reading a barcode generation specification from an XML file, converting measurement units from points to pixels, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to export and import barcode settings via XML, perform unit conversions, and produce image files. It uses BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and related parameter classes. Developers often need to automate batch barcode creation from stored specifications, adjust dimensions, and output common image formats.
// Prompt: Develop batch job reading barcode specs from XML, applying unit conversions, and saving PNGs to directory.
// Tags: barcode symbology, generation, xml, unit conversion, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a batch process that creates a barcode, exports its configuration to XML,
/// re-imports the configuration, converts measurement units, and saves the barcode as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, export, import, conversion, and saving steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Step 0: Prepare a unique temporary working folder for all files.
        // --------------------------------------------------------------------
        string workFolder = Path.Combine(Path.GetTempPath(), "BatchBarcodes_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Path for the XML specification that will hold the barcode generation state.
        string xmlSpecPath = Path.Combine(workFolder, "spec.xml");

        // --------------------------------------------------------------------
        // Step 1: Generate a sample QR barcode and export its generation state to XML.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "SampleCode"))
        {
            // Set initial dimensions using points (1 point = 1/72 inch).
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Persist the current configuration to an XML file.
            generator.ExportToXml(xmlSpecPath);
        }

        // Verify that the XML file was created successfully.
        if (!File.Exists(xmlSpecPath))
        {
            Console.WriteLine("Specification XML not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Step 2: Import the saved configuration, convert units to pixels, and save as PNG.
        // --------------------------------------------------------------------
        try
        {
            using (var generator = BarcodeGenerator.ImportFromXml(xmlSpecPath))
            {
                // Convert XDimension from points to pixels (1 point ≈ 1.3333 pixels).
                float xDimPoints = generator.Parameters.Barcode.XDimension.Point;
                generator.Parameters.Barcode.XDimension.Pixels = xDimPoints * 1.3333f;

                // Convert image width and height from points to pixels.
                float widthPoints = generator.Parameters.ImageWidth.Point;
                float heightPoints = generator.Parameters.ImageHeight.Point;
                generator.Parameters.ImageWidth.Pixels = widthPoints * 1.3333f;
                generator.Parameters.ImageHeight.Pixels = heightPoints * 1.3333f;

                // Optionally set foreground and background colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the final barcode image as a PNG file.
                string outputPath = Path.Combine(workFolder, "barcode.png");
                generator.Save(outputPath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error processing barcode: {ex.Message}");
        }
    }
}