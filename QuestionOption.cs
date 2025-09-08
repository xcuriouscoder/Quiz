using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz
{
    public class QuestionOption
    {
        public string Option { get; set; }

        public bool IsAnswer { get; set; }

        public override string ToString()
        {
            return this.Option;
        }
    }
}
