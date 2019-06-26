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
            if (args[0] == "telegraf")
            {
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
            else if (args[0] == "mackerel")
            {
                var datetime = DateTime.Now.ToUniversalTime();
                var timestamp = (long)datetime.Subtract(datetimeUnixEpoch).TotalSeconds;
                corecount = 0;
                foreach (var cpu in cpus)
                {
                    foreach (var core in cpu.CoreTemperatures)
                    {
                        Console.WriteLine($"temp.core{corecount}\t{core.Value}\t{timestamp}");
                        corecount++;
                    }
                }
            }
        }
    }
}
