using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates importing a barcode generator from XML and handling expected exceptions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Executes the test for XML import exception handling.
    /// </summary>
    static void Main()
    {
        TestImportFromXmlThrows();
    }

    /// <summary>
    /// Tests that importing a <see cref="BarcodeGenerator"/> from XML without image data throws an exception.
    /// </summary>
    static void TestImportFromXmlThrows()
    {
        // Create a minimal XML that does not contain any barcode image data.
        string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
  <CodeText>12345</CodeText>
  <EncodeType>Code128</EncodeType>
</BarcodeGenerator>";

        // Write the XML to a memory stream so it can be read by the ImportFromXml method.
        using (var memoryStream = new MemoryStream())
        {
            // Use a StreamWriter with UTF-8 encoding to write the XML string into the stream.
            using (var writer = new StreamWriter(memoryStream, System.Text.Encoding.UTF8, 1024, true))
            {
                writer.Write(xmlContent);
                writer.Flush();               // Ensure all data is written to the underlying stream.
                memoryStream.Position = 0;    // Reset position to the beginning for reading.
            }

            try
            {
                // Attempt to import from XML without having set a barcode image.
                // According to the requirement, this should throw an exception.
                BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(memoryStream);

                // If no exception is thrown, the test fails.
                Console.WriteLine("Test Failed: No exception was thrown.");

                // Dispose the generator if it was created to release resources.
                generator?.Dispose();
            }
            catch (Exception ex)
            {
                // Expected path: an exception is thrown.
                Console.WriteLine("Test Passed: Caught expected exception.");
                Console.WriteLine("Exception Message: " + ex.Message);
            }
        }
    }
}