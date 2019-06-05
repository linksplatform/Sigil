﻿using NUnit.Framework;
using Sigil;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigilTests
{
    [TestFixture, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class Divide
    {
        [Test]
        public void Simple()
        {
            var e1 = Emit<Func<double, double, double>>.NewDynamicMethod("E1");
            e1.LoadArgument(0);
            e1.LoadArgument(1);
            e1.Divide();
            e1.Return();

            var d1 = e1.CreateDelegate();

            Assert.AreEqual(3.14 / 1.59, d1(3.14, 1.59));
        }

        [Test]
        public void Unsigned()
        {
            var e1 = Emit<Func<uint, uint, uint>>.NewDynamicMethod("E1");
            e1.LoadArgument(0);
            e1.LoadArgument(1);
            e1.UnsignedDivide();
            e1.Return();

            var d1 = e1.CreateDelegate();

            Assert.AreEqual(uint.MaxValue / ((uint)1234), d1(uint.MaxValue, (uint)1234));
        }
    }
}
