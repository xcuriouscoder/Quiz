using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Quiz
{
    public partial class QuizItemForm : Form
    {
        private List<QuizItem> QuizItems { get; set; }
        public bool InResultsMode { get; private set; }
        public int CurrentQuestionNumber { get; private set; }

        private string QuizFilePath { get; set; }
        private string QuizFolder { get; set; }

        public QuizItemForm()
        {
            var filePicker = new OpenFileDialog()
            {
                Filter = "Quiz Files (*.json)|*.json",
                Title = "Open Quiz file"
            };

            if (filePicker.ShowDialog() == DialogResult.OK)
            {
                var filePath = filePicker.FileName;

                this.QuizFilePath = filePath;
                this.QuizFolder = System.IO.Path.GetDirectoryName(filePath);

                var quizItems = RandomizeOrder(filePath);

                this.QuizItems = quizItems;
                //                RunQuiz(quizItems);
            }

            InitializeComponent();

            this.SetFieldsForCurrentItem();
        }

        public QuizItemForm(List<QuizItem> quiz)
        {
            this.QuizItems = quiz;

            InitializeComponent();

            this.SetFieldsForCurrentItem();
        }

        private static List<QuizItem> RandomizeOrder(string filePath)
        {
            var random = new Random();

            var contents = File.ReadAllText(filePath);
            var quizItems = JsonSerializer.Deserialize<List<QuizItem>>(contents);

            quizItems = quizItems.OrderBy(item => random.Next()).ToList();

            foreach (var qi in quizItems)
            {
                qi.Options = qi.Options.OrderBy(i => random.Next()).ToList();
            }

            return quizItems;
        }

        private void NextButton_Click(object sender, EventArgs e)
        {
            var currQ = OptionsList.SelectedItem as QuestionOption;
            if (currQ != null)
            {
                var qItem = this.QuizItems[this.CurrentQuestionNumber];
                qItem.SelectedAnswer = currQ.Option;
                qItem.WasAnsweredCorrectly = currQ.IsAnswer;

            }

            if (this.CurrentQuestionNumber < QuizItems.Count - 1)
            {
                this.CurrentQuestionNumber++;
                this.SetFieldsForCurrentItem();

                if (this.CurrentQuestionNumber == QuizItems.Count - 1 && this.InResultsMode)
                {
                    this.NextButton.Text = "Close";
                }

            }
            else if (!this.InResultsMode)
            {
                //                this.InResultsMode = true;
                this.ShowResults();
            }
            else
            {
                this.Close();
            }
        }

        private void ExportButton_Click(object sender, EventArgs e)
        {
            var fails = this.QuizItems.Where(q => !q.WasAnsweredCorrectly).ToList();
            foreach(var fail in fails)
            {
                foreach(var opt in fail.Options)
                {
                    if(opt.IsAnswer)
                    {
                        opt.Option = opt.Option.Replace(CorrectIdentifierText, "");
                    }
                    else if(opt.Option.StartsWith(IncorrectIdentifierText))
                    {
                        opt.Option = opt.Option.Replace(IncorrectIdentifierText, "");
                    }
                }

                fail.SelectedAnswer = null;
                fail.WasAnsweredCorrectly = false;
            }

            var json = JsonSerializer.Serialize(fails, new JsonSerializerOptions() { WriteIndented = true });
            var failFile = System.IO.Path.Combine(this.QuizFolder, "FailedItems.json");
            File.WriteAllText(failFile, json);
            MessageBox.Show($"Failures written to {failFile}" );
        }
    }
}
