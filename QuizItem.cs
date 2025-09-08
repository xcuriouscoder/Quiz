using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class QuizItem
    {
        public string Question { get; set; }

        public List<QuestionOption> Options { get;set;}

        public string SelectedAnswer { get; set; }

        public bool WasAnsweredCorrectly { get; set; }
    }
}
