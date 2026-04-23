using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace ITFQuietZoneDemo
{
    class Program
    {
        static void Main()
        {
            // Example margin in points
            double userMarginPoints = 20.0;

            // Generate barcode image bytes
            byte[] jpegData = GenerateITFBarcodeWithQuietZone(userMarginPoints);

            // Save to file for verification
            File.WriteAllBytes("ITF_QuietZone.jpg", jpegData);
        }

        /// <summary>
        /// Generates an ITF14 barcode image with quiet zone coefficient adjusted according to the supplied margin.
        /// Returns the JPEG image as a byte array.
        /// </summary>
        /// <param name="marginPoints">Desired quiet zone margin in points.</param>
        /// <returns>JPEG image bytes.</returns>
        static byte[] GenerateITFBarcodeWithQuietZone(double marginPoints)
        {
            // Define a base x-dimension (smallest bar width) in points.
            // This value can be changed as needed; here we use 2 points.
            const float xDimensionPoints = 2f;

            // Calculate the quiet zone coefficient (must be at least 10).
            int quietZoneCoef = (int)Math.Ceiling(marginPoints / xDimensionPoints);
            if (quietZoneCoef < 10)
                quietZoneCoef = 10;

            // Create the barcode generator for ITF14.
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14))
            {
                // Sample numeric code text (ITF14 requires 13 digits + checksum, but generator can compute it).
                generator.CodeText = "123456789012";

                // Set the x-dimension.
                generator.Parameters.Barcode.XDimension.Point = xDimensionPoints;

                // Adjust the quiet zone coefficient.
                generator.Parameters.Barcode.ITF.QuietZoneCoef = quietZoneCoef;

                // Generate the bitmap.
                Bitmap bitmap = generator.GenerateBarCodeImage();

                // Save bitmap to a memory stream in JPEG format.
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }
    }
}