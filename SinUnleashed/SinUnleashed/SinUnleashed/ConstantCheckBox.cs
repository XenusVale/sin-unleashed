using System;
using System.Windows.Forms;
using System.Drawing;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConstantCheckBox
{
    class ConstantCheckBox : CheckBox
    {
        private int theboxSize = 18;
        private int boxX = 0;
        private int boxY = 0;
        private int textX = 14;
        private int textY = 4;
        private String text = "";

        /* public event EventHandler Checker;

         protected override void OnCheckedChanged(EventArgs e)
         {
             base.OnCheckedChanged(e);

             if(Checked)
             {
                 CheckOn(e);
             }
         }

         protected virtual void CheckOn(EventArgs e)
         {
             if(Checker != null)
             {
                 Checker(this, e);
                 isOn++;
             }
         }*/

        public String DisplayText
        {
            get { return text; }
            set { text = value; Invalidate(); }
        }

        public int TextLocationX
        {
            get { return textX; }
            set { textX = value; Invalidate(); }
        }

        public int TextLocationY
        {
            get { return textY; }
            set { textY = value; Invalidate(); }
        }

        public int BoxSize
        {
            get { return theboxSize; }
            set { theboxSize = value; Invalidate(); }
        }

        public int BoxLocationX
        {
            get { return boxX; }
            set { boxX = value; Invalidate(); }
        }

        public int BoxLocationY
        {
            get { return boxY; }
            set { boxY = value; Invalidate(); }
        }

        public ConstantCheckBox()
        {
            this.ForeColor = Color.White;
            this.AutoSize = false;
        }
    }
}
