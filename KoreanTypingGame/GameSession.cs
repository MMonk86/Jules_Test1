using System;
using System.Diagnostics;

namespace KoreanTypingGame
{
    public class GameSession
    {
        public int Score { get; private set; }
        public int TotalAttempts { get; private set; }
        public string CurrentTarget { get; private set; }

        private WordProvider _wordProvider;
        private Stopwatch _timer;

        public GameSession()
        {
            _wordProvider = new WordProvider();
            _timer = new Stopwatch();
            Score = 0;
            TotalAttempts = 0;
            NextWord();
        }

        public void StartGame()
        {
            Score = 0;
            TotalAttempts = 0;
            _timer.Restart();
            NextWord();
        }

        public void StopGame()
        {
            _timer.Stop();
        }

        public bool CheckInput(string input)
        {
            TotalAttempts++;
            if (input.Trim() == CurrentTarget)
            {
                Score += 10; // 10 points per correct word
                NextWord();
                return true;
            }
            return false;
        }

        private void NextWord()
        {
            CurrentTarget = _wordProvider.GetRandomWord();
        }

        public TimeSpan GetElapsedTime()
        {
            return _timer.Elapsed;
        }

        public double GetWPM()
        {
            double minutes = _timer.Elapsed.TotalMinutes;
            if (minutes <= 0) return 0;
            // Rough estimation: Score / 10 is number of words, divided by minutes
            return (Score / 10.0) / minutes;
        }
    }
}
