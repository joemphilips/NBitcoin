using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace NBitcoin.Scripting.Miniscript
{
	internal class NonTerm : IEquatable<NonTerm>
	{
		#region Subtype definitions

		internal static class Tags
		{
			public const int Expression = 0;
			public const int MaybeSwap = 1;
			public const int MaybeAndV = 2;
			public const int Alt = 3;
			public const int Check = 4;
			public const int DupIf = 5;
			public const int Verify = 6;
			public const int NonZero = 7;
			public const int ZeroNotEqual = 8;
			public const int AndV = 9;
			public const int AndB = 10;
			public const int Tern = 11;
			public const int OrB = 12;
			public const int OrD = 13;
			public const int OrC = 14;
			public const int ThreshW = 15;
			public const int ThreshE = 16;
			/// <summary>
			/// Could be or_d, or_c, or_i, d, n
			/// </summary>
			public const int EndIf = 17;

			public const int EndIfNotIf = 18;
			public const int EndIfElse = 19;
		}

		private int Tag;
		private NonTerm(int tag) => Tag = tag;
		public static NonTerm Expression { get; } = new NonTerm(Tags.Expression);
		public static NonTerm MaybeSwap { get; } = new NonTerm(Tags.MaybeSwap);
		public static NonTerm MaybeAndV { get; } = new NonTerm(Tags.MaybeAndV);
		public static NonTerm Alt { get; } = new NonTerm(Tags.Alt);
		public static NonTerm Check  { get; } = new NonTerm(Tags.Check);
		public static NonTerm DupIf { get; } = new NonTerm(Tags.DupIf);
		public static NonTerm Verify { get; } = new NonTerm(Tags.Verify);
		public static NonTerm NonZero { get; } = new NonTerm(Tags.NonZero);
		public static NonTerm ZeroNotEqual { get; } = new NonTerm(Tags.ZeroNotEqual);
		public static NonTerm AndV { get; } = new NonTerm(Tags.AndV);
		public static NonTerm AndB { get; } = new NonTerm(Tags.AndB);
		public static NonTerm Tern { get; } = new NonTerm(Tags.Tern);
		public static NonTerm OrB { get; } = new NonTerm(Tags.OrB);
		public static NonTerm OrD { get; } = new NonTerm(Tags.OrD);
		public static NonTerm OrC { get; } = new NonTerm(Tags.OrC);
		public static NonTerm EndIf { get; } = new NonTerm(Tags.EndIf);
		public static NonTerm EndIfNotIf { get; } = new NonTerm(Tags.EndIfNotIf);
		public static NonTerm EndIfElse { get; } = new NonTerm(Tags.EndIfElse);

		public class ThreshW : NonTerm
		{
			public ulong K;
			public ulong N;

			public ThreshW(ulong k, ulong n) : base(Tags.ThreshW)
			{
				N = n;
				K = k;
			}
		}

		public class ThreshE : NonTerm
		{
			public ulong K;
			public ulong N;

			public ThreshE(ulong k, ulong n) : base(Tags.ThreshW)
			{
				N = n;
				K = k;
			}
		}
		#endregion

		#region Equatable members
		public bool Equals(NonTerm other)
		{
			throw new NotImplementedException("");
		}
		#endregion
	}

	public partial class Terminal<TPk, TPKh> : IEquatable<Terminal<TPk, TPKh>>
		where TPk : class, IMiniscriptKey<TPKh>, new()
		where TPKh : class, IMiniscriptKeyHash, new()
	{
		# region Subtype definitions
		internal static class Tags
		{
			public const int True = 0;
			public const int False = 1;
			public const int Pk = 2;
			public const int PkH = 3;
			public const int After = 4;
			public const int Older = 5;
			public const int Sha256 = 6;
			public const int Hash256 = 7;
			public const int Ripemd160 = 8;
			public const int Hash160 = 9;
			public const int Alt = 10;
			public const int Swap = 11;
			public const int Check = 12;
			public const int DupIf = 13;
			public const int Verify = 14;
			public const int NonZero = 15;
			public const int ZeroNotEqual = 16;
			public const int AndV = 17;
			public const int AndB = 18;
			public const int AndOr = 19;
			public const int OrB = 20;
			public const int OrD = 21;
			public const int OrC = 22;
			public const int OrI = 23;
			public const int Thresh = 24;
			public const int ThreshM = 25;
		}

		internal int Tag;

		private Terminal(int tag) => Tag = tag;
		public static Terminal<TPk, TPKh> True { get; } = new Terminal<TPk, TPKh>(Tags.True);
		public static Terminal<TPk, TPKh> False { get; } = new Terminal<TPk, TPKh>(Tags.False);

		internal class Pk : Terminal<TPk, TPKh>
		{
			readonly public TPk Item;
			public Pk(TPk pk) : base(Tags.Pk) => Item = pk;
		}

		internal class PkH : Terminal<TPk, TPKh>
		{
			readonly public TPKh Item;
			public PkH(TPKh item) : base(Tags.PkH) => Item = item;
		}

		internal class After : Terminal<TPk, TPKh>
		{
			readonly public uint Item;
			public After(uint item) : base(Tags.After) => Item = item;
		}

		internal class Older : Terminal<TPk, TPKh>
		{
			readonly public uint Item;
			public Older(uint item) : base(Tags.Older) => Item = item;
		}

		internal class Sha256 : Terminal<TPk, TPKh>
		{
			readonly public uint256 Item;
			public Sha256(uint256 item) : base(Tags.Sha256) => Item = item;
		}
		internal class Hash256 : Terminal<TPk, TPKh>
		{
			readonly public uint256 Item;
			public Hash256(uint256 item) : base(Tags.Hash256) => Item = item;
		}

		internal class Ripemd160 : Terminal<TPk, TPKh>
		{
			readonly public uint160 Item;
			public Ripemd160(uint160 item) : base(Tags.Ripemd160) => Item = item;
		}

		internal class Hash160 : Terminal<TPk, TPKh>
		{
			readonly public uint160 Item;
			public Hash160(uint160 item) : base(Tags.Hash160) => Item = item;
		}

		internal class Alt : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item;
			public Alt(Miniscript<TPk, TPKh> item) : base(Tags.Alt) => Item = item;
		}

		internal class Swap : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item;
			public Swap(Miniscript<TPk, TPKh> item): base(Tags.Swap) => Item = item;
		}

		internal class Check : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item;
			public Check(Miniscript<TPk, TPKh> item): base(Tags.Check) => Item = item;
		}
		internal class DupIf : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item;
			public DupIf (Miniscript<TPk, TPKh> item): base(Tags.DupIf ) => Item = item;
		}
		internal class Verify : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item;
			public Verify(Miniscript<TPk, TPKh> item): base(Tags.Verify) => Item = item;
		}
		internal class NonZero : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item;
			public NonZero(Miniscript<TPk, TPKh> item): base(Tags.NonZero) => Item = item;
		}
		internal class ZeroNotEqual : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item;
			public ZeroNotEqual(Miniscript<TPk, TPKh> item): base(Tags.ZeroNotEqual) => Item = item;
		}
		internal class AndV : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item1;
			readonly public Miniscript<TPk, TPKh> Item2;
			public AndV(Miniscript<TPk, TPKh> item1,Miniscript<TPk, TPKh> item2): base(Tags.AndV)
			{
				Item1 = item1;
				Item2 = item2;
			}
		}
		internal class AndB : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item1;
			readonly public Miniscript<TPk, TPKh> Item2;
			public AndB(Miniscript<TPk, TPKh> item, Miniscript<TPk, TPKh> item2): base(Tags.AndB)
			{
				Item1 = item;
				Item2 = item2;
			}
		}
		internal class AndOr : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item1;
			readonly public Miniscript<TPk, TPKh> Item2;
			readonly public Miniscript<TPk, TPKh> Item3;

			public AndOr(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2, Miniscript<TPk, TPKh> item3) : base(Tags.AndOr)
			{
				Item1 = item1;
				Item2 = item2;
				Item3 = item3;
			}
		}
		internal class OrB : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item1;
			readonly public Miniscript<TPk, TPKh> Item2;
			public OrB(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2): base(Tags.OrB)
			{
				Item1 = item1;
				Item2 = item2;
			}
		}
		internal class OrD : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item1;
			readonly public Miniscript<TPk, TPKh> Item2;
			public OrD(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2): base(Tags.OrD)
			{
				Item1 = item1;
				Item2 = item2;
			}
		}

		internal class OrC : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item1;
			readonly public Miniscript<TPk, TPKh> Item2;
			public OrC(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2): base(Tags.OrC)
			{
				Item1 = item1;
				Item2 = item2;
			}
		}

		internal class OrI : Terminal<TPk, TPKh>
		{
			readonly public Miniscript<TPk, TPKh> Item1;
			readonly public Miniscript<TPk, TPKh> Item2;
			public OrI(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2): base(Tags.OrI)
			{
				Item1 = item1;
				Item2 = item2;
			}
		}
		internal class Thresh : Terminal<TPk, TPKh>
		{
			readonly public uint Item1;
			readonly public Miniscript<TPk, TPKh>[] Item2;
			public Thresh(uint item1, Miniscript<TPk, TPKh>[] item2): base(Tags.Thresh)
			{
				Item1 = item1;
				Item2 = item2;
			}
		}
		internal class ThreshM : Terminal<TPk, TPKh>
		{
			readonly public uint Item1;
			readonly public TPk[] Item2;
			public ThreshM(uint item1, TPk[] item2): base(Tags.ThreshM)
			{
				Item1 = item1;
				Item2 = item2;
			}
		}

		public static Terminal<TPk, TPKh> NewTrue() => Terminal<TPk, TPKh>.True;
		public static Terminal<TPk, TPKh> NewFalse() => Terminal<TPk, TPKh>.False;
		public static Terminal<TPk, TPKh> NewPk(TPk item) => new Pk(item);
		public static Terminal<TPk, TPKh> NewPkH(TPKh item) => new PkH(item);
		public static Terminal<TPk, TPKh> NewAfter(uint item) => new After(item);
		public static Terminal<TPk, TPKh> NewOlder(uint item) => new Older(item);
		public static Terminal<TPk, TPKh> NewSha256(uint256 item)
			=>  new Sha256(item);
		public static Terminal<TPk, TPKh> NewHash256(uint256 item)
			=> new Hash256(item);
		public static Terminal<TPk, TPKh> NewRipemd160(uint160 item)
			=> new Ripemd160(item);
		public static Terminal<TPk, TPKh> NewHash160(uint160 item)
			=> new Hash160(item);
		public static Terminal<TPk, TPKh> NewAlt(Miniscript<TPk, TPKh> item) => new Terminal<TPk, TPKh>.Alt(item);
		public static Terminal<TPk, TPKh> NewSwap(Miniscript<TPk, TPKh> item) => new Terminal<TPk, TPKh>.Swap(item);
		public static Terminal<TPk, TPKh> NewCheck(Miniscript<TPk, TPKh> item) => new Terminal<TPk, TPKh>.Check(item);
		public static Terminal<TPk, TPKh> NewDupIf(Miniscript<TPk, TPKh> item) => new DupIf(item);
		public static Terminal<TPk, TPKh> NewVerify(Miniscript<TPk, TPKh> item) => new Verify(item);
		public static Terminal<TPk, TPKh> NewNonZero(Miniscript<TPk, TPKh> item) => new NonZero(item);
		public static Terminal<TPk, TPKh> NewZeroNotEqual(Miniscript<TPk, TPKh> item) => new ZeroNotEqual(item);

		public static Terminal<TPk, TPKh> NewAndV(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2)
			=> new AndV(item1, item2);

		public static Terminal<TPk, TPKh> NewAndB(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2)
			=> new AndB(item1, item2);

		public static Terminal<TPk, TPKh> NewAndOr(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2, Miniscript<TPk, TPKh> item3)
			=> new AndOr(item1, item2, item3);

		public static Terminal<TPk, TPKh> NewOrB(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2)
			=> new OrB(item1, item2);

		public static Terminal<TPk, TPKh> NewOrD(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2)
			=> new OrD(item1, item2);
		public static Terminal<TPk, TPKh> NewOrC(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2)
			=> new OrC(item1, item2);
		public static Terminal<TPk, TPKh> NewOrI(Miniscript<TPk, TPKh> item1, Miniscript<TPk, TPKh> item2)
			=> new OrI(item1, item2);

		public static Terminal<TPk, TPKh> NewThresh(uint item1, IEnumerable<Miniscript<TPk, TPKh>> item2)
			=> new Thresh(item1, item2.ToArray());
		public static Terminal<TPk, TPKh> NewThreshM(uint item1, IEnumerable<TPk> item2)
			=> new ThreshM(item1, item2.ToArray());
		#endregion

		#region Equatable members
		public bool Equals(Terminal<TPk, TPKh> other)
		{
			if (other == null)
				return false;
			if (this.Tag != other.Tag)
				return false;

			switch (this.Tag)
			{
				case Tags.True: return true;
				case Tags.False: return true;
			}

			switch (this)
			{
				case Pk self:
					return self.Item.Equals(((Pk) other).Item);
				case PkH self:
					return self.Item.Equals(((PkH) other).Item);
				case After self:
					return self.Item.Equals(((After) other).Item);
				case Older self:
					return self.Item.Equals(((Older) other).Item);
				case Sha256 self:
					return self.Item.Equals(((Sha256) other).Item);
				case Hash256 self:
					return self.Item.Equals(((Hash256) other).Item);
				case Ripemd160 self:
					return self.Item.Equals(((Ripemd160) other).Item);
				case Hash160 self:
					return self.Item.Equals(((Hash160) other).Item);
				case Alt self:
					return self.Item.Equals(((Alt) other).Item);
				case Swap self:
					return self.Item.Equals(((Swap) other).Item);
				case Check self:
					return self.Item.Equals(((Check) other).Item);
				case DupIf self:
					return self.Item.Equals(((DupIf) other).Item);
				case Verify self:
					return self.Item.Equals(((Verify) other).Item);
				case NonZero self:
					return self.Item.Equals(((NonZero) other).Item);
				case ZeroNotEqual self:
					return self.Item.Equals(((ZeroNotEqual) other).Item);
				case AndV self:
					var andv = (AndV) other;
					return self.Item1.Equals(andv.Item1) && self.Item2.Equals(andv.Item2);
				case AndB self:
					var andb = (AndB) other;
					return self.Item1.Equals(andb.Item1) && self.Item2.Equals(andb.Item2);
				case AndOr self:
					var andOr = (AndOr) other;
					return self.Item1.Equals(andOr.Item1) && self.Item2.Equals(andOr.Item2) && self.Item3.Equals(andOr.Item3);
				case OrB self:
					var orb = (OrB) other;
					return self.Item1.Equals(orb.Item1) && self.Item2.Equals(orb.Item2);
				case OrD self:
					var ord = (OrD) other;
					return self.Item1.Equals(ord.Item1) && self.Item2.Equals(ord.Item2);
				case OrC self:
					var orc = (OrC) other;
					return self.Item1.Equals(orc.Item1) && self.Item2.Equals(orc.Item2);
				case OrI self:
					var ori = (OrI) other;
					return self.Item1.Equals(ori.Item1) && self.Item2.Equals(ori.Item2);
				case Thresh self:
					var t = (Thresh) other;
					return self.Item1.Equals(t.Item1) && self.Item2.SequenceEqual(t.Item2);
				case ThreshM self:
					var tm = (ThreshM) other;
					return self.Item1.Equals(tm.Item1) && self.Item2.SequenceEqual(tm.Item2);
			}
			throw new Exception("Unreachable!");
		}
		#endregion

		public Script ToScript() =>
			new Script(this.ToOpList());
		private List<Op> ToOpList()
		{
			var l = new List<Op>();
			switch (this.Tag)
			{
				case Tags.True:
					l.Add(OpcodeType.OP_TRUE);
					return l;
				case Tags.False:
					l.Add(OpcodeType.OP_FALSE);
					return l;
			}
			switch (this)
			{
				case Pk self:
					l.Add(Op.GetPushOp(self.Item.ToPublicKey().ToBytes()));
					return l;
				case PkH self:
					l.Add(OpcodeType.OP_DUP);
					l.Add(OpcodeType.OP_HASH160);
					l.Add(Op.GetPushOp(self.Item.ToHash160().ToBytes()));
					l.Add(OpcodeType.OP_EQUALVERIFY);
					return l;
				case After self:
					l.Add(Op.GetPushOp(self.Item));
					l.Add(OpcodeType.OP_CHECKLOCKTIMEVERIFY);
					return l;
				case Older self:
					l.Add(Op.GetPushOp(self.Item));
					l.Add(OpcodeType.OP_CHECKSEQUENCEVERIFY);
					return l;
				case Sha256 self:
					l.Add(OpcodeType.OP_SIZE);
					l.Add(Op.GetPushOp(32));
					l.Add(OpcodeType.OP_EQUALVERIFY);
					l.Add(OpcodeType.OP_SHA256);
					l.Add(Op.GetPushOp(self.Item.ToBytes()));
					l.Add(OpcodeType.OP_EQUAL);
					return l;
				case Hash256 self:
					l.Add(OpcodeType.OP_SIZE);
					l.Add(Op.GetPushOp(32));
					l.Add(OpcodeType.OP_EQUALVERIFY);
					l.Add(OpcodeType.OP_HASH256);
					l.Add(Op.GetPushOp(self.Item.ToBytes()));
					l.Add(OpcodeType.OP_EQUAL);
					return l;
				case Ripemd160 self:
					l.Add(OpcodeType.OP_SIZE);
					l.Add(Op.GetPushOp(20));
					l.Add(OpcodeType.OP_EQUALVERIFY);
					l.Add(OpcodeType.OP_RIPEMD160);
					l.Add(Op.GetPushOp(self.Item.ToBytes()));
					l.Add(OpcodeType.OP_EQUAL);
					return l;
				case Hash160 self:
					l.Add(OpcodeType.OP_SIZE);
					l.Add(Op.GetPushOp(20));
					l.Add(OpcodeType.OP_EQUALVERIFY);
					l.Add(OpcodeType.OP_HASH160);
					l.Add(Op.GetPushOp(self.Item.ToBytes()));
					l.Add(OpcodeType.OP_EQUAL);
					return l;
				case Alt self:
					l.Add(OpcodeType.OP_TOALTSTACK);
					l.AddRange(self.Item.Node.ToOpList());
					l.Add(OpcodeType.OP_FROMALTSTACK);
					return l;
				case Swap self:
					l.Add(OpcodeType.OP_SWAP);
					l.AddRange(self.Item.Node.ToOpList());
					return l;
				case Check self:
					l.AddRange(self.Item.Node.ToOpList());
					l.Add(OpcodeType.OP_CHECKSIG);
					return l;
				case DupIf self:
					l.Add(OpcodeType.OP_DUP);
					l.Add(OpcodeType.OP_IF);
					l.AddRange(self.Item.Node.ToOpList());
					l.Add(OpcodeType.OP_ENDIF);
					return l;
				case Verify self:
					l.AddRange(self.Item.Node.ToOpList());
					l.PushVerify();
					return l;
				case NonZero self:
					l.Add(OpcodeType.OP_SIZE);
					l.Add(OpcodeType.OP_0NOTEQUAL);
					l.Add(OpcodeType.OP_IF);
					l.AddRange((self.Item.Node.ToOpList()));
					l.Add(OpcodeType.OP_ENDIF);
					return l;
				case ZeroNotEqual self:
					l.AddRange(self.Item.Node.ToOpList());
					l.Add(OpcodeType.OP_0NOTEQUAL);
					return l;
				case AndV self:
					l.AddRange(self.Item1.Node.ToOpList());
					l.AddRange(self.Item2.Node.ToOpList());
					return l;
				case AndB self:
					l.AddRange(self.Item1.Node.ToOpList());
					l.AddRange(self.Item2.Node.ToOpList());
					l.Add(OpcodeType.OP_BOOLAND);
					return l;
				case AndOr self:
					l.AddRange(self.Item1.Node.ToOpList());
					l.Add(OpcodeType.OP_NOTIF);
					l.AddRange(self.Item3.Node.ToOpList());
					l.Add(OpcodeType.OP_ELSE);
					l.AddRange(self.Item2.Node.ToOpList());
					l.Add(OpcodeType.OP_ENDIF);
					return l;
				case OrB self:
					l.AddRange(self.Item1.Node.ToOpList());
					l.AddRange(self.Item2.Node.ToOpList());
					l.Add(OpcodeType.OP_BOOLOR);
					return l;
				case OrD self:
					l.AddRange(self.Item1.Node.ToOpList());
					l.Add(OpcodeType.OP_IFDUP);
					l.Add(OpcodeType.OP_NOTIF);
					l.AddRange(self.Item2.Node.ToOpList());
					l.Add(OpcodeType.OP_ENDIF);
					return l;
				case OrI self:
					l.Add(OpcodeType.OP_IF);
					l.AddRange(self.Item1.Node.ToOpList());
					l.Add(OpcodeType.OP_ELSE);
					l.AddRange(self.Item2.Node.ToOpList());
					l.Add(OpcodeType.OP_ENDIF);
					return l;
				case Thresh self:
					l.AddRange(self.Item2[0].Node.ToOpList());
					foreach (var sub in self.Item2.Skip(1))
					{
						l.AddRange(sub.Node.ToOpList());
						l.Add(OpcodeType.OP_ADD);
					}
					l.Add(Op.GetPushOp((long)self.Item1));
					l.Add(OpcodeType.OP_EQUAL);
					return l;
				case ThreshM self:
					l.Add(Op.GetPushOp((long)self.Item1));
					foreach (var sub in self.Item2)
					{
						l.Add(Op.GetPushOp(sub.ToPublicKey().ToBytes()));
					}
					l.Add(Op.GetPushOp(self.Item2.Length));
					l.Add(OpcodeType.OP_CHECKMULTISIG);
					return l;
			}
			throw new Exception(("Unreachable!"));
		}

		private int ScriptNumSize(int n) => ScriptNumSize((ulong)n);
		private int ScriptNumSize(ulong n)
		{
			if (n <= 0x10) // OP_n
				return 1;
			if (n < 0x10) // OP_PUSH1 <n>
				return 2;
			if (n < 0x8000) // OP_PUSH2 <n>
				return 3;
			if (n < 0x800000) // OP_PUSH3 <n>
				return 4;
			if (n < 0x80000000) // OP_PUSH4 <n>
				return 5;

			return 6; // OP_PUSH5 <n>
		}
		public int ScriptSize()
		{
			switch (this.Tag)
			{
				case Tags.True:
					return 1;
				case Tags.False:
					return 1;
			}

			switch (this)
			{
				case Pk self:
					return self.Item.SerializedLength();
				case PkH _:
					return 24;
				case After self:
					return ScriptNumSize(self.Item) + 1;
				case Older self:
					return ScriptNumSize(self.Item) + 1;
				case Sha256 _:
					return 33 + 6;
				case Hash256 _:
					return 33 + 6;
				case Ripemd160 _:
					return 21 + 6;
				case Hash160 _:
					return 21 + 6;
				case Alt self:
					return self.Item.Node.ScriptSize() + 2;
				case Swap self:
					return self.Item.Node.ScriptSize() + 1;
				case Check self:
					return self.Item.Node.ScriptSize() + 1;
				case DupIf self:
					return self.Item.Node.ScriptSize() + 3;
				case Verify self:
					return self.Item.Node.ScriptSize() + (self.Item.Ext.HasVerifyForm ? 0 : 1);
				case NonZero self:
					return self.Item.Node.ScriptSize() + 4;
				case ZeroNotEqual self:
					return self.Item.Node.ScriptSize() + 1;
				case AndV self:
					return self.Item1.Node.ScriptSize() + self.Item2.Node.ScriptSize();
				case AndB self:
					return self.Item1.Node.ScriptSize() + self.Item2.Node.ScriptSize() + 1;
				case AndOr self:
					return
						self.Item1.Node.ScriptSize() +
						self.Item2.Node.ScriptSize() +
						self.Item3.Node.ScriptSize() + 3;
				case OrB self:
					return
						self.Item1.Node.ScriptSize() + self.Item2.Node.ScriptSize() + 1;
				case OrD self:
					return
						self.Item1.Node.ScriptSize() + self.Item2.Node.ScriptSize() + 3;
				case OrC self:
					return
						self.Item1.Node.ScriptSize() + self.Item2.Node.ScriptSize() + 2;
				case OrI self:
					return
						self.Item1.Node.ScriptSize() + self.Item2.Node.ScriptSize() + 3;
				case Thresh self:
					Debug.Assert(self.Item2.Length != 0);
					return
						ScriptNumSize(self.Item1) + // k
						1 + // EQUAL
						self.Item2.Select(s => s.Node.ScriptSize()).Sum() +
						self.Item2.Length // ADD
                        - 1; // no ADD on first element
				case ThreshM self:
					return
						ScriptNumSize(self.Item1) +
						1 +
						ScriptNumSize(self.Item2.Length) +
						self.Item2.Select(x => x.SerializedLength()).Sum();
			}

			throw new Exception("Unreachable!");
		}

		public int? MaxDissatisfactionWitnessElements()
		{
			switch (this.Tag)
			{
				case (Tags.False):
					return 0;
			}
			switch (this)
			{
				case Pk _:
					return 1;
				case PkH _:
					return 2;
				case Alt self:
					return self.Item.Node.MaxDissatisfactionWitnessElements();
				case Swap self:
					return self.Item.Node.MaxDissatisfactionWitnessElements();
				case Check self:
					return self.Item.Node.MaxDissatisfactionWitnessElements();
				case DupIf _:
					return 1;
				case NonZero _:
					return 1;
				case AndB self:
					return
						self.Item1.Node.MaxDissatisfactionWitnessElements() +
						self.Item2.Node.MaxDissatisfactionWitnessElements();
				case AndOr self:
					return
						self.Item1.Node.MaxDissatisfactionWitnessElements() +
						self.Item3.Node.MaxDissatisfactionWitnessElements();
				case OrB self:
					return
						self.Item1.Node.MaxDissatisfactionWitnessElements() +
						self.Item2.Node.MaxDissatisfactionWitnessElements();
				case OrI self:
					var l = self.Item1.Node.MaxDissatisfactionWitnessElements();
					var r = self.Item2.Node.MaxDissatisfactionWitnessElements();
					if (!l.HasValue && r.HasValue)
						return 1 + r.Value;
					else if (l.HasValue && !r.HasValue)
						return 1 + l.Value;
					else if (!(l.HasValue) && (!r.HasValue))
						return null;
					throw new Exception($"tried to dissatisfy or_i with both branches being dissatisfiable");
				case Thresh self:
					var sum = 0;
					foreach (var sub in self.Item2)
					{
						var s = sub.Node.MaxDissatisfactionWitnessElements();
						if (s.HasValue)
							sum += s.Value;
						else
							return null;
					}
					return sum;
				case ThreshM self:
					return 1 + (int)self.Item1;
			}

			return null;
		}

		public int? MaxDissatisfactionSize(int oneCost)
		{
			switch (this.Tag)
			{
				case (Tags.False):
					return 0;
			}

			switch (this)
			{
				case Pk _:
					return 1;
				case PkH _:
					return 35;
				case Alt self:
					return self.Item.Node.MaxDissatisfactionSize(oneCost);
				case Swap self:
					return self.Item.Node.MaxDissatisfactionSize(oneCost);
				case Check self:
					return self.Item.Node.MaxDissatisfactionSize(oneCost);
				case DupIf _:
					return 1;
				case NonZero _:
					return 1;
				case AndB self:
					return
						self.Item1.Node.MaxDissatisfactionSize(oneCost) +
						self.Item2.Node.MaxDissatisfactionSize(oneCost);
				case AndOr self:
					return
						self.Item1.Node.MaxDissatisfactionSize(oneCost) +
						self.Item3.Node.MaxDissatisfactionSize(oneCost);
				case OrB self:
					return
						self.Item1.Node.MaxDissatisfactionSize(oneCost) +
						self.Item2.Node.MaxDissatisfactionSize(oneCost);
				case OrD self:
					return
						self.Item1.Node.MaxDissatisfactionSize(oneCost) +
						self.Item2.Node.MaxDissatisfactionSize(oneCost);
				case OrI self:
					var l = self.Item1.Node.MaxDissatisfactionSize(oneCost);
					var r = self.Item2.Node.MaxDissatisfactionSize(oneCost);
					if (!l.HasValue && r.HasValue)
						return 1 + r.Value;
					else if (l.HasValue && !r.HasValue)
						return oneCost + l.Value;
					else if (!(l.HasValue) && (!r.HasValue))
						return null;
					throw new Exception($"tried to dissatisfy or_i with both branches being dissatisfiable");

				case Thresh self:
					var sum = 0;
					foreach (var sub in self.Item2)
					{
						var s = sub.Node.MaxDissatisfactionSize(oneCost);
						if (s.HasValue)
							sum += s.Value;
						else
							return null;
					}
					return sum;
				case ThreshM self:
					return 1 + (int)self.Item1;
			}

			return null;
		}

		public int MaxSatisfactionWitnessElements()
		{
			switch (this.Tag)
			{
				case Tags.True: return 0;
				case Tags.False: return 0;
			}

			switch (this)
			{
				case Pk _:
					return 1;
				case PkH _: return 2;
				case After _: return 0;
				case Older _: return 0;
				case Sha256 _: return 1;
				case Hash256 _: return 1;
				case Ripemd160 _: return 1;
				case Hash160 _: return 1;
				case Alt self:
					return self.Item.Node.MaxSatisfactionWitnessElements();
				case Swap self:
					return self.Item.Node.MaxSatisfactionWitnessElements();
				case Check self:
					return self.Item.Node.MaxSatisfactionWitnessElements();
				case DupIf self:
					return 1 + self.Item.Node.MaxSatisfactionWitnessElements();
				case Verify self:
					return self.Item.Node.MaxSatisfactionWitnessElements();
				case NonZero self:
					return self.Item.Node.MaxSatisfactionWitnessElements();
				case ZeroNotEqual self:
					return self.Item.Node.MaxSatisfactionWitnessElements();
				case AndV self:
					return
						self.Item1.Node.MaxSatisfactionWitnessElements() +
						self.Item2.Node.MaxSatisfactionWitnessElements();
				case AndB self:
					return
						self.Item1.Node.MaxSatisfactionWitnessElements() +
						self.Item2.Node.MaxSatisfactionWitnessElements();
				case AndOr self:
					var aSat = self.Item1.Node.MaxSatisfactionWitnessElements();
					var aDissat = self.Item1.Node.MaxDissatisfactionWitnessElements();
					return
						Math.Max(
							(aSat + self.Item3.Node.MaxSatisfactionWitnessElements()),
							 aDissat.Value + self.Item2.Node.MaxSatisfactionWitnessElements());
				case OrB self:
					return
						Math.Max(
							(self.Item1.Node.MaxSatisfactionWitnessElements() +
							 self.Item2.Node.MaxDissatisfactionWitnessElements().Value),
							(self.Item1.Node.MaxDissatisfactionWitnessElements().Value +
							 self.Item2.Node.MaxSatisfactionWitnessElements())
						);
				case OrD self:
					return
						Math.Max(
							self.Item1.Node.MaxSatisfactionWitnessElements(),
							self.Item1.Node.MaxDissatisfactionWitnessElements().Value + self.Item2.Node.MaxSatisfactionWitnessElements()
							);
				case OrC self:
					return
						Math.Max(
							self.Item1.Node.MaxSatisfactionWitnessElements(),
							self.Item1.Node.MaxDissatisfactionWitnessElements().Value + self.Item2.Node.MaxSatisfactionWitnessElements()
							);
				case OrI self:
					return
						1 + Math.Max(
							self.Item1.Node.MaxSatisfactionWitnessElements(),
							self.Item2.Node.MaxSatisfactionWitnessElements());
				case Thresh self:
					return
						self.Item2
							.Select(sub =>
								Tuple.Create(sub.Node.MaxSatisfactionWitnessElements(),
									sub.Node.MaxDissatisfactionWitnessElements().Value)
							)
							.OrderBy(t => t.Item1 - t.Item2)
							.Reverse()
							.Select((t, i) => i < self.Item1 ? t.Item1 : t.Item2)
							.Sum();
						;
				case ThreshM self: return 1 + (int)self.Item1;
			}

			throw new Exception("Unreachable!");
		}

		/// <summary>
		/// Maximum size, in bytes, of a satisfying witness.
		/// </summary>
		/// <returns></returns>
		public int MaxSatisfactionSize(int oneCost)
		{
			switch (Tag)
			{
				case Tags.True: return 0;
				case Tags.False: return 0;
			}

			switch (this)
			{
				case Pk _: return 73;
				case PkH _: return 34 + 73;
				case After _: return 0;
				case Older _: return 0;
				case Sha256 _: return 33;
				case Hash256 _: return 33;
				case Ripemd160 _: return 33;
				case Hash160 _: return 33;
				case Alt self: return self.Item.Node.MaxSatisfactionSize(oneCost);
				case Swap self: return self.Item.Node.MaxSatisfactionSize(oneCost);
				case Check self: return self.Item.Node.MaxSatisfactionSize(oneCost);
				case DupIf self: return oneCost + self.Item.Node.MaxSatisfactionSize(oneCost);
				case Verify self: return self.Item.Node.MaxSatisfactionSize(oneCost);
				case NonZero self: return self.Item.Node.MaxSatisfactionSize(oneCost);
				case ZeroNotEqual self: return self.Item.Node.MaxSatisfactionSize(oneCost);
				case AndV self:
					return self.Item1.Node.MaxSatisfactionSize(oneCost) + self.Item2.Node.MaxSatisfactionSize(oneCost);
				case AndB self:
					return self.Item1.Node.MaxSatisfactionSize(oneCost) + self.Item2.Node.MaxSatisfactionSize(oneCost);
				case AndOr self:
					return
						Math.Max(
							self.Item1.Node.MaxSatisfactionSize(oneCost) + self.Item3.Node.MaxSatisfactionSize(oneCost),
							self.Item1.Node.MaxDissatisfactionSize(oneCost).Value +
							self.Item2.Node.MaxSatisfactionSize(oneCost)
						);
				case OrB self:
					return
						Math.Max(
							self.Item1.Node.MaxSatisfactionSize(oneCost) +
							self.Item2.Node.MaxDissatisfactionSize(oneCost).Value,
							self.Item1.Node.MaxDissatisfactionSize(oneCost).Value +
							self.Item2.Node.MaxSatisfactionSize(oneCost)
						);
				case OrD self:
					return
						Math.Max(
							self.Item1.Node.MaxSatisfactionSize(oneCost),
							self.Item1.Node.MaxDissatisfactionSize(oneCost).Value +
							self.Item2.Node.MaxSatisfactionSize(oneCost)
						);
				case OrI self:
					return
						Math.Max(
							oneCost + self.Item1.Node.MaxSatisfactionSize(oneCost),
							1 + self.Item2.Node.MaxSatisfactionSize(oneCost)
						);
				case Thresh self:
					return
						self.Item2
							.Select(sub =>
								new {
									Sat = sub.Node.MaxSatisfactionSize(oneCost),
									Dissat = sub.Node.MaxDissatisfactionSize(oneCost).Value
								})
							.OrderBy(v => v.Sat - v.Dissat)
							.Reverse()
							.Select((v, i) => i < self.Item1 ? v.Sat : v.Dissat)
							.Sum();
				case ThreshM self : return 1 + 73 + (int)self.Item1;
			}
			throw new Exception("unreachable!");
		}
	}

}
