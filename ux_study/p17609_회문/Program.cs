#define SOLVE 



/*
https://www.acmicpc.net/problem/17609

회문
시간 제한	메모리 제한	제출	정답	맞힌 사람	정답 비율
1 초 (추가 시간 없음)	512 MB 17847   4776    3466    28.780 %
문제
회문(回文) 또는 팰린드롬(palindrome)은 앞 뒤 방향으로 볼 때 같은 순서의 문자로 구성된 문자열을 말한다.
예를 들어 ‘abba’ ‘kayak’, ‘reviver’, ‘madam’은 모두 회문이다. 
만일 그 자체는 회문이 아니지만 한 문자를 삭제하여 회문으로 만들 수 있는 문자열이라면 우리는 이런 문자열을 “유사회문”(pseudo palindrome)이라고 부른다.
예를 들어 ‘summuus’는 5번째나 혹은 6번째 문자 ‘u’를 제거하여 ‘summus’인 회문이 되므로 유사회문이다.

여러분은 제시된 문자열을 분석하여 그것이 그 자체로 회문인지,
또는 한 문자를 삭제하면 회문이 되는 “유사회문”인지, 
아니면 회문이나 유사회문도 아닌 일반 문자열인지를 판단해야 한다. 
만일 문자열 그 자체로 회문이면 0, 유사회문이면 1, 그 외는 2를 출력해야 한다.

입력
입력의 첫 줄에는 주어지는 문자열의 개수를 나타내는 정수 T(1 ≤ T ≤ 30)가 주어진다.
다음 줄부터 T개의 줄에 걸쳐 한 줄에 하나의 문자열이 입력으로 주어진다. 
주어지는 문자열의 길이는 3 이상 100,000 이하이고, 영문 알파벳 소문자로만 이루어져 있다.

출력
각 문자열이 회문인지, 유사 회문인지, 둘 모두 해당되지 않는지를 판단하여 회문이면 0, 유사 회문이면 1, 
둘 모두 아니면 2를 순서대로 한 줄에 하나씩 출력한다.

예제 입력 1 
    7
    abba
    summuus
    xabba
    xabbay
    comcom
    comwwmoc
    comwwtmoc
예제 출력 1 
    0
    1
    1
    2
    2
    0
    1

*/

#if SOLVE 


using System.Diagnostics;

int T = 0;  // 1 <= T <= 30
const int CIRCULAR_LETTER = 0;      // 회문
const int SIMILAR_CIRCULAR_LETTER = 1;  // 유사회문
const int NOT_CIRCULAR_LETTER = 2;  // 회문아님
List<string> problems = new List<string>();

string input = Console.ReadLine();
int.TryParse(input, out T);
for (int i = 0; i < T; i++)
{
    input = Console.ReadLine();
    problems.Add(input);
}

int solve (string problem, bool is_partial = false, int _diff_cnt = 0)
{
    int r = 0;
    int l = problem.Length - 1;

    int diff_cnt = _diff_cnt;

    for (int n = 0; n < problem.Length; n++)
    {
        if (r >= l)
        {
            break;
        }
        char ch_r = problem[r];
        char ch_l = problem[l];

        bool is_same = (ch_r == ch_l);
        if (is_same == false)
        {
            diff_cnt++;

            if (diff_cnt > 1)
            {
                return NOT_CIRCULAR_LETTER;
            }
            if (problem[r] == problem[l-1] &&
                problem[r + 1] != problem[l])
            {
                l -= 1;
            }
            else if (problem[r] != problem[l - 1] &&
                problem[r+1] == problem[l])
            {
                r += 1;
            }
            else if (problem[r] == problem[l - 1] &&
                problem[r + 1] == problem[l])
            {
                int ret_l;
                int ret_r;
                {
                    string sub_problem = problem.Substring(r, l - r);
                    ret_l = solve(sub_problem, true, diff_cnt);
                }
                {
                    string sub_problem = problem.Substring(r + 1, l - r);
                    ret_r = solve(sub_problem, true, diff_cnt);
                }
                int ret = Math.Min(ret_r, ret_l);
                return ret;
            }
            else
            {
                return NOT_CIRCULAR_LETTER;
            }

        }
        r++;
        l--;
    }

    if (diff_cnt == 0)
    {
        return CIRCULAR_LETTER;

    }
    else if (diff_cnt == 1)
    {
        return SIMILAR_CIRCULAR_LETTER;
    }
    return NOT_CIRCULAR_LETTER;
}

foreach (string problem in problems)
{
    Console.WriteLine("{0}", solve(problem));
}



#else
using System.Diagnostics;
using System.Runtime.InteropServices;

int T = 0;  // 1 <= T <= 30
const int CIRCULAR_LETTER = 0;      // 회문
const int SIMILAR_CIRCULAR_LETTER = 1;  // 유사회문
const int NOT_CIRCULAR_LETTER = 2;  // 회문아님
List<string> problems = new List<string>();

//string input = Console.ReadLine();
//int.TryParse(input, out T);
//for (int i = 0; i < T; i++)
//{
//    input = Console.ReadLine();
//    problems.Add(input);
//}

T = 28;
//problems.AddRange(new string[] {
//    "abba",
//    "summuus",
//    "xabba",
//    "xabbay",
//    "comcom",
//    "comwwmoc",
//    "comwwtmoc" });


problems.AddRange(new string[] {
"abba",
"summuus",
"xabba",
"xabbay",
"comcom",
"comwwmoc",
"comwwtmoc",
"abbab",
"aab",
"aaab",
"aaaab",
"aaaaab",
"aaaaaab",
"axaaxaa",
"abcddadca",
"aabcbcaa",
"ababbabaa",
"abca",
"babba",
"sumumuus",
"XYXYAAYXY",
"abc",
"cccfccfcc",
"abcddcdba",
"ppbpppb",
"aabcdeddcba",
"aabab",
"aapqbcbqpqaa" });

/*
0
1
1
2
2
0
1
1
1
1
1
1
1
1
2
1
1
1
1
1
1
2
1
1
2
2
2
1
*/


int solve (string problem, bool is_partial = false, int _diff_cnt = 0)
{

    if (is_partial == false)
    {
        Console.WriteLine("\n\n ############### 문제시작: {0} #################", problem);
        Console.WriteLine(problem);
    }
    else
    {
        Console.WriteLine("파생문제 시작. _diff_cnt: {0}", _diff_cnt);
        Console.WriteLine(problem);
    }
    Console.WriteLine("\n--------------------------------------------");
    


    int r = 0;
    int l = problem.Length - 1;

    int diff_cnt = _diff_cnt;

    for (int n = 0; n < problem.Length; n++)
    {
        if (r >= l)
        {
            Console.WriteLine("r과 l이 만났다. 종료, r:{0}, l:{1}", r, l);
            break;
        }
        char ch_r = problem[r];
        char ch_l = problem[l];

        bool is_same = (ch_r == ch_l);
        Console.WriteLine(" n:{0}, r:{1}, ch_r:{2},   l:{3}, ch_l:{4}, is_same:{5}", n, r, ch_r, l, ch_l, is_same);


        if (is_same == false)
        {
            Console.WriteLine("다른값이다.");
            diff_cnt++;

            if (diff_cnt > 1)
            {
                Console.WriteLine("회문아님");
                return NOT_CIRCULAR_LETTER;
            }

            // 각자 다음 문자열과 같은지 비교해본다. 
            // 각자 자기가 빠졌을때의 회문 여부를 재귀적으로 찾아본다

            if (problem[r] == problem[l-1] &&
                problem[r + 1] != problem[l])
            {
                l -= 1;
                Console.WriteLine("[경우1] 왼쪽 전진");
            }
            else if (problem[r] != problem[l - 1] &&
                problem[r+1] == problem[l])
            {
                r += 1;
                Console.WriteLine("[경우2] 오른쪽 전진");
            }
            else if (problem[r] == problem[l - 1] &&
                problem[r + 1] == problem[l])
            {
                // 2가지 방향 모두 가능한 경우
                int ret_l;
                int ret_r;
                Console.WriteLine("-------------------------> 특수한 경우");
                {
                    string sub_problem = problem.Substring(r, l - r);
                    ret_l = solve(sub_problem, true, diff_cnt);
                    Console.WriteLine("부분 문제로 파생. 오른쪽 문자 제거될 경우");
                    Console.WriteLine(sub_problem);
                    Console.WriteLine("결과는 : {0}", ret_l);
                }
                {
                    string sub_problem = problem.Substring(r + 1, l - r);
                    ret_r = solve(sub_problem, true, diff_cnt);
                    Console.WriteLine("부분 문제로 파생. 왼쪽 문자 제거될 경우");
                    Console.WriteLine(sub_problem);
                    Console.WriteLine("결과는 : {0}", ret_r);
                }
                int ret = Math.Min(ret_r, ret_l);
                return ret;
            }
            else
            {
                //모두 다른경우
                // 회문이 아니다
                Console.WriteLine("회문이 아님이 확실하다.");
                return NOT_CIRCULAR_LETTER;
            }

        }
        r++;
        l--;
    }

    if (diff_cnt == 0)
    {
        Console.WriteLine("회문이다");
        return CIRCULAR_LETTER;

    }
    else if (diff_cnt == 1)
    {
        Console.WriteLine("유사회문");
        return SIMILAR_CIRCULAR_LETTER;
    }
    else
    {
        Console.WriteLine("회문아님. diff_cnt:{0}", diff_cnt);
        return NOT_CIRCULAR_LETTER;
    }

    return 0;
}

int solve_ (string problem)
{
    Console.WriteLine("\n---------------------------------");
    Console.WriteLine(problem);
    int n = problem.Length - 1;
    int i = 0;

    int delta_left = 0;
    int delta_right = 0;
    int mismatch_count = 0;

    bool is_odd = (problem.Length % 2 != 0);
    int result = NOT_CIRCULAR_LETTER;


    while (i < problem.Length/2)
    {
        // 한 문자씩 진행하며 거울 대응되는 문자와 비교한다.
        char a1 = problem[i + delta_left];
        char a2 = problem[n - i - delta_right];

        if (a1 == a2)
        {
            Console.WriteLine("i:{0}, '{1}'", i, a1);
        }
        else
        {
            Console.WriteLine("--->매칭되지 않음 발견 i:{0},    a1:'{1}', a2:'{2}'", i, a1, a2);
            Console.WriteLine("미스매칭 카운트 : {0}", mismatch_count);
            if (problem[i + 1] == a2 &&
                a1 != problem[n - i - 1])
            {
                Console.WriteLine("\t case 1. 다음문자와 매칭 {0}, {1}", problem[i + 1], a2);
                delta_left = 1;
                mismatch_count++;
                //if (is_odd == true) mismatch_count++;
            }
            else if (problem[i + 1] != a2 &&
                a1 == problem[n - i - 1])
            {
                Console.WriteLine("\t case 2. 문자와 매칭 {0}, {1}", a1, problem[n - i - 1]);
                delta_right = 1;
                mismatch_count++;
                //if (is_odd == true) mismatch_count++;
            }
            else if (problem[i + 1] == a2 &&
                a1 == problem[n - i - 1])
            {
                Console.WriteLine("\t case 3. 둘 다 가능한 경우. 한단계 더 들어가 본다");

                bool left_mismatch = false;
                bool right_mismatch = false;
                {
                    // 왼쪽을 한칸 건너뛰고 끝까지 가봅
                    int k1 = i + 2 + delta_left;
                    int k2 = n - i - 1 + delta_right;

                    int index = 0;
                    while ((k1 + index) < problem.Length -1)
                    {
                        if (problem[k1 + index] != problem[k2 - index])
                        {
                            Console.WriteLine("\t\t\t [좌측전진] 인덱스 :{0} 미스매칭 발견.  {1} {2}", index, problem[k1 + index], problem[k2 - index]);
                            left_mismatch = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\t\t\t [좌측전진] 인덱스 :{0} 문자 같음 '{1}'", index, problem[k1 + index]);
                        }
                        index++;
                    }
                }
                {
                    // 오른쪽을 한칸 건너뛰고 끝까지 가봅
                    int k1 = i + 1 + delta_left;
                    int k2 = n - i - 2 + delta_right;

                    int index = 0;
                    while ((k1 + index) < problem.Length -1)
                    {
                        if (problem[k1 + index] != problem[k2 - index])
                        {
                            Console.WriteLine("\t\t\t [우측전진] 인덱스 :{0} 미스매칭 발견", index);
                            right_mismatch = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\t\t\t [우측전진] 인덱스 :{0} 문자 같음 '{1}'", index, problem[k1 + index]);
                        }
                        index++;
                    }
                }

                if (left_mismatch == false)
                {
                    delta_left = 1;
                }
                else
                {
                    mismatch_count++;
                }

                if (right_mismatch == false)
                {
                    delta_right = 1;
                }
                else 
                {
                    mismatch_count++;
                }

                //if (problem[i + delta_left + 1 + 1] == problem[n - i - delta_right - 1])
                //{
                //    Console.WriteLine("\t\t 왼쪽이 맞다");
                //    delta_left = 1;
                //    mismatch_count++;
                //}
                //else if (problem[i + delta_left + 1] == problem[n - i - delta_right - 1 - 1])
                //{
                //    Console.WriteLine("\t\t 오른쪽이 맞다");
                //    delta_right = 1;
                //    mismatch_count++;
                //}
                //else
                //{
                //    mismatch_count++;
                //}
            }
            else
            {
                Console.WriteLine("\t case 3. 회문이 아닌게 확실");
                mismatch_count += 2;
            }


            if (mismatch_count > 1)
            {
                Console.WriteLine("이미 미스매칭 카운트가 있음. mismatch_count: {0}", mismatch_count);
                Console.WriteLine("회문이 아닌 것 같다.");
                break;
            }
        }
        i++;
    }


    if (mismatch_count == 0) result = CIRCULAR_LETTER;
    else if (mismatch_count == 1) result = SIMILAR_CIRCULAR_LETTER;
    return result;

    
}


Stopwatch sw = Stopwatch.StartNew();
List<int> answers = new List<int>();
foreach (string problem in problems)
{
    int ret = solve(problem);
    Console.WriteLine("{0} {1}", ret, problem);
    answers.Add(ret);
}
sw.Stop();
Console.WriteLine("elapsed {0:N0} ms", sw.ElapsedMilliseconds);


Console.WriteLine("--- answer ----");
for (int i = 0; i < problems.Count; i++)
{
    Console.WriteLine("{0} {1}", answers[i], problems[i]);
}

#endif