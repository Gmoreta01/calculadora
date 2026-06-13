using System;

namespace CalculadoraEstudiantil
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            bool salir = false;

            Console.WriteLine("=== CALCULADORA ESTUDIANTIL ===");

            while (!salir)
            {
                Console.WriteLine("\n-- Menu Principal --");
                Console.WriteLine("1) Calculadora");
                Console.WriteLine("2) Notas");
                Console.WriteLine("3) Salir");
                Console.Write("Opcion: ");

                string op = Console.ReadLine();

                if (op == "1")
                    MenuCalculadora();
                else if (op == "2")
                    MenuNotas();
                else if (op == "3")
                {
                    salir = true;
                    Console.WriteLine("Hasta luego.");
                }
                else
                    Console.WriteLine("Opcion incorrecta, intente de nuevo.");
            }
        }

        static void MenuCalculadora()
        {
            bool volver = false;

            while (!volver)
            {
                Console.WriteLine("\n-- Calculadora --");
                Console.WriteLine("1) Suma");
                Console.WriteLine("2) Resta");
                Console.WriteLine("3) Multiplicacion");
                Console.WriteLine("4) Division");
                Console.WriteLine("5) Volver");
                Console.Write("Que desea hacer: ");

                string op = Console.ReadLine();

                if (op == "5")
                {
                    volver = true;
                    continue;
                }

                if (op != "1" && op != "2" && op != "3" && op != "4")
                {
                    Console.WriteLine("Eso no es valido.");
                    continue;
                }

                Console.Write("Primer numero: ");
                string e1 = Console.ReadLine();
                double n1;
                if (!double.TryParse(e1, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out n1))
                {
                    Console.WriteLine("Eso no es un numero.");
                    continue;
                }

                Console.Write("Segundo numero: ");
                string e2 = Console.ReadLine();
                double n2;
                if (!double.TryParse(e2, System.Globalization.NumberStyles.Any,
                    System.Globalization.CultureInfo.InvariantCulture, out n2))
                {
                    Console.WriteLine("Eso no es un numero.");
                    continue;
                }

                if (op == "4" && n2 == 0)
                {
                    Console.WriteLine("No se puede dividir entre cero.");
                    continue;
                }

                double res = Operar(n1, n2, op);
                string[] syms = { "", "+", "-", "*", "/" };
                Console.WriteLine("Resultado: " + n1 + " " + syms[int.Parse(op)] + " " + n2 + " = " + Math.Round(res, 10));

                Console.Write("Otra operacion? (s/n): ");
                if (Console.ReadLine().Trim().ToLower() != "s")
                    volver = true;
            }
        }

        static double Operar(double a, double b, string op)
        {
            if (op == "1") return a + b;
            if (op == "2") return a - b;
            if (op == "3") return a * b;
            if (op == "4") return a / b;
            return 0;
        }

        static void MenuNotas()
        {
            bool volver = false;

            while (!volver)
            {
                Console.WriteLine("\n-- Evaluador de Notas --");
                Console.WriteLine("Minimo para aprobar: 70");

                Console.Write("Nombre del estudiante: ");
                string nom = Console.ReadLine().Trim();
                if (nom == "") nom = "Estudiante";

                Console.Write("Cuantas notas va a ingresar: ");
                string cantStr = Console.ReadLine();
                int cant;
                if (!int.TryParse(cantStr, out cant) || cant <= 0)
                {
                    Console.WriteLine("Numero de notas invalido.");
                    continue;
                }

                double[] notas = new double[cant];
                bool ok = true;

                for (int i = 0; i < cant; i++)
                {
                    Console.Write("Nota " + (i + 1) + " (0-100): ");
                    string entN = Console.ReadLine();
                    double n;
                    if (!double.TryParse(entN, System.Globalization.NumberStyles.Any,
                        System.Globalization.CultureInfo.InvariantCulture, out n))
                    {
                        Console.WriteLine("\"" + entN + "\" no es un numero valido.");
                        ok = false;
                        break;
                    }
                    if (n < 0 || n > 100)
                    {
                        Console.WriteLine("La nota debe estar entre 0 y 100.");
                        ok = false;
                        break;
                    }
                    notas[i] = n;
                }

                if (!ok) continue;

                double suma = 0;
                for (int i = 0; i < cant; i++)
                    suma = Operar(suma, notas[i], "1");

                double prom = Operar(suma, cant, "4");

                Console.WriteLine("\n>>> Resultado de " + nom);
                Console.WriteLine("Notas: " + string.Join(", ", notas));
                Console.WriteLine("Promedio: " + prom.ToString("F2"));
                Console.WriteLine("Calificacion: " + Calificacion(prom));
                Console.WriteLine("Estado: " + (prom >= 70 ? "APROBADO" : "REPROBADO"));

                Console.Write("Evaluar otro estudiante? (s/n): ");
                if (Console.ReadLine().Trim().ToLower() != "s")
                    volver = true;
            }
        }

        static string Calificacion(double n)
        {
            if (n >= 90) return "Excelente";
            if (n >= 80) return "Muy Bueno";
            if (n >= 70) return "Bueno";
            if (n >= 60) return "Suficiente";
            if (n >= 50) return "Casi Suficiente";
            if (n >= 40) return "Deficiente";
            return "Muy Deficiente";
        }
    }
}