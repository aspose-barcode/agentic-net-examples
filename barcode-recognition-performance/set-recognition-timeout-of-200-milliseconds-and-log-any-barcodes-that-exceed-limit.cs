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
        const string imagePath = "sample.png";

        // Generate a sample barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath);
        }

        // Verify the image file exists before attempting recognition.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Perform barcode recognition with a timeout of 200 milliseconds.
        using (var reader = new BarCodeReader(imagePath))
        {
            reader.Timeout = 200; // timeout in milliseconds

            try
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"BarCode Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                }
            }
            catch (RecognitionAbortedException)
            {
                Console.WriteLine("Recognition exceeded the timeout limit of 200 ms.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An unexpected error occurred: {ex.Message}");
            }
        }
    }
}