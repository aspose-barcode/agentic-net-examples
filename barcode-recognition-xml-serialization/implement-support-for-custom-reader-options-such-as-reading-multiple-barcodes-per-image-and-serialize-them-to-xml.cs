using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Create first barcode (Code128) and save to memory stream
        using (var generator1 = new BarcodeGenerator(EncodeTypes.Code128, "ABC123"))
        {
            using (var ms1 = new MemoryStream())
            {
                generator1.Save(ms1, BarCodeImageFormat.Png);
                ms1.Position = 0;

                // Create second barcode (QR) and save to memory stream
                using (var generator2 = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
                {
                    using (var ms2 = new MemoryStream())
                    {
                        generator2.Save(ms2, BarCodeImageFormat.Png);
                        ms2.Position = 0;

                        // Load both barcodes as bitmaps
                        using (var bmp1 = new Bitmap(ms1))
                        using (var bmp2 = new Bitmap(ms2))
                        {
                            // Create a canvas large enough for both barcodes
                            int canvasWidth = Math.Max(bmp1.Width, bmp2.Width) + 20;
                            int canvasHeight = bmp1.Height + bmp2.Height + 30;
                            using (var canvas = new Bitmap(canvasWidth, canvasHeight))
                            using (var graphics = Graphics.FromImage(canvas))
                            {
                                graphics.Clear(Color.White);
                                // Draw first barcode at top-left
                                graphics.DrawImage(bmp1, new Rectangle(10, 10, bmp1.Width, bmp1.Height));
                                // Draw second barcode below the first one
                                graphics.DrawImage(bmp2, new Rectangle(10, bmp1.Height + 20, bmp2.Width, bmp2.Height));

                                // Save combined image
                                string combinedPath = "multiBarcodes.png";
                                canvas.Save(combinedPath, ImageFormat.Png);

                                // -------------------- Reader with custom options --------------------
                                using (var reader = new BarCodeReader())
                                {
                                    // Set to read both Code128 and QR codes
                                    reader.BarCodeReadType = new MultiDecodeType(DecodeType.Code128, DecodeType.QR);
                                    // Example of another custom option: enable checksum validation
                                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                                    // Assign image
                                    reader.SetBarCodeImage(combinedPath);

                                    // Export reader options to XML
                                    string xmlPath = "readerOptions.xml";
                                    reader.ExportToXml(xmlPath);

                                    // Read barcodes
                                    Console.WriteLine("Reading barcodes with custom options:");
                                    foreach (BarCodeResult result in reader.ReadBarCodes())
                                    {
                                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                                    }
                                }

                                // -------------------- Import options from XML and read again --------------------
                                using (var importedReader = BarCodeReader.ImportFromXml("readerOptions.xml"))
                                {
                                    // Image must be set after import
                                    importedReader.SetBarCodeImage(combinedPath);

                                    Console.WriteLine("\nReading barcodes after importing options from XML:");
                                    foreach (BarCodeResult result in importedReader.ReadBarCodes())
                                    {
                                        Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}