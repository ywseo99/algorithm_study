using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Net.Configuration;
using System.Runtime.InteropServices;
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
            int n = 8;
            Stopwatch sw = Stopwatch.StartNew();
            byte[] arr = new byte[n];
            answer = new int[n];
            move(arr);

            sw.Stop();
            Console.WriteLine(" Problem 9663. N-Queen");
            Console.WriteLine(" n: {0}", n);
            foreach (string _answer in arr_answer)
            {
                Console.WriteLine("\t {0}", _answer);
            }
            Console.WriteLine(" answer count: {0}, elapsed {1:N0} ms", arr_answer.Count, sw.ElapsedMilliseconds);
        }

        List<string> arr_answer = new List<string>();
        int[] answer;

        bool move(byte[] arr, int row = 0)
        {           
            if (row >= arr.Length)
            {
                Console.WriteLine("end. row: {0}\n\n", row);
                return true;
            }

            /*
             * Cell의 상태는 모두 4가지 
             * 0x0 (0000) : 비어있는 셀. 여기에 새로 Queen을 놓을 수 있다.
             * 0x1 (0001) : 우측아래로 확장하는 셀. 다음 row의 col+1도 같은 속성을 가짐 ( |= 0x1 )
             * 0x2 (0010) : 아래로 확장하는 셀. 다음 row의 col도 같은 속성을 가짐 ( |= 0x2 )
             * 0x4 (0100) : 좌측아래로 확장하는 셀. 다음 row의 col-1도 같은 속성을 가짐 ( |= 0x4 )
             * 0x7 (0111) : 새로 놓여지는 Queen은 위의 3가지 속성을 모두 가짐 ( |= 0x7 )
             * 
             * 현재 row값들을 기준으로 다음 next_row를 채우고, 비어있는 셀에 Queen을 놓는 시도를 재귀적으로 반복한다. 
             * Queen을 놓을 수 있는 셀이 존재하지 않는다면 실패이다. 
             * Queen을 계속 놓으며 마지막 row까지 진행했다면 정답이다.
             */

            // 현재 row 값으로 다음 row를 계산한다
            bool col_exist = false;
            for (int col = 0; col < arr.Length; col++)
            {
                byte[] next_arr = new byte[arr.Length];
                for (int _col = 0; _col < arr.Length; _col++)
                {
                    if (arr[_col] == 0) continue;

                    if (_col >= 1)
                        next_arr[_col - 1] |= (byte)(arr[_col] & 0x4);

                    next_arr[_col] |= (byte)(arr[_col] & 0x2);

                    if (_col < arr.Length -1)
                        next_arr[_col + 1] |= (byte)(arr[_col] & 0x1);
                }

                // 새로 놓을 Queen에 의한 다음 row의 변화
                if (arr[col] != 0) continue;
                if ((col - 1) >= 0)
                    next_arr[col - 1] |= 0x4;

                next_arr[col] |= 0x2;

                if ((col + 1) < arr.Length)
                    next_arr[col + 1] |= 0x1;

                Console.WriteLine(" ----> TRY row:{0}, col:{1}  is Queen", row, col);
                answer[row] = col;

                Console.WriteLine("\t  row:{0}  curr_arr: {1}", row, string.Join(",", arr));
                Console.WriteLine("\t  row:{0}  next_arr: {1}", row + 1, string.Join(",", next_arr));
                bool ret = move(next_arr, row + 1);

                col_exist = true;
                //Console.WriteLine("\t  RESULT: [{0},{1}] {2}, move next col ", row, col, ret);
                if (ret == true)
                {
                    Console.WriteLine("\t\t CORRECT");
                    Console.WriteLine("\t\t {0}", string.Join(",", answer));
                    arr_answer.Add(string.Join(",", answer));
                }

            }

            if (col_exist == false)
            {
                Console.WriteLine("\t ------- FAIL row {0} not available col", row);
                Console.WriteLine("\t {0}", string.Join(",", arr));
            }
            return false;
        }
    }
}
