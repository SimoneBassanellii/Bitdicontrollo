using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace bitdiparità
{


    class Program
    {
        static void Stampamatrice(int[,] M, string[] element)
        {
            int a = 2;

            int b = 6;


            Console.ForegroundColor = ConsoleColor.White;
            //stampa dela matrice 
            for (int i = 0; i < element.Length; i++)
            {

                Console.SetCursorPosition(0, a);
                Console.WriteLine(element[i]);

                for (int x = 0; x < element.Length; x++)
                {


                    Console.SetCursorPosition(b, a);
                    Console.WriteLine(M[i, x]);

                    b++;
                    b++;
                }

                b = 6;

                a++;
                a++;
            }
        }

        static void stampaMatricerrori(int[,] M, int r, int c, bool[,] errori)
        {
            int B = 2;

            for (int i = 0; i < r; i++)
            {
                int A = 6;

                for (int j = 0; j < c; j++)
                {

                    if (errori[i, j] == true)
                    {
                        Console.SetCursorPosition(A, B);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write(M[i, j]);
                    }
                    else

                        Console.ForegroundColor = ConsoleColor.White;
                    Console.SetCursorPosition(A, B);
                    Console.Write(M[i, j]);
                    A++;
                    A++;
                }
                B++;
                B++;

            }

            Console.ForegroundColor = ConsoleColor.White;
        }


        public static void Main(string[] args)
        {
            string[] ele = new string[7];
            int[,] matrice = new int[8, 8];
            int[] orizzontale = new int[7];
            int[] verticale = new int[7];
            int[] orizzontale2 = new int[7];
            int[] verticale2 = new int[7];

            //acqusizione parola
            Acqusizioneparola(ele);

            //Conversione in codice ASCII
            ASCII(ele);

            //Generazione matrice
            Generazionematrice(matrice, ele);

            //Stampa della matrice
            Stampamatrice(matrice, ele);

            //Stampa dei bit di parità
            Bitdiparità(matrice, orizzontale, verticale);

            //Generazione matrice contenente gli k 
            bool[,] errors = new bool[8, 8];

            //Generazione degli k
            Errori(matrice, errors);

            //stampa della nuova matrice contenete gli k
            stampaMatricerrori(matrice, 7, 7, errors);

            //Stampa dei bit di parità sulla matrice con gli k 
            Paritàerrori(matrice, orizzontale, verticale, orizzontale2, verticale2);

            Console.SetCursorPosition(32, 10);
            Console.WriteLine("Tutti gli k correggibili sono stati corretti");

            //Correggo gli k che sono correggibili
            CorrError(matrice, orizzontale, verticale, orizzontale2, verticale2);

            Console.WriteLine("");
        }






        //Riempimento array inserendo i caratteri dopo averli convertiti da decimale d ascii
        public static void Acqusizioneparola(string[] arr)
        {
            string lett;


            //Richiesta della parola in caso sia troppo corta 
            do
            {
                Console.Clear();
                //Chiedo all'utente la parola che vuole inviare
                Console.Write("Inserisci la parola di 7 caratteri: ");
                lett = Console.ReadLine();

            } while (lett.Length < 6 || lett.Length > 7);

            for (int j = 0; j < arr.Length; j++)
            {

                //Associo ogni lettera della parola a un carattere
                char a = lett[j];

                //converto da carattere a stringa
                string f = a.ToString();

                //Associo a ogni posizione dell'array un unica lettera della parola dichiarata dall'utente
                arr[j] = f;
            }
        }

        
        //Converte in ASCII
       
        public static void ASCII(string[] arr2)
        {
            // variabile da aumentare
            int j = 0;

            //sostituzione delle lettere con il loro valore in ASCII
            for (int i = 0; i < arr2.Length; i++)
            {

                string str = arr2[i];

                //Trovo il valore ASCII delle lettere
                foreach (var c in str)
                {
                    int g = (int)c;
                    string f = g.ToString();
                    //Sostituisco ogni lettera con il codice in ascii
                }
                j++;
            }
        }

        public static void Generazionematrice(int[,] mat, string[] cod)
        {

            for (int g = 0; g < cod.Length; g++)
            {
                string a;
                a = cod[g];
                int p = int.Parse(a);
                string lun = Convert.ToString(p, 2);

                //Se la stringa non raggiunge la lunghezza minima 7, riempio gli spazi mancanti con gli 0 

                if (lun.Length < 7)
                {
                    string x = "0";
                    lun = x + lun;
                }

                //Riempio l'arrry 
                for (int l = 0; l < lun.Length; l++)
                {
                    char q = lun[l];
                    string f = q.ToString();
                    int c = int.Parse(f);
                    mat[g, l] = c;
                }
            }
        }

        //Restiruisce il pi di parità
      
        public static int Bitdiparità(int[,] mat, int[] a, int[] b)
        {

            //Bit orizzontali
            int s = 2;
            int k = 0;

            for (int o = 0; o < 7; o++)
            {
                k = 0;
                
                for (int i = 0; i < 7; i++)
                {
                    k = k + mat[o, i];
                }

                if (k % 2 == 0)
                {
                    Console.SetCursorPosition(27, s);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("0");
                    mat[o, 7] = 0;
                    a[o] = 0;
                }
                else
                {
                    Console.SetCursorPosition(27, s);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("1");
                    mat[o, 7] = 1;
                    a[o] = 1;
                }
                s++;
                s++;


            }

            s = 6;


            //Bit verticali
            for (int o = 0; o < 7; o++)
            {
                k = 0;
                
                for (int i = 0; i < 7; i++)
                {
                    k = k + mat[i, o];
                }

                
                if (k % 2 == 0)
                {
                    Console.SetCursorPosition(s, 16);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("0");
                    mat[7, o] = 0;
                    b[o] = 0;
                }
                else
                {
                    Console.SetCursorPosition(s, 16);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("1");
                    mat[7, o] = 1;
                    b[o] = 1;
                }
                s++;
                s++;

            }
            Console.WriteLine("");
            return k;
        }

        public static void Errori(int[,] arr, bool[,] errors)
        {

            int Errori;

            //Riporto il colore a come era all'inizio
            Console.ForegroundColor = ConsoleColor.White;

            //Ciclo per obbligare l'utente a mettere un numero di k maggiore di 0 e minore di 100
            do
            {
                Console.SetCursorPosition(0, 18);

                //Percentuale di errore 
                Console.Write("Inserisci la percentuale di errore ");
                Errori = Convert.ToInt16(Console.ReadLine());

            } while (Errori < 0 || Errori > 100);

            //Numero bit da cambiare in base agli k 

            int Nerrori = 7 * 7 * Errori / 100;

            Console.WriteLine("In totale ci sono: " + Nerrori + " Errori");
            Random generatore = new Random();

            for (int k = 0; k < Nerrori; k++)
            {
                int righe;
                int colonne;
                do
                {
                    righe = generatore.Next(0, 7);
                    colonne = generatore.Next(0, 7);

                } while (errors[righe, colonne] == true);

                errors[righe, colonne] = true;

                if (arr[righe, colonne] == 0)
                {
                    arr[righe, colonne] = 1;
                }
                else
                {
                    arr[righe, colonne] = 0;
                }

            }

        }

        //Funzione di stampa per i bit di parità evidenziando quelli colorati di rosso
        public static int Paritàerrori(int[,] mat, int[] a, int[] b, int[] a2, int[] b2)
        {

            //Bit orizzontali
            int n = 2;
            int y = 0;

            for (int o = 0; o < 7; o++)
            {
                y = 0;
                //sommo tutti gli 1 e 0 di una singola riga
                for (int i = 0; i < 7; i++)
                {
                    y = y + mat[o, i];
                }

                //divido la somma per 2 e se ottengo resto uguale a 0 scriverò 1 altrimenti 0 e coloro di verde
                if (y % 2 == 0)
                {
                    a2[o] = 0;

                    //Confronto i due array e se sono uguali stampero bit di parità verde altrimenti rosso
                    if (a2[o] == a[o])
                    {
                        Console.SetCursorPosition(27, n);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("0");
                    }
                    else
                    {
                        Console.SetCursorPosition(27, n);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("0");
                    }
                }
                else
                {

                    a2[o] = 1;

                    //Confronto i due array e se sono uguali stampero bit di parità verde altrimenti rosso
                    if (a2[o] == a[o])
                    {
                        Console.SetCursorPosition(27, n);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("1");
                    }
                    else
                    {
                        Console.SetCursorPosition(27, n);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("1");
                    }
                }
                n++;
                n++;


            }

            n = 6;


            //Bit verticali
            for (int o = 0; o < 7; o++)
            {
                y = 0;
                //sommo tutti gli 1 e 0 di una singola riga
                for (int i = 0; i < 7; i++)
                {
                    y = y + mat[i, o];
                }

                //divido la somma per 2 e se ottengo resto uguale a 0 scriverò 1 altrimenti 0 e coloro di verde
                if (y % 2 == 0)
                {
                    b2[o] = 0;

                    //Confronto i due array e se sono uguali stampero bit di parità verde altrimenti rosso
                    if (b2[o] == b[o])
                    {
                        Console.SetCursorPosition(n, 16);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("0");
                    }
                    else
                    {
                        Console.SetCursorPosition(n, 16);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("1");
                    }
                }
                else
                {

                    b2[o] = 1;

                    //Confronto dei due array per 
                    if (b2[o] == b[o])
                    {
                        Console.SetCursorPosition(n, 16);
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("1");
                    }
                    else
                    {
                        Console.SetCursorPosition(n, 16);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("0");
                    }
                }
                n++;
                n++;
            }

            Console.WriteLine("");

            //Rimposto il colore su bianco
            Console.ForegroundColor = ConsoleColor.White;

            return y;
        }

        //Stampa dei Bit di parità evidenziando quelli diversi 
        public static void CorrError(int[,] mat, int[] orizz, int[] vert, int[] orizz2, int[] vert2)
        {

            int h = 2;

            for (int i = 0; i < 7; i++)
            {
                int l = 6;

                for (int j = 0; j < 7; j++)
                {
                    if (orizz[i] != orizz2[i])
                    {
                        if (vert[j] != vert2[j])
                        {
                            Console.SetCursorPosition(l, h);

                            if (mat[i, j] == 0)
                            {
                                mat[i, j] = 1;

                                Console.WriteLine("1");
                            }
                            else
                            {
                                mat[i, j] = 0;
                                Console.WriteLine("0");
                            }
                        }
                    }
                    l++;
                    l++;
                }
                h++;
                h++;
            }
        }
    }
}