﻿namespace TesteSoft.Taxa.Domain.Entities
{
    public class TaxaJuros
    {
        public static readonly double taxaAtual = 0.01;
        public TaxaJuros()
        {
            TaxadeJuros = taxaAtual;
        }

        public double TaxadeJuros { get; private set; }
    }
}
