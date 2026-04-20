using System;
using System.Net.Sockets;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample codetext for Dutch KIX barcode
        const string codeText = "1234567890";

        // Connect to a TCP server (example: localhost on port 5000)
        using (var client = new TcpClient())
        {
            try
            {
                client.Connect("localhost", 5000);
                using (var networkStream = client.GetStream())
                {
                    // Create the barcode generator for Dutch KIX symbology
                    using (var generator = new BarcodeGenerator(EncodeTypes.DutchKIX, codeText))
                    {
                        // Save the generated barcode image directly to the network stream in PNG format
                        generator.Save(networkStream, BarCodeImageFormat.Png);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}