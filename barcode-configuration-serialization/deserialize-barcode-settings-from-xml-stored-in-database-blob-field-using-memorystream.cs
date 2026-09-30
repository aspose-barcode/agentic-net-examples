// Title: Deserialize barcode settings from XML stored in a BLOB using MemoryStream
// Description: Demonstrates how to read barcode configuration XML from a database BLOB, deserialize it with Aspose.BarCode, and generate a barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation and serialization category. It shows how to use BarcodeGenerator.ImportFromXml to reconstruct barcode settings, a common task when persisting configurations in databases. Developers often need to store and retrieve barcode parameters, then generate images on demand using classes like BarcodeGenerator, Bitmap, and ImageFormat.
// Prompt: Deserialize barcode settings from XML stored in a database BLOB field using a MemoryStream.
// Tags: barcode symbology, xml deserialization, memorystream, aspose.barcode, image generation, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that deserializes barcode settings from XML stored in a simulated
/// database BLOB, generates the barcode image, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Reads XML from a byte array, deserializes it,
    /// generates a barcode image, and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Simulated XML that would normally be stored in a database BLOB field.
        string sampleXml = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <Parameters>
    <Barcode>
      <CodeText>1234567890</CodeText>
      <Symbology>Code128</Symbology>
      <BarColor>#FF0000</BarColor>
      <BackColor>#FFFFFF</BackColor>
    </Barcode>
  </Parameters>
</BarcodeGenerator>";

        // Convert the XML string to a UTF‑8 encoded byte array.
        byte[] xmlBytes = System.Text.Encoding.UTF8.GetBytes(sampleXml);

        // Load the XML bytes into a MemoryStream to mimic reading from a BLOB.
        using (var xmlStream = new MemoryStream(xmlBytes))
        {
            // Deserialize the barcode settings from the XML stream.
            BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlStream);

            // Generate the barcode image using the deserialized settings.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Determine a temporary file path for the output PNG.
                string outputPath = Path.Combine(Path.GetTempPath(), "deserialized_barcode.png");

                // Save the generated bitmap as a PNG file.
                bitmap.Save(outputPath, ImageFormat.Png);

                // Inform the user where the image was saved.
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
    }
}