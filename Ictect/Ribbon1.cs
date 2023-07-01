using Ictect.Helpers;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.Word;
using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Ictect
{
    public partial class Ribbon1
    {
        private static bool button2EvenPress = true; 
        private void Ribbon1_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button4_Click(object sender, RibbonControlEventArgs e)
        {
            var paragraphs = Globals.ThisAddIn.Application.ActiveDocument.Paragraphs;

            var th = new Thread(new ParameterizedThreadStart(TestRibbonHelper.ChangeMiddleWord));

            th.SetApartmentState(ApartmentState.STA);
            th.Start(paragraphs);
        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            var application = Globals.ThisAddIn.Application;

            TestRibbonHelper.FindAndHiglightText("of", application);
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            var word = "of";
            button2EvenPress = !button2EvenPress;


            var application = Globals.ThisAddIn.Application;

            var find = application.ActiveDocument.Content.Find;

            if (button2EvenPress)
            {
                var range = application.ActiveDocument.Content;

                var th = new Thread(new ParameterizedThreadStart(TestRibbonHelper.FindAndUnderlineWordsBeforeOf));

                th.SetApartmentState(ApartmentState.STA);
                th.Start(range);

            }
            else
            {
                find.ClearFormatting();
                find.Replacement.ClearFormatting();
                find.Replacement.Font.AllCaps = 1;

                find.Execute(FindText: "of",
                    Replace:WdReplace.wdReplaceAll);
            }
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            var selection = comboBox1.Text.ToLower().Trim();

            if(selection == "document")
            {
                var th = new Thread(new ParameterizedThreadStart(TestRibbonHelper.ReverseWords));

                var range = Globals.ThisAddIn.Application.ActiveDocument.Content;

                th.SetApartmentState(ApartmentState.STA);
                th.Start(range);
            }
            else
            {
                var range = Globals.ThisAddIn.Application.Selection.Paragraphs.First.Range;
                TestRibbonHelper.ReverseWords(range);
            }
        }

        private void comboBox1_TextChanged(object sender, RibbonControlEventArgs e)
        { 
        }
    }
}
