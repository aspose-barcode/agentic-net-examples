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
        // Create a temporary folder for any intermediate files (not strictly required here)
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarCodeDemo");
        Directory.CreateDirectory(tempFolder);

        // Generate a Code128 barcode image
        using (BarcodeGenerator gen128 = new BarcodeGenerator(EncodeTypes.Code128, "CODE128TEXT"))
        {
            using (Bitmap bmp128 = gen128.GenerateBarCodeImage())
            {
                // Generate a DataMatrix barcode image
                using (BarcodeGenerator genDM = new BarcodeGenerator(EncodeTypes.DataMatrix, "DMATRIX"))
                {
                    using (Bitmap bmpDM = genDM.GenerateBarCodeImage())
                    {
                        // Determine size for a combined image (side‑by‑side with a small margin)
                        int combinedWidth = bmp128.Width + bmpDM.Width + 20; // 10px margin each side
                        int combinedHeight = Math.Max(bmp128.Height, bmpDM.Height) + 20;

                        // Create the combined bitmap
                        using (Bitmap combined = new Bitmap(combinedWidth, combinedHeight))
                        {
                            using (Graphics graphics = Graphics.FromImage(combined))
                            {
                                graphics.Clear(Aspose.Drawing.Color.White);
                                graphics.DrawImage(bmp128, 10, 10);
                                graphics.DrawImage(bmpDM, bmp128.Width + 20, 10);
                            }

                            // Recognize both Code128 and DataMatrix barcodes using MultiDecodeType
                            using (BarCodeReader reader = new BarCodeReader(combined, DecodeType.Code128, DecodeType.DataMatrix))
                            {
                                foreach (var result in reader.ReadBarCodes())
                                {
                                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}