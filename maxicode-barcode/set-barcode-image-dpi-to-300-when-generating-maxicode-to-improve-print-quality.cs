// Title: Generate a MaxiCode barcode with 300 DPI resolution
// Description: Demonstrates how to set the image resolution to 300 DPI when creating a MaxiCode barcode, improving print quality for high‑resolution output.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on image resolution settings. It showcases the use of BarcodeGenerator, EncodeTypes, and the Resolution property to control DPI. Developers often need to adjust DPI for printing or publishing barcodes at higher quality, and this snippet illustrates the typical steps for configuring and saving a high‑resolution barcode image.
// Prompt: Set the barcode image DPI to 300 when generating a MaxiCode to improve print quality.
// Tags: maxicode, dpi, resolution, barcode generation, aspose.barcode, image output, png

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode image with a resolution of 300 DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a MaxiCode barcode, sets its DPI to 300, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a MaxiCode generator with sample codetext
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            // Configure the image resolution (dots per inch) to 300
            generator.Parameters.Resolution = 300f;

            // Generate the barcode image as a bitmap
            using (Aspose.Drawing.Bitmap image = generator.GenerateBarCodeImage())
            {
                // Persist the bitmap to a PNG file
                image.Save("maxicode_300dpi.png");
            }
        }
    }
}