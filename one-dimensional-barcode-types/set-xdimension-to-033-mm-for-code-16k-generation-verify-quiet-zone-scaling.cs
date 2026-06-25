using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a Code16K barcode using Aspose.BarCode,
/// configures X-dimension and displays calculated quiet zone sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Code16K barcode, saves it to a PNG file,
    /// and writes configuration details to the console.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated barcode image
        string outputPath = "code16k.png";

        // Define the barcode symbology (Code16K) and the data to encode
        BaseEncodeType encodeType = EncodeTypes.Code16K;

        // Create a BarcodeGenerator instance within a using block to ensure proper disposal
        using (var generator = new BarcodeGenerator(encodeType, "1234567890123456"))
        {
            // Set the X-dimension (module width) to 0.33 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.33f;

            // Optional: explicitly set quiet zone coefficients (defaults are left=10, right=1)
            // generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef = 10;
            // generator.Parameters.Barcode.Code16K.QuietZoneRightCoef = 1;

            // Save the generated barcode image to the specified file
            generator.Save(outputPath);

            // Retrieve the configured X-dimension and quiet zone coefficients
            float xDimMm = generator.Parameters.Barcode.XDimension.Millimeters;
            int leftCoef = generator.Parameters.Barcode.Code16K.QuietZoneLeftCoef;
            int rightCoef = generator.Parameters.Barcode.Code16K.QuietZoneRightCoef;

            // Calculate quiet zone sizes in millimeters based on coefficients
            float leftQuietZoneMm = leftCoef * xDimMm;
            float rightQuietZoneMm = rightCoef * xDimMm;

            // Output configuration details to the console
            Console.WriteLine($"XDimension set to {xDimMm} mm");
            Console.WriteLine($"Quiet zone left coefficient: {leftCoef}, size: {leftQuietZoneMm} mm");
            Console.WriteLine($"Quiet zone right coefficient: {rightCoef}, size: {rightQuietZoneMm} mm");
            Console.WriteLine($"Barcode image saved to: {outputPath}");
        }
    }
}