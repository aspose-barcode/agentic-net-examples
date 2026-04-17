using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Path to the multi‑page TIFF file (adjust as needed)
        string tiffPath = "sample.tif";

        // Verify that the file exists
        if (!File.Exists(tiffPath))
        {
            Console.WriteLine($"File not found: {tiffPath}");
            return;
        }

        // Load the TIFF image using Aspose.Drawing
        using (Image tiffImage = Image.FromFile(tiffPath))
        {
            // Obtain the frame dimension that represents pages
            Guid pageGuid = tiffImage.FrameDimensionsList[0];
            var pageDimension = new FrameDimension(pageGuid);
            int pageCount = tiffImage.GetFrameCount(pageDimension);

            // Iterate through each page/frame
            for (int pageIndex = 0; pageIndex < pageCount; pageIndex++)
            {
                // Select the current page
                tiffImage.SelectActiveFrame(pageDimension, pageIndex);

                // Create a bitmap for the selected page
                using (Bitmap pageBitmap = new Bitmap(tiffImage))
                {
                    // Initialize the barcode reader for all supported types
                    using (BarCodeReader reader = new BarCodeReader(pageBitmap, DecodeType.AllSupportedTypes))
                    {
                        // Read all barcodes on this page
                        foreach (BarCodeResult result in reader.ReadBarCodes())
                        {
                            // Output barcode information including orientation angle
                            Console.WriteLine($"Page {pageIndex + 1}: Type = {result.CodeTypeName}, Text = {result.CodeText}, Angle = {result.Region.Angle}");
                        }
                    }
                }
            }
        }
    }
}