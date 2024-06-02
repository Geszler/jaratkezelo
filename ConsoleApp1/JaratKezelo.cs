using System;
using System.Collections.Generic;

namespace JaratKezeloProject
{
    public class JaratKezelo
    {
        private class Jarat
        {
            public string JaratSzam { get; }
            public string RepterHonnan { get; }
            public string RepterHova { get; }
            public DateTime Indulas { get; }
            public TimeSpan Keses { get; set; }

            public Jarat(string jaratSzam, string repterHonnan, string repterHova, DateTime indulas)
            {
                JaratSzam = jaratSzam;
                RepterHonnan = repterHonnan;
                RepterHova = repterHova;
                Indulas = indulas;
                Keses = TimeSpan.Zero;
            }
        }

        private readonly Dictionary<string, Jarat> _jaratok = new Dictionary<string, Jarat>();

        public void UjJarat(string jaratSzam, string repterHonnan, string repterHova, DateTime indulas)
        {
            if (_jaratok.ContainsKey(jaratSzam))
            {
                throw new ArgumentException("A járatszám már létezik.");
            }
            _jaratok[jaratSzam] = new Jarat(jaratSzam, repterHonnan, repterHova, indulas);
        }

        public void Keses(string jaratSzam, int keses)
        {
            if (!_jaratok.ContainsKey(jaratSzam))
            {
                throw new ArgumentException("A járat nem létezik.");
            }
            var jarat = _jaratok[jaratSzam];
            var ujKeses = jarat.Keses + TimeSpan.FromMinutes(keses);
            if (ujKeses < TimeSpan.Zero)
            {
                throw new ArgumentException("A késés nem lehet negatív.");
            }
            jarat.Keses = ujKeses;
        }

        public DateTime MikorIndul(string jaratSzam)
        {
            if (!_jaratok.ContainsKey(jaratSzam))
            {
                throw new ArgumentException("A járat nem létezik.");
            }
            var jarat = _jaratok[jaratSzam];
            return jarat.Indulas + jarat.Keses;
        }

        public List<string> JaratokRepuloterrol(string repter)
        {
            var eredmeny = new List<string>();
            foreach (var jarat in _jaratok.Values)
            {
                if (jarat.RepterHonnan == repter)
                {
                    eredmeny.Add(jarat.JaratSzam);
                }
            }
            return eredmeny;
        }
    }
}
