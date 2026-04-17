using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace HanXinBarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Sample text to encode
            string sampleText = "Han Xin Code Example";

            // Generate barcode bytes
            byte[] barcodeBytes = GenerateHanXinBarcode(sampleText);

            // Output size to verify generation
            Console.WriteLine($"Generated barcode byte array length: {barcodeBytes.Length}");
        }

        /// <summary>
        /// Generates a Han Xin barcode image in PNG format and returns it as a byte array.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <returns>Byte array containing the PNG image of the barcode.</returns>
        static byte[] GenerateHanXinBarcode(string codeText)
        {
            if (string.IsNullOrEmpty(codeText))
                throw new ArgumentException("Code text must not be null or empty.", nameof(codeText));

            // Create the barcode generator for Han Xin symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
            {
                // Optional: set encoding mode (default is Auto)
                // generator.Parameters.Barcode.HanXin.EncodeMode = EncodeMode.Auto;

                // Render the barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }
}