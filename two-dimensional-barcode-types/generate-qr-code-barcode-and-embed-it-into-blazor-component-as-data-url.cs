using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Text to encode in the QR code
        string qrText = "https://example.com";

        // Create a QR code generator with the specified text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set QR error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the bitmap to a memory stream in PNG format
                using (var memoryStream = new MemoryStream())
                {
                    bitmap.Save(memoryStream, ImageFormat.Png);
                    byte[] imageBytes = memoryStream.ToArray();

                    // Convert the image bytes to a Base64 data URL
                    string base64 = Convert.ToBase64String(imageBytes);
                    string dataUrl = $"data:image/png;base64,{base64}";

                    // Create a simple Blazor component that displays the QR code
                    string componentContent = $@"@code {{
    private string qrDataUrl = ""{dataUrl}"";
}}

<img src=""@qrDataUrl"" alt=""QR Code"" />";

                    // Write the component to a .razor file
                    File.WriteAllText("QrComponent.razor", componentContent);
                }
            }
        }
    }
}