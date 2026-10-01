// Title: Export barcode recognition state to XML and transmit via TCP socket
// Description: Demonstrates generating a barcode, exporting its recognition state to XML, and sending that XML over a TCP connection.
// Category-Description: This example belongs to the Aspose.BarCode recognition and serialization category, showcasing how to use BarCodeReader.ExportToXml and BarCodeReader.ImportFromXml. Typical use cases include persisting recognition settings, sharing state between services, or transmitting over networks. Developers often work with BarcodeGenerator, BarCodeReader, and stream APIs to handle barcode data in distributed applications.
// Prompt: Demonstrate how to use ExportToXml(Stream) to send barcode recognition state over a network socket.
// Tags: barcode generation, barcode recognition, export to xml, tcp socket, aspose.barcode, code128, serialization

using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates exporting barcode recognition state to XML and transmitting it over a TCP socket.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, exports its recognition state to XML, sends it to a TCP server,
    /// and then imports the state on the server side to perform recognition.
    /// </summary>
    static void Main()
    {
        // Generate a sample barcode image and save to a temporary file
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");
        string codeText = "1234567890";

        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Create a BarCodeReader, read the barcode, and export its state to XML
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Perform a read to ensure the reader has processed the image
            var results = reader.ReadBarCodes();

            // Export recognition state to XML in a memory stream
            using (var xmlStream = new MemoryStream())
            {
                reader.ExportToXml(xmlStream);
                xmlStream.Position = 0;

                // Start a simple TCP server to receive the XML
                int port = 5000;
                var serverTask = Task.Run(() => RunServer(port, barcodePath));

                // Retry connecting to the server a few times and send the XML
                for (int attempt = 0; attempt < 5 && !serverTask.IsCompleted; attempt++)
                {
                    try
                    {
                        using (var client = new TcpClient())
                        {
                            client.Connect(IPAddress.Loopback, port);
                            using (var networkStream = client.GetStream())
                            {
                                // Send the length of the XML data (4-byte int, network order)
                                int length = (int)xmlStream.Length;
                                byte[] lengthBytes = BitConverter.GetBytes(IPAddress.HostToNetworkOrder(length));
                                networkStream.Write(lengthBytes, 0, lengthBytes.Length);
                                // Send the XML bytes
                                xmlStream.CopyTo(networkStream);
                            }
                        }
                        break; // success
                    }
                    catch (SocketException)
                    {
                        // Server might not be ready yet; retry
                    }
                }

                // Wait for server to finish processing
                serverTask.Wait();
            }
        }

        // Clean up temporary files
        try { Directory.Delete(tempDir, true); } catch { /* ignore cleanup errors */ }
    }

    static void RunServer(int port, string barcodeImagePath)
    {
        // Listen for a single client connection
        using (var listener = new TcpListener(IPAddress.Loopback, port))
        {
            listener.Start();
            using (var client = listener.AcceptTcpClient())
            using (var networkStream = client.GetStream())
            {
                // Read the length prefix (4 bytes)
                byte[] lengthBytes = new byte[4];
                int read = 0;
                while (read < 4)
                {
                    int r = networkStream.Read(lengthBytes, read, 4 - read);
                    if (r == 0) throw new EndOfStreamException("Unexpected end of stream while reading length.");
                    read += r;
                }
                int xmlLength = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(lengthBytes, 0));

                // Read the XML data
                using (var xmlStream = new MemoryStream())
                {
                    int remaining = xmlLength;
                    byte[] buffer = new byte[8192];
                    while (remaining > 0)
                    {
                        int toRead = Math.Min(buffer.Length, remaining);
                        int r = networkStream.Read(buffer, 0, toRead);
                        if (r == 0) throw new EndOfStreamException("Unexpected end of stream while reading XML.");
                        xmlStream.Write(buffer, 0, r);
                        remaining -= r;
                    }
                    xmlStream.Position = 0;

                    // Import the reader configuration from XML
                    using (var importedReader = BarCodeReader.ImportFromXml(xmlStream))
                    {
                        // Set the same barcode image for recognition
                        importedReader.SetBarCodeImage(barcodeImagePath);
                        // Perform recognition
                        var results = importedReader.ReadBarCodes();
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected CodeText: {result.CodeText}");
                            Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                            Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                        }
                    }
                }
            }
            listener.Stop();
        }
    }
}