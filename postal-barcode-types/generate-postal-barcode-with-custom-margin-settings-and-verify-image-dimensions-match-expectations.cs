// Title: Generate Postnet Barcode with Custom Margins and Verify Image Size
// Description: Creates a Postnet postal barcode, applies custom padding, saves as PNG, and checks that the resulting image dimensions meet the expected minimum based on the margins.
// Category-Description: This example belongs to the Aspose.BarCode generation category, demonstrating how to configure barcode appearance (padding, X‑dimension) and export to an image file. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes—common tools for developers who need to produce printable barcodes with precise layout requirements. Ideal for tutorials, reference guides, and search results about barcode image customization.
// Prompt: Generate a postal barcode with custom margin settings and verify image dimensions match expectations.
// Tags: postnet, barcode, margin, png, aspose.barcode, image verification

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Postnet barcode with custom margins,
/// saving it as a PNG image, and verifying the resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode, applies padding,
    /// saves the image, and performs a simple size verification.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "postal.png";

        // Initialize a BarcodeGenerator for the Postnet symbology with sample data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Apply custom padding of 10 points on each side.
            generator.Parameters.Barcode.Padding.Left.Point = 10f;
            generator.Parameters.Barcode.Padding.Top.Point = 10f;
            generator.Parameters.Barcode.Padding.Right.Point = 10f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 10f;

            // Optionally adjust the X‑dimension to influence overall barcode size.
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Error: Barcode image file '{outputPath}' was not created.");
            return;
        }

        // Load the saved image to retrieve its width and height in pixels.
        using (Image image = Image.FromFile(outputPath))
        {
            int width = image.Width;
            int height = image.Height;

            Console.WriteLine($"Barcode image dimensions: Width = {width} px, Height = {height} px");

            // Expected minimum dimensions based on the total padding (10 + 10 points each side).
            const int expectedMinWidth = 20;  // left + right padding
            const int expectedMinHeight = 20; // top + bottom padding

            // Simple verification: ensure the image dimensions are not smaller than the padding.
            if (width < expectedMinWidth || height < expectedMinHeight)
            {
                Console.WriteLine("Verification failed: Image dimensions are smaller than expected based on padding.");
            }
            else
            {
                Console.WriteLine("Verification succeeded: Image dimensions meet the expected minimum size.");
            }
        }
    }
}