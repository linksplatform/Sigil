﻿using NUnit.Framework;
using Sigil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SigilTests
{
    [TestFixture, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class Throw
    {
        [Test]
        public void Simple()
        {
            var e1 = Emit<Action>.NewDynamicMethod();
            e1.LoadConstant("Hello!");
            e1.NewObject<Exception, string>();
            e1.Throw();

            var d1 = e1.CreateDelegate();

            try
            {
                d1();
                Assert.Fail();
            }
            catch (Exception e)
            {
                Assert.AreEqual("Hello!", e.Message);
            }
        }
    }
}
