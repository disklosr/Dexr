using Dex.Core.Entities;
using NUnit.Framework;
using Shouldly;

namespace Dex.Core.Test.Entities
{
    [TestFixture]
    public class CpTest
    {
        [TestCase((ushort)118, (ushort)118, (ushort)90, (ushort)981)]
        public void ShouldCalculateCorrectMaxCP(ushort attack, ushort defense, ushort stamina, ushort expectedCp)
        {
            var actualCp = new CpCalculator(new CombatStat(attack), new CombatStat(defense), new CombatStat(stamina));

            actualCp.Max.Value.ShouldBe(expectedCp);
        }
    }
}