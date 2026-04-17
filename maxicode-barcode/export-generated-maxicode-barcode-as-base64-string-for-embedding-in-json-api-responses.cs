using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample codetext for the MaxiCode barcode
        string codeText = "Test message";

        // Create a MaxiCode barcode generator with the specified codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
        {
            // Optional: set a specific MaxiCode mode (e.g., Mode4)
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode4;

            // Save the generated barcode image to a memory stream in PNG format
            using (var memoryStream = new MemoryStream())
            {
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string for JSON embedding
                string base64String = Convert.ToBase64String(memoryStream.ToArray());

                // Output the Base64 string
                Console.WriteLine(base64String);
            }
        }
    }
}