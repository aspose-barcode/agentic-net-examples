// Title: Generate QR Code and Save Asynchronously
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and writing the resulting PNG image to disk using async I/O to avoid blocking the thread.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It shows how to use the BarcodeGenerator class with EncodeTypes.QR, configure QR-specific parameters, render the barcode to a Bitmap, and persist the image using asynchronous file streams. Developers working with barcode creation, image rendering, or non‑blocking I/O can reference this pattern for QR Code generation, custom error correction levels, and efficient file output.
// Prompt: Generate QR Code barcode and use async method to write image file without blocking thread.
// Tags: qr code,barcode generation,async io,aspose.barcode,bitmap,imagemanipulation,output png

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code barcode and saves it to a PNG file asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code and writes the image file without blocking the thread.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define output file path and the text to encode.
        string outputPath = "qr_code.png";
        string codeText = "https://example.com";

        // Create a BarcodeGenerator for QR Code with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Optional: set QR error correction level to improve readability.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Optional: set the size of each QR module (pixel dimension).
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Render the barcode to a Bitmap object.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Asynchronously write the bitmap to the file system.
                await WriteBitmapAsync(outputPath, bitmap);
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR Code saved to {Path.GetFullPath(outputPath)}");
    }

    /// <summary>
    /// Saves a Bitmap as a PNG file using asynchronous streams to avoid blocking the calling thread.
    /// </summary>
    /// <param name="filePath">The full path where the image will be saved.</param>
    /// <param name="bitmap">The Bitmap containing the barcode image.</param>
    private static async Task WriteBitmapAsync(string filePath, Bitmap bitmap)
    {
        // Encode the bitmap into a memory stream in PNG format.
        using (var memoryStream = new MemoryStream())
        {
            bitmap.Save(memoryStream, ImageFormat.Png);
            memoryStream.Position = 0; // Reset stream position for reading.

            // Open a file stream with async support.
            using (var fileStream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 4096,
                useAsync: true))
            {
                // Copy the memory stream to the file stream asynchronously.
                await memoryStream.CopyToAsync(fileStream);
            }
        }
    }
}