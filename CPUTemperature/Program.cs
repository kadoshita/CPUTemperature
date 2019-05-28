using HardwareProviders.CPU;
using System;

namespace CPUTemperature
{
    class Program
    {
        static void Main(string[] args)
        {
            var measurement = "cpu_temperature";

            var cpus = Cpu.Discover();

            int corecount, cpucount = 0;
            foreach (var cpu in cpus)
            {
                corecount = 0;
                foreach (var core in cpu.CoreTemperatures)
                {
                    Console.WriteLine($"{measurement},cpu={cpucount},core={corecount} max={core.Max},min={core.Min},value={core.Value}");
                    corecount++;
                }
                cpucount++;
            }
        }
    }
}
