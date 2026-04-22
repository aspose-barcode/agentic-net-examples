using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace MailmarkReaderSample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a sample Mailmark barcode using ComplexBarcodeGenerator
            MailmarkCodetext mailmarkData = new MailmarkCodetext();
            mailmarkData.Format = 1; // Letter format (int)
            mailmarkData.VersionID = 1;
            mailmarkData.Class = "0";
            mailmarkData.SupplychainID = 384224;
            mailmarkData.ItemID = 16563762;
            mailmarkData.DestinationPostCodePlusDPS = "EF61AH8T ";

            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmarkData))
            {
                // Generate the barcode image (Bitmap implements IDisposable)
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the image to a memory stream (PNG format)
                    using (MemoryStream imageStream = new MemoryStream())
                    {
                        bitmap.Save(imageStream, ImageFormat.Png);
                        imageStream.Position = 0; // Reset stream position for reading

                        // Read the Mailmark barcode from the stream
                        using (BarCodeReader reader = new BarCodeReader())
                        {
                            // Specify that we want to decode Mailmark barcodes
                            reader.BarCodeReadType = DecodeType.Mailmark;

                            // Assign the image stream to the reader
                            reader.SetBarCodeImage(imageStream);

                            // Perform recognition
                            foreach (BarCodeResult result in reader.ReadBarCodes())
                            {
                                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                                // Decode the complex Mailmark codetext
                                MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                                if (decoded != null)
                                {
                                    Console.WriteLine("Decoded Mailmark Details:");
                                    Console.WriteLine($"  Format: {decoded.Format}");
                                    Console.WriteLine($"  VersionID: {decoded.VersionID}");
                                    Console.WriteLine($"  Class: {decoded.Class}");
                                    Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                                    Console.WriteLine($"  ItemID: {decoded.ItemID}");
                                    Console.WriteLine($"  DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
                                }
                                else
                                {
                                    Console.WriteLine("Failed to decode Mailmark codetext.");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}