// Title: Generate DataMatrix barcode from binary data and obtain raw PNG byte array
// Description: Demonstrates how to use Aspose.BarCode's BarcodeGenerator to encode binary data into a DataMatrix barcode and retrieve the resulting image as a raw byte array for transmission.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat to create 2‑D barcodes. Typical scenarios include encoding binary payloads for inventory, authentication, or data‑exchange applications where the generated image must be sent over a network or stored without writing to disk. Developers often need to obtain the image bytes directly for APIs, web services, or custom transport layers.
// Prompt: Use BarCodeBuilder to create a barcode and retrieve the raw byte array for network transmission.
// Tags: datamatrix, binary, barcode generation, raw byte array, network transmission, aspnet, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a DataMatrix barcode from binary data and extracting the image bytes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, saves it to a memory stream, and outputs the byte array length.
    /// </summary>
    static void Main()
    {
        // Check if the legacy BarCodeBuilder type exists (it may not be present in newer versions)
        Type builderType = Type.GetType("Aspose.BarCode.Generation.BarCodeBuilder, Aspose.BarCode");
        if (builderType != null)
        {
            Console.WriteLine("BarCodeBuilder type found, but this example uses BarcodeGenerator for compatibility.");
        }
        else
        {
            Console.WriteLine("BarCodeBuilder type not found; using BarcodeGenerator instead.");
        }

        // Sample binary data to encode into the barcode
        byte[] dataToEncode = new byte[] { 0x01, 0x02, 0x03, 0x04, 0xFF, 0x00 };

        // Create a BarcodeGenerator configured for DataMatrix, which supports binary encoding
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix))
        {
            // Assign the binary payload to the barcode
            generator.SetCodeText(dataToEncode);
            // Set the DataMatrix encoding mode to Binary to handle raw bytes
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Binary;
            // Define the size of each module (pixel) in the generated image
            generator.Parameters.Barcode.XDimension.Pixels = 8f;
            // Optional human‑readable text displayed alongside the barcode
            generator.Parameters.Barcode.CodeTextParameters.TwoDDisplayText = "Binary data";

            // Save the generated barcode image to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Retrieve the raw image bytes for further processing or network transmission
                byte[] rawBytes = ms.ToArray();

                Console.WriteLine($"Generated barcode image byte array length: {rawBytes.Length}");
                // Example placeholder: transmit rawBytes over a network here
            }
        }
    }
}