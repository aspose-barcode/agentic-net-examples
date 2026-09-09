// Title: Han Xin Barcode Generation with Quiet Zone Validation
// Description: Demonstrates how to generate a Han Xin barcode, apply a minimum quiet zone, and verify the padding meets scanner reliability requirements.
// Category-Description: Shows Aspose.BarCode barcode generation and validation techniques, focusing on setting XDimension, padding, and image output. Useful for developers needing to ensure quiet zone compliance for 2D symbologies like Han Xin, QR, and DataMatrix. Key API classes include BarcodeGenerator, EncodeTypes, BarCodeImageFormat, and Aspose.Drawing.Image. This example belongs to a collection of barcode creation and quality‑check samples that help developers produce scanner‑ready barcodes.
// Prompt: Validate that generated Han Xin barcode meets required quiet zone dimensions for scanner reliability.
// Tags: hanxin, quietzone, barcode, generation, validation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Han Xin barcode, enforces a minimum quiet zone,
/// validates the padding, and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, checks quiet‑zone settings,
    /// and outputs image dimensions.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder for the generated image.
        // --------------------------------------------------------------------
        string outputFolder = Path.Combine(Path.GetTempPath(), "HanXinQuietZoneDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputFolder);
        string imagePath = Path.Combine(outputFolder, "hanxin.png");

        // --------------------------------------------------------------------
        // Create a Han Xin barcode generator with the desired data string.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, "123456"))
        {
            // Set the module (X) dimension – the size of a single barcode element.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Define the minimum quiet zone as twice the X dimension.
            float minQuietZone = generator.Parameters.Barcode.XDimension.Pixels * 2f;

            // Apply the calculated quiet zone to all four sides of the barcode.
            generator.Parameters.Barcode.Padding.Left.Pixels   = minQuietZone;
            generator.Parameters.Barcode.Padding.Right.Pixels  = minQuietZone;
            generator.Parameters.Barcode.Padding.Top.Pixels    = minQuietZone;
            generator.Parameters.Barcode.Padding.Bottom.Pixels = minQuietZone;

            // Save the generated barcode image to the temporary folder.
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // ----------------------------------------------------------------
            // Validate that each padding side meets or exceeds the minimum.
            // ----------------------------------------------------------------
            bool paddingValid =
                generator.Parameters.Barcode.Padding.Left.Pixels   >= minQuietZone &&
                generator.Parameters.Barcode.Padding.Right.Pixels  >= minQuietZone &&
                generator.Parameters.Barcode.Padding.Top.Pixels    >= minQuietZone &&
                generator.Parameters.Barcode.Padding.Bottom.Pixels >= minQuietZone;

            Console.WriteLine($"Quiet zone validation: {(paddingValid ? "PASS" : "FAIL")}");
        }

        // --------------------------------------------------------------------
        // Load the saved PNG image to report its pixel dimensions.
        // --------------------------------------------------------------------
        using (Image img = Image.FromFile(imagePath))
        {
            Console.WriteLine($"Generated image size: {img.Width}x{img.Height} pixels");
        }

        // Cleanup: optionally delete the temporary folder (commented out to allow inspection).
        // Directory.Delete(outputFolder, true);
    }
}