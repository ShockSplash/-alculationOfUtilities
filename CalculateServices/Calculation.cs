using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculateServices
{
    public class Calculation : ISubsidyCalculation
    {
        public event EventHandler<string> OnNotify;
        public event EventHandler<Tuple<string, Exception>> OnException;

        private bool CheckPeriod(DateTime startPeriod, DateTime endPeriod, DateTime nowPeriod)
        {
            if (nowPeriod > startPeriod && nowPeriod < endPeriod)
                return true;
            else
                return false;
        }

        private bool CheckConditions(Volume volumes, Tariff tariff)
        {
            if (volumes.ServiceId == tariff.ServiceId && CheckPeriod(tariff.PeriodBegin, tariff.PeriodEnd, volumes.Month)
                && volumes.HouseId == tariff.HouseId && tariff.Value > 0 && volumes.Value >= 0)
                return true;
            else
                return false;
        }

        private string GetUnvalidDate(Volume volumes, Tariff tariff)
        {
            if (volumes.HouseId != tariff.HouseId)
                return "Значения идентификаторов у домов не совпадают.";
            else if (CheckPeriod(tariff.PeriodBegin, tariff.PeriodEnd, volumes.Month))
                return "Объем не входит в период действия тарифа.";
            else if (volumes.ServiceId != tariff.ServiceId)
                return "Значения идентификаторов услуг не совпадают.";
            else if (tariff.Value <= 0)
                return "Не допускается нулевых или отрицательных значений тарифа.";
            else if (volumes.Value < 0)
                return "Не допускаются отрицательные значений объема.";
            else
                return "";
        }

        public Charge CalculateSubsidy(Volume volumes, Tariff tariff)
        {
            OnNotify?.Invoke(this,"Расчет начат в: " + (DateTime.Now.ToShortTimeString()));
            Charge chargeResult = new Charge();
            try
            {
                if (CheckConditions(volumes, tariff))
                {
                    chargeResult.Value = volumes.Value * tariff.Value;
                }
                else
                {
                    throw new Exception(GetUnvalidDate(volumes,tariff));
                }
                    
            }
            catch (Exception e)
            {
                OnException?.Invoke(this, new Tuple<string, Exception>(e.Message, e));
                throw;

            }
            OnNotify?.Invoke(this, "Расчет окончен в: " + DateTime.Now.ToShortTimeString());
            return chargeResult;
        }
    }
}
