using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Path to the multi‑page TIFF file (default sample if not provided)
        string tiffPath = args.Length > 0 ? args[0] : "sample.tif";

        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Determine number of pages/frames
            int pageCount = tiffImage.GetFrameCount(FrameDimension.Page);
            Console.WriteLine($"Total pages in TIFF: {pageCount}");

            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Activate the current frame
                tiffImage.SelectActiveFrame(FrameDimension.Page, pageIndex);

                // Clone the active frame into a Bitmap for processing
                using (Bitmap bitmap = new Bitmap(tiffImage))
                {
                    // Initialize the reader for Australia Post barcode type
                    using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
                    {
                        // Optional: set interpreting type if known (using default Other here)
                        // reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.Other;

                        // Perform recognition
                        BarCodeResult[] results = reader.ReadBarCodes();

                        if (results.Length == 0)
                        {
                            Console.WriteLine($"Page {pageIndex + 1}: No Australia Post barcode detected.");
                        }
                        else
                        {
                            foreach (BarCodeResult result in results)
                            {
                                Console.WriteLine($"Page {pageIndex + 1}: Type = {result.CodeTypeName}, Text = {result.CodeText}");
                            }
                        }
                    }
                }
            }
        }
    }
}