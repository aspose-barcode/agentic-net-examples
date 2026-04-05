using System;
using System.IO;
using System.Text;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator for Code128 and set the code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123456";

            // Example of setting a size-related property using the .Point member
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Serialize the generator settings to a memory stream
            using (var memoryStream = new MemoryStream())
            {
                bool exported = generator.ExportToXml(memoryStream);
                Console.WriteLine($"Export successful: {exported}");

                // Reset the stream position to read the XML content
                memoryStream.Position = 0;
                string xml = Encoding.UTF8.GetString(memoryStream.ToArray());
                Console.WriteLine("Exported XML:");
                Console.WriteLine(xml);
            }
        }
    }
}