using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a GS1 Composite barcode and writes it to a file.
    /// </summary>
    static void Main()
    {
        // Sample GS1 Composite barcode data.
        // Linear part and 2D part are separated by the '|' character.
        string codeText = "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Output file path for the generated barcode image.
        string outputPath = "gs1composite.jpg";

        // Create the barcode generator for GS1 Composite Bar.
        // The generator is disposed automatically at the end of the using block.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, codeText))
        {
            // Optional: specify component types for the composite barcode.
            // Linear component uses GS1 Code128, 2D component uses CC-A.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Set resolution suitable for web display (e.g., 150 DPI).
            generator.Parameters.Resolution = 150f;

            // Save the barcode as JPEG.
            // Note: Aspose.BarCode does not expose a direct API to set JPEG quality.
            // The default JPEG quality is used (typically high enough for web display).
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the file was created and report the result.
        if (File.Exists(outputPath))
        {
            Console.WriteLine($"GS1 Composite barcode saved successfully to '{outputPath}'.");
        }
        else
        {
            Console.WriteLine("Failed to save the barcode image.");
        }
    }
}