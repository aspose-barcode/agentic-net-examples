using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code image and saving it to a file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point. Generates a QR code from the provided argument or a default URL.
    /// </summary>
    /// <param name="args">Command‑line arguments; the first argument is used as the QR code text.</param>
    static async Task Main(string[] args)
    {
        // Use a default QR code text if none is provided.
        string codeText = args.Length > 0 ? args[0] : "https://example.com";
        string outputPath = "qr_code.png";

        try
        {
            // Generate the QR code image and write it to the specified file.
            await GenerateQrAsync(codeText, outputPath);
            Console.WriteLine($"QR code saved to '{outputPath}'.");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during generation or file I/O.
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    /// <summary>
    /// Generates a QR code image and writes it to a file asynchronously.
    /// </summary>
    /// <param name="text">The text to encode in the QR code.</param>
    /// <param name="filePath">The file path where the PNG image will be saved.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    private static async Task GenerateQrAsync(string text, string filePath)
    {
        // Create the barcode generator for a QR code with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
        {
            // Optional: set the error correction level to Medium (Level M).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Asynchronously copy the memory stream contents to the target file.
                using (var fileStream = new FileStream(
                    filePath,
                    FileMode.Create,
                    FileAccess.Write,
                    FileShare.None,
                    bufferSize: 4096,
                    useAsync: true))
                {
                    await ms.CopyToAsync(fileStream);
                }
            }
        }
    }
}