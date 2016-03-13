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
            int Row_n, Column_n;
            Console.Write("Row =");
            Row_n = int.Parse(Console.ReadLine());
            Console.Write("Column =");
            Column_n = int.Parse(Console.ReadLine());
            // 行列用の配列
            double[,] mtr_input = new double[Row_n, Column_n];
            // 入力用リスト
            List<string> mat_s = new List<string> { };
            // 行数分, ReadLineでデータの入力受付
            /*
             *  入力例
             *  Row = 3
             *  Column = 2
             *  >> 32 3.9 \n
             *  >> 0 3 \n
             *  >> 100 0.3 \n
             * 
             */
            int i = 0;
            while (i < Row_n)
            {
                // 入力受付
                mat_s.Add(Console.ReadLine());
                // 入力文字処理
                // スペース1文字分で, 文字を分解
                int cnt = 0;
                foreach (var num in mat_s[i].Split(' '))
                {
                    //列数分のみ文字->数値に変換して取得
                    if (cnt < Column_n)
                    {
                        mtr_input[i, cnt] = double.Parse(num);
                    }
                    cnt++;
                }
                i++;
            }
            // 入力確認
            CalcMat DebugPrint = new CalcMat(mtr_input);
            DebugPrint.Print();
            // CalcMatクラスのmethodの利用例
            /*
             * 行＝ROW
             * 列＝COLUMN
             * 
             * CalcMat test = new CalcMat(new double[,]{{1,2},{3,4}});
             * => --       --
             *    |  1   2  |
             *    |  3   4  |
             *    --       --
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
            debug1.CopyFromInit();
            debug1.SwapColumns(0, 1);
            debug1.Print();
            debug1.CopyFromInit();
            debug1.Print();
            debug1.SwapColumns(1, 0);
            debug1.SwapColumns(0, 2);
            debug1.SwapColumns(2, 1);
            debug1.Print();
            debug1.SwapColumns(1, 0);
            debug1.CopyFromInit();
            debug2.SwapColumns(0, 1);
            debug2.Print();
            debug2.CopyFromInit();
            // 行の入れ替え SwapRows(arg1, arg2)
            // arg1 < -- > arg2
            Console.WriteLine("行の入れ替え");
            debug1.SwapRows(0, 1);
            debug1.Print();
            debug1.CopyFromInit();
            debug1.Print();
            debug2.SwapRows(0, 2);
            debug2.Print();
            debug2.CopyFromInit();
            // ある列を定数倍する
            debug1.ColumnMultipliedByConst(3, 3);
            debug2.ColumnMultipliedByConst(1, 2);
            // ある行を定数倍する
            debug1.RowMultipliedByConst(2, 4);
            debug2.RowMultipliedByConst(3, 2.2);
            debug1.Print();
            debug2.Print();
            debug1.CopyFromInit();
            debug2.CopyFromInit();
            // i 列 + const * j 列 = i 列
            debug1.ColumnMulAdd(3, 1, 2);
            debug2.ColumnMulAdd(2, 2, 3);
            debug1.Print();
            debug2.Print();
            debug1.CopyFromInit();
            debug2.CopyFromInit();
            // i 行 + const * j 行 = i 行
            debug1.RowMulAdd(1, 2, 3);
            debug2.RowMulAdd(3, 1, 2);
            debug1.Print();
            debug2.Print();
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
            init_mtrx = new double[Inputmat.GetLength(0), Inputmat.GetLength(1)];
            CopyToInitialMatrix(Inputmat, init_mtrx);
            mtrx = Inputmat;
        }
        /// <summary>
        /// 初期入力の行列を保存する
        /// </summary>
        /// <param name="source"></param>
        /// <param name="destination"></param>
        private void CopyToInitialMatrix(double[,] source, double[,] destination)
        {
            for (int i = 0; i < source.GetLength(0); i++)
            {
                for (int j = 0; j < source.GetLength(1); j++)
                {
                    destination[i, j] = source[i, j];
                }
            }
        }
        /// <summary>
        /// 初期の行列への初期化
        /// </summary>
        public void CopyFromInit()
        {
            CopyToInitialMatrix(init_mtrx, this.mtrx);
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
        /// 行の入れ替え: 行基本変形
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
        /// 列の入れ替え: 列基本変形
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
        /// ある列の定数倍: 列基本変形
        /// </summary>
        /// <param name="Col"></param>
        /// <param name="Const"></param>
        public void ColumnMultipliedByConst(int Col, double Const)
        {
            for (int i = 0; i < this.RowLength(); i++)
            {
                mtrx[i, Col - 1] = Const * mtrx[i, Col - 1];
            }
        }
        /// <summary>
        /// ある行の定数倍: 行基本変形
        /// </summary>
        /// <param name="Row"></param>
        /// <param name="Const"></param>
        public void RowMultipliedByConst(int Row, double Const)
        {
            for (int i = 0; i < this.ColumnLength(); i++)
            {
                mtrx[Row - 1, i] = Const * mtrx[Row - 1, i];
            }
        }
        /// <summary>
        /// ある列を定数倍して, 他の列に加える: 列基本変形
        /// </summary>
        /// <param name="Col_src"></param>
        /// <param name="Col_dest"></param>
        /// <param name="Const"></param>
        public void ColumnMulAdd(int Col_src, int Col_dest, double Const)
        {
            for (int i = 0; i < this.RowLength(); i++)
            {
                mtrx[i, Col_dest - 1] = Const * mtrx[i, Col_src - 1] + mtrx[i, Col_dest - 1];
            }
        }
        /// <summary>
        /// ある行を定数倍して, 他の行に加える: 行基本変形
        /// </summary>
        /// <param name="Row_src"></param>
        /// <param name="Row_dest"></param>
        /// <param name="Const"></param>
        public void RowMulAdd(int Row_src, int Row_dest, double Const)
        {
            for (int i = 0; i < this.ColumnLength(); i++)
            {
                mtrx[Row_dest - 1, i] = Const * mtrx[Row_src - 1, i] + mtrx[Row_dest - 1, i];
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