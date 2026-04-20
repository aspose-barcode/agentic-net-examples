using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        try
        {
            TestRoutingAndServiceCode();
            Console.WriteLine("All tests passed.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Test failed: {ex.Message}");
        }
    }

    static void TestRoutingAndServiceCode()
    {
        // Expected values
        string expectedRouting = "RT123";
        string expectedService = "SC456";
        string expectedFullCode = $"{expectedRouting}{expectedService}";

        // Generate barcode with the combined code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, expectedFullCode))
        {
            // Use a memory stream to avoid file I/O
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream for reading

                // Recognize the barcode from the generated image
                using (var reader = new BarCodeReader(ms, DecodeType.Code128))
                {
                    // Read first (and only) barcode result
                    BarCodeResult result = null;
                    foreach (var r in reader.ReadBarCodes())
                    {
                        result = r;
                        break;
                    }

                    if (result == null)
                        throw new InvalidOperationException("No barcode was recognized.");

                    // Verify that the full code text matches the expected value
                    if (!string.Equals(result.CodeText, expectedFullCode, StringComparison.Ordinal))
                        throw new InvalidOperationException($"Full code text mismatch. Expected: {expectedFullCode}, Actual: {result.CodeText}");

                    // For demonstration, split the recognized text into routing and service parts
                    // (Assuming fixed lengths: 5 for routing, 5 for service)
                    if (result.CodeText.Length < 10)
                        throw new InvalidOperationException("Recognized code text is shorter than expected.");

                    string actualRouting = result.CodeText.Substring(0, 5);
                    string actualService = result.CodeText.Substring(5, 5);

                    if (!string.Equals(actualRouting, expectedRouting, StringComparison.Ordinal))
                        throw new InvalidOperationException($"Routing code mismatch. Expected: {expectedRouting}, Actual: {actualRouting}");

                    if (!string.Equals(actualService, expectedService, StringComparison.Ordinal))
                        throw new InvalidOperationException($"Service code mismatch. Expected: {expectedService}, Actual: {actualService}");
                }
            }
        }
    }
}