using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a UPC‑A barcode and saving it as a TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a UPC‑A barcode with a specified resolution and writes it to a TIFF file.
    /// </summary>
    static void Main()
    {
        // Define the output file name and location.
        string outputPath = "upc_a.tiff";

        // Specify the barcode symbology (UPC‑A) to be used.
        BaseEncodeType encodeType = EncodeTypes.UPCA;

        // Initialize the barcode generator with the chosen symbology and the data to encode.
        using (var generator = new BarcodeGenerator(encodeType, "123456789012"))
        {
            // Configure the image resolution (dots per inch) for the generated barcode.
            generator.Parameters.Resolution = 600f;

            // Save the generated barcode image to the specified path in TIFF format.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);

            // Inform the user that the barcode has been successfully saved.
            Console.WriteLine($"UPC‑A barcode saved to '{outputPath}' with resolution {generator.Parameters.Resolution} DPI.");
        }
    }
}