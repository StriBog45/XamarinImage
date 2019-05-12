using System;
using System.Collections.Generic;
using System.Text;

namespace XamarinImage
{
    public class ExcelField
    {
        public ExcelField(int left, int top, int right, int down)
        {
            Left = left;
            Top = top;
            Right = right;
            Down = down;
        }
        public int Left { get; set; }
        public int Right { get; set; }
        public int Top { get; set; }
        public int Down { get; set; }
    }
}
