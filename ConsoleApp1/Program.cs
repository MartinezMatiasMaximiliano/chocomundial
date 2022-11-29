using System;
using System.Text.RegularExpressions;

namespace ConsoleApp1
{
    internal class Program
    {

        static void Main(string[] args)
        {
            helpers hp = new helpers();
           
            int idPersona = 2;

            string CopiadoExcel = "Qatar 1-2 Ecuador	Senegal 0-2 Países bajos	Qatar 0-1 senegal	Ecuador 1-2 Paise bajos	Qatar 1-3 países bajos	Ecuador 1-0 senegal	Inglaterra 4-0 iran	Estados unidos 1-0 Gales	Inglaterra 2-0 Estados unidos	Iran 0-1 Gales	Inglaterra 1-1 Gales	Iran 0-1 Estados unidos	Argentina 3-0 Arabia Saudita	Mexico 2-1 Polonia	Argentina 2-0 Mexico	Arabia Saudita 0-2 Polonia	Polonia 1-2 Argentina	Arabia Saudita 0-1 Mexico	Francia 2-0 Australia	Dinamarca 3-0 Túnez	Francia 1-2 Dinamarca	Australia 1-1 Túnez	Francia 2-0 Tunez	Australia 1-2 Dinamarca	España 2-0 Costa rica	Alemania 3-0 Japón	España 2-1 Alemania	Costa rica 1-1 japon	España 1-0 japon	Costa rica 0-2 Alemania	Bélgica 2-0 canada	Marruecos 0-3 Croacia	Bélgica 4-1 Marruecos	Canada 2-2 Croacia	Bélgica 2-1 Croacia	Canada 1-0 Marruecos	Brasil 3-1 Serbia	Suiza 2-1 Camerún	Brasil 2-0 Suiza	Serbia 0-2 Camerún	Brasil 3-1 Camerún	Serbia 1-2 Suiza	Portugal 2-0 Ghana	Uruguay 2-1 Corea del sur	Portugal 2-1 Uruguay	Ghana 1-1 Corea del sur	Portugal 2-0 Corea del sur	Ghana 0-2 Uruguay";

            string[] separado = CopiadoExcel.Split("	");
            string[] duplas = new string[48];

            for (int i = 0; i < separado.Length; i++)
            {
                duplas[i] = Regex.Replace(separado[i], "[^0-9]", string.Empty);
                duplas[i] = Regex.Replace(duplas[i], @"(?<=\d)(?<=\d)", ",");
                duplas[i] = duplas[i].Remove(duplas[i].Length - 1);
            }


            string ConsultaSQL = hp.CrearSQL(duplas, idPersona);

            ConsultaSQL = ConsultaSQL.Remove(ConsultaSQL.Length - 1);

            Console.WriteLine(ConsultaSQL);

            bool stop = true;

        }

    }


    class helpers
    {

        public string ArreglarScore(string cadena)
        {
            char[] subarreglo = cadena.ToCharArray();
            string arreglado = subarreglo[2] + "," + subarreglo[0];
            return arreglado;
        }


        public string CrearSQL(string[] res, int idPersona)
        {
            int[] partidos = { 1, 3, 18, 19, 33, 34, 2, 4, 20, 17, 36, 35, 5, 7, 24, 22, 40, 39, 8, 6, 23, 21, 38, 37, 11, 10, 28, 25, 47, 48, 12, 9, 26, 27, 42, 41, 16, 13, 31, 29, 45, 46, 15, 14, 32, 30, 43, 44 };

            string SQL = "INSERT INTO Personas VALUES ";

            for (int i = 0; i < res.Length; i++)
            {
                if (partidos[i]==19 
                    || partidos[i] == 33 
                    || partidos[i] == 17 
                    || partidos[i] == 36 
                    || partidos[i] == 22 
                    || partidos[i] == 21 
                    || partidos[i] == 38 
                    || partidos[i] == 25 
                    || partidos[i] == 47 
                    || partidos[i] == 27 
                    || partidos[i] == 42 
                    || partidos[i] == 29 
                    || partidos[i] == 45 
                    || partidos[i] == 30 
                    || partidos[i] == 43)
                {
                    string corregido = ArreglarScore(res[i]);
                    SQL = SQL + $"({idPersona},{partidos[i]},{corregido}),";
                }
                else
                {
                    SQL = SQL + $"({idPersona},{partidos[i]},{res[i]}),";
                }
            }

            return SQL;
        }
    }
}
