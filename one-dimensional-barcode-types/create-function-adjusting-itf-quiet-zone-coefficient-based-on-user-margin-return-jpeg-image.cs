// Title: ITF14 Barcode Generation with Adjustable Quiet Zone
// Description: Demonstrates generating an ITF14 barcode, adjusting its quiet‑zone based on a user‑specified margin, and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as XDimension and quiet‑zone coefficient using the BarcodeGenerator class. Typical use cases include creating product packaging barcodes (e.g., ITF14) with custom margins for printing workflows. Developers often need to fine‑tune quiet‑zone settings to meet scanner requirements or layout constraints.
// Prompt: Create function adjusting ITF quiet zone coefficient based on user margin, return JPEG image.
// Tags: itf, barcode, quietzone, jpeg, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace ITFQuietZoneExample
{
    /// <summary>
    /// Provides a console entry point that generates an ITF14 barcode with a user‑defined quiet‑zone margin
    /// and saves the resulting JPEG image to disk.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Sample usage: generate an ITF14 barcode with a 20‑point margin and save as JPEG.
        /// </summary>
        static void Main()
        {
            // 14‑digit ITF14 code to encode.
            string codeText = "12345678901231";

            // Desired quiet‑zone margin in points.
            float userMargin = 20f;

            // Output file name for the generated JPEG image.
            string outputFile = "itf14.jpg";

            try
            {
                // Generate the barcode and obtain JPEG bytes.
                byte[] jpegBytes = GenerateITFBarcode(codeText, userMargin);

                // Write the JPEG bytes to the specified file.
                File.WriteAllBytes(outputFile, jpegBytes);

                Console.WriteLine($"Barcode saved to {outputFile}");
            }
            catch (Exception ex)
            {
                // Output any errors that occur during generation or file I/O.
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        /// <summary>
        /// Generates an ITF14 barcode, adjusts the quiet‑zone coefficient based on the supplied margin,
        /// and returns the image as a JPEG byte array.
        /// </summary>
        /// <param name="codeText">The 14‑digit code to encode.</param>
        /// <param name="margin">Desired quiet‑zone margin in points.</param>
        /// <returns>JPEG image bytes.</returns>
        static byte[] GenerateITFBarcode(string codeText, float margin)
        {
            // Validate that the code text is not null, empty, or whitespace.
            if (string.IsNullOrWhiteSpace(codeText))
                throw new ArgumentException("Code text cannot be null or empty.", nameof(codeText));

            // ITF14 requires exactly 14 numeric digits.
            if (codeText.Length != 14 || !long.TryParse(codeText, out _))
                throw new ArgumentException("ITF14 code must be a 14‑digit numeric string.", nameof(codeText));

            // Create the barcode generator for ITF14.
            using (var generator = new BarcodeGenerator(EncodeTypes.ITF14, codeText))
            {
                // Set a reasonable XDimension (module width) – 2 points by default.
                generator.Parameters.Barcode.XDimension.Point = 2f;

                // Calculate the quiet‑zone coefficient.
                // QuietZoneCoef = ceil(margin / XDimension). Minimum allowed value is 10.
                int calculatedCoef = (int)Math.Ceiling(margin / generator.Parameters.Barcode.XDimension.Point);
                if (calculatedCoef < 10)
                    calculatedCoef = 10;

                // Apply the coefficient to the ITF parameters.
                generator.Parameters.Barcode.ITF.QuietZoneCoef = calculatedCoef;

                // Generate the image into a memory stream as JPEG.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Jpeg);
                    return ms.ToArray();
                }
            }
        }
    }
}