using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample numeric code for OneCode (20 digits)
        const string oneCodeText = "12345678901234567890";

        try
        {
            // Generate a OneCode barcode image and write it to a memory stream
            using (var generator = new BarcodeGenerator(EncodeTypes.OneCode, oneCodeText))
            {
                using (var networkStream = new MemoryStream())
                {
                    // Save the barcode as PNG into the stream (simulating a network response)
                    generator.Save(networkStream, BarCodeImageFormat.Png);
                    networkStream.Position = 0; // Reset for reading

                    // Decode the barcode from the stream, specifying OneCode decode type
                    using (var reader = new BarCodeReader(networkStream, DecodeType.OneCode))
                    {
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            Console.WriteLine("No OneCode barcode detected in the stream.");
                        }
                        else
                        {
                            foreach (BarCodeResult result in results)
                            {
                                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                                Console.WriteLine($"Decoded Text: {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            // Log any decoding or processing errors
            Console.WriteLine($"Error during barcode decoding: {ex.Message}");
        }
    }
}