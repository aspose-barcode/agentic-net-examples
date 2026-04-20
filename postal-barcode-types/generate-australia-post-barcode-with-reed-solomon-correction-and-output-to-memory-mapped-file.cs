using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample Australia Post barcode text (CTable encoding)
        const string codeText = "5912345678ABCde";

        // Create the barcode generator for Australia Post
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Set the interpreting type to CTable (allows letters, digits, space, #)
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Generate the barcode image
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                // Save the image to a memory stream in PNG format
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, ImageFormat.Png);
                    byte[] imageBytes = ms.ToArray();

                    // Create or open a memory‑mapped file and write the image bytes
                    using (MemoryMappedFile mmf = MemoryMappedFile.CreateOrOpen("AustraliaPostBarcode", imageBytes.Length))
                    {
                        using (MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(0, imageBytes.Length, MemoryMappedFileAccess.Write))
                        {
                            accessor.WriteArray(0, imageBytes, 0, imageBytes.Length);
                        }
                    }

                    Console.WriteLine("Australia Post barcode generated and written to memory‑mapped file 'AustraliaPostBarcode'.");
                }
            }
        }
    }
}