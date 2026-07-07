// Title: Decode MaxiCode from Network Stream using BarcodeReader
// Description: Demonstrates decoding a MaxiCode barcode received over a TCP socket without writing the image to disk.
// Category-Description: This example belongs to the Aspose.BarCode reading and complex barcode handling category. It showcases the use of BarCodeReader, ComplexCodetextReader, and related classes to decode MaxiCode symbols directly from a streamed image. Developers working with real‑time barcode scanning, networked devices, or in‑memory image processing will find such patterns useful for building low‑latency barcode solutions.
// Prompt: Use the BarcodeReader to decode a MaxiCode image streamed from a network socket without saving to disk.
// Tags: maxicode, barcode decoding, network stream, barcodereader, aspose.barcode, complexbarcode, tcp

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode barcode, streams it over a TCP socket,
/// and decodes it directly from the received network stream using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a MaxiCode image in memory, sends it via a TCP server,
    /// receives it as a client, and decodes the barcode without persisting the image to disk.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Generate a sample MaxiCode image in memory
        // ------------------------------------------------------------
        var maxiCode = new MaxiCodeCodetextMode2();
        maxiCode.PostalCode = "524032140";
        maxiCode.CountryCode = 56;
        maxiCode.ServiceCategory = 999;
        var secondMessage = new MaxiCodeStandardSecondMessage();
        secondMessage.Message = "Sample message";
        maxiCode.SecondMessage = secondMessage;

        byte[] imageBytes;
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                imageBytes = ms.ToArray(); // Capture the image bytes for transmission
            }
        }

        // ------------------------------------------------------------
        // Start a simple TCP server that sends the image bytes
        // ------------------------------------------------------------
        var listener = new TcpListener(IPAddress.Loopback, 5000);
        listener.Start();
        var serverTask = Task.Run(() =>
        {
            using (var client = listener.AcceptTcpClient())
            using (var networkStream = client.GetStream())
            {
                // Write the entire image byte array to the connected client
                networkStream.Write(imageBytes, 0, imageBytes.Length);
            }
        });

        // ------------------------------------------------------------
        // Connect as a client and read the image stream
        // ------------------------------------------------------------
        using (var client = new TcpClient())
        {
            client.Connect(IPAddress.Loopback, 5000);
            using (var netStream = client.GetStream())
            using (var receivedMs = new MemoryStream())
            {
                // Copy the incoming data into a memory stream for decoding
                netStream.CopyTo(receivedMs);
                receivedMs.Position = 0; // Reset position to the beginning

                // --------------------------------------------------------
                // Decode the MaxiCode from the received stream
                // --------------------------------------------------------
                using (var reader = new BarCodeReader(receivedMs, DecodeType.MaxiCode))
                {
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Attempt to parse the complex MaxiCode codetext into a strongly‑typed object
                        var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                            result.Extended.MaxiCode.MaxiCodeMode,
                            result.CodeText);

                        if (decoded is MaxiCodeCodetextMode2 decodedMode2)
                        {
                            Console.WriteLine("Postal Code: " + decodedMode2.PostalCode);
                            Console.WriteLine("Country Code: " + decodedMode2.CountryCode);
                            Console.WriteLine("Service Category: " + decodedMode2.ServiceCategory);
                            if (decodedMode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                            {
                                Console.WriteLine("Message: " + stdMsg.Message);
                            }
                        }
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // Ensure the server task completes and clean up resources
        // ------------------------------------------------------------
        serverTask.Wait();
        listener.Stop();
    }
}