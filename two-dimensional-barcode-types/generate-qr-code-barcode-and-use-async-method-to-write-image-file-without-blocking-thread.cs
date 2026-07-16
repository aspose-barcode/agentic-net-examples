// Title: Generate QR Code and save asynchronously as PNG
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode and writing the image to disk using async I/O to avoid blocking the thread.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with QR symbology, configure parameters such as error correction level, and employ asynchronous file operations. Developers often need to generate barcodes on the fly and persist them efficiently in web or service applications without blocking threads.
// Prompt: Generate QR Code barcode and use async method to write image file without blocking thread.
// Tags: qr code, barcode, async, png, aspose.barcode, generation

using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode and saving it asynchronously as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronous entry point that creates the QR Code and writes it to disk without blocking.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the output file path for the generated PNG image.
        string outputPath = "qr_code.png";

        // Initialize the QR Code generator with the desired text/content.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set a high error correction level to improve readability under damage.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Enable automatic image sizing using interpolation mode.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Generate the barcode image as a Bitmap object.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Prepare a memory stream to hold the PNG data.
                using (var memoryStream = new MemoryStream())
                {
                    // Save the bitmap into the memory stream in PNG format.
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    memoryStream.Position = 0; // Reset stream position for reading.

                    // Asynchronously copy the memory stream to a file stream.
                    using (var fileStream = new FileStream(
                        outputPath,
                        FileMode.Create,
                        FileAccess.Write,
                        FileShare.None,
                        bufferSize: 4096,
                        useAsync: true))
                    {
                        await memoryStream.CopyToAsync(fileStream);
                    }
                }
            }
        }

        // Output the full path of the saved QR Code image.
        Console.WriteLine($"QR Code saved to '{Path.GetFullPath(outputPath)}'");
    }
}