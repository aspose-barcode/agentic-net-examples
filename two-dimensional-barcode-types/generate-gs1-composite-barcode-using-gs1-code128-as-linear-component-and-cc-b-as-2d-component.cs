using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 Composite barcode with a linear GTIN component and a 2D serial component,
    /// then saves the image to a file.
    /// </summary>
    static void Main()
    {
        // Define the code text: linear part (01) GTIN and 2D part (21) serial, separated by '|'
        string codetext = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Initialize a barcode generator for GS1 Composite barcodes with the specified code text
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Configure the linear component to use GS1 Code128 encoding
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Configure the 2D component to use CC_B (MicroPDF417) encoding
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_B;

            // Define the output file path for the generated barcode image
            string outputPath = "gs1_composite.png";

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);

            // Inform the user that the barcode has been saved
            Console.WriteLine($"GS1 Composite barcode saved to {outputPath}");
        }
    }
}