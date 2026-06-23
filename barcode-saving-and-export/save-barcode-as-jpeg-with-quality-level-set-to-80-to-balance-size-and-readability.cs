using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, saves it to a file, and writes a confirmation message to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        const string outputPath = "barcode.jpg";

        // Initialize a BarcodeGenerator for Code128 with the sample text "1234567890".
        // The generator implements IDisposable, so we use a using block to ensure proper resource cleanup.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode as a JPEG file.
            // Aspose.BarCode does not expose a parameter to control JPEG quality,
            // therefore the default quality settings are used.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user that the barcode has been saved and note the limitation regarding JPEG quality.
        Console.WriteLine($"Barcode saved to '{outputPath}'. JPEG quality cannot be set via Aspose.BarCode API.");
    }
}