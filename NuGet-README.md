# Sigil

A fail-fast, validating helper for [DynamicMethod](http://msdn.microsoft.com/en-us/library/system.reflection.emit.dynamicmethod.aspx) and [ILGenerator](http://msdn.microsoft.com/en-us/library/system.reflection.emit.ilgenerator.aspx).

## Usage

Sigil is a roughly 1-to-1 replacement for ILGenerator.  Rather than calling ILGenerator.Emit(OpCode, ...), you call Emit<DelegateType>.OpCode(...).

Unlike ILGenerator, Sigil will fail as soon as an error is detected in the emitted IL.

### Performance

Since Sigil performs a great deal of verification it is necessarily slower than using ILGenerator directly.  That being said, Sigil should be adequately performant for most purposes.

Sigil may be too slow for practical use if you need:

  - More than ~100 labels and branches
  - Methods with more than ~10,000 instructions

Some costly optimizations can be disabled via the OptimizationOptions enumeration, and some validation steps can be deferred via the ValidationOptions enumeration.

### Unsupported Operations

Fault blocks are not supported because of their rarity (there is no C# equivalent) and because they are forbidden in dynamic methods.

Sigil does not support Calli when disassembling delegates, as the C# compilers will not emit that instruction it is currently untestable.
