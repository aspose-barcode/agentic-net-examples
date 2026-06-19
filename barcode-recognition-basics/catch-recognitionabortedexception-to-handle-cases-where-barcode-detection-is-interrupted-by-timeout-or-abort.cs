using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a memory stream,
/// and then recognizing it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, reads it back, and prints detection results.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with the sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Store the generated barcode image in a memory stream.
            using (var ms = new MemoryStream())
            {
                // Save the barcode as a PNG image into the memory stream.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Reset stream position to the beginning for reading.
                ms.Position = 0;

                // Load the image from the memory stream into a Bitmap for recognition.
                using (var bitmap = new Bitmap(ms))
                {
                    // Initialize a barcode reader with the bitmap and specify the decode type.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.Code128))
                    {
                        // Set a very short timeout (1 ms) to simulate an abort scenario.
                        reader.Timeout = 1; // milliseconds

                        try
                        {
                            // Attempt to read all barcodes from the image.
                            foreach (var result in reader.ReadBarCodes())
                            {
                                // Output the detected barcode type.
                                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                                // Output the decoded text of the barcode.
                                Console.WriteLine($"Code Text: {result.CodeText}");
                            }
                        }
                        catch (RecognitionAbortedException ex)
                        {
                            // Handle the case where recognition was aborted (e.g., due to timeout).
                            Console.WriteLine("Barcode recognition was aborted: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            // General exception handling for any other errors.
                            Console.WriteLine("An error occurred: " + ex.Message);
                        }
                    }
                }
            }
        }
    }
}