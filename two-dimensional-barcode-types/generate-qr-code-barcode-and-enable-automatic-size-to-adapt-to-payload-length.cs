using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a QR code image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code from a sample payload and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Define the data to encode in the QR code.
        // The length can be altered to observe automatic sizing behavior.
        string payload = "https://example.com/very/long/path?query=parameters&more=data";

        // Specify the output file name for the generated QR code image.
        string outputPath = "qr.png";

        // Initialize the QR code generator with the desired encoding type and payload.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, payload))
        {
            // Configure the generator to automatically size the QR code based on content length.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set a higher resolution (dots per inch) for improved image quality.
            generator.Parameters.Resolution = 300f;

            // Persist the generated QR code image to the specified file path.
            generator.Save(outputPath);
        }

        // Inform the user that the QR code has been saved.
        Console.WriteLine($"QR Code saved to {outputPath}");
    }
}