using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates decoding a Base64‑encoded PNG image and reading any barcodes it contains using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Decodes a Base64 image, loads it into a bitmap, and extracts barcodes.
    /// </summary>
    static void Main()
    {
        // Base64‑encoded PNG image that contains a barcode.
        // Replace this string with a valid base64 image when testing.
        string base64Image = "iVBORw0KGgoAAAANSUhEUgAAAJYAAABkCAYAAAB...";

        try
        {
            // Convert the Base64 string into a byte array representing the image data.
            byte[] imageBytes = Convert.FromBase64String(base64Image);

            // Wrap the byte array in a MemoryStream for bitmap creation.
            using (var memoryStream = new MemoryStream(imageBytes))
            {
                // Create a bitmap from the stream. Bitmap implements IDisposable, so we use a using block.
                using (var bitmap = new Bitmap(memoryStream))
                {
                    // Initialize the barcode reader to detect all supported barcode types.
                    using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes present in the image.
                        BarCodeResult[] results = reader.ReadBarCodes();

                        // If no barcodes were found, inform the user.
                        if (results.Length == 0)
                        {
                            Console.WriteLine("No barcode detected in the image.");
                        }
                        else
                        {
                            // Iterate through each detected barcode and display its type and decoded text.
                            foreach (var result in results)
                            {
                                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                                Console.WriteLine($"Barcode Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Output any errors that occur during processing (e.g., invalid Base64 string or image format issues).
            Console.WriteLine($"Error processing barcode image: {ex.Message}");
        }
    }
}