using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of an ITF14 barcode with a thick frame and handling of quiet zone settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates an ITF14 barcode and saves it as a TIFF image.
    /// </summary>
    static void Main()
    {
        // Sample ITF14 code text (must be numeric and have an even number of digits)
        const string codeText = "123456789012";

        // Initialize the barcode generator for ITF14 symbology using the provided code text
        using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
        {
            // ------------------------------------------------------------
            // Configure barcode appearance
            // ------------------------------------------------------------

            // Set the border type to a thick frame around the barcode
            generator.Parameters.Barcode.ITF.BorderType = ITF14BorderType.Frame;

            // Define the thickness of the border (5 points in this example)
            generator.Parameters.Barcode.ITF.BorderThickness.Point = 5f;

            // ------------------------------------------------------------
            // Quiet zone handling
            // ------------------------------------------------------------

            // The API expects an integer quiet zone coefficient >= 10.
            // The requested value (0.3) is invalid, so we report it without applying.
            const float requestedQuietZoneCoef = 0.3f;
            if (requestedQuietZoneCoef < 10f)
            {
                Console.WriteLine(
                    $"Requested quiet zone coefficient {requestedQuietZoneCoef} is invalid. " +
                    "ITF quiet zone coefficient must be an integer >= 10. Skipping this setting.");
            }
            else
            {
                // This block would set a valid quiet zone coefficient.
                generator.Parameters.Barcode.ITF.QuietZoneCoef = (int)requestedQuietZoneCoef;
            }

            // ------------------------------------------------------------
            // Save the generated barcode
            // ------------------------------------------------------------

            const string outputPath = "itf_barcode.tiff";
            try
            {
                // Save the barcode image in TIFF format to the specified path
                generator.Save(outputPath, BarCodeImageFormat.Tiff);
                Console.WriteLine($"Barcode saved to {outputPath}");
            }
            catch (Exception ex)
            {
                // Report any errors that occur during the save operation
                Console.WriteLine($"Failed to save barcode: {ex.Message}");
            }
        }
    }
}