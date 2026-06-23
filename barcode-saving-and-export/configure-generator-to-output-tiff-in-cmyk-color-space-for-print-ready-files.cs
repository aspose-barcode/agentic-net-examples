using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, configures resolution,
    /// informs about CMYK limitations, and saves the image.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "PrintReady123"))
        {
            // Set a high resolution (300 DPI) suitable for print quality.
            generator.Parameters.Resolution = 300f;

            // Inform the user that CMYK color space is not directly supported.
            // Aspose.BarCode saves images in the default RGB color space.
            // Achieving true CMYK output would require additional image processing
            // not provided by the current Aspose.BarCode API.
            Console.WriteLine("Note: CMYK color space is not directly supported by Aspose.BarCode. The TIFF will be saved in the default color space.");

            // Save the generated barcode as a TIFF file.
            generator.Save("barcode.tiff");
        }

        // Confirm that the barcode file has been saved.
        Console.WriteLine("Barcode saved as 'barcode.tiff'.");
    }
}