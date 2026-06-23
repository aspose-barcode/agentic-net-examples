using System;
using System.IO;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a memory stream,
/// and optionally sending it over a network connection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a MemoryStream, and displays the size.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        string codeText = "1234567890";

        // Initialize a barcode generator for Code128 with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            // Set visual appearance: black bars on a white background.
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Use a MemoryStream to simulate real‑time transmission of the barcode image.
            using (var memoryStream = new MemoryStream())
            {
                // Save the generated barcode as a GIF image into the memory stream.
                generator.Save(memoryStream, BarCodeImageFormat.Gif);

                // Output the size of the generated GIF in bytes.
                Console.WriteLine($"Generated GIF size: {memoryStream.Length} bytes");
            }

            // Example of sending the barcode over a network (commented out because no server is available).
            // using (var client = new TcpClient("example.com", 12345))
            // {
            //     using (NetworkStream networkStream = client.GetStream())
            //     {
            //         // Write the GIF directly to the network stream.
            //         generator.Save(networkStream, BarCodeImageFormat.Gif);
            //         networkStream.Flush();
            //     }
            // }
        }
    }
}