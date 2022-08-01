using System.Collections;
using System.Collections.Generic;
using System.Text;

string name = "Hello Wordld";

Dictionary<char, int> charInNames = new Dictionary<char, int>();
charInNames = GetCharacterCount(name.ToLower());

foreach(var item in charInNames)
{
    Console.WriteLine(item.Key + ":" + item.Value);
}


static Dictionary<char, int> GetCharacterCount(string name)
{
    var result = new Dictionary<char, int>();

    foreach(char c in name)
    {
        if (result.ContainsKey(c))
        {
            result[c]++;
        }
        else
        {
            if (c != ' ')
            {
                result.Add(c, 1);
            }
        }
    }

    return result;
}

string textWithNumbers = "Mam 7 jablk a moj PIN je 0895. To by bol 6, 8 krat vacsi pluser 5";

string textWithStringNumbers = ReplaceDigits(textWithNumbers);

Console.WriteLine(textWithStringNumbers);

static string ReplaceDigits(string sentence)
{
    int length = sentence.Length;
    StringBuilder result = new StringBuilder(length);
    Dictionary<char, string> numberInWords = new Dictionary<char, string>();
    numberInWords.Add('0', "nula");
    numberInWords.Add('1', "jedna");
    numberInWords.Add('2', "dva");
    numberInWords.Add('3', "tri");
    numberInWords.Add('4', "styri");
    numberInWords.Add('5', "pat");
    numberInWords.Add('6', "sest");
    numberInWords.Add('7', "sedem");
    numberInWords.Add('8', "osem");
    numberInWords.Add('9', "devet");

    for (int i = 0; i < length; i++)
    {
        char currentChar = sentence[i];
        if (Char.IsNumber(currentChar))
        {
            if (numberInWords.ContainsKey(currentChar))
            {
                result.Append(numberInWords[currentChar]);
                if (i + 1 < length && Char.IsNumber(sentence[i + 1]))
                {
                    result.Append(" ");
                }
            }
        }
        else
        {
            result.Append(sentence[i]);
        }
    }

    return result.ToString();
}

public class Matrix : IEnumerable
{
    private int[][] _data;
    public int Rows { get { return _data.GetLength(0); } }
    public int Columns { get { return _data.GetLength(1); } }

    public int this[int row, int col]
    {
        get { return _data[row][col]; }
        set { _data[row] [col] = value; }
    }
    public Matrix(int[][] value)
    {
        _data = value;
    }

    public IEnumerator GetEnumerator()
    {
       return _data.GetEnumerator();
    }
}
public class Exercise
{
    public Matrix Multiply(Matrix a, Matrix b)
    {
        Matrix result = new Matrix(new int[a.Rows][b.Columns]);
        for (int i = 0; i < result.Rows; i++)
        {
            for (int j = 0; j < result.Columns; j++)
            {
                result[i][j] = 0;
                for (int k = 0; k < a.Columns; k++)
                {
                    result[i][j] += (a[i][k] * b[k][j]);
                }
            }
        }
        return result;
    }
}