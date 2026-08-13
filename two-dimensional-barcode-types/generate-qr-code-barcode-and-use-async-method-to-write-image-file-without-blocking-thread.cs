// Title: Generate QR Code and Save Asynchronously with Aspose.BarCode
// Description: Demonstrates generating a QR Code barcode using Aspose.BarCode and writing the resulting PNG image to disk asynchronously, avoiding thread blocking.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category. It showcases the use of BarcodeGenerator to create QR Code images, Aspose.Drawing.Bitmap for image handling, and asynchronous file I/O to persist the image without blocking the calling thread. Developers commonly use these APIs to integrate barcode creation into web services, background jobs, or UI applications where responsiveness is critical.
// Prompt: Generate QR Code barcode and use async method to write image file without blocking thread.
// Tags: qr code, barcode generation, async, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a QR Code barcode and saves it to a PNG file asynchronously.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code and writes it to a temporary file without blocking the thread.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the text to encode in the QR Code.
        string codeText = "Hello Aspose QR Code";

        // Determine a temporary file path for the output PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr_code.png");

        // Create a BarcodeGenerator for QR Code symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Assign the text to be encoded.
            generator.CodeText = codeText;

            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to disk asynchronously.
                await SaveBitmapAsync(bitmap, outputPath);
            }
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }

    // Asynchronously saves a bitmap to a file using a memory stream.
    private static async Task SaveBitmapAsync(Bitmap bitmap, string filePath)
    {
        // Copy the bitmap into a memory stream in PNG format.
        using (var memoryStream = new MemoryStream())
        {
            bitmap.Save(memoryStream, ImageFormat.Png);
            memoryStream.Position = 0;

            // Open a file stream with async support and write the memory stream contents.
            using (var fileStream = new FileStream(
                filePath,
                FileMode.Create,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 81920,
                useAsync: true))
            {
                await memoryStream.CopyToAsync(fileStream);
            }
        }
    }
}