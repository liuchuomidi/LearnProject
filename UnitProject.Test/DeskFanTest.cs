using System;
using UnitProjcet;
using Xunit;

namespace UnitProject.Test
{
    public class DeskFanTest
    {
        [Fact]
        public void PowerLowerTanZero_OK()
        {
            var fan = new DeskFan(new PowerSupplyLowerThanZero());
            var excepet = "Won't work";
            var actual = fan.StartWork();
            Assert.Equal(excepet, actual);
        }
        class PowerSupplyLowerThanZero : IPowerSupply
        {
            public int GetPower()
            {
                return -1;
            }
        }
    }
}
