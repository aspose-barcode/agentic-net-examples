using System;
using System.IO;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Barcode content and type
        const string barcodeText = "1234567890";

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, barcodeText))
        {
            // Optional visual settings
            generator.Parameters.Barcode.BarColor = Color.Blue;
            generator.Parameters.BackColor = Color.White;

            // Destination network endpoint (example: localhost:5000)
            const string host = "127.0.0.1";
            const int port = 5000;

            try
            {
                // Establish TCP connection
                using (var client = new TcpClient())
                {
                    client.Connect(host, port);
                    using (NetworkStream networkStream = client.GetStream())
                    {
                        // Write the barcode image as GIF directly to the network stream
                        generator.Save(networkStream, BarCodeImageFormat.Gif);
                        networkStream.Flush();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Error transmitting barcode: {ex.Message}");
            }
        }
    }
}