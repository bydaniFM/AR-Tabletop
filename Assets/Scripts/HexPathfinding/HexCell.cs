using System;
using System.Collections.Generic;
using UnityEngine;

namespace HexMap{
	public struct HexCell { //it's p-toointy tpped

		public readonly int q; // upper right axis or cubic "x" or column
		public readonly int r; // down axis or cubic "z" or row
		public readonly int s; // down right axis??
		//public readonly int h;// cell height over "ground"

		public HexCell(int q, int r, int s){
			this.q = q;
			this.r = r;
			this.s = s;
		}
		public HexCell(int q, int r){
			this.q = q;
			this.r = r;
			this.s = -q - r;
		}

		static int HexLength(HexCell hex){
			return (int)((Mathf.Abs(hex.q)+Mathf.Abs(hex.r)+Mathf.Abs(hex.s))/2);
		}

		public static int Distance(HexCell a, HexCell b){
			return HexLength(a-b);
		}

		HexCell Direction (int dir){
			return directions[dir];
		}

		/*public static HexCell GetNeighbor(HexCell hex, int dir){
			return hex + directions[dir];
		}*/

		public HexCell Neighbor(int dir){
			return this + directions[dir];
		}




		// +, -, ==, !=
		public static HexCell operator + (HexCell a, HexCell b){ 
			return new HexCell(a.q+b.q, a.r+b.r, a.s+b.s); 
		}
		public static HexCell operator - (HexCell a, HexCell b){ 
			return new HexCell(a.q-b.q, a.r-b.r, a.s-b.s); 
		}
		public static HexCell operator - (HexCell a, int k){ 
			return new HexCell(a.q*k, a.r*k, a.s*k); 
		}
		public static bool operator == (HexCell a, HexCell b){
			return a.q == b.q && a.r == b.r && a.s == b.s;
		}
		public static bool operator != (HexCell a, HexCell b){
			return !(a == b);
		}
		public override bool Equals(object o){ 
			return (o is HexCell) && this == (HexCell)o; 
		}
		public override int GetHashCode(){
			return q & (int)0xFFFF | r<<16; // I have no idea what's happening here (>囗＜?)
		}

		static readonly HexCell[] directions = {

			new HexCell(0, 1, -1), 
			new HexCell(1, 0, -1),
			new HexCell(1, -1, 0),
			new HexCell(0, -1, 1),
			new HexCell(-1, 0, 1),
			new HexCell(-1, 1, 0)
		};
	}


	struct FractionalHex
	{
	    public FractionalHex(double q, double r, double s)
	    {
	        this.q = q;
	        this.r = r;
	        this.s = s;
	    }
	    public readonly double q;
	    public readonly double r;
	    public readonly double s;

	    static public HexCell HexRound(FractionalHex h)
	    {
	        int q = (int)(Math.Round(h.q));
	        int r = (int)(Math.Round(h.r));
	        int s = (int)(Math.Round(h.s));
	        double q_diff = Math.Abs(q - h.q);
	        double r_diff = Math.Abs(r - h.r);
	        double s_diff = Math.Abs(s - h.s);
	        if (q_diff > r_diff && q_diff > s_diff)
	        {
	            q = -r - s;
	        }
	        else
	            if (r_diff > s_diff)
	            {
	                r = -q - s;
	            }
	            else
	            {
	                s = -q - r;
	            }
	        return new HexCell(q, r, s);
	    }


	    static public FractionalHex HexLerp(FractionalHex a, FractionalHex b, double t)
	    {
	        return new FractionalHex(a.q * (1 - t) + b.q * t, a.r * (1 - t) + b.r * t, a.s * (1 - t) + b.s * t);
	    }


	    static public List<HexCell> HexLinedraw(HexCell a, HexCell b)
	    {
	        int N = HexCell.Distance(a, b);
	        FractionalHex a_nudge = new FractionalHex(a.q + 0.000001, a.r + 0.000001, a.s - 0.000002);
	        FractionalHex b_nudge = new FractionalHex(b.q + 0.000001, b.r + 0.000001, b.s - 0.000002);
	        List<HexCell> results = new List<HexCell>{};
	        double step = 1.0 / Math.Max(N, 1);
	        for (int i = 0; i <= N; i++)
	        {
	            results.Add(FractionalHex.HexRound(FractionalHex.HexLerp(a_nudge, b_nudge, step * i)));
	        }
	        return results;
	    }

	}
}