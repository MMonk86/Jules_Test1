using System;

namespace KoreanTypingGame
{
    class ConsoleTestRunner
    {
        // This method is just for testing if the user wants to run in console.
        // For WinForms, the entry point will be in a different file or this main needs to be adjusted.
        // However, standard WinForms apps use 'Program.cs' as entry point too.
        // I will name this ConsoleRunner to avoid conflict if I create a WinForms Program.cs later.
        // But since this is a simple example, I'll put the console logic here.

        static void MainConsole(string[] args)
        {
            GameSession game = new GameSession();
            Console.WriteLine("=== 한글 타자 연습 게임 (콘솔 버전) ===");
            Console.WriteLine("종료하려면 'exit'를 입력하세요.");
            Console.WriteLine("엔터를 눌러 시작합니다...");
            Console.ReadLine();

            game.StartGame();

            while (true)
            {
                Console.Clear();
                Console.WriteLine($"점수: {game.Score} | 시간: {game.GetElapsedTime().ToString(@"mm\:ss")}");
                Console.WriteLine("------------------------------------------------");
                Console.WriteLine($"제시어: {game.CurrentTarget}");
                Console.Write("입력: ");

                string input = Console.ReadLine();

                if (input == "exit") break;

                bool isCorrect = game.CheckInput(input);

                if (isCorrect)
                {
                    Console.WriteLine("정답입니다!");
                }
                else
                {
                    Console.WriteLine("오답입니다.");
                }
                // Small pause to let user see result
                System.Threading.Thread.Sleep(500);
            }
        }
    }
}
