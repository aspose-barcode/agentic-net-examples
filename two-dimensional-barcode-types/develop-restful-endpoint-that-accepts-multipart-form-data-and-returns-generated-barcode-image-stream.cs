using System;
using System.IO;
using System.Text;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main(string[] args)
    {
        // Default values if arguments are not provided
        string codeText = "1234567890";
        string symbologyName = "Code128";

        if (args.Length >= 1 && !string.IsNullOrWhiteSpace(args[0]))
            codeText = args[0];
        if (args.Length >= 2 && !string.IsNullOrWhiteSpace(args[1]))
            symbologyName = args[1];

        // Resolve symbology name to EncodeTypes field via reflection
        BaseEncodeType encodeType = ResolveEncodeType(symbologyName);
        if (encodeType == null)
        {
            Console.Error.WriteLine($"Unsupported symbology: {symbologyName}");
            return;
        }

        // Generate barcode and write image to a memory stream
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Example: set resolution and background color
            generator.Parameters.Resolution = 300;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            using (var ms = new MemoryStream())
            {
                // Save as PNG
                bitmap.Save(ms, ImageFormat.Png);
                ms.Position = 0;

                // Output the image as Base64 string (simulating a stream response)
                string base64 = Convert.ToBase64String(ms.ToArray());
                Console.WriteLine(base64);
            }
        }
    }

    // Helper to map a symbology name to EncodeTypes static field
    private static BaseEncodeType ResolveEncodeType(string name)
    {
        FieldInfo field = typeof(EncodeTypes).GetField(name, BindingFlags.Public | BindingFlags.Static);
        if (field != null && typeof(BaseEncodeType).IsAssignableFrom(field.FieldType))
        {
            return (BaseEncodeType)field.GetValue(null);
        }
        return null;
    }
}