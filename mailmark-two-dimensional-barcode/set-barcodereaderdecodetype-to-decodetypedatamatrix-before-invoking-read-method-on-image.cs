using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define temporary image path
        string imagePath = Path.Combine(Path.GetTempPath(), "datamatrix.png");

        // Generate a DataMatrix barcode and save it
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "12345"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode, setting the decode type before reading
        using (BarCodeReader reader = new BarCodeReader())
        {
            // Set the decode type to DataMatrix
            reader.BarCodeReadType = DecodeType.DataMatrix;

            // Assign the image to the reader
            reader.SetBarCodeImage(imagePath);

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine("BarCode Type: " + result.CodeTypeName);
                    Console.WriteLine("BarCode CodeText: " + result.CodeText);
                }
            }
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}