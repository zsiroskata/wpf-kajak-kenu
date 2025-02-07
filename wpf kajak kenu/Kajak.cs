﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation;

namespace wpf_kajak_kenu
{
    class Kajak
    {
        public string Nev { get; set; }
        public int Azonosito { get; set; }
        public string Tipus { get; set; }
        public int SzemelyekSzama { get; set; }
        public int ElvitelOraja { get; set; }
        public int ElvitelPerce { get; set; }
        public int VisszahozatalOraja { get; set; }
        public int VisszahozatalPerc { get; set; }

        public Kajak(string sor)
        {
            var s = sor.Split(";");
            Nev = s[0];
            Azonosito = int.Parse(s[1]);
            Tipus = s[2];
            SzemelyekSzama = int.Parse(s[3]);
            ElvitelOraja = int.Parse(s[4]);
            ElvitelPerce = int.Parse(s[5]);
            VisszahozatalOraja = int.Parse(s[6]);
            VisszahozatalPerc = int.Parse(s[7]);
        }

        public bool Viz(int aktOra, int aktPerc)
        {
            int aktPercekben = aktOra * 60 + aktPerc;
            int elvitelPercekben = ElvitelOraja * 60 + ElvitelPerce;
            int visszahozatalPercekben = VisszahozatalOraja * 60 + VisszahozatalPerc;
            return aktPercekben >= elvitelPercekben + 1 && aktPercekben <= visszahozatalPercekben;
        }
        public int Kolcsonzes()
        {
            int elvitelPercekben = ElvitelOraja * 60 + ElvitelPerce;
            int visszahozatalPercekben = VisszahozatalOraja * 60 + VisszahozatalPerc;
            return visszahozatalPercekben - elvitelPercekben;
        }
        public int FelOrakSzama()
        {
            int percek = Kolcsonzes();
            return (percek + 29) / 30;
        }

        public override string ToString()
        {
            return $"{Azonosito}\t{Kolcsonzes()} perc\t{FelOrakSzama()} félóra";
        }
        public bool ErvenyesHajo(List<Kajak> hajok)
        {
            return hajok.Any(x => x.Azonosito == this.Azonosito && x.Tipus == this.Tipus && x.SzemelyekSzama == this.SzemelyekSzama);
        }

        

    }
}
