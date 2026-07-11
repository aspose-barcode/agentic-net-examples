// Title: Generate Dutch KIX barcode and write to network stream
// Description: Demonstrates creating a Dutch KIX barcode image using Aspose.BarCode and sending it over a TCP network stream, with a memory‑stream fallback.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.DutchKIX. Typical use cases include printing shipping labels, inventory tags, or any application requiring Dutch KIX symbology. Developers often need to output barcode images directly to streams for network transmission, file storage, or further processing.
// Prompt: Use a barcode generator to produce a Dutch KIX barcode and write the image to a network stream.
// Tags: dutch kix, barcode generation, network stream, png, aspose.barcode, encode types

using System;
using System.IO;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace DutchKixBarcodeExample
{
    /// <summary>
    /// Contains the entry point for generating a Dutch KIX barcode and transmitting it via a network stream.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Generates a Dutch KIX barcode image and attempts to write it to a TCP network stream.
        /// If the network connection fails, the image is written to a memory stream as a fallback.
        /// </summary>
        static void Main()
        {
            // Sample codetext for Dutch KIX (numeric, up to 20 characters)
            const string codeText = "123456789012";

            // Try to send the barcode image over a TCP connection.
            // On failure, fall back to a memory stream to ensure the image is still generated.
            try
            {
                // Replace "localhost" and 12345 with the actual server address and port.
                using (TcpClient client = new TcpClient())
                {
                    client.Connect("localhost", 12345);
                    using (NetworkStream networkStream = client.GetStream())
                    {
                        // Generate the barcode and write it directly to the network stream.
                        GenerateAndSaveBarcode(codeText, networkStream);
                    }
                }
            }
            catch (Exception ex) when (ex is SocketException || ex is IOException)
            {
                // Network unavailable – use a memory stream as a safe fallback.
                using (MemoryStream fallbackStream = new MemoryStream())
                {
                    // Generate the barcode and write it to the fallback memory stream.
                    GenerateAndSaveBarcode(codeText, fallbackStream);
                    // For demonstration, output the size of the generated image.
                    Console.WriteLine($"Barcode image written to fallback stream ({fallbackStream.Length} bytes).");
                }
            }
        }

        /// <summary>
        /// Creates a Dutch KIX barcode using the specified text and saves it to the provided stream in PNG format.
        /// </summary>
        /// <param name="text">The data to encode in the barcode.</param>
        /// <param name="outputStream">The stream to which the barcode image will be written.</param>
        private static void GenerateAndSaveBarcode(string text, Stream outputStream)
        {
            // Initialize the barcode generator with Dutch KIX symbology.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DutchKIX, text))
            {
                // Optional: adjust image size or resolution if needed.
                // generator.Parameters.ImageWidth.Point = 300f;
                // generator.Parameters.ImageHeight.Point = 150f;
                // generator.Parameters.Resolution = 96;

                // Save the barcode image directly to the provided stream in PNG format.
                generator.Save(outputStream, BarCodeImageFormat.Png);
                // Ensure all data is flushed to the stream.
                outputStream.Flush();
            }
        }
    }
}