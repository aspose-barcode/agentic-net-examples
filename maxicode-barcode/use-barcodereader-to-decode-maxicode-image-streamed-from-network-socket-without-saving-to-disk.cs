using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

class Program
{
    static void Main()
    {
        // Generate a simple MaxiCode image and keep it in memory
        byte[] imageBytes;
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "Sample MaxiCode"))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }
            imageBytes = ms.ToArray();
        }

        // Set up a TCP listener on a free port
        int port;
        using (var listener = new TcpListener(IPAddress.Loopback, 0))
        {
            listener.Start();
            port = ((IPEndPoint)listener.LocalEndpoint).Port;

            // Connect a client to the listener
            using (var client = new TcpClient())
            {
                client.Connect(IPAddress.Loopback, port);

                // Accept the incoming connection on the server side
                using (var serverClient = listener.AcceptTcpClient())
                {
                    // Server: send the image bytes over the network stream
                    using (var serverStream = serverClient.GetStream())
                    {
                        serverStream.Write(imageBytes, 0, imageBytes.Length);
                        serverStream.Flush();
                    }

                    // Client: receive the image bytes into a memory stream
                    using (var clientStream = client.GetStream())
                    using (var receivedMs = new MemoryStream())
                    {
                        clientStream.CopyTo(receivedMs);
                        receivedMs.Position = 0;

                        // Decode the MaxiCode image from the received stream
                        using (var reader = new BarCodeReader(receivedMs, DecodeType.MaxiCode))
                        {
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                Console.WriteLine("Detected Type: " + result.CodeTypeName);
                                Console.WriteLine("CodeText: " + result.CodeText);

                                // Decode complex MaxiCode codetext if possible
                                var maxiCodeData = ComplexCodetextReader.TryDecodeMaxiCode(
                                    result.Extended.MaxiCode.MaxiCodeMode,
                                    result.CodeText);

                                if (maxiCodeData != null)
                                {
                                    Console.WriteLine("MaxiCode Mode: " + maxiCodeData.GetMode());
                                    Console.WriteLine("Constructed Codetext: " + maxiCodeData.GetConstructedCodetext());
                                }
                            }
                        }
                    }
                }
            }

            listener.Stop();
        }
    }
}