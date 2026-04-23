using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the PNG image containing the Swiss Post Parcel barcode
        const string imagePath = "SwissPostParcel.png";

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader for the Swiss Post Parcel symbology
        using (var reader = new BarCodeReader(imagePath, DecodeType.SwissPostParcel))
        {
            // Enable checksum validation to ensure identifier validity
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyFound = false;

            // Read all barcodes from the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                // Additional information can be accessed via result.Extended if needed
            }

            if (!anyFound)
            {
                Console.WriteLine("No Swiss Post Parcel barcode detected in the image.");
            }
        }
    }
}