using System;

namespace CalculateServices
{
    class Program
    {
        static void Main(string[] args)
        {
            Tariff tariff = new Tariff();
            tariff.HouseId = 15;
            tariff.ServiceId = 122;
            tariff.Value = 10;
            tariff.PeriodBegin = new DateTime(2018,1,1);
            tariff.PeriodEnd = new DateTime(2018, 9, 12);


            Volume volume = new Volume();
            volume.HouseId = 15;
            volume.ServiceId = 122;
            volume.Month = new DateTime(2018, 8, 14);
            volume.Value = 14;
            Calculation calc = new Calculation();
            calc.OnNotify += TestDelegate.TestMethod;
            var res = calc.CalculateSubsidy(volume, tariff);
            Console.WriteLine(res.Value);

        }
    }
}
