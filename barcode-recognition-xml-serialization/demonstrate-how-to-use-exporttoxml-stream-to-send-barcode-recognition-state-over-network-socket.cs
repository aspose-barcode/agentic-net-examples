// Title: Export barcode recognition state to XML and transmit via TCP socket
// Description: This example generates a Code128 barcode, captures the recognition state as XML, and sends it over a TCP socket to a client, which then imports the state and reads the barcode.
// Category-Description: Demonstrates Aspose.BarCode recognition state export and import using ExportToXml and ImportFromXml. The example covers BarCodeGenerator, BarCodeReader, XML serialization, and network transmission with TcpListener/TcpClient. Ideal for developers needing to share barcode processing state across processes or services.
// Prompt: Demonstrate how to use ExportToXml(Stream) to send barcode recognition state over a network socket.
// Tags: barcode, code128, export, xml, network, tcp, aspose.barcode, barcodereader, barcodegenerator

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
/// Shows how to export a <see cref="BarCodeReader"/> state to XML,
/// transmit it over a TCP socket, import it on the client side,
/// and then read the barcode using the imported state.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Generate a sample Code128 barcode image in memory.
        // ------------------------------------------------------------
        byte[] imageData;
        using (MemoryStream imgStream = new MemoryStream())
        {
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                generator.Save(imgStream, BarCodeImageFormat.Png);
            }
            imageData = imgStream.ToArray();
        }

        // ------------------------------------------------------------
        // 2. Create a BarCodeReader, configure it, and export its state to an XML stream.
        // ------------------------------------------------------------
        MemoryStream xmlStream = new MemoryStream();
        using (MemoryStream imgForReader = new MemoryStream(imageData))
        {
            using (BarCodeReader reader = new BarCodeReader(imgForReader, DecodeType.Code128))
            {
                // Example setting: ignore FNC characters during decoding.
                reader.BarcodeSettings.StripFNC = true;
                reader.ExportToXml(xmlStream);
            }
        }
        // Reset the XML stream position for later reading.
        xmlStream.Position = 0;

        // ------------------------------------------------------------
        // 3. Start a simple TCP server that sends the XML over the socket.
        // ------------------------------------------------------------
        const int port = 5000;
        Task serverTask = Task.Run(() =>
        {
            using (TcpListener listener = new TcpListener(IPAddress.Loopback, port))
            {
                listener.Start();
                using (TcpClient client = listener.AcceptTcpClient())
                {
                    using (NetworkStream ns = client.GetStream())
                    {
                        // Transmit the XML data to the connected client.
                        xmlStream.CopyTo(ns);
                        ns.Flush();
                    }
                }
                listener.Stop();
            }
        });

        // Give the server a moment to start listening.
        Task.Delay(100).Wait();

        // ------------------------------------------------------------
        // 4. Client connects, receives the XML, imports the reader state,
        //    and reads the barcode from the original image.
        // ------------------------------------------------------------
        using (TcpClient client = new TcpClient())
        {
            client.Connect(IPAddress.Loopback, port);
            using (NetworkStream ns = client.GetStream())
            {
                using (MemoryStream receivedXml = new MemoryStream())
                {
                    // Receive the XML data sent by the server.
                    ns.CopyTo(receivedXml);
                    receivedXml.Position = 0;

                    // Recreate the BarCodeReader from the received XML.
                    using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(receivedXml))
                    {
                        // Provide the original barcode image to the imported reader.
                        using (MemoryStream imgForImport = new MemoryStream(imageData))
                        {
                            importedReader.SetBarCodeImage(imgForImport);
                            BarCodeResult[] results = importedReader.ReadBarCodes();

                            Console.WriteLine($"Barcodes read after import: {results.Length}");
                            foreach (BarCodeResult result in results)
                            {
                                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }

        // ------------------------------------------------------------
        // 5. Ensure the server task has completed before exiting.
        // ------------------------------------------------------------
        serverTask.Wait();
    }
}