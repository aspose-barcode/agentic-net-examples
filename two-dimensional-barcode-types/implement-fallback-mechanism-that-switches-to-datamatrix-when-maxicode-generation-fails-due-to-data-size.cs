using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and falling back to DataMatrix if needed.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode with sample data,
    /// and if generation fails, falls back to generating a DataMatrix barcode.
    /// </summary>
    static void Main()
    {
        // Prepare sample data that exceeds typical MaxiCode capacity.
        string longData = new string('A', 200);

        // Attempt to generate a MaxiCode (Mode 2) using ComplexBarcodeGenerator.
        try
        {
            // Configure MaxiCode parameters.
            var maxiCode = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140", // Required 9‑digit postal code for Mode 2.
                CountryCode = 56,
                ServiceCategory = 999,
                // Place the long data in the standard second message field.
                SecondMessage = new MaxiCodeStandardSecondMessage { Message = longData }
            };

            // Create a generator for the configured MaxiCode.
            using (var complexGenerator = new ComplexBarcodeGenerator(maxiCode))
            {
                // Generate the barcode image.
                using (var image = complexGenerator.GenerateBarCodeImage())
                {
                    // Save the image to a PNG file via a memory stream.
                    using (var ms = new MemoryStream())
                    {
                        image.Save(ms, ImageFormat.Png);
                        File.WriteAllBytes("maxicode.png", ms.ToArray());
                    }
                }
            }

            Console.WriteLine("MaxiCode generated successfully: maxicode.png");
        }
        catch (Exception ex)
        {
            // Generation failed (e.g., data too large). Log the error and fall back.
            Console.WriteLine($"MaxiCode generation failed: {ex.Message}");
            Console.WriteLine("Falling back to DataMatrix...");

            try
            {
                // Generate a DataMatrix barcode with the same data.
                using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, longData))
                {
                    // Optional: set a specific DataMatrix version if desired.
                    // generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;

                    // Save the DataMatrix image directly to a file.
                    generator.Save("datamatrix.png");
                }

                Console.WriteLine("DataMatrix generated successfully: datamatrix.png");
            }
            catch (Exception fallbackEx)
            {
                // Log any errors that occur during the fallback generation.
                Console.WriteLine($"DataMatrix generation also failed: {fallbackEx.Message}");
            }
        }
    }
}