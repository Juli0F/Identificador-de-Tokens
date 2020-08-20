using System;
using System.Text.RegularExpressions;

namespace PracticaCorta.Identify
{
    public class Identify
    {
        private String statement;
        private System.Collections.Generic.List<String> palabras;
        private System.Collections.Generic.List<String> decimales;
        private System.Collections.Generic.List<String> enteros;
        private System.Collections.Generic.List<String> monedas;

        public Identify(String statement)
        {
            this.statement = statement;
            this.palabras = new System.Collections.Generic.List<string>();
            this.decimales = new System.Collections.Generic.List<string>();
            this.enteros = new System.Collections.Generic.List<string>();
            this.monedas = new System.Collections.Generic.List<string>();

        }
        public void applySplit()
        {
            String[] tokens = this.statement.Split(' ');
            foreach(var token in tokens)
            {
                initRecognize(token);
            }
        }
        public void initRecognize(String token)
        {
          
                Regex entero = new Regex("[0-9]");
                Regex tipoDecimal = new Regex("[0-9].[0-9]");
                Regex palabra = new Regex("[a-z]");
                Regex moneda = new Regex("[Q.]");

                bool isDecimal = tipoDecimal.IsMatch(token);
                bool isEntero = entero.IsMatch(token);
                bool isPalabra = palabra.IsMatch(token);
                bool isMoneda = moneda.IsMatch(token);

                if (isDecimal)
                {
                    this.decimales.Add(token);
                    Console.WriteLine("token Decimal: " + token);
                }else if (isEntero)
                {
                    this.enteros.Add(token);
                    Console.WriteLine("Token Numerico: " + token);
                }

                else if (isPalabra)
                {
                    this.palabras.Add(token);
                    Console.WriteLine("token Palabra: " + token);
                }
                else if (isMoneda)
                {
                    this.monedas.Add(token);
                    Console.WriteLine("token Moneda: " + token);
                }
        }

        public System.Collections.Generic.List<String> GetListEnteros() { return enteros; }
        public System.Collections.Generic.List<String> GetListDecimaless() { return decimales; }
        public System.Collections.Generic.List<String> GetListPalabras() { return palabras; }
        public System.Collections.Generic.List<String> GetListMoneda() { return monedas; }
    }
}
