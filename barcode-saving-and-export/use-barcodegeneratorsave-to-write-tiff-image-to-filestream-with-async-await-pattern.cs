using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode and saving it as a TIFF file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Asynchronously generates a barcode image and writes it to a file.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static async Task Main(string[] args)
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "barcode.tiff";

        // Resolve the full directory path and ensure it exists.
        string directory = Path.GetDirectoryName(Path.GetFullPath(outputPath));
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // Open a file stream with asynchronous support for writing the barcode image.
        using (var fileStream = new FileStream(
            outputPath,
            FileMode.Create,
            FileAccess.Write,
            FileShare.None,
            bufferSize: 4096,
            useAsync: true))
        {
            // Initialize the barcode generator for Code128 with the desired data.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the generated barcode as a TIFF image into the file stream.
                // The operation is wrapped in Task.Run to avoid blocking the async method.
                await Task.Run(() => generator.Save(fileStream, BarCodeImageFormat.Tiff));
            }
        }

        // Inform the user that the barcode has been saved.
        Console.WriteLine($"Barcode saved to {outputPath}");
    }
}