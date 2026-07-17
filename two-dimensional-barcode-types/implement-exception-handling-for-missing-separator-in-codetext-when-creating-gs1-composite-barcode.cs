// Title: GS1 Composite Barcode Generation with Separator Validation
// Description: Demonstrates creating a GS1 Composite barcode, ensuring the linear and 2D components are separated by a '|' character.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on GS1 Composite symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and TwoDComponentType classes to produce combined linear/2D barcodes. Developers often need to validate input data, configure component types, and export the result as an image file; this snippet illustrates those common steps.
/// Prompt: Implement exception handling for missing ‘|’ separator in CodeText when creating a GS1 Composite barcode.
/// Tags: gs1 composite, barcode generation, png output, aspose.barcode, generation, exception handling

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a GS1 Composite barcode, validates the required '|' separator,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates and saves a GS1 Composite barcode.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite barcode code text.
        // Linear and 2D parts must be separated by the '|' character.
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        try
        {
            // Validate that the required separator is present.
            if (!codeText.Contains("|"))
            {
                throw new ArgumentException("GS1 Composite barcode CodeText must contain a '|' separator between linear and 2D components.");
            }

            // Create the barcode generator with GS1 Composite symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
            {
                // Configure linear component type (GS1 Code 128) and 2D component type (CC-A).
                generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
                generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

                // Example visual settings: set module width and bar height.
                generator.Parameters.Barcode.XDimension.Pixels = 3f;
                generator.Parameters.Barcode.BarHeight.Pixels = 100f;

                // Save the generated barcode image to a PNG file.
                string outputPath = "gs1_composite.png";
                generator.Save(outputPath);
                Console.WriteLine($"Barcode saved to '{outputPath}'.");
            }
        }
        catch (ArgumentException ex)
        {
            // Handle validation errors (e.g., missing separator).
            Console.WriteLine($"Error: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors.
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}