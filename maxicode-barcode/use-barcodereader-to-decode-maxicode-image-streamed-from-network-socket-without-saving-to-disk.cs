// Title: Decode MaxiCode from network stream using BarcodeReader
// Description: Demonstrates how to generate a MaxiCode barcode, stream it over a TCP socket, and decode it directly from the received memory stream without writing to disk.
// Category-Description: This example belongs to the Aspose.BarCode reading and decoding category, showcasing the use of BarCodeGenerator, BarCodeReader, and ComplexCodetextReader for MaxiCode symbology. Typical use cases include real-time barcode scanning from network sources such as sockets, streams, or web services where persisting images is undesirable. Developers often need to read barcodes from in-memory data, handle complex MaxiCode payloads, and integrate with .NET networking APIs.
// Prompt: Use the BarcodeReader to decode a MaxiCode image streamed from a network socket without saving to disk.
// Tags: maxicode, barcode, decoding, stream, network, socket, aspose.barcode, barcodereader, complexcodetextreader

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a MaxiCode barcode, sends it over a TCP socket,
/// and decodes it directly from the received stream using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Executes the end‑to‑end demonstration of streaming and decoding a MaxiCode image.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample MaxiCode image and store it in a byte array.
        // ------------------------------------------------------------
        byte[] imageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
        {
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray();
            }
        }

        // ------------------------------------------------------------
        // 2. Start a simple TCP listener that will send the image bytes to a client.
        // ------------------------------------------------------------
        int port = 5000;
        var listener = new TcpListener(IPAddress.Loopback, port);
        listener.Start();

        var serverTask = Task.Run(() =>
        {
            using (var client = listener.AcceptTcpClient())
            using (var networkStream = client.GetStream())
            {
                // Transmit the generated image bytes over the network.
                networkStream.Write(imageBytes, 0, imageBytes.Length);
            }
            listener.Stop();
        });

        // ------------------------------------------------------------
        // 3. Connect as a client, receive the image bytes, and decode the barcode.
        // ------------------------------------------------------------
        using (var client = new TcpClient())
        {
            client.Connect(IPAddress.Loopback, port);
            using (var netStream = client.GetStream())
            using (var memStream = new MemoryStream())
            {
                // Copy the incoming data into a memory stream for in‑memory processing.
                netStream.CopyTo(memStream);
                memStream.Position = 0;

                // Initialize the reader for MaxiCode decoding.
                using (var reader = new BarCodeReader(memStream, DecodeType.MaxiCode))
                {
                    // Reset stream position before assigning it to the reader.
                    memStream.Position = 0;
                    reader.SetBarCodeImage(memStream);

                    // Iterate through all detected barcodes (should be one in this case).
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");

                        // Attempt to decode complex MaxiCode payloads (e.g., mode 2 or 3).
                        var complex = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.Mode, result.CodeText);
                        if (complex != null)
                        {
                            Console.WriteLine("Decoded MaxiCode complex data:");
                            if (complex is MaxiCodeCodetextMode2 mode2)
                            {
                                Console.WriteLine($"PostalCode: {mode2.PostalCode}");
                                Console.WriteLine($"CountryCode: {mode2.CountryCode}");
                                Console.WriteLine($"ServiceCategory: {mode2.ServiceCategory}");
                            }
                            else if (complex is MaxiCodeCodetextMode3 mode3)
                            {
                                Console.WriteLine($"PostalCode: {mode3.PostalCode}");
                                Console.WriteLine($"CountryCode: {mode3.CountryCode}");
                                Console.WriteLine($"ServiceCategory: {mode3.ServiceCategory}");
                            }
                        }
                    }
                }
            }
        }

        // Ensure the server task completes before exiting.
        serverTask.Wait();
    }
}