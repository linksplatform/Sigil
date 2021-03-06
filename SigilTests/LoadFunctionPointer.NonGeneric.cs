using NUnit.Framework;
using Sigil.NonGeneric;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection.Emit;
using System.Reflection;

namespace SigilTests
{
    public partial class LoadFunctionPointer
    {
        [Test]
        public void CanValidateUnbakedNonGeneric()
        {
            var asm = AssemblyBuilder.DefineDynamicAssembly(new AssemblyName("MethodBuilders"), AssemblyBuilderAccess.Run);
            var mod = asm.DefineDynamicModule("Mod");
            var tb = mod.DefineType("Type");

            var targetMethodBuilder = Emit.BuildStaticMethod(typeof(void), new Type[0], tb, "UnbakedFunction", MethodAttributes.Public);
            targetMethodBuilder.Return();
            var targetMethod = targetMethodBuilder.CreateMethod();

            var testMethod = Emit.BuildStaticMethod(typeof(void), new Type[0], tb, "Create", MethodAttributes.Public | MethodAttributes.Static);
            testMethod.LoadFunctionPointer(targetMethod, new Type[0]);
            testMethod.Pop();//dump useless obj
            testMethod.Return();

            testMethod.CreateMethod();//will throw on failure
        }        
    }
}