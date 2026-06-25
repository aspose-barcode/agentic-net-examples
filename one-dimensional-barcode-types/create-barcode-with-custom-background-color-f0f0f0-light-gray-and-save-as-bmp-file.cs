using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode using Aspose.BarCode and saving it as a BMP file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, sets a custom background color,
    /// saves it to disk, and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 with the sample text "123456"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Apply a light gray background color (#F0F0F0) to the barcode image
            generator.Parameters.BackColor = Color.FromArgb(0xF0, 0xF0, 0xF0);

            // Save the generated barcode image as a BMP file named "barcode.bmp"
            generator.Save("barcode.bmp");
        }

        // Output a confirmation message to the console
        Console.WriteLine("Barcode generated and saved as barcode.bmp");
    }
}