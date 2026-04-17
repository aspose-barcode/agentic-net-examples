using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the MaxiCode image that may be unreadable
        const string imagePath = "unreadable_maxicode.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        try
        {
            // Open the image as a stream and create a reader that supports all barcode types
            using (FileStream stream = File.OpenRead(imagePath))
            {
                using (BarCodeReader reader = new BarCodeReader(stream, DecodeType.AllSupportedTypes))
                {
                    // Attempt to read all barcodes from the image
                    foreach (BarCodeResult result in reader.ReadBarCodes())
                    {
                        // Only process MaxiCode results
                        if (result.Extended?.MaxiCode != null)
                        {
                            // Decode the MaxiCode codetext using the ComplexCodetextReader
                            MaxiCodeCodetext decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                                result.Extended.MaxiCode.MaxiCodeMode,
                                result.CodeText);

                            if (decoded != null)
                            {
                                Console.WriteLine("Decoded MaxiCode:");
                                Console.WriteLine($"  Barcode Type: {decoded.GetBarcodeType()}");
                                Console.WriteLine($"  Mode: {decoded.GetMode()}");
                                Console.WriteLine($"  Codetext: {decoded.GetConstructedCodetext()}");
                            }
                            else
                            {
                                Console.WriteLine("Failed to decode MaxiCode codetext.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No MaxiCode barcode detected in the image.");
                        }
                    }
                }
            }
        }
        catch (BarCodeException ex)
        {
            // Handle errors specific to Aspose.BarCode (e.g., unreadable image)
            Console.WriteLine("A BarcodeException was caught while decoding the image:");
            Console.WriteLine(ex.Message);
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors
            Console.WriteLine("An unexpected error occurred:");
            Console.WriteLine(ex.Message);
        }
    }
}