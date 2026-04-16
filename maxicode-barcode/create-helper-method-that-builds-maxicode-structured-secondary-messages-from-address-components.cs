using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

namespace MaxiCodeHelperDemo
{
    class Program
    {
        static void Main()
        {
            // Build a structured second message from address components
            var secondMessage = BuildStructuredSecondMessage(
                line1: "634 ALPHA DRIVE",
                line2: "PITTSBURGH",
                line3: "PA",
                year: 99);

            // Prepare MaxiCode codetext for Mode 2 (postal info + structured second message)
            var maxiCodeCodetext = new MaxiCodeCodetextMode2
            {
                PostalCode = "524032140",   // 9‑digit US postal code
                CountryCode = 056,          // USA
                ServiceCategory = 999
            };
            maxiCodeCodetext.SecondMessage = secondMessage;

            // Generate and save the MaxiCode barcode image
            using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.GenerateBarCodeImage();
                generator.Save("maxicode.png");
            }

            Console.WriteLine("MaxiCode barcode generated and saved as 'maxicode.png'.");
        }

        /// <summary>
        /// Creates a MaxiCodeStructuredSecondMessage and populates it with address lines and year.
        /// </summary>
        /// <param name="line1">First address line (e.g., street).</param>
        /// <param name="line2">Second address line (e.g., city).</param>
        /// <param name="line3">Third address line (e.g., state or region).</param>
        /// <param name="year">Two‑digit year value.</param>
        /// <returns>Configured MaxiCodeStructuredSecondMessage instance.</returns>
        static MaxiCodeStructuredSecondMessage BuildStructuredSecondMessage(string line1, string line2, string line3, int year)
        {
            var message = new MaxiCodeStructuredSecondMessage();
            if (!string.IsNullOrEmpty(line1))
                message.Add(line1);
            if (!string.IsNullOrEmpty(line2))
                message.Add(line2);
            if (!string.IsNullOrEmpty(line3))
                message.Add(line3);
            message.Year = year;
            return message;
        }
    }
}