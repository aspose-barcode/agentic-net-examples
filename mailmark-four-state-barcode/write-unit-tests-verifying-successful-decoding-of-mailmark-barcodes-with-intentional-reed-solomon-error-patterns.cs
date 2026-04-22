using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Prepare Mailmark codetext (valid sample)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format
            VersionID = 1,                  // version
            Class = "0",                    // Null/Test class
            SupplychainID = 384224,         // known valid supply chain ID
            ItemID = 16563762,              // known valid item ID
            DestinationPostCodePlusDPS = "EF61AH8T " // valid destination with trailing spaces
        };

        // Generate Mailmark barcode image
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set a reasonable bar height (use inches to avoid zero‑pixel size)
            generator.Parameters.Barcode.BarHeight.Inches = 0.5f;

            // Save original image to memory
            using (var originalStream = new MemoryStream())
            {
                generator.Save(originalStream, BarCodeImageFormat.Png);
                originalStream.Position = 0;

                // Load image with Aspose.Drawing to introduce damage
                using (var image = Image.FromStream(originalStream))
                {
                    // Draw a white rectangle over a portion of the barcode to simulate errors
                    using (var graphics = Graphics.FromImage(image))
                    {
                        var rect = new Rectangle(image.Width / 4, image.Height / 4, image.Width / 2, image.Height / 2);
                        graphics.FillRectangle(Brushes.White, rect);
                    }

                    // Save damaged image to a new stream
                    using (var damagedStream = new MemoryStream())
                    {
                        image.Save(damagedStream, ImageFormat.Png);
                        damagedStream.Position = 0;

                        // Decode the damaged barcode
                        using (var reader = new BarCodeReader(damagedStream, DecodeType.AllSupportedTypes))
                        {
                            var results = reader.ReadBarCodes();

                            if (results.Length == 0)
                            {
                                Console.WriteLine("FAIL: No barcode detected.");
                                return;
                            }

                            var result = results[0];
                            string decodedText = result.CodeText;
                            string expectedText = mailmark.GetConstructedCodetext();

                            bool textMatches = string.Equals(decodedText, expectedText, StringComparison.Ordinal);
                            bool strongConfidence = result.Confidence == BarCodeConfidence.Strong;

                            if (textMatches && strongConfidence)
                            {
                                Console.WriteLine("PASS: Decoded Mailmark barcode correctly with strong confidence.");
                            }
                            else
                            {
                                Console.WriteLine("FAIL: Decoding mismatch or low confidence.");
                                Console.WriteLine($"Expected: {expectedText}");
                                Console.WriteLine($"Decoded : {decodedText}");
                                Console.WriteLine($"Confidence: {result.Confidence}");
                            }
                        }
                    }
                }
            }
        }
    }
}