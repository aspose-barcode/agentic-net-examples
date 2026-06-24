using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a MaxiCode barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a standard MaxiCode codetext (Mode 4) with a simple message.
        var maxiCode = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode4,
            Message = "Sample MaxiCode"
        };

        // Initialize ComplexBarcodeGenerator with the codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Set a custom margin of 10 points on all sides.
            generator.Parameters.Barcode.Padding.Left.Point   = 10f;
            generator.Parameters.Barcode.Padding.Top.Point    = 10f;
            generator.Parameters.Barcode.Padding.Right.Point  = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Save the generated barcode image to a PNG file.
            generator.Save("maxicode.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("MaxiCode barcode generated: maxicode.png");
    }
}