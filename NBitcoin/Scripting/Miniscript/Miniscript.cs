using System;
using NBitcoin.Scripting.Miniscript.Types;

namespace NBitcoin.Scripting.Miniscript
{
	public partial class Miniscript<TPk, TPKh>
		where TPk : class, IMiniscriptKey<TPKh>, new()
		where TPKh : class, IMiniscriptKeyHash, new()
	{
		internal readonly Terminal<TPk, TPKh> Node;
		public readonly MiniscriptFragmentType Type;

		/// <summary>
		/// Additional information helpful for extra analysis.
		/// </summary>
		/// <returns></returns>
		internal readonly ExtData Ext;

		internal Miniscript(MiniscriptFragmentType type, Terminal<TPk, TPKh> node, ExtData ext)
		{
			Type = type;
			Node = node;
			Ext = ext;
		}

		public static Miniscript<TPk, TPKh> FromAst(Terminal<TPk, TPKh> t)
		{
			return
				new Miniscript<TPk, TPKh>(
					Property<MiniscriptFragmentType, TPk, TPKh>.TypeCheck(t),
					t,
					Property<ExtData, TPk, TPKh>.TypeCheck(t)
				);
		}

		public static Miniscript<TPk, TPKh> Parse(string str)
		{
			var inner = MiniscriptDSLParser<TPk, TPKh>.ParseTerminal(str);
			return new Miniscript<TPk, TPKh>(
				Property<MiniscriptFragmentType, TPk, TPKh>.TypeCheck(inner),
				inner,
				Property<ExtData, TPk, TPKh>.TypeCheck(inner)
				);
		}

		public Script ToScript() =>
			Node.ToScript();

	}
}
