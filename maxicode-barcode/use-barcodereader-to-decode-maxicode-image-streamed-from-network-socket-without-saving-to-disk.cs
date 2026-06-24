using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a MaxiCode barcode, transmitting it over a TCP connection,
/// and decoding it using Aspose.BarCode libraries.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode image, sends it via a TCP listener, receives it on a client,
    /// and decodes the barcode data.
    /// </summary>
    static void Main()
    {
        // Generate a sample MaxiCode image in memory and obtain its PNG byte array.
        byte[] maxiCodeBytes = GenerateMaxiCodeImage();

        // Set up a TCP listener on a free (ephemeral) port bound to the loopback address.
        using (var listener = new TcpListener(IPAddress.Loopback, 0))
        {
            listener.Start();
            int port = ((IPEndPoint)listener.LocalEndpoint).Port;

            // Server task: accept a client connection and send the image bytes.
            Task serverTask = Task.Run(() =>
            {
                using (TcpClient serverClient = listener.AcceptTcpClient())
                using (NetworkStream serverStream = serverClient.GetStream())
                {
                    // Write the entire image byte array to the network stream.
                    serverStream.Write(maxiCodeBytes, 0, maxiCodeBytes.Length);
                    serverStream.Flush();
                }
            });

            // Client: connect to the server and read the transmitted image stream.
            using (var client = new TcpClient())
            {
                client.Connect(IPAddress.Loopback, port);
                using (NetworkStream clientStream = client.GetStream())
                using (var memory = new MemoryStream())
                {
                    // Copy the incoming data into a memory stream for later processing.
                    clientStream.CopyTo(memory);
                    memory.Position = 0; // Reset position to the beginning for reading.

                    // Decode the MaxiCode from the received memory stream.
                    using (var reader = new BarCodeReader(memory, DecodeType.MaxiCode))
                    {
                        foreach (var result in reader.ReadBarCodes())
                        {
                            // Attempt to decode complex MaxiCode codetext based on its mode.
                            var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                                result.Extended.MaxiCode.MaxiCodeMode,
                                result.CodeText);

                            // Output basic barcode information.
                            Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                            Console.WriteLine($"Raw CodeText: {result.CodeText}");

                            // If the decoded data matches Mode 2, display its specific fields.
                            if (decoded is MaxiCodeCodetextMode2 mode2)
                            {
                                Console.WriteLine($"Postal Code: {mode2.PostalCode}");
                                Console.WriteLine($"Country Code: {mode2.CountryCode}");
                                Console.WriteLine($"Service Category: {mode2.ServiceCategory}");

                                // If a standard second message is present, display it.
                                if (mode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                                {
                                    Console.WriteLine($"Message: {stdMsg.Message}");
                                }
                            }
                        }
                    }
                }
            }

            // Ensure the server task has completed before exiting.
            serverTask.Wait();
        }
    }

    /// <summary>
    /// Generates a simple MaxiCode (Mode 2) image and returns its PNG byte array.
    /// </summary>
    /// <returns>Byte array containing the PNG representation of the generated MaxiCode.</returns>
    private static byte[] GenerateMaxiCodeImage()
    {
        // Define the codetext for a Mode 2 MaxiCode with sample data.
        var codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Test message" }
        };

        // Use the ComplexBarcodeGenerator to create the barcode and save it to a memory stream.
        using (var generator = new ComplexBarcodeGenerator(codetext))
        using (var ms = new MemoryStream())
        {
            generator.Save(ms, BarCodeImageFormat.Png);
            return ms.ToArray(); // Return the PNG bytes.
        }
    }
}