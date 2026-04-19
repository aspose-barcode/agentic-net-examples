using System;
using System.Net.Http;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main(string[] args)
    {
        // Remote image URL (replace with a valid image containing a barcode)
        string imageUrl = "https://example.com/barcode.png";

        // Download the image as a stream without saving to disk
        using (var httpClient = new HttpClient())
        {
            try
            {
                using (var imageStream = httpClient.GetStreamAsync(imageUrl).GetAwaiter().GetResult())
                {
                    // Initialize the barcode reader
                    using (var reader = new BarCodeReader())
                    {
                        // Specify which barcode types to look for (add more if needed)
                        reader.SetBarCodeReadType(DecodeType.Code128, DecodeType.QR, DecodeType.Code39, DecodeType.EAN13);

                        // Provide the image stream to the reader
                        reader.SetBarCodeImage(imageStream);

                        // Perform recognition
                        foreach (var result in reader.ReadBarCodes())
                        {
                            Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                            Console.WriteLine($"BarCode Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Failed to download image: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during barcode recognition: {ex.Message}");
            }
        }
    }
}