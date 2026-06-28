using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode, saving it as a PNG file,
/// and outputting its Base64 representation. Intended for console usage.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode from the provided text (or a default),
    /// writes the PNG to disk, and prints the Base64 string to the console.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument is used as input text.</param>
    static void Main(string[] args)
    {
        // Determine the text to encode: use first argument if present, otherwise a sample.
        string inputText = args.Length > 0 ? args[0] : "Sample Text";

        // Generate the barcode image as a PNG byte array.
        byte[] pngBytes = GenerateDataMatrixBarcode(inputText);

        // Define the output file name and write the PNG bytes to disk.
        const string outputFile = "datamatrix.png";
        File.WriteAllBytes(outputFile, pngBytes);
        Console.WriteLine($"Barcode image saved to {outputFile}");

        // Convert the PNG bytes to a Base64 string to simulate a REST response payload.
        string base64 = Convert.ToBase64String(pngBytes);
        Console.WriteLine("Base64 PNG:");
        Console.WriteLine(base64);
    }

    /// <summary>
    /// Generates a DataMatrix barcode in PNG format from the specified text.
    /// </summary>
    /// <param name="text">The text to encode into the barcode.</param>
    /// <returns>A byte array containing the PNG image data.</returns>
    static byte[] GenerateDataMatrixBarcode(string text)
    {
        // BarcodeGenerator implements IDisposable; ensure proper disposal with using.
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
        {
            // Use a memory stream to capture the PNG output.
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the stream contents as a byte array.
                return ms.ToArray();
            }
        }
    }
}