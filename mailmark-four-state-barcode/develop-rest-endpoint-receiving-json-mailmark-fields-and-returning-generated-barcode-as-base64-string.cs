using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

namespace MailmarkBarcodeApp
{
    public class MailmarkRequest
    {
        public int Format { get; set; }
        public int VersionID { get; set; }
        public string Class { get; set; }
        public int SupplychainID { get; set; }
        public int ItemID { get; set; }
        public string DestinationPostCodePlusDPS { get; set; }
    }

    class Program
    {
        static void Main()
        {
            string json = @"{
                ""Format"": 4,
                ""VersionID"": 1,
                ""Class"": ""0"",
                ""SupplychainID"": 384224,
                ""ItemID"": 16563762,
                ""DestinationPostCodePlusDPS"": ""EF61AH8T ""
            }";

            MailmarkRequest request;
            try
            {
                request = JsonSerializer.Deserialize<MailmarkRequest>(json);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Invalid JSON payload.", ex);
            }

            if (request == null ||
                string.IsNullOrWhiteSpace(request.Class) ||
                string.IsNullOrWhiteSpace(request.DestinationPostCodePlusDPS))
            {
                throw new ArgumentException("Missing required Mailmark fields.");
            }

            var mailmark = new MailmarkCodetext
            {
                Format = request.Format,
                VersionID = request.VersionID,
                Class = request.Class,
                SupplychainID = request.SupplychainID,
                ItemID = request.ItemID,
                DestinationPostCodePlusDPS = request.DestinationPostCodePlusDPS
            };

            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set a reasonable bar height (e.g., 5 millimeters)
                generator.Parameters.Barcode.BarHeight.Millimeters = 5;

                using (var memoryStream = new MemoryStream())
                {
                    generator.Save(memoryStream, BarCodeImageFormat.Png);
                    string base64 = Convert.ToBase64String(memoryStream.ToArray());
                    Console.WriteLine(base64);
                }
            }
        }
    }
}