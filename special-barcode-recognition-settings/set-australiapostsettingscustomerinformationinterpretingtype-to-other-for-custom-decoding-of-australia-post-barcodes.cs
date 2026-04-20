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
        // Sample code text for Australia Post barcode (customer information part is empty, using Other interpreting type)
        const string codeText = "59123456780123012301230123";
        const string outputPath = "AustraliaPost.png";

        // Create the barcode generator for Australia Post symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Explicitly set the interpreting type to Other (default is Other, but we set it for clarity)
            generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.Other;

            // Generate the barcode image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to a file
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        // Verify that the image file was created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine($"Failed to create barcode image at '{outputPath}'.");
            return;
        }

        // Load the saved image and decode it with custom settings
        using (Bitmap bitmap = (Bitmap)Image.FromFile(outputPath))
        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
        {
            // Set the decoding interpreting type to Other for custom decoding
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.Other;

            // Read all barcodes from the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeType}");
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");
            }
        }
    }
}