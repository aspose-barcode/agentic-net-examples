using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, storing it in memory,
/// and then reading it back using Aspose.BarCode with specific quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a memory stream, and reads it back.
    /// </summary>
    static void Main()
    {
        // Create a memory stream to hold the generated barcode image.
        using (var ms = new MemoryStream())
        {
            // Initialize the barcode generator for Code128 with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            ms.Position = 0;

            // Attempt to read the barcode using a BarCodeReader with specific quality settings.
            try
            {
                // Initialize the reader for Code128 barcodes from the memory stream.
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Enable minimal X dimension mode to improve detection of narrow bars.
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

                    // Allow the reader to attempt recognition even if the barcode appears incorrect.
                    reader.QualitySettings.AllowIncorrectBarcodes = true;

                    // Perform the barcode reading operation.
                    var results = reader.ReadBarCodes();

                    // Check if any barcodes were detected.
                    if (results.Length > 0)
                    {
                        Console.WriteLine("Barcode read successfully with both settings enabled.");

                        // Output details of each detected barcode.
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No barcode detected.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Report any errors that occurred during the reading process.
                Console.WriteLine($"Error during barcode reading: {ex.Message}");
            }
        }
    }
}