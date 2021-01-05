using System;
using System.Collections.Generic;
using System.Text;

namespace CSharpUtility.Helpers
{
    class DoorsDXLHelper
    {
        /// <summary>
        /// Run given dxl script on Doors COM Interface
        /// </summary>
        /// <param name="query"></param>
        public void Eval(string query)
        {
            try
            {
                DoorsDXLClass obj = new DoorsDXLClass();
                // Use obj.result to communicate with dxl results
                // oleSetResult, oleGetResult

                obj.runStr("print \"Hello\" \"\n\"; oleSetResult \"Yes\"");
                obj.runStr("print \"\n\"");
                obj.runStr(query);
            }
            catch (System.Exception e) { }
        }

    }
}
