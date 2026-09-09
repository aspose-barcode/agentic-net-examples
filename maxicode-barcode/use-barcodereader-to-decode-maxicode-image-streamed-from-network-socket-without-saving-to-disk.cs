// Title: Decode MaxiCode from Network Stream Using BarcodeReader
// Description: Demonstrates reading a MaxiCode barcode image sent over a TCP socket directly from a memory stream without persisting the image to disk.
// Category-Description: This example belongs to the Aspose.BarCode reading and decoding category. It shows how to use BarCodeReader together with DecodeType.MaxiCode to extract barcode data from a stream received over a network connection. Typical use cases include real‑time scanning of barcode images transmitted from remote devices, IoT sensors, or web services where saving intermediate files is undesirable. Developers often need to combine .NET networking APIs with Aspose.BarCode classes such as BarCodeReader, ComplexCodetextReader, and MaxiCodeCodetext types.
// Prompt: Use the BarcodeReader to decode a MaxiCode image streamed from a network socket without saving to disk.
// Tags: maxicode, barcode decoding, network stream, aspose.barcode, barcodereader, memory stream, tcp, c#

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a MaxiCode barcode, streams it over a TCP socket,
/// and decodes it directly from the received memory stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, sends it through a local TCP listener,
    /// receives the image into a MemoryStream, and decodes the MaxiCode data.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample MaxiCode barcode image and keep it in memory.
        // ------------------------------------------------------------
        byte[] barcodeBytes;
        using (var imageStream = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "1234567890"))
            {
                // Save the generated barcode as PNG into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }
            // Extract the raw byte array for transmission.
            barcodeBytes = imageStream.ToArray();
        }

        // ------------------------------------------------------------
        // 2. Set up a TCP listener to act as the server side of the socket.
        // ------------------------------------------------------------
        int port;
        using (var listener = new TcpListener(IPAddress.Loopback, 0))
        {
            listener.Start();
            port = ((IPEndPoint)listener.LocalEndpoint).Port;

            // --------------------------------------------------------
            // 3. Launch a background thread that accepts a client connection
            //    and writes the barcode bytes to the network stream.
            // --------------------------------------------------------
            Thread senderThread = new Thread(() =>
            {
                using (var client = listener.AcceptTcpClient())
                using (var networkStream = client.GetStream())
                {
                    networkStream.Write(barcodeBytes, 0, barcodeBytes.Length);
                }
            });
            senderThread.Start();

            // --------------------------------------------------------
            // 4. Client side: connect to the listener and read the image data.
            // --------------------------------------------------------
            using (var client = new TcpClient())
            {
                client.Connect(IPAddress.Loopback, port);
                using (var networkStream = client.GetStream())
                using (var receivedStream = new MemoryStream())
                {
                    // Copy all incoming bytes into a MemoryStream.
                    networkStream.CopyTo(receivedStream);
                    receivedStream.Position = 0; // Reset position for reading.

                    // ----------------------------------------------------
                    // 5. Decode the MaxiCode barcode directly from the stream.
                    // ----------------------------------------------------
                    using (var reader = new BarCodeReader(receivedStream, DecodeType.MaxiCode))
                    {
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"CodeText: {result.CodeText}");
                            Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");

                            // Attempt to decode complex MaxiCode data (Mode 2 or 3).
                            MaxiCodeCodetext complexCodetext = ComplexCodetextReader.TryDecodeMaxiCode(
                                result.Extended.MaxiCode.Mode, result.CodeText);

                            if (complexCodetext is MaxiCodeCodetextMode2 mode2)
                            {
                                Console.WriteLine("Decoded MaxiCode Mode 2:");
                                Console.WriteLine($"PostalCode: {mode2.PostalCode}");
                                Console.WriteLine($"CountryCode: {mode2.CountryCode}");
                                Console.WriteLine($"ServiceCategory: {mode2.ServiceCategory}");
                                if (mode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                                {
                                    Console.WriteLine($"Second Message: {stdMsg.Message}");
                                }
                            }
                            else if (complexCodetext is MaxiCodeCodetextMode3 mode3)
                            {
                                Console.WriteLine("Decoded MaxiCode Mode 3:");
                                Console.WriteLine($"PostalCode: {mode3.PostalCode}");
                                Console.WriteLine($"CountryCode: {mode3.CountryCode}");
                                Console.WriteLine($"ServiceCategory: {mode3.ServiceCategory}");
                                if (mode3.SecondMessage is MaxiCodeStandardSecondMessage stdMsg3)
                                {
                                    Console.WriteLine($"Second Message: {stdMsg3.Message}");
                                }
                            }
                            else
                            {
                                Console.WriteLine("Decoded MaxiCode in an unexpected mode.");
                            }
                        }
                    }
                }
            }

            // ------------------------------------------------------------
            // 6. Clean up: wait for the sender thread to finish and stop the listener.
            // ------------------------------------------------------------
            senderThread.Join();
            listener.Stop();
        }
    }
}