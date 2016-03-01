using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_CalcMat
{
    class Program
    {
        static void Main(string[] args)
        {
            int[,] mat = new int[3, 3];
            int nofrow = 3;
            int[,] matcal = new int[3, 3] { { 1, 3, 4 }, { 2, -2, 1 }, { 4, 5, 2 } };
            
            for (int i = 0; i < nofrow; i++)
            {
                Console.WriteLine(matcal[i,i]);
            }

            // 1次元分の長さ
            //Console.WriteLine(matcal.GetLength(0));

            // 3 x 3 の行列
            CalcMat hoge = new CalcMat(new double[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } });
            Console.WriteLine(hoge.mtrx);
            for (int i = 0; i < nofrow; i++)
            {
                for (int j = 0; j < nofrow; j++)
                {
                    Console.Write(hoge.mtrx[i, j]);
                }
                Console.WriteLine();
            }
            Console.WriteLine(hoge.Length());
            
            // CalcMatクラスのmethodの利用例
            /*
             * 行＝ROW
             * 列＝COLUMN
             * 
             * CalcMat test = new CalcMat(new double[,][1,2],[3,4]);
             * => --       --
             *    |  1   2  |
             *    |  3   4  |
             *    --       --
             * 
             */
            // 2 x 3 の行列
            CalcMat debug1 = new CalcMat(new double[,] { { 1, 2, 3 }, { 4, 5, 6 } });
            // 3 x 2 の行列
            CalcMat debug2 = new CalcMat(new double[,] { { 1, 2 }, { 3, 4 }, { 5, 6 } });
            // 行と列の数
            Console.WriteLine("Row x Column =" + debug1.RowLength().ToString() + "x" + debug1.ColumnLength().ToString());
            Console.WriteLine("Row x Column =" + debug2.RowLength().ToString() + "x" + debug2.ColumnLength().ToString());
            // 行列の出力
            Console.WriteLine("行列の出力");
            debug1.Print();
            debug2.Print();
            // 行の出力
            Console.WriteLine("行の出力");
            debug1.PrintRow(0);
            debug1.PrintRow(1);
            debug2.PrintRow(0);
            debug2.PrintRow(1);
            debug2.PrintRow(2);
            // 列の出力
            Console.WriteLine("列の出力");
            debug1.PrintColumn(0);
            debug1.PrintColumn(1);
            debug1.PrintColumn(2);
            debug2.PrintColumn(0);
            debug2.PrintColumn(1);
            // 列の入れ替え SwapColumns(arg1, arg2)
            // arg1 < -- > arg2
            Console.WriteLine("列の入れ替え");
            debug1.SwapColumns(0, 0);
            Console.WriteLine(debug1.mtrx == debug1.init_mtrx);
            debug1.mtrx.CopyTo(debug1.init_mtrx, 0);
            debug1.SwapColumns(0, 1);
            debug1.Print();
            debug1.mtrx.CopyTo(debug1.init_mtrx, 0);
            debug1.Print();
            debug1.SwapColumns(1, 0);
            debug1.mtrx.CopyTo(debug1.init_mtrx, 0);
            debug1.SwapColumns(0, 2);
            debug1.SwapColumns(2, 1);
            debug1.Print();
            debug1.SwapColumns(1, 0);
            debug1.mtrx.CopyTo(debug1.init_mtrx, 0);
            debug2.SwapColumns(0, 1);
            debug2.Print();
            debug2.mtrx.CopyTo(debug1.init_mtrx, 0);
            // 行の入れ替え SwapRows(arg1, arg2)
            // arg1 < -- > arg2
            Console.WriteLine("行の入れ替え");
            debug1.SwapRows(0, 1);
            debug1.Print();
            debug1.mtrx = debug1.init_mtrx;
            debug1.Print();
            debug2.SwapRows(0, 2);
            debug2.Print();
            debug2.mtrx = debug2.init_mtrx;

            // 列の入れ替え（定数倍あり）

            // 行の入れ替え (定数倍あり)

            // i 列 + j 列 = i 列

            // i 行 + j 行 = i 行

        }
    }
    /// <summary>
    /// 掃き出し法実装
    /// </summary>
    class CalcMat
    {
        /// <summary>
        /// 表示用の行列
        /// </summary>
        public double[,] mtrx { get; set; }
        /// <summary>
        /// 入れ替え用の行列
        /// </summary>
        private double[] row1, row2, col1, col2;
        /// <summary>
        /// 入力時の行列
        /// </summary>
        public double[,] init_mtrx { get; private set; }
        /// <summary>
        /// m x n 行列の入力
        /// </summary>
        /// <param name="Inputmat"></param>
        public CalcMat(double[,] Inputmat)
        {
            // 入れ替え用の行, 列の要素の確保
            row1 = new double[Inputmat.GetLength(1)];
            row2 = new double[Inputmat.GetLength(1)];
            col1 = new double[Inputmat.GetLength(0)];
            col2 = new double[Inputmat.GetLength(0)];
            // 初期入力の行列を保存
            CopyToInitialMatrix(Inputmat, init_mtrx);
            mtrx = Inputmat;
        }
        private void CopyToInitialMatrix(double[,] source, double[,] destination)
        {
            for (int i = 0; i < this.RowLength(); i++)
            {
                
            }
        }
        /// <summary>
        /// 行と列数の最大数
        /// </summary>
        /// <returns></returns>
        public int Length()
        {
            return Math.Max(mtrx.GetLength(0),mtrx.GetLength(1));
        }
        /// <summary>
        /// 行数
        /// </summary>
        /// <returns></returns>
        public int RowLength()
        {
            return mtrx.GetLength(0);
        }
        /// <summary>
        /// 列数
        /// </summary>
        /// <returns></returns>
        public int ColumnLength()
        {
            return mtrx.GetLength(1);
        }
        /// <summary>
        /// 行の入れ替え
        /// </summary>
        /// <param name="Row1"></param>
        /// <param name="Row2"></param>
        public void SwapRows(int Row1, int Row2)
        {
            // Row1 行目のコピー
            for (int i = 0; i < this.ColumnLength(); i++)
            {
                row1[i] = this.mtrx[Row1, i];
            }
            // Row2 行目のコピー
            for (int i = 0; i < this.ColumnLength(); i++)
            {
                row2[i] = this.mtrx[Row2, i];
            }
            // Row1 と Row2 の入れ替え
            for (int i = 0; i < this.ColumnLength(); i++)
            {
                this.mtrx[Row1, i] = row2[i];
                this.mtrx[Row2, i] = row1[i];
            }
        }
        /// <summary>
        /// 列の入れ替え
        /// </summary>
        /// <param name="Col1"></param>
        /// <param name="Col2"></param>
        public void SwapColumns(int Col1, int Col2)
        {
            // Col1 列目のコピー
            for (int i = 0; i < this.RowLength(); i++)
            {
                col1[i] = this.mtrx[i, Col1];
            }
            // Col2 列目のコピー
            for (int i = 0; i < this.RowLength(); i++)
            {
                col2[i] = this.mtrx[i, Col2];
            }
            // Col1 と Col2 の入れ替え
            for (int i = 0; i < this.RowLength(); i++)
            {
                this.mtrx[i, Col1] = col2[i];
                this.mtrx[i, Col2] = col1[i];
            }
        }
        /// <summary>
        /// 行列出力
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < this.RowLength(); i++)
            {
                for (int j = 0; j < this.ColumnLength(); j++)
                {
                    Console.Write(this.mtrx[i, j] + "  ");
                }
                Console.WriteLine("");
            }
        }
        /// <summary>
        /// 行の出力 (0から(i-1)行目)
        /// </summary>
        /// <param name="i"></param>
        public void PrintRow(int i)
        {
            for (int j = 0; j < this.ColumnLength(); j++)
            {
                Console.Write(this.mtrx[i, j]+"  ");
            }
            Console.WriteLine("");
        }
        /// <summary>
        /// 列の出力 (0から(i-1)列目)
        /// </summary>
        /// <param name="i"></param>
        public void PrintColumn(int i)
        {
            for (int j = 0; j < this.RowLength(); j++)
            {
                Console.WriteLine(this.mtrx[j, i] + "  ");
            }
        }

    }
}