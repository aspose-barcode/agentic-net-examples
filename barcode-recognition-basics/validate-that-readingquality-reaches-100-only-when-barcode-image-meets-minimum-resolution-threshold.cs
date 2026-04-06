using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string filePath = "barcode.png";
        const float minResolutionDpi = 200f; // minimum DPI threshold
        const float testResolutionDpi = 300f; // resolution used for generation

        // Generate a barcode with a specific resolution
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set the image resolution (dpi)
            generator.Parameters.Resolution = testResolutionDpi;

            // Save the barcode image to a file
            generator.Save(filePath);
        }

        // Read the generated barcode and obtain the reading quality
        using (BarCodeReader reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                double readingQuality = result.ReadingQuality; // value in percent

                // Validate reading quality against the resolution threshold
                if (testResolutionDpi >= minResolutionDpi && readingQuality < 99.0)
                {
                    throw new InvalidOperationException(
                        $"ReadingQuality is {readingQuality}%, but expected at least 99% for resolution {testResolutionDpi} DPI.");
                }
                else if (testResolutionDpi < minResolutionDpi && readingQuality >= 99.0)
                {
                    throw new InvalidOperationException(
                        $"ReadingQuality is {readingQuality}% despite resolution {testResolutionDpi} DPI being below the threshold of {minResolutionDpi} DPI.");
                }
                else
                {
                    Console.WriteLine($"Resolution: {testResolutionDpi} DPI, ReadingQuality: {readingQuality}% - validation passed.");
                }
            }
        }
    }
}