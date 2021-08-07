using System;

namespace UnitProjcet
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
    public class PowerSupply:IPowerSupply
    {
        public int GetPower()
        {
            return 100;
        }
    }
    
    public class DeskFan
    {
        private IPowerSupply _powerSupply;
        public DeskFan(IPowerSupply powerSupply)
        {
            _powerSupply = powerSupply;
        }
        public string StartWork()
        {
            int power = _powerSupply.GetPower();
            if (power <= 0)
            {
                return "Won't work";
            }
            else if (power < 100)
            {
                return "Slow";
            }
            else if (power >= 100 && power < 200)
            {
                return "Fine";
            }
            else { return "Boom"; }
        }
    }
    public interface IPowerSupply
    {
        public int GetPower();
    }
}
