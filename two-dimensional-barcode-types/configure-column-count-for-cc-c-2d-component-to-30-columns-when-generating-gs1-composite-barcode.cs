using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode with linear and 2D components using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates and saves a GS1 Composite barcode image.
    /// </summary>
    static void Main()
    {
        // Define the GS1 Composite barcode text.
        // Linear part (GS1-128) and 2D part (PDF417) are separated by '|'.
        string codetext = "(01)03212345678906|(21)A12345678";

        // Initialize the barcode generator for GS1 Composite Bar with the specified code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codetext))
        {
            // Specify the linear component type (GS1 Code 128).
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Specify the 2D component type (CC_C, which uses PDF417).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_C;

            // Set the number of columns for the PDF417 component to 30.
            generator.Parameters.Barcode.Pdf417.Columns = 30;

            // Save the generated barcode image to a PNG file.
            generator.Save("gs1_composite_ccc.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated and saved as gs1_composite_ccc.png");
    }
}