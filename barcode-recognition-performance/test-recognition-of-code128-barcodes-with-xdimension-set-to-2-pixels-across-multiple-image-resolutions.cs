using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating and recognizing Code128 barcodes at different image resolutions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode at several DPI settings,
    /// saves it to a memory stream, and then attempts to recognize it.
    /// </summary>
    static void Main()
    {
        // Sample Code128 text to encode.
        const string codeText = "ABC1234567890";

        // Resolutions to test (pixels per inch).
        float[] resolutions = new float[] { 72f, 150f, 300f };

        // Iterate over each resolution and process the barcode.
        foreach (float res in resolutions)
        {
            try
            {
                // Create a barcode generator for Code128 with the specified text.
                using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
                {
                    // Set the X-dimension (module width) to 2 pixels.
                    generator.Parameters.Barcode.XDimension.Pixels = 2f;
                    // Apply the current resolution (DPI) to the generator.
                    generator.Parameters.Resolution = res;

                    // Save the generated barcode image to a memory stream in PNG format.
                    using (var ms = new MemoryStream())
                    {
                        generator.Save(ms, BarCodeImageFormat.Png);
                        // Reset stream position to the beginning for reading.
                        ms.Position = 0;

                        // Load the image from the memory stream into a Bitmap for recognition.
                        using (var bitmap = new Bitmap(ms))
                        {
                            // Initialize a barcode reader configured for Code128 decoding.
                            using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                            {
                                // Attempt to read barcodes from the image.
                                var results = reader.ReadBarCodes();

                                // If at least one barcode is detected, output its details.
                                if (results.Length > 0)
                                {
                                    var result = results[0];
                                    Console.WriteLine($"Resolution: {res} DPI");
                                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                                    Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                                    Console.WriteLine();
                                }
                                else
                                {
                                    // No barcode detected at this resolution.
                                    Console.WriteLine($"Resolution: {res} DPI - No barcode detected.");
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occur during generation or recognition.
                Console.WriteLine($"Resolution: {res} DPI - Error: {ex.Message}");
            }
        }
    }
}