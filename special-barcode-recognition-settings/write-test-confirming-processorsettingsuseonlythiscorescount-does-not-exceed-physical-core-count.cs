using System;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Ensure the processor settings are configured to use a specific number of cores
        BarCodeReader.ProcessorSettings.UseAllCores = false;

        // Attempt to set a value larger than the physical core count
        int requestedCores = Environment.ProcessorCount + 2;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = requestedCores;

        // Retrieve the actual value after assignment
        int actualCores = BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount;
        int physicalCores = Environment.ProcessorCount;

        // Verify that the configured core count does not exceed the physical core count
        if (actualCores > physicalCores)
        {
            Console.WriteLine($"Test Failed: UseOnlyThisCoresCount ({actualCores}) exceeds physical core count ({physicalCores}).");
        }
        else
        {
            Console.WriteLine($"Test Passed: UseOnlyThisCoresCount ({actualCores}) is within physical core count ({physicalCores}).");
        }
    }
}