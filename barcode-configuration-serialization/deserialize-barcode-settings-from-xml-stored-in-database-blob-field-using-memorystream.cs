using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample XML representing barcode settings.
        // In a real scenario this XML would be read from a database BLOB field.
        string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <Parameters>
    <Barcode>
      <CodeText>1234567890</CodeText>
      <EncodeType>Code128</EncodeType>
      <BarColor>#0000FF</BarColor>
    </Barcode>
    <ImageWidth>
      <Pixels>300</Pixels>
    </ImageWidth>
    <ImageHeight>
      <Pixels>150</Pixels>
    </ImageHeight>
    <Resolution>96</Resolution>
  </Parameters>
</BarcodeGenerator>";

        // Convert the XML string to a byte array as it would be stored in a BLOB.
        byte[] xmlBytes = System.Text.Encoding.UTF8.GetBytes(xmlContent);

        // Load the XML from the byte array using a MemoryStream.
        using (var memoryStream = new MemoryStream(xmlBytes))
        {
            // Import barcode settings from the XML stream.
            using (var generator = BarcodeGenerator.ImportFromXml(memoryStream))
            {
                // Generate the barcode image with the imported settings.
                using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
                {
                    // Save the generated image to a file.
                    barcodeImage.Save("output.png", ImageFormat.Png);
                }
            }
        }

        Console.WriteLine("Barcode generated and saved as output.png");
    }
}