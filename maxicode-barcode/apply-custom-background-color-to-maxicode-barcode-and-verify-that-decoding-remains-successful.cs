using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Original codetext to encode
        string originalText = "Test message";

        // Create a MaxiCode barcode generator with the codetext
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, originalText))
        {
            // Apply a custom background color
            generator.Parameters.BackColor = Color.LightYellow;

            // Save the generated barcode to a memory stream
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Decode the barcode from the memory stream
                using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                {
                    bool decodingSuccessful = false;
                    foreach (var result in reader.ReadBarCodes())
                    {
                        if (result.CodeText == originalText)
                        {
                            decodingSuccessful = true;
                            break;
                        }
                    }

                    Console.WriteLine(decodingSuccessful
                        ? "Decoding successful: code text matches."
                        : "Decoding failed: code text does not match.");
                }
            }
        }
    }
}