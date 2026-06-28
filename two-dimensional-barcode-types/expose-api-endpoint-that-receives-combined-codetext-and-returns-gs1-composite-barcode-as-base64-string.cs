using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a GS1 Composite barcode and outputs the image as a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a GS1 Composite barcode using provided or default data,
    /// configures its components, and writes the PNG image as a Base64 string to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument can specify the combined code text.</param>
    static void Main(string[] args)
    {
        // Determine the combined code text for the GS1 Composite barcode.
        // Expected format: linear part | two‑dimensional part, separated by '|'.
        // Example: "(01)03212345678906|(21)A1B2C3D4E5F6G7H8"
        string combinedCodeText = args.Length > 0 ? args[0] : "(01)03212345678906|(21)A1B2C3D4E5F6G7H8";

        // Create a barcode generator for the GS1 Composite Bar type with the combined code text.
        using (var generator = new BarcodeGenerator(EncodeTypes.GS1CompositeBar, combinedCodeText))
        {
            // Set the linear component to GS1‑Code128.
            generator.Parameters.Barcode.GS1CompositeBar.LinearComponentType = EncodeTypes.GS1Code128;

            // Set the 2D component to Composite Component Type A (CC_A).
            generator.Parameters.Barcode.GS1CompositeBar.TwoDComponentType = TwoDComponentType.CC_A;

            // Optional additional settings for visual appearance.
            generator.Parameters.Barcode.Pdf417.AspectRatio = 3f;          // Aspect ratio for PDF417 component (if used)
            generator.Parameters.Barcode.XDimension.Pixels = 3f;          // Width of a single module (pixel size)
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;        // Height of the linear component (pixel size)

            // Render the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);               // Save barcode image to stream
                byte[] imageBytes = ms.ToArray();                         // Retrieve byte array from stream
                string base64 = Convert.ToBase64String(imageBytes);       // Convert image bytes to Base64 string
                Console.WriteLine(base64);                                // Output Base64 string to console
            }
        }
    }
}