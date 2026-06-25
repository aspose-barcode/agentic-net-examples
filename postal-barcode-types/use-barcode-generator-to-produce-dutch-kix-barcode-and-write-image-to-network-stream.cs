using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Dutch KIX barcode and writing it to a stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Dutch KIX barcode, saves it as PNG to a memory stream,
    /// and outputs the number of bytes written.
    /// </summary>
    static void Main()
    {
        // Sample code text for Dutch KIX barcode (numeric, up to 13 digits)
        const string codeText = "1234567890123";

        // Create the barcode generator for Dutch KIX symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
        {
            // Simulate a network stream using a MemoryStream.
            // In a real scenario this could be a NetworkStream from a TcpClient.
            using (var networkStream = new MemoryStream())
            {
                // Save the generated barcode image directly to the stream in PNG format
                generator.Save(networkStream, BarCodeImageFormat.Png);

                // Reset the stream position to the beginning for any further processing
                networkStream.Position = 0;

                // Output the length of the stream (number of bytes written)
                Console.WriteLine($"Dutch KIX barcode generated. Bytes written to stream: {networkStream.Length}");
            }
        }
    }
}