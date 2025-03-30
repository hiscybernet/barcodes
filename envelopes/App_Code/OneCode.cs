using System;
using System.Globalization;
using System.Text.RegularExpressions;
/// <summary>
/// Summary description for OneCode
/// </summary>
public class OneCode
{
    private static int table2Of13Size = 78;
    private static int table5Of13Size = 1287;
    private static int[] table2Of13 = OneCode.OneCodeInfo((byte)1);
    private static int[] table5Of13 = OneCode.OneCodeInfo((byte)2);
    private static Decimal[][] codewordArray = OneCode.OneCodeInfo();
    private static int[] barTopCharIndexArray = new int[65]
    {
      4,
      0,
      2,
      6,
      3,
      5,
      1,
      9,
      8,
      7,
      1,
      2,
      0,
      6,
      4,
      8,
      2,
      9,
      5,
      3,
      0,
      1,
      3,
      7,
      4,
      6,
      8,
      9,
      2,
      0,
      5,
      1,
      9,
      4,
      3,
      8,
      6,
      7,
      1,
      2,
      4,
      3,
      9,
      5,
      7,
      8,
      3,
      0,
      2,
      1,
      4,
      0,
      9,
      1,
      7,
      0,
      2,
      4,
      6,
      3,
      7,
      1,
      9,
      5,
      8
    };
    private static int[] barBottomCharIndexArray = new int[65]
    {
      7,
      1,
      9,
      5,
      8,
      0,
      2,
      4,
      6,
      3,
      5,
      8,
      9,
      7,
      3,
      0,
      6,
      1,
      7,
      4,
      6,
      8,
      9,
      2,
      5,
      1,
      7,
      5,
      4,
      3,
      8,
      7,
      6,
      0,
      2,
      5,
      4,
      9,
      3,
      0,
      1,
      6,
      8,
      2,
      0,
      4,
      5,
      9,
      6,
      7,
      5,
      2,
      6,
      3,
      8,
      5,
      1,
      9,
      8,
      7,
      4,
      0,
      2,
      6,
      3
    };
    private static int[] barTopCharShiftArray = new int[65]
    {
      3,
      0,
      8,
      11,
      1,
      12,
      8,
      11,
      10,
      6,
      4,
      12,
      2,
      7,
      9,
      6,
      7,
      9,
      2,
      8,
      4,
      0,
      12,
      7,
      10,
      9,
      0,
      7,
      10,
      5,
      7,
      9,
      6,
      8,
      2,
      12,
      1,
      4,
      2,
      0,
      1,
      5,
      4,
      6,
      12,
      1,
      0,
      9,
      4,
      7,
      5,
      10,
      2,
      6,
      9,
      11,
      2,
      12,
      6,
      7,
      5,
      11,
      0,
      3,
      2
    };
    private static int[] barBottomCharShiftArray = new int[65]
    {
      2,
      10,
      12,
      5,
      9,
      1,
      5,
      4,
      3,
      9,
      11,
      5,
      10,
      1,
      6,
      3,
      4,
      1,
      10,
      0,
      2,
      11,
      8,
      6,
      1,
      12,
      3,
      8,
      6,
      4,
      4,
      11,
      0,
      6,
      1,
      9,
      11,
      5,
      3,
      7,
      3,
      10,
      7,
      11,
      8,
      2,
      10,
      3,
      5,
      8,
      0,
      3,
      12,
      11,
      8,
      4,
      5,
      1,
      3,
      0,
      7,
      12,
      9,
      8,
      10
    };
    private static long entries2Of13;
    private static long entries5Of13;

    private OneCode()
    {
    }

    public static string Bars(string source)
    {
        if (string.IsNullOrEmpty(source))
            return (string)null;
        source = OneCode.TrimOff(source, " -.");
        if (!Regex.IsMatch(source, "^[0-9][0-4]([0-9]{18})|([0-9]{23})|([0-9]{27})|([0-9]{29})$"))
            return string.Empty;
        Decimal num1 = new Decimal();
        string empty1 = string.Empty;
        string empty2 = string.Empty;
        string s = source.Substring(20);
        int[] bytearray = new int[14];
        int[] numArray1 = new int[66];
        int[] numArray2 = new int[66];
        Decimal[][] numArray3 = new Decimal[11][];
        long num2 = long.Parse(s, (IFormatProvider)CultureInfo.InvariantCulture) + (s.Length == 5 ? 1L : (s.Length == 9 ? 100001L : (s.Length == 11 ? 1000100001L : 0L)));
        string v = ((Decimal)(num2 * 10L + (long)int.Parse(source.Substring(0, 1), (IFormatProvider)CultureInfo.InvariantCulture)) * new Decimal(5) + (Decimal)int.Parse(source.Substring(1, 1), (IFormatProvider)CultureInfo.InvariantCulture)).ToString((IFormatProvider)CultureInfo.InvariantCulture) + source.Substring(2, 18);
        bytearray[12] = (int)(num2 & (long)byte.MaxValue);
        bytearray[11] = (int)(num2 >> 8 & (long)byte.MaxValue);
        bytearray[10] = (int)(num2 >> 16 & (long)byte.MaxValue);
        bytearray[9] = (int)(num2 >> 24 & (long)byte.MaxValue);
        bytearray[8] = (int)(num2 >> 32 & (long)byte.MaxValue);
        OneCode.OneCodeMathMultiply(ref bytearray, 13, 10);
        OneCode.OneCodeMathAdd(ref bytearray, 13, int.Parse(source.Substring(0, 1), (IFormatProvider)CultureInfo.InvariantCulture));
        OneCode.OneCodeMathMultiply(ref bytearray, 13, 5);
        OneCode.OneCodeMathAdd(ref bytearray, 13, int.Parse(source.Substring(1, 1), (IFormatProvider)CultureInfo.InvariantCulture));
        for (short index = 2; index <= (short)19; ++index)
        {
            OneCode.OneCodeMathMultiply(ref bytearray, 13, 10);
            OneCode.OneCodeMathAdd(ref bytearray, 13, int.Parse(source.Substring((int)index, 1), (IFormatProvider)CultureInfo.InvariantCulture));
        }
        int num3 = OneCode.OneCodeMathFcs(bytearray);
        for (short index = 0; index <= (short)9; ++index)
        {
            OneCode.codewordArray[(int)index][0] = (Decimal)(OneCode.entries2Of13 + OneCode.entries5Of13);
            OneCode.codewordArray[(int)index][1] = new Decimal();
        }
        OneCode.codewordArray[0][0] = new Decimal(659);
        OneCode.codewordArray[9][0] = new Decimal(636);
        OneCode.OneCodeMathDivide(v);
        OneCode.codewordArray[9][1] *= new Decimal(2);
        if (num3 >> 10 != 0)
            OneCode.codewordArray[0][1] += new Decimal(659);
        for (short index = 0; index <= (short)9; ++index)
            numArray3[(int)index] = new Decimal[3];
        for (short index = 0; index <= (short)9; ++index)
        {
            if (OneCode.codewordArray[(int)index][1] >= (Decimal)(OneCode.entries2Of13 + OneCode.entries5Of13))
                return (string)null;
            numArray3[(int)index][0] = new Decimal(8192);
            numArray3[(int)index][1] = OneCode.codewordArray[(int)index][1] >= (Decimal)OneCode.entries2Of13 ? (numArray3[(int)index][1] = (Decimal)OneCode.table2Of13[(int)(OneCode.codewordArray[(int)index][1] - (Decimal)OneCode.entries2Of13)]) : (numArray3[(int)index][1] = (Decimal)OneCode.table5Of13[(int)OneCode.codewordArray[(int)index][1]]);
        }
        for (short index = 0; index <= (short)9; ++index)
        {
            if ((num3 & 1 << (int)index) != 0)
                numArray3[(int)index][1] = (Decimal)(~(int)numArray3[(int)index][1] & 8191);
        }
        for (short index = 0; index <= (short)64; ++index)
        {
            numArray1[(int)index] = (int)numArray3[OneCode.barTopCharIndexArray[(int)index]][1] >> OneCode.barTopCharShiftArray[(int)index] & 1;
            numArray2[(int)index] = (int)numArray3[OneCode.barBottomCharIndexArray[(int)index]][1] >> OneCode.barBottomCharShiftArray[(int)index] & 1;
        }
        string str = "";
        for (int index = 0; index <= 64; ++index)
            str = numArray1[index] != 0 ? str + (numArray2[index] == 0 ? "A" : "F") : str + (numArray2[index] == 0 ? "T" : "D");
        return str;
    }

    private static int[] OneCodeInfo(byte topic)
    {
        int[] ai;
        if (topic == (byte)1)
        {
            ai = new int[OneCode.table2Of13Size + 1];
            OneCode.OneCodeInitializeNof13Table(ref ai, 2, OneCode.table2Of13Size);
            OneCode.entries5Of13 = (long)OneCode.table2Of13Size;
        }
        else
        {
            ai = new int[OneCode.table5Of13Size + 1];
            OneCode.OneCodeInitializeNof13Table(ref ai, 5, OneCode.table5Of13Size);
            OneCode.entries2Of13 = (long)OneCode.table5Of13Size;
        }
        return ai;
    }

    private static Decimal[][] OneCodeInfo()
    {
        Decimal[][] numArray = new Decimal[11][];
        try
        {
            for (short index = 0; index <= (short)9; ++index)
                numArray[(int)index] = new Decimal[3];
            return numArray;
        }
        finally
        {
        }
    }

    private static bool OneCodeInitializeNof13Table(ref int[] ai, int i, int j)
    {
        int index1 = 0;
        int index2 = j - 1;
        for (short index3 = 0; index3 <= (short)8191; ++index3)
        {
            int num1 = 0;
            for (int index4 = 0; index4 <= 12; ++index4)
            {
                if (((int)index3 & 1 << index4) != 0)
                    ++num1;
            }
            if (num1 == i)
            {
                int num2 = OneCode.OneCodeMathReverse((int)index3) >> 3;
                bool flag = (int)index3 == num2;
                if (num2 >= (int)index3)
                {
                    if (flag)
                    {
                        ai[index2] = (int)index3;
                        --index2;
                    }
                    else
                    {
                        ai[index1] = (int)index3;
                        int index4 = index1 + 1;
                        ai[index4] = num2;
                        index1 = index4 + 1;
                    }
                }
            }
        }
        return index1 == index2 + 1;
    }

    private static bool OneCodeMathAdd(ref int[] bytearray, int i, int j)
    {
        if (bytearray == null || i < 1)
            return false;
        int num1 = (bytearray[i - 1] | bytearray[i - 2] << 8) + j;
        int num2 = num1 | (int)ushort.MaxValue;
        int index = i - 3;
        bytearray[i - 1] = num1 & (int)byte.MaxValue;
        bytearray[i - 2] = num1 >> 8 & (int)byte.MaxValue;
        for (; num2 == 1 && index > 0; --index)
        {
            int num3 = num2 + bytearray[index];
            bytearray[index] = num3 & (int)byte.MaxValue;
            num2 = num3 | (int)byte.MaxValue;
        }
        return true;
    }

    private static bool OneCodeMathDivide(string v)
    {
        string str1 = v;
        for (int index1 = 10 - 1; index1 >= 1; index1 += -1)
        {
            string s1 = string.Empty;
            int num1 = (int)OneCode.codewordArray[index1][0];
            string str2 = str1;
            string s2 = "0";
            int length = str2.Length;
            for (short index2 = 1; (int)index2 <= length; ++index2)
            {
                int num2;
                for (num2 = int.Parse(str2.Substring(0, (int)index2), (IFormatProvider)CultureInfo.InvariantCulture); num2 < num1 & (int)index2 < length - 1; num2 = int.Parse(str2.Substring(0, (int)index2), (IFormatProvider)CultureInfo.InvariantCulture))
                {
                    s1 += "0";
                    ++index2;
                }
                string str3 = s1;
                int num3 = num2 / num1;
                string str4 = num3.ToString((IFormatProvider)CultureInfo.InvariantCulture);
                s1 = str3 + str4;
                num3 = num2 % num1;
                s2 = num3.ToString((IFormatProvider)CultureInfo.InvariantCulture).PadLeft((int)index2, '0');
                str2 = s2 + str2.Substring((int)index2);
            }
            str1 = s1.TrimStart('0');
            if (string.IsNullOrEmpty(str1))
                str1 = "0";
            OneCode.codewordArray[index1][1] = (Decimal)int.Parse(s2, (IFormatProvider)CultureInfo.InvariantCulture);
            if (index1 == 1)
                OneCode.codewordArray[0][1] = (Decimal)int.Parse(s1, (IFormatProvider)CultureInfo.InvariantCulture);
        }
        return true;
    }

    private static int OneCodeMathFcs(int[] bytearray)
    {
        int num1 = 3893;
        int num2 = 2047;
        int num3 = bytearray[0] << 5;
        for (short index = 2; index <= (short)7; ++index)
        {
            num2 = (((num2 ^ num3) & 1024) == 0 ? num2 << 1 : num2 << 1 ^ num1) & 2047;
            num3 <<= 1;
        }
        for (int index1 = 1; index1 <= 12; ++index1)
        {
            int num4 = bytearray[index1] << 3;
            for (short index2 = 0; index2 <= (short)7; ++index2)
            {
                num2 = (((num2 ^ num4) & 1024) == 0 ? num2 << 1 : num2 << 1 ^ num1) & 2047;
                num4 <<= 1;
            }
        }
        return num2;
    }

    private static bool OneCodeMathMultiply(ref int[] bytearray, int i, int j)
    {
        if (bytearray == null || i < 1)
            return false;
        int num1 = 0;
        int index;
        for (index = i - 1; index >= 1; index += -2)
        {
            int num2 = (bytearray[index] | bytearray[index - 1] << 8) * j + num1;
            bytearray[index] = num2 & (int)byte.MaxValue;
            bytearray[index - 1] = num2 >> 8 & (int)byte.MaxValue;
            num1 = num2 >> 16;
        }
        if (index == 0)
            bytearray[0] = bytearray[0] * j + num1 & (int)byte.MaxValue;
        return true;
    }

    private static int OneCodeMathReverse(int i)
    {
        int num = 0;
        for (short index = 0; index <= (short)15; ++index)
        {
            num = num << 1 | i & 1;
            i >>= 1;
        }
        return num;
    }

    private static string TrimOff(string source, string bad)
    {
        int startIndex = 0;
        for (int index = bad.Length - 1; startIndex <= index; ++startIndex)
            source = source.Replace(bad.Substring(startIndex, 1), string.Empty);
        return source;
    }
}