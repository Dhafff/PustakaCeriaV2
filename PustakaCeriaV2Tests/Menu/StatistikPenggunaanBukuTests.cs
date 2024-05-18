using Microsoft.VisualStudio.TestTools.UnitTesting;
using PustakaCeriaV2.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PustakaCeriaV2.Menu.Tests
{
    [TestClass()]
    public class StatistikPenggunaanBukuTests
    {
        [TestMethod()]
        public void HitungFrekuensiTest()
        {
            // Arrange
            var statistika = new StatistikPenggunaanBuku<string>();
            var data = new List<string> { "A", "B", "A", "C", "B", "A", "D", "D" };
            var expected = new Dictionary<string, int>
            {
                { "A", 3 },
                { "B", 2 },
                { "C", 1 },
                { "D", 2 }
            };

            // Act
            var result = statistika.HitungFrekuensi(data);

            // Assert
            CollectionAssert.AreEqual(expected, result);
        }
    }
}