using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program. Generates and saves a GS1 Composite barcode image.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite barcode code text.
        // Linear and 2D parts must be separated by the '|' character.
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Validate that the required separator is present.
        if (!codeText.Contains("|"))
        {
            Console.WriteLine("Error: GS1 Composite barcode code text must contain a '|' separator between linear and 2D components.");
            return;
        }

        try
        {
            // Create the barcode generator for GS1 Composite Bar.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Configure the linear component to use GS1 Code128.
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

                // Configure the 2D component to use CC-A (Composite Component A).
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Example additional settings (optional).
                generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;          // Set aspect ratio for PDF417 component.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;         // Set X-dimension (module width) in pixels.
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;       // Set bar height for the linear component.

                // Save the generated barcode image to a file.
                string outputPath = "gs1_composite.png";
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to: {outputPath}");
            }
        }
        catch (ArgumentException ex)
        {
            // Handle validation errors (e.g., invalid code text format).
            Console.WriteLine($"Argument error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other Aspose.BarCode exceptions.
            Console.WriteLine($"Failed to generate barcode: {ex.Message}");
        }
    }
}