using NUnit.Framework;
using Sigil;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SigilTests
{
    [TestFixture, System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage]
    public partial class Break
    {
        [Test]
        public void Simple()
        {
            var e1 = Emit<Action>.NewDynamicMethod("E1");
            e1.LoadConstant(123);
            e1.Break();
            e1.Pop();
            e1.Return();

            var d1 = e1.CreateDelegate();

            d1();
        }
    }
}
