using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode with a custom background color using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply a LightCoral background color to match the UI theme
            generator.Parameters.BackColor = Color.LightCoral;

            // Save the generated barcode as a PNG file named "barcode.png"
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated and saved
        Console.WriteLine("Barcode generated with LightCoral background: barcode.png");
    }
}