// Title: Generate MaxiCode Mode 4 Barcode and Save as BMP
// Description: Creates a MaxiCode Mode 4 barcode with default primary data and writes it to a BMP image file.
// Category-Description: This example demonstrates how to use Aspose.BarCode's ComplexBarcodeGenerator to produce a MaxiCode barcode, specifically Mode 4, which is commonly used for shipping and logistics. It showcases the MaxiCodeStandardCodetext class for setting mode and message, configuring image resolution, and saving the result in BMP format. Developers working with advanced barcode symbologies can refer to this pattern for generating other complex barcodes.
// Prompt: Generate a MaxiCode Mode 4 barcode with default primary data and store the result in BMP format.
// Tags: maxicode, barcode generation, bmp, aspose.barcode, complexbarcode, csharp

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a MaxiCode Mode 4 barcode and saving it as a BMP image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode barcode, configures parameters, and saves the image.
    /// </summary>
    static void Main()
    {
        // Initialize standard codetext for MaxiCode Mode 4
        var maxiCodeCodetext = new MaxiCodeStandardCodetext();
        maxiCodeCodetext.Mode = MaxiCodeMode.Mode4;          // Set barcode mode to Mode 4
        maxiCodeCodetext.Message = "Test message";          // Set the primary data message

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Optional: define image resolution (dots per inch)
            generator.Parameters.Resolution = 300;

            // Define output file name and format
            string outputFile = "maxicode_mode4.bmp";

            // Save the generated barcode as BMP
            generator.Save(outputFile, BarCodeImageFormat.Bmp);
        }

        // Inform the user that the barcode has been saved
        Console.WriteLine("MaxiCode Mode 4 barcode saved successfully.");
    }
}