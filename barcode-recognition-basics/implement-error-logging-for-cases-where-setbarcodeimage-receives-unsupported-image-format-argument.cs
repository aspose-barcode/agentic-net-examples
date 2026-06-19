using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates handling of invalid image streams when using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Prepare a stream that does NOT contain a valid image (plain text)
        byte[] textData = System.Text.Encoding.UTF8.GetBytes("This is not an image");
        using (var invalidStream = new MemoryStream(textData))
        {
            // Attempt to create a Bitmap from the invalid stream
            Bitmap bitmap = null;
            try
            {
                // This will throw because the stream does not represent a supported image format
                bitmap = new Bitmap(invalidStream);
            }
            catch (Exception ex)
            {
                // Log the error – expected for non‑image data
                Console.WriteLine($"Error creating Bitmap from unsupported format: {ex.Message}");
            }

            // If bitmap creation failed, demonstrate handling when passing it to SetBarCodeImage
            if (bitmap == null)
            {
                // Use a dummy bitmap (1x1 white pixel) to keep the example runnable
                using (var dummyBitmap = new Bitmap(1, 1))
                {
                    dummyBitmap.SetPixel(0, 0, Color.White);
                    using (var reader = new BarCodeReader())
                    {
                        try
                        {
                            // This call is expected to succeed with a valid bitmap,
                            // but we log the attempt for completeness.
                            reader.SetBarCodeImage(dummyBitmap);
                            Console.WriteLine("SetBarCodeImage succeeded with dummy bitmap.");
                        }
                        catch (Exception ex)
                        {
                            // Log any unexpected errors from SetBarCodeImage
                            Console.WriteLine($"Error in SetBarCodeImage: {ex.Message}");
                        }
                    }
                }
            }
            else
            {
                // If bitmap was somehow created, attempt to set it and log any errors
                using (bitmap)
                using (var reader = new BarCodeReader())
                {
                    try
                    {
                        reader.SetBarCodeImage(bitmap);
                        Console.WriteLine("SetBarCodeImage succeeded.");
                    }
                    catch (Exception ex)
                    {
                        // Log any errors that occur during SetBarCodeImage
                        Console.WriteLine($"Error in SetBarCodeImage: {ex.Message}");
                    }
                }
            }
        }
    }
}