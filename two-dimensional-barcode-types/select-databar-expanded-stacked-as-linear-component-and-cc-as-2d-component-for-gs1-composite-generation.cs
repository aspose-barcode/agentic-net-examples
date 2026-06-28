using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 Composite barcode with specified linear and 2D components and saves it as an image file.
    /// </summary>
    static void Main()
    {
        // Define the sample codetext.
        // The linear (1D) part and the 2D part are separated by the '|' character.
        string codetext = "(01)03212345678906|(21)A12345678";

        // Determine the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "gs1composite.png");

        // Initialize the barcode generator for a GS1 Composite Bar with the provided codetext.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Configure the linear component to use Databar Expanded Stacked symbology.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.DatabarExpandedStacked;

            // Configure the 2D component to use the CC_A (Composite Component A) symbology.
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Save the generated barcode image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"GS1 Composite barcode saved to: {outputPath}");
    }
}