using System;
using System.IO;
using System.Linq;
using System.Reflection;
using NUnit.Framework;
using Sigil.NonGeneric;

namespace SigilTests
{
    [TestFixture]
    public partial class Save
    {
#if !NETCOREAPP1_1
        [Test]
        public void EmitAddNonGeneric()
        {
            var filename = Path.GetFileName(Path.GetTempFileName() + ".dll");
#if NET452
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename);
#else
            var path = Path.Combine(AppContext.BaseDirectory, filename);
#endif

            Emit.DefineAssembly(new AssemblyName("Foo.NonGeneric"));
            Emit.DefineModule("Bar.NonGeneric", filename);
            var myClass = Emit.DefineType("MyClass");

            var e1 = Emit.BuildInstanceMethod(typeof(int), new[] { typeof(int) }, myClass, "E1", MethodAttributes.Public);
            e1.LoadArgument(1);
            e1.LoadConstant(2);
            e1.Add();
            e1.Convert<int>();
            e1.Return();

            e1.CreateMethod();

            var type = myClass.CreateType();
            var inst = type.GetConstructor(Type.EmptyTypes).Invoke(new object[0]);
            var add = type.GetMethod("E1");

            Assert.AreEqual(2, add.Invoke(inst, new object[] { 0 }));
            Assert.AreEqual(3, add.Invoke(inst, new object[] { 1 }));
            Assert.AreEqual(0, add.Invoke(inst, new object[] { -2 }));

            Assert.False(File.Exists(path));

            e1.Save(Path.GetFileName(path));

            Assert.True(File.Exists(path));

            var assembly = Assembly.LoadFrom(path);
            var module = assembly.GetModules().First();
            var obj = assembly.CreateInstance(module.GetTypes().First().FullName);
            var mi = obj.GetType().GetMethod("E1");
            mi.Invoke(obj, new object[] { 0 });

            Assert.AreEqual(2, (int) mi.Invoke(obj, new object[] { 0 }));
            Assert.AreEqual(3, (int) mi.Invoke(obj, new object[] { 1 }));
            Assert.AreEqual(0, (int) mi.Invoke(obj, new object[] { -2 }));
        }
#endif
    }
}
