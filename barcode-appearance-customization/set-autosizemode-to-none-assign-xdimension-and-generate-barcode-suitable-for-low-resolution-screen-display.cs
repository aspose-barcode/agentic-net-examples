using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, configures its appearance, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Disable automatic sizing so we can set dimensions manually.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the X-dimension (module width) to 2 points for better visibility on low‑resolution screens.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Define a low resolution (72 DPI) appropriate for screen display.
            generator.Parameters.Resolution = 72f;

            // Save the generated barcode image as a PNG file named "barcode.png".
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Barcode generated: barcode.png");
    }
}