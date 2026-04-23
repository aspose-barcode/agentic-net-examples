using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace AustraliaPostDemo
{
    class Program
    {
        static void Main()
        {
            // Path for the generated barcode image
            const string imagePath = "australiapost.png";

            // Create a barcode generator for Australia Post symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, "5912345678"))
            {
                // Set the encoding table to NTable (digits only) for generation
                generator.Parameters.Barcode.AustralianPost.EncodingTable = CustomerInformationInterpretingType.NTable;

                // Save the generated barcode image
                generator.Save(imagePath);
            }

            // Read and decode the barcode using NTable interpreting type
            using (var reader = new BarCodeReader(imagePath, DecodeType.AustraliaPost))
            {
                // Set the decoding interpreting type to NTable
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.NTable;

                // Iterate through detected barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine("BarCode Type: " + result.CodeType);
                    Console.WriteLine("BarCode CodeText: " + result.CodeText);
                }
            }
        }
    }
}