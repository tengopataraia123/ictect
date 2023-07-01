using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Word;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ictect.Helpers
{
    public static class TestRibbonHelper
    {
        public static void FindAndHiglightText(string text,Application application)
        {
            Find findObject = application.ActiveDocument.Content.Find;
            findObject.ClearFormatting();
            findObject.Replacement.ClearFormatting();
            findObject.Replacement.Highlight = 1;


            findObject.Execute(FindText: "of",
                Replace: WdReplace.wdReplaceAll);

            int count = 0;
            while (findObject.Found)
            {
                count++;
                findObject.Execute(FindText: "of");
            }

            count -= count > 0 ? 1 : 0;

            System.Windows.Forms.MessageBox.Show($"Count of: {count}");
        }

        public static void FindAndUnderlineWordsBeforeOf(object rangeObj)
        {
            var range = (Range)rangeObj;
            for (int i = 1; i <= range.Words.Count; i++)
            {
                var text = range.Words[i];
                if (text.Text.ToLower().Trim() == "of")
                {
                    if (i > 2)
                        range.Words[i - 1].Underline = WdUnderline.wdUnderlineSingle;
                    range.Words[i].Font.Reset();
                }
            }
        }

        public static void ReverseWords(object rangeObj)
        {
            var range = (Range)rangeObj;
            var count = range.Words.Count;

            var leftPos = 1;
            var rightPos = range.Words.Count;

            while (leftPos < rightPos)
            {
                var left = range.Words[leftPos];
                while(left.Text.Trim().Length == 0)
                {
                    leftPos++;
                    left = range.Words[leftPos];
                }

                var right = range.Words[rightPos];
                while (right.Text.Trim().Length == 0)
                {
                    rightPos--;
                    right = range.Words[rightPos];
                }

                var temp = right.Text.EndsWith(" ") ? right.Text: right.Text + " ";
                right.Text = left.Text.EndsWith(" ") ? left.Text : left.Text + " ";
                left.Text = temp;
                var temp1 = right.Text;
                leftPos++;
                rightPos--;
            }

        }

        public static void ChangeMiddleWord(object paragraphList)
        {
            var paragraphs = (Paragraphs)paragraphList;

            for (int i = paragraphs.Count; i > 1; i--)
            {
                var thisCount = paragraphs[i].Range.Words.Count;
                var prevCount = paragraphs[i - 1].Range.Words.Count;

                paragraphs[i].Range.Words[thisCount / 2].Text = paragraphs[i - 1].Range.Words[prevCount / 2].Text;
            }
        }
    }
}
