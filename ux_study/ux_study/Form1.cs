using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ux_study
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();


            this.Load += Form1_Load;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            byte[] arr = new byte[4];
            move(arr);
        }

        bool move(byte[] arr, int row = 0)
        {
            if (row == arr.Length)
            {
                Console.WriteLine("end. row: {0}", row);
                return true;
            }

            Console.WriteLine("-------- row: {0} -------", row);

            // 현재 row 값으로 다음 row를 계산한다
            bool col_exist = false;
            for (int col = 0; col < arr.Length; col++)
            {
                byte[] next_arr = new byte[arr.Length];
                for (int _col = 0; _col < arr.Length; _col++)
                {
                    if (arr[_col] == 0) continue;

                    if ((_col - 1) > 0)
                        next_arr[_col - 1] |= 0x04;
                    next_arr[_col] |= 0x02;
                    if ((_col + 1) < arr.Length - 1)
                        next_arr[_col + 1] |= 0x01;
                    
                }


                if (arr[col] != 0) continue;

                if ((col - 1) > 0)
                    next_arr[col - 1] |= 0x4;
                next_arr[col] |= 0x2;

                if ((col + 1) < arr.Length -1)
                    next_arr[col + 1] |= 0x1;

                Console.WriteLine(" ---> TRY COL:{0} is Queen", col);
                Console.WriteLine("  MOVE. row:{0}, next_arr: {1}", row + 1, string.Join(",", next_arr));
                bool ret = move(next_arr, ++row);

                col_exist = true;
                Console.WriteLine("[{0},{1}] ret: {2}", row, col, ret);
                
            }

            if (col_exist == false)
            {
                Console.WriteLine("row {0} not available col", row);
                Console.WriteLine("{0}", string.Join(",", arr));
            }
            return false;
        }
    }
}
