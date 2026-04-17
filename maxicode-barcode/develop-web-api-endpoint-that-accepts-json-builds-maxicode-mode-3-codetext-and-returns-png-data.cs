using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

namespace MaxiCodeMode3Demo
{
    // Simulated request payload
    public class MaxiCodeRequest
    {
        public string PostalCode { get; set; } = "B1050";      // 6‑character alphanumeric postal code
        public int CountryCode { get; set; } = 56;            // 3‑digit numeric country code
        public int ServiceCategory { get; set; } = 999;      // 3‑digit service category
        public string Message { get; set; } = "Test message"; // Standard second message
    }

    class Program
    {
        static void Main()
        {
            // Create a sample request (in a real Web API this would be deserialized from JSON)
            var request = new MaxiCodeRequest();

            // Build the MaxiCode Mode 3 codetext
            var codetext = new MaxiCodeCodetextMode3
            {
                PostalCode = request.PostalCode,
                CountryCode = request.CountryCode,
                ServiceCategory = request.ServiceCategory
            };

            var secondMessage = new MaxiCodeStandardSecondMessage
            {
                Message = request.Message
            };
            codetext.SecondMessage = secondMessage;

            // Generate the barcode and obtain PNG bytes
            byte[] pngBytes;
            using (var generator = new ComplexBarcodeGenerator(codetext))
            {
                // Generate image and save to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    pngBytes = ms.ToArray();
                }
            }

            // Return PNG data as Base64 string (simulating an HTTP response body)
            string base64Png = Convert.ToBase64String(pngBytes);
            Console.WriteLine(base64Png);
        }
    }
}