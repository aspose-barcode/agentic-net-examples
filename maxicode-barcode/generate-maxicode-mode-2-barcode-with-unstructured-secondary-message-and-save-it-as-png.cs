// Title: Generate MaxiCode Mode 2 barcode with secondary message
// Description: Demonstrates creating a MaxiCode Mode 2 barcode that includes an unstructured secondary message and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode2 and MaxiCodeStandardSecondMessage classes to produce a MaxiCode with custom data. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode symbols for package tracking and require secondary message support.
// Prompt: Generate a MaxiCode Mode 2 barcode with an unstructured secondary message and save it as PNG.
// Tags: maxicode, barcode generation, png, complexbarcode, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a MaxiCode Mode 2 barcode with an unstructured secondary message
/// and saves the result as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Builds the MaxiCode codetext, generates the barcode image, and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode Mode 2 codetext with an unstructured (standard) secondary message
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",          // 9‑digit postal code required for Mode 2
            CountryCode = 56,                  // 3‑digit country code
            ServiceCategory = 999              // 3‑digit service category
        };

        // Define the secondary message (unstructured)
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Unstructured secondary message"
        };
        maxiCode.SecondMessage = secondMessage;

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Create the barcode image
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Save the image as PNG
                image.Save("maxicode_mode2.png", ImageFormat.Png);
            }
        }

        Console.WriteLine("MaxiCode Mode 2 barcode saved as maxicode_mode2.png");
    }
}