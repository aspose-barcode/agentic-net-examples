using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define the path for the barcode image
        string imagePath = "barcode.png";

        // Create a barcode image and save it to the file system
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Initialize the barcode reader with a very short timeout to trigger abort
        using (var reader = new BarCodeReader(imagePath))
        {
            // Set timeout to 1 millisecond to force a timeout scenario
            reader.Timeout = 1;

            try
            {
                // Attempt to read barcodes from the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                    Console.WriteLine("BarCode CodeText: " + result.CodeText);
                }
            }
            catch (RecognitionAbortedException ex)
            {
                // Handle the case where recognition was aborted due to timeout
                Console.WriteLine("Recognition aborted: " + ex.Message);
                Console.WriteLine("Execution time (ms): " + ex.ExecutionTime);
            }
        }
    }
}