using System;

static class AmountConverter
{
    static public string Convert(double x, out string Kop)
    {
        byte b;
        byte b1;
        byte b2;
        string kop;

        int fix = (int)x;

        x = Math.Round(x, 2);

        b = (byte)(Math.Round(x - fix, 2) * 100);

        b2 = (byte)(b / 10);

        b1 = (byte)(b % 10);

        if (b2 != 1 && b1 == 1)
        {
            kop = " копейка";
        }
        else if (b2 != 1 && b1 > 1 && b1 < 5)
        {
            kop = " копейки";
        }
        else
        {
            kop = " копеек";
        }

        kop = string.Format("{0}{1}{2}", b2, b1, kop);

        Kop = string.Format("{0}{1}", b2, b1);

        int[] y = new int[4];

        int fix1 = fix;

        for (int i = 0; i < 4; i++)
        {
            x = (double)fix / 1000;
            fix = (int)x;
            y[i] = (int)(Math.Round(x - fix, 3) * 1000);
        }

        string[] Text = new string[4];

        byte y1;
        byte y2;
        byte y3;
        string Text0 = "";
        string Text1 = "";
        string Text2 = "";
        string Text3 = "";
        string Text4 = "";

        string[] T1 = { "", "сто ", "двести ", "триста ", "четыреста ", "пятьсот ", "шестьсот ", "семьсот ", "восьмесот ", "девятьсот " };
        string[] T2 = { "", "", "двадцать ", "тридцать ", "сорок ", "пятьдесят ", "шестьдесят ", "семьдесят ", "восьмедясят ", "девяносто "};
        string[] T3 = { "десять ", "одиннадцать ", "двенадцать ", "тринадцать ", "четырнадцать ", "пятнадцать ", "шестнадцать ", "семнадцать ", "восемнадцать ", "девятнадцать " };
        string[] T4 = { "", "одна ", "две ", "три ", "четыре ", "пять ", "шесть ", "семь ", "восемь ", "девять " };
        string[] T5 = { "", "один ", "два ", "три ", "четыре ", "пять ", "шесть ", "семь ", "восемь ", "девять " };
        string[] T6 = { "рубль ", "тысяча ", "миллион ", "миллиард" };
        string[] T7 = { "рубля ", "тысячи ", "миллиона ", "миллиарда" };
        string[] T8 = { "рублей ", "", "", "" };
        string[] T9 = { "рублей ", "тысяч ", "миллионов ", "миллиардов" };


        for (int i = 0; i < 4; i++)
        {
            y1 = (byte)(y[i] % 10);
            y2 = (byte)((y[i] - y1) / 10 % 10);
            y3 = (byte)(y[i] / 100);
            Text1 = T1[y3];
            Text2 = T2[y2];

            if (y2 == 1)
                Text3 = T3[y1];
            else if (y2 != 1 && i == 1)
                Text3 = T4[y1];
            else
                Text3 = T5[y1];

            if (y2 != 1 && y1 == 1)
                Text4 = T6[i];
            else if (y2 != 1 && y1 > 1 && y1 < 5)
                Text4 = T7[i];
            else if (y1 == 0 && y2 == 0 && y3 == 0)
                Text4 = T8[i];
            else
                Text4 = T9[i];

            Text[i] = Text1 + Text2 + Text3 + Text4;
        }

        if (y[0] + y[1] + y[2] + y[3] == 0)
            Text0 = "ноль рублей " + kop;
        else
            Text0 = Text[3] + Text[2] + Text[1] + Text[0] + kop;


        return Text0;

    }
}