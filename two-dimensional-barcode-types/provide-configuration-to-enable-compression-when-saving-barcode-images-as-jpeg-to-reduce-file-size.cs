using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a compressed JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a barcode, applies compression settings, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 format with the sample text "1234567890".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set the image resolution to 72 DPI to reduce the output file size.
            generator.Parameters.Resolution = 72f;

            // Disable anti-aliasing; this further reduces the JPEG size at the cost of visual smoothness.
            generator.Parameters.UseAntiAlias = false;

            // Save the generated barcode as a JPEG file using the compression settings defined above.
            generator.Save("barcode_compressed.jpg");

            // Inform the user that the barcode has been saved with the applied compression.
            Console.WriteLine("Barcode saved as 'barcode_compressed.jpg' with compression settings.");
        }
    }
}