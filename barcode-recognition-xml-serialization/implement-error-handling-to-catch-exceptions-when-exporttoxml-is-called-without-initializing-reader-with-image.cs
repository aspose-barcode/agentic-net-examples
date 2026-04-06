using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path for the exported XML file
        string xmlPath = Path.Combine(Path.GetTempPath(), "reader_settings.xml");

        // Create a BarCodeReader without setting an image
        using (var reader = new BarCodeReader())
        {
            try
            {
                // Attempt to export settings to XML – this should fail because no image is set
                bool success = reader.ExportToXml(xmlPath);
                Console.WriteLine($"Export succeeded: {success}");
            }
            catch (Exception ex)
            {
                // Expected exception handling
                Console.WriteLine("Exception caught while exporting to XML without an image:");
                Console.WriteLine(ex.GetType().FullName);
                Console.WriteLine(ex.Message);
            }
        }

        // Clean up the temporary XML file if it was created
        if (File.Exists(xmlPath))
        {
            try
            {
                File.Delete(xmlPath);
            }
            catch
            {
                // Ignore any cleanup errors
            }
        }
    }
}