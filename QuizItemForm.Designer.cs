using System;
using System.Drawing.Design;

namespace Quiz
{
    partial class QuizItemForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void ShowResults()
        {
            this.InResultsMode = true;
            this.CurrentQuestionNumber = 0;

            SetTextBasedOnResults();
            this.SetFieldsForCurrentItem();

            this.ResultsLabel.Visible = true;

            var total = this.QuizItems.Count;
            var correct = this.QuizItems.Where(it => it.WasAnsweredCorrectly).Count();

            var pct = ((float)correct * 100) / (float)total;

            this.ResultsLabel.Text = $"Final Score: {correct} correct out of {total}.  {pct.ToString("F1")}%";
        }

        private void SetTextBasedOnResults()
        {
            foreach (var qi in this.QuizItems)
            {
                if(!qi.WasAnsweredCorrectly)
                {
                    var correctOption = qi.Options.First(op => op.IsAnswer);
                    var selectedOption = qi.Options.First(op => op.Option == qi.SelectedAnswer);
                    correctOption.Option = "Correct ==> " + correctOption.Option;
                    selectedOption.Option = "You Selected ==> " + selectedOption.Option;
                }
            }
        }

        private void SetFieldsForCurrentItem()
        {
            var item = this.QuizItems[this.CurrentQuestionNumber];

            this.QuestionLabel.Text = item.Question;
            this.OptionsList.DataSource = item.Options;
            //            this.OptionsList.DataSource = item.Options.Select(op => op.Option).OrderBy(x => random.Next()).ToList();
            this.OptionsList.SelectedItems.Clear();

            SetColorForResult(item);
        }

        private void SetColorForResult(QuizItem item)
        {
            if (!string.IsNullOrEmpty(item.SelectedAnswer))
            {
                if (item.WasAnsweredCorrectly)
                {
                    this.OptionsList.BackColor = Color.LightGreen;
                }
                else
                {
                    this.OptionsList.BackColor = Color.LightPink;
                }
            }
        }


        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            QuestionLabel = new Label();
            OptionsList = new ListBox();
            NextButton = new Button();
            ResultsLabel = new Label();
            SuspendLayout();
            // 
            // QuestionLabel
            // 
            QuestionLabel.AutoSize = true;
            QuestionLabel.Location = new Point(14, 6);
            QuestionLabel.Name = "QuestionLabel";
            QuestionLabel.Size = new Size(125, 25);
            QuestionLabel.TabIndex = 0;
            QuestionLabel.Text = "QuestionLabel";
            // 
            // OptionsList
            // 
            OptionsList.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            OptionsList.FormattingEnabled = true;
            OptionsList.ItemHeight = 25;
            OptionsList.Location = new Point(12, 56);
            OptionsList.Name = "OptionsList";
            OptionsList.Size = new Size(776, 204);
            OptionsList.TabIndex = 1;
            // 
            // NextButton
            // 
            NextButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            NextButton.Location = new Point(14, 277);
            NextButton.Name = "NextButton";
            NextButton.Size = new Size(112, 34);
            NextButton.TabIndex = 2;
            NextButton.Text = "Next";
            NextButton.UseVisualStyleBackColor = true;
            NextButton.Click += NextButton_Click;
            // 
            // ResultsLabel
            // 
            ResultsLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ResultsLabel.AutoSize = true;
            ResultsLabel.Location = new Point(233, 282);
            ResultsLabel.Name = "ResultsLabel";
            ResultsLabel.Size = new Size(67, 25);
            ResultsLabel.TabIndex = 3;
            ResultsLabel.Text = "Results";
            ResultsLabel.Visible = false;
            // 
            // QuizItemForm
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 323);
            Controls.Add(ResultsLabel);
            Controls.Add(NextButton);
            Controls.Add(OptionsList);
            Controls.Add(QuestionLabel);
            Name = "QuizItemForm";
            Text = "QuizItemForm";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label QuestionLabel;
        private ListBox OptionsList;
        private Button NextButton;
        private Label ResultsLabel;
    }
}