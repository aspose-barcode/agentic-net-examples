using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode with specified dimensions and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator with Code128 symbology and the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Disable automatic sizing; we'll set dimensions manually.
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set the barcode's bar height to 30 millimeters.
            generator.Parameters.Barcode.BarHeight.Millimeters = 30f;   // BarCodeHeight = 30 mm

            // Set the overall image width to 50 millimeters.
            generator.Parameters.ImageWidth.Millimeters = 50f;       // BarCodeWidth = 50 mm

            // Save the generated barcode as a JPEG file named "barcode.jpg".
            generator.Save("barcode.jpg");
        }

        // Inform the user that the barcode has been successfully generated.
        Console.WriteLine("Barcode generated and saved as barcode.jpg");
    }
}