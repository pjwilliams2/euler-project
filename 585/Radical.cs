using System;

namespace Euler585
{
    class Radical
    {
        public int Coefficient { get; set; }
        public int Radicand { get; set; }

        public Radical(int radicand) : this(1, radicand)
        {

        }

        public Radical(int coeff, int radicand)
        {
            Coefficient = coeff;
            Radicand = radicand;
        }

        public bool CanBeAddedTo(Radical other)
        {
            return this.Radicand == other.Radicand;
        }

        public static Radical operator+(Radical other)
        {
            return null;
        }
    }
}