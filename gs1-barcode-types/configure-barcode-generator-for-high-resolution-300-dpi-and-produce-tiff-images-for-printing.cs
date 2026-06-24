using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Define the output file path where the barcode image will be saved.
        string outputPath = "barcode.tiff";

        // Create a barcode generator for Code128 with the sample code text "1234567890".
        // The 'using' statement ensures the generator is properly disposed after use.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Set a high resolution (300 DPI) to ensure good printing quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode as a TIFF image to the specified output path.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }

        // Inform the user that the barcode image has been saved successfully.
        Console.WriteLine($"Barcode image saved to: {outputPath}");
    }
}