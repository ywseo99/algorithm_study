//#define SOLVE 
#define BFS 
/*

리조트 서브태스크
시간 제한	메모리 제한	제출	정답	맞힌 사람	정답 비율
2 초	512 MB	4970	2145	1576	44.319%
문제
수영이는 여름방학을 맞이하여 많은 놀이 시설이 있는 KOI 리조트에 놀러가려고 한다. 
리조트의 하루 이용권의 가격은 만원이다. 
하지만 리조트의 규모는 상상을 초월하여 모든 시설을 충분히 즐기기 위해서는 하루로는 터무니없이 부족하다. 
그래서 많은 이용객들은 3일 이상 연속으로 이용하기도 한다. 
KOI 리조트에서는 3일 연속 이용권을 할인된 가격 이만오천원에, 연속 5일권은 삼만칠천원에 판매하고 있다. 
게다가 연속 3일권, 연속 5일권에는 쿠폰이 각각 1장, 2장이 함께 포함되어 있다. 
쿠폰 3장은 하루 이용권 한 장으로 교환할 수 있다.

    이용권 종류	가격	쿠폰지급
    하루 이용권	10,000원	없음
    연속 3일권	25,000원	쿠폰 1장
    연속 5일권	37,000원	쿠폰 2장
연속 3일권과 연속 5일권은 구입일로부터 연속으로 3일 혹은 5일간만 이용이 가능하지만 해당 기간을 모두 이용할 필요는 없다.

수영이는 N일의 여름방학 중 다른 일정으로 리조트에 갈 수 없는 날이 M일 있다. 
KOI 리조트를 사랑하는 수영이는 그 외의 모든 날을 KOI 리조트에서 보내고자 한다. 
물론, 가장 저렴한 비용으로 리조트를 이용하고자 한다.

예를 들어, 여름방학이 13일이라고 하고, 여름방학 기간 중 리조트에 갈 수 없는 날이 4번째, 6번째, 7번째, 11번째, 12번째 날이라고 하자. 
다음 표의 첫 번째 행은 13일의 여름방학을 나타내고, 리조트에 갈 수 없는 날은 검정색으로 표시되어 있다. 
표의 두 번째 행과 세 번째 행은 수영이가 이용권을 구입하는 두 가지 방법을 나타낸다. 

두 번째 행의 구입 방법은 다음과 같다. 
여름방학의 첫 번째 날에 연속 3일권을 구입하여 3번째 날까지 리조트를 이용하고, 구매시 1장의 쿠폰을 받는다. 
5번째 날에는 하루 이용권을 구입하여 이용한다. 
8번째 날에는 연속 3일권을 구입하여 10번째 날까지 리조트를 이용하고, 역시 구매시 쿠폰 1장을 받는다. 
13번째 날에는 하루 이용권을 구입하여 리조트를 이용한다. 
이렇게 하여 수영이가 리조트 이용을 위해 지불한 전체 비용은 70,000원이다. 

세 번째 행은 더 저렴한 비용으로 리조트를 이용하는 구입 방법이다. 
여름방학의 첫 번째 날에 연속 5일권을 구입하여 5번째 날까지 리조트를 이용하고(4번째 날 제외), 구매시 2장의 쿠폰을 받는다. 
그리고 8번째 날에 연속 3일권을 구입하여 10번째 날까지 리조트를 이용하고, 역시 구매시 쿠폰 1장을 받는다. 
13번째 날에는 그때까지 받은 3장의 쿠폰을 하루 이용권 한 장으로 교환하여 리조트를 이용한다. 
이렇게 하여 수영이가 리조트 이용을 위해 지불한 전체 비용은 62,000원이다.

여름방학 기간과 리조트에 갈 수 없는 날의 정보가 주어질 때, 리조트를 이용하기 위해서 수영이가 지불해야 하는 최소비용을 계산하는 프로그램을 작성하시오.

입력
    표준 입력으로 다음 정보가 주어진다. 첫 번째 줄에는 수영이의 여름방학의 일수를 뜻하는 정수 N(1 ≤ N ≤ 100)과 수영이가 리조트에 갈 수 없는 날의 수 M (0 ≤ M ≤ N)이 순서대로 주어진다. 
    M이 0인 경우 더 이상의 입력은 주어지지 않으며, M이 0보다 큰 경우 그 다음 줄에는 수영이가 리조트에 갈 수 없는 날이 1 이상 N 이하의 정수로 날짜 순서대로 M개 주어진다.

    예를 들어, M이 3이고 입력의 두 번째 줄에 정수 “12 14 17”이 주어진다면 여름방학의 12번째, 14번째, 17번째 날에는 리조트에 갈 수 없음을 의미한다.

출력
    표준 출력으로 주어진 입력에서 제시된 날들을 제외한 나머지 날 모두 리조트에 입장하기 위해서 지불해야 하는 비용의 최솟값을 출력한다.

서브태스크
번호	배점	제한
1	11	
N ≤ 5

2	17	
M = 0

3	72	
원래의 제약조건 이외에 아무 제약조건이 없다.


예제 입력 1 
13 5
4 6 7 11 12

예제 출력 1 
    62000

예제 입력 2 
    50 10
    3 5 7 11 15 16 22 23 24 34
예제 출력 2 
    288000


*/


#if SOLVE

#elif BFS


using System.Collections.Concurrent;

int n = 0; // 여름방학 일수 (1 <= n <= 100)
int m = 0; // 리조트에 갈 수 없는 날의 수 (0 <= M <= n)

const int COST_ONE = 10000;
const int COST_THREE = 25000;
const int COST_FIVE = 37000;

string input;
string[] words;
//string input = Console.ReadLine();
//string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
//int.TryParse(words[0], out n);
//int.TryParse(words[1], out m);
//input = Console.ReadLine();


//n = 13;
//m = 5;
//input = "4 6 7 11 12";

n = 50;
m = 10;
input = "3 5 7 11 15 16 22 23 24 34";


words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
int[] maze = new int[n + 1];
foreach (var word in words)
{
    int val = int.Parse(word);
    maze[val] = 0xff;
}
for (int i = 0; i < maze.Length; i++)
{
    Console.WriteLine(" day:{0,2}  {1,2:x}", i, maze[i]);
}

int[] steps = new int[3] { 1, 3, 5 };

Stack<int> stack = new Stack<int>();
List<int[]> results = new List<int[]>();

ConcurrentQueue<int> queue = new ConcurrentQueue<int>();

void move(int pos, Stack<int> stack)
{
    if (pos > n)
    {
        //Console.WriteLine("\t ---------------------- END --- stack: {0}", string.Join(" ", stack));
        results.Add(stack.Reverse().ToArray());
        return;
    }

    int curr_pos = 0;
    int next_pos = 0;
    queue.Enqueue(pos);

    while (queue.IsEmpty == false)
    {        
        queue.TryDequeue(out curr_pos);
        //Console.WriteLine("dequeue. curr_pos: {0}", curr_pos);
        if (curr_pos > n)
        {
            Console.WriteLine("끝을 찾았다. curr_pos: {0}", curr_pos);
            break;
        }

        while (curr_pos < maze.Length &&
            maze[curr_pos] == 0xff)
        {
            curr_pos += 1;
        }

        for (int i = 0; i < steps.Length; i++)
        {
            //Console.WriteLine("\t POS:{0} 에서 {1}일권 선택", curr_pos, steps[i]);
            //Console.WriteLine("\t push {0}", steps[i]);

            next_pos = curr_pos + steps[i];
            //Console.WriteLine("\t enqueue next_pos: {0}", next_pos);
            queue.Enqueue(next_pos);
        }

    }

    //if (stack.Count > 0)
    //{
    //    Console.WriteLine("--- 방향 탐색 끝");
    //    stack.Pop();
    //}

}

move(0, stack);

//int min_cost = 1000000000;
//foreach (var result in results)
//{
//    int cost = 0;
//    int cnt_one = 0;
//    int cnt_three = 0;
//    int cnt_five = 0;

//    int cnt_coupon = 0;

//    for (int i = 0; i < result.Length; i++)
//    {
//        if (result[i] == 1)
//        {
//            cnt_one += 1;
//        }
//        if (result[i] == 3)
//        {
//            cnt_three += 1;
//            cnt_coupon += 1;
//        }
//        if (result[i] == 5)
//        {
//            cnt_five += 1;
//            cnt_coupon += 2;
//        }
//    }

//    int discount_day_by_coupon = (int)(cnt_coupon / 3);

//    cnt_one -= discount_day_by_coupon;
//    if (cnt_one < 0)
//    {
//        cnt_one = 0;
//    }


//    cost = (COST_ONE * cnt_one) +
//            (COST_THREE * cnt_three) +
//            (COST_FIVE * cnt_five);

//    //Console.WriteLine("{0}", string.Join(" ", result));
//    //Console.WriteLine("ONE:{0} THREE:{1} FIVE:{2} COUPON:{3} DISCOUNT:{4}   COST:{5,8:N0}",
//    //    cnt_one, cnt_three, cnt_five, cnt_coupon, discount_day_by_coupon, cost);

//    if (cost < min_cost)
//    {
//        min_cost = cost;
//        Console.WriteLine(" 최소비용 갱신. {0:N0} -> {1:N0}", min_cost, cost);
//    }
//}


//Console.WriteLine("가능한 결과 조합 수: {0}", results.Count);
//Console.WriteLine("최소비용  : {0,8:N0} 원", min_cost);


#else


int n = 0; // 여름방학 일수 (1 <= n <= 100)
int m = 0; // 리조트에 갈 수 없는 날의 수 (0 <= M <= n)

const int COST_ONE = 10000;
const int COST_THREE = 25000;
const int COST_FIVE = 37000;

string input;
string[] words;
//string input = Console.ReadLine();
//string[] words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
//int.TryParse(words[0], out n);
//int.TryParse(words[1], out m);
//input = Console.ReadLine();


//n = 13;
//m = 5;
//input = "4 6 7 11 12";

n = 50;
m = 10;
input = "3 5 7 11 15 16 22 23 24 34";


words = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
int[] maze = new int[n + 1];
foreach (var word in words)
{
    int val = int.Parse(word);
    maze[val] = 0xff;
}
for (int i = 0; i < maze.Length; i++)
{
    Console.WriteLine(" day:{0,2}  {1,2:x}", i, maze[i]);
}

int[] steps = new int[3] { 1, 3, 5 };

Stack<int> stack = new Stack<int>();
List<int[]> results = new List<int[]>();



void move (int pos, Stack<int> stack)
{
    if (pos > n)
    {
        //Console.WriteLine("\t ---------------------- END --- stack: {0}", string.Join(" ", stack));
        results.Add(stack.Reverse().ToArray());
        return;
    }

    //Console.WriteLine("move (curr_pos: {0})", pos);
    if (maze[pos] == 0xff)
    {
        //Console.WriteLine("{0}번째 날은 갈수 없는 날임. ", pos);
        pos++;
        move(pos, stack);
    }
    else
    {

        for (int i = 0; i < steps.Length; i++)
        {
            //Console.WriteLine("\t POS:{0} 에서 {1}일권 선택", pos, steps[i]);
            //Console.WriteLine("\t push {0}", steps[i]);
            stack.Push(steps[i]);

            pos += steps[i];
            move(pos, stack);

            stack.Pop();
        }   

    }
    //if (stack.Count > 0)
    //{
    //    Console.WriteLine("--- 방향 탐색 끝");
    //    stack.Pop();
    //}

}

move(0, stack);

int min_cost = 1000000000;
foreach (var result in results)
{
    int cost = 0;
    int cnt_one = 0;
    int cnt_three = 0;
    int cnt_five = 0;

    int cnt_coupon = 0;

    for (int i = 0; i < result.Length; i++)
    {
        if (result[i] == 1)
        {
            cnt_one += 1;
        }
        if (result[i] == 3)
        {
            cnt_three += 1;
            cnt_coupon += 1;
        }
        if (result[i] == 5)
        {
            cnt_five += 1;
            cnt_coupon += 2;
        }
    }

    int discount_day_by_coupon = (int)(cnt_coupon / 3);

    cnt_one -= discount_day_by_coupon;
    if (cnt_one < 0)
    {
        cnt_one = 0;
    }


    cost = (COST_ONE * cnt_one) +
            (COST_THREE * cnt_three) +
            (COST_FIVE * cnt_five);

    //Console.WriteLine("{0}", string.Join(" ", result));
    //Console.WriteLine("ONE:{0} THREE:{1} FIVE:{2} COUPON:{3} DISCOUNT:{4}   COST:{5,8:N0}",
    //    cnt_one, cnt_three, cnt_five, cnt_coupon, discount_day_by_coupon, cost);

    if (cost < min_cost)
    {
        min_cost = cost;
        Console.WriteLine(" 최소비용 갱신. {0:N0} -> {1:N0}", min_cost, cost);
    }
}


Console.WriteLine("가능한 결과 조합 수: {0}", results.Count);
Console.WriteLine("최소비용  : {0,8:N0} 원", min_cost);

#endif