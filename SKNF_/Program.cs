using System;
using System.Collections.Generic;


namespace SKNF
{
    /// <summary>
    /// Класс с описанием выражения
    /// в случае добавления аргументов модифицировать строки, помеченные комментариями
    /// Если нужна другая функция от 3х аргументов, просто изменить строку return
    /// </summary>
    class Expression
    {
        public readonly int LenArgs = 3; //Изменить количество аргументов
        public List<string> args = new List<string> { "a", "b", "c", "d", "e", "f" };
        public bool Expr(bool a, bool b, bool c /* ,bool d, bool e */ )  //Добавить аргументы
        {
            return (a && b && !c) || a;  //Здесь пишется нужная функция
        }
    }

    class Program
    {
        static void Main()
        {
            var exp = new Expression();
            Console.WriteLine(ToSKNF(exp));
        }


        /// <summary>
        /// В случае добавления аргументов модифицировать стоку, помеченную комментарием
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        static string ToSKNF(Expression exp)
        {
            var znak = "";
            bool begin = true;
            var str = "";
            int columns = (int)Math.Pow(2, exp.LenArgs);
            for (int i = 0; i < columns; i++)
            {
                var line = (Convert.ToString(i, 2).PadLeft(exp.LenArgs, '0'));
                if (exp.Expr(ToBool(line[0]), ToBool(line[1]), ToBool(line[2]) /* ,ToBool(line[3]),ToBool(line[4])*/ ) == true)
                {
                    str += (begin) ? "(" : " & (";
                    begin = false;
                    for (int j = 0; j < exp.LenArgs; j++)
                    {
                        if (j < exp.LenArgs - 1)
                            znak = "|";
                        else
                            znak = "";
                        str += (line[j] == '1') ? exp.args[j] + znak : "!" + exp.args[j] + znak;
                    }
                    str += ") ";
                }
            }
            return str;
        }

        static bool ToBool(char s)
        {
            return (s == '1');
        }
    }
}
