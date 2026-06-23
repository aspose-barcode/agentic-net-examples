using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code39 barcode, saving it as SVG, and reading its viewBox attribute.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to an SVG file, and outputs the SVG viewBox.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output SVG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.svg");

        // Create a barcode generator for Code39FullASCII (supported for SVG in evaluation mode).
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "12345"))
        {
            // Configure padding (margins) around the barcode in points.
            generator.Parameters.Barcode.Padding.Left.Point   = 10f;
            generator.Parameters.Barcode.Padding.Top.Point    = 10f;
            generator.Parameters.Barcode.Padding.Right.Point  = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Attempt to save the barcode as an SVG file.
            try
            {
                generator.Save(outputPath, BarCodeImageFormat.Svg);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
            catch (Exception ex)
            {
                // Handle any errors that occur during the save operation (e.g., evaluation license restrictions).
                Console.WriteLine($"Error saving SVG: {ex.Message}");
                return;
            }
        }

        // Verify that the SVG file was created and read its viewBox attribute.
        if (File.Exists(outputPath))
        {
            try
            {
                // Load the SVG document.
                XDocument svgDoc = XDocument.Load(outputPath);

                // Retrieve the viewBox attribute from the root <svg> element.
                XAttribute viewBoxAttr = svgDoc.Root.Attribute("viewBox");

                if (viewBoxAttr != null)
                {
                    Console.WriteLine($"SVG viewBox: {viewBoxAttr.Value}");
                }
                else
                {
                    Console.WriteLine("viewBox attribute not found in SVG.");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur while reading the SVG file.
                Console.WriteLine($"Error reading SVG: {ex.Message}");
            }
        }
        else
        {
            Console.WriteLine("SVG file was not created.");
        }
    }
}