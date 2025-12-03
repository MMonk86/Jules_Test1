using System;
using System.Collections.Generic;

namespace KoreanTypingGame
{
    public class WordProvider
    {
        private List<string> words;
        private Random random;

        public WordProvider()
        {
            random = new Random();
            InitializeWords();
        }

        private void InitializeWords()
        {
            words = new List<string>
            {
                // Basic words
                "안녕하세요",
                "반갑습니다",
                "대한민국",
                "프로그래밍",
                "컴퓨터",
                "개발자",
                "키보드",
                "모니터",
                "마우스",
                "사랑합니다",

                // Short sentences
                "가는 말이 고와야 오는 말이 곱다",
                "나의 꿈은 훌륭한 개발자가 되는 것입니다",
                "타자 연습을 열심히 합시다",
                "오늘 점심은 무엇을 먹을까요",
                "하늘이 무너져도 솟아날 구멍이 있다",
                "티끌 모아 태산",
                "늦었다고 생각할 때가 가장 빠르다",
                "시작이 반이다",
                "천 리 길도 한 걸음부터",
                "C# 프로그래밍은 재미있습니다"
            };
        }

        public string GetRandomWord()
        {
            if (words.Count == 0) return "끝";
            int index = random.Next(words.Count);
            return words[index];
        }
    }
}
