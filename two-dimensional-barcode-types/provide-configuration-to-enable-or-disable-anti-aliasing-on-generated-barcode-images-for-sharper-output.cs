using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes with and without anti-aliasing using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two barcode images:
    /// one with anti-aliasing enabled and one with it disabled.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate a barcode with anti-aliasing enabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Enable anti-aliasing to smooth the barcode edges
            generator.Parameters.UseAntiAlias = true;

            // Save the generated barcode to a PNG file
            generator.Save("barcode_aa_enabled.png");

            // Inform the user that the file has been saved
            Console.WriteLine("Saved barcode with anti-aliasing enabled.");
        }

        // ------------------------------------------------------------
        // Generate a barcode with anti-aliasing disabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // Disable anti-aliasing for a crisp, pixelated appearance
            generator.Parameters.UseAntiAlias = false;

            // Save the generated barcode to a PNG file
            generator.Save("barcode_aa_disabled.png");

            // Inform the user that the file has been saved
            Console.WriteLine("Saved barcode with anti-aliasing disabled.");
        }
    }
}