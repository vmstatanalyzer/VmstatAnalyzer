using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VmstatAnalyzer.Domain
{
    class UICallback
    {
        public static void Callback(Control control, Action action)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action);
            }
            else
            {
                action();
            }
        }

        public static void Callback<T>(Control control, Action<T> action, T arg)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, arg);
            }
            else
            {
                action(arg);
            }
        }

        public static void Callback<T1, T2>(Control control, Action<T1, T2> action, T1 arg1, T2 arg2)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(action, arg1, arg2);
            }
            else
            {
                action(arg1, arg2);
            }
        }
    }
}
