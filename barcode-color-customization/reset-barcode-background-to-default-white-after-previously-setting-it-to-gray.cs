using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating Code128 barcodes with different background colors using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcode images: one with a gray background and one with the default white background.
    /// </summary>
    static void Main()
    {
        // Initialize a BarcodeGenerator for Code128 format with the sample text "123456".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Configure the generator to use a gray background.
            generator.Parameters.BackColor = Color.Gray;
            // Save the barcode image with the gray background to a PNG file.
            generator.Save("barcode_gray.png");

            // Change the background color back to white (default).
            generator.Parameters.BackColor = Color.White;
            // Save the barcode image with the white background to a separate PNG file.
            generator.Save("barcode_white.png");
        }

        // Output a confirmation message to the console.
        Console.WriteLine("Barcodes generated successfully.");
    }
}