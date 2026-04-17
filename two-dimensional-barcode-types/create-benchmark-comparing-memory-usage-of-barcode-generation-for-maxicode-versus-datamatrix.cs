using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        const int sampleCount = 5;

        long totalMaxiCodeMemory = 0;
        long totalDataMatrixMemory = 0;

        // Benchmark MaxiCode generation
        for (int i = 0; i < sampleCount; i++)
        {
            // Ensure a clean heap before measurement
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long before = GC.GetTotalMemory(true);

            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, "1234567890"))
            {
                // Save to a memory stream to force image generation
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }

            long after = GC.GetTotalMemory(true);
            totalMaxiCodeMemory += (after - before);
        }

        // Benchmark DataMatrix generation
        for (int i = 0; i < sampleCount; i++)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();

            long before = GC.GetTotalMemory(true);

            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "1234567890"))
            {
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }

            long after = GC.GetTotalMemory(true);
            totalDataMatrixMemory += (after - before);
        }

        double avgMaxiCodeMemory = totalMaxiCodeMemory / (double)sampleCount;
        double avgDataMatrixMemory = totalDataMatrixMemory / (double)sampleCount;

        Console.WriteLine($"Average memory used per MaxiCode generation: {avgMaxiCodeMemory:N0} bytes");
        Console.WriteLine($"Average memory used per DataMatrix generation: {avgDataMatrixMemory:N0} bytes");
    }
}