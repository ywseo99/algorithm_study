﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace p2605
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /*
            문제
            점심시간이 되면 반 학생 모두가 한 줄로 줄을 서서 급식을 탄다. 
            그런데 매일 같이 앞자리에 앉은 학생들이 앞에 줄을 서 먼저 점심을 먹고, 뒷자리에 앉은 학생들은 뒤에 줄을 서 늦게 점심을 먹게 된다.
            어떻게 하면 이러한 상황을 바꾸어 볼 수 있을까 고민하던 중 선생님이 한 가지 방법을 내 놓았다. 
            그 방법은 다음과 같다.

            학생들이 한 줄로 줄을 선 후, 첫 번째 학생부터 차례로 번호를 뽑는다.
            첫 번째로 줄을 선 학생은 무조건 0번 번호를 받아 제일 앞에 줄을 선다.
            두 번째로 줄을 선 학생은 0번 또는 1번 둘 중 하나의 번호를 뽑는다.
            0번을 뽑으면 그 자리에 그대로 있고, 1번을 뽑으면 바로 앞의 학생 앞으로 가서 줄을 선다.
            세 번째로 줄을 선 학생은 0, 1 또는 2 중 하나의 번호를 뽑는다. 
            그리고 뽑은 번호만큼 앞자리로 가서 줄을 선다.
            마지막에 줄을 선 학생까지 이와 같은 방식으로 뽑은 번호만큼 앞으로 가서 줄을 서게 된다. 
            각자 뽑은 번호는 자신이 처음에 선 순서보다는 작은 수이다.

            예를 들어 5명의 학생이 줄을 서고, 첫 번째로 줄을 선 학생부터 다섯 번째로 줄을 선 학생까지 차례로 0, 1, 1, 3, 2번의 번호를 뽑았다고 하자, 
            첫 번째 학생부터 다섯 번째 학생까지 1부터 5로 표시하면 학생들이 줄을 선 순서는 다음과 같이 된다.

            첫 번째 학생이 번호를 뽑은 후 : 1
            두 번째 학생이 번호를 뽑은 후 : 2 1
            세 번째 학생이 번호를 뽑은 후 : 2 3 1
            네 번째 학생이 번호를 뽑은 후 : 4 2 3 1
            다섯 번째 학생이 번호를 뽑은 후 : 4 2 5 3 1
            따라서 최종적으로 학생들이 줄을 선 순서는 4, 2, 5, 3, 1이 된다.

            줄을 선 학생들이 차례로 뽑은 번호가 주어질 때 학생들이 최종적으로 줄을 선 순서를 출력하는 프로그램을 작성하시오.

            입력
            첫째 줄에는 학생의 수가 주어지고 둘째 줄에는 줄을 선 차례대로 학생들이 뽑은 번호가 주어진다.
            학생의 수가 100 이하이고, 학생들이 뽑는 번호는 0 또는 자연수이며 학생들이 뽑은 번호 사이에는 빈 칸이 하나씩 있다.

            출력
            학생들이 처음에 줄을 선 순서대로 1번부터 번호를 매길 때, 첫째 줄에 학생들이 최종적으로 줄을 선 순서를 그 번호로 출력한다.
            학생 번호 사이에는 한 칸의 공백을 출력한다.

            예시 
            입력 
                5
                0 1 1 3 2
            정답
                4 2 5 3 1
            */

            string input = Console.ReadLine();
            int n = int.Parse(input);

            input = Console.ReadLine();
            string[] numbers = input.Split(' ');


            List<int> students = new List<int>();
            // 초기 학생들 줄세우기
            for (int i = 0; i < n; i++)
            {
                students.Add(i+1);
            }
            //Console.WriteLine("초기값: {0}", string.Join(",", students));
            //Console.WriteLine("n: {0}, numbers: {1}", n, string.Join(",", numbers));

            // 학생들이 뽑은 번호대로 자리이동을 시도한다.
            for (int i = 0; i < n; i++)
            {
                // 0을 뽑아 제자리에 있거나, 0이 아닌 번호를 뽑아 그 수 만큼 앞의 학생 자리로 이동.
                // 그 이후 학생들은 모두 한자리씩 뒤로 이동한다.
                int num = int.Parse(numbers[i]);
                if (num == 0)
                {
                    // 0번을 뽑은 학생은 자리이동을 하지 않는다.
                    continue;
                }

                int insert_pos = i - num;
                int value = students[i];
                students.RemoveAt(i);       // 자리의 값을 빼서 
                students.Insert(insert_pos, value); // 이동할 위치에 삽입
            }

            Console.WriteLine("{0}", string.Join(" ", students));
            //Console.ReadKey();
        }
    }
}
