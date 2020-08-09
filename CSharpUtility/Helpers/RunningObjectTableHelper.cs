using EnvDTE;
using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace CSharpUtility.Helpers
{
    // Example on code project https://www.codeproject.com/Articles/7984/Automating-a-specific-instance-of-Visual-Studio-NE
    class RunningObjectTableHelper
    {
        /// <summary>
        /// Running Object Table Class
        /// </summary>
        public class ROTHelper
        {
            /// <summary>
            /// Get a snapshot of the running object table (ROT).
            /// </summary>
            [DllImport("ole32.dll")]
            public static extern int GetRunningObjectTable(int reserved,
                                         out IRunningObjectTable prot);

            [DllImport("ole32.dll")]
            public static extern int CreateBindCtx(int reserved,
                                          out IBindCtx ppbc);

            /// <summary>
            /// Get running object table as key/value pair
            /// </summary>
            /// <returns></returns>
            public static Hashtable GetRunningObjectTable()
            {
                Hashtable result = new Hashtable();

                IntPtr numFetched = new IntPtr();
                IRunningObjectTable runningObjectTable;
                IEnumMoniker monikerEnumerator;
                IMoniker[] monikers = new IMoniker[1];

                GetRunningObjectTable(0, out runningObjectTable);
                runningObjectTable.EnumRunning(out monikerEnumerator);
                monikerEnumerator.Reset();

                while (monikerEnumerator.Next(1, monikers, numFetched) == 0)
                {
                    IBindCtx ctx;
                    CreateBindCtx(0, out ctx);

                    string runningObjectName;
                    monikers[0].GetDisplayName(ctx, null, out runningObjectName);

                    object runningObjectVal;
                    runningObjectTable.GetObject(monikers[0], out runningObjectVal);

                    result[runningObjectName] = runningObjectVal;
                }

                return result;
            }

            /// <summary>
            /// Get IDE instances according to given type
            /// </summary>
            /// <param name="openSolutionsOnly: instances only with open project"></param>
            /// <param name="_ide: type of ide"></param>
            /// <returns>Hashtable contains running object instances</returns>
            public static Hashtable GetIDEInstances(bool openSolutionsOnly, IDE _ide)
            {
                Hashtable runningIDEInstances = new Hashtable();
                Hashtable runningObjects = GetRunningObjectTable();

                IDictionaryEnumerator rotEnumerator = runningObjects.GetEnumerator();
                while (rotEnumerator.MoveNext())
                {
                    // C# 8.0 switch syntax
                    string ideROTName = _ide switch
                    {
                        IDE.Rhapsody => "Rhapsody",
                        IDE.VisualStudio => "!VisualStudio.DTE",
                        _ => string.Empty
                    };

                    string candidateName = (string)rotEnumerator.Key;
                    if (!candidateName.StartsWith(ideROTName))
                        continue;

                    object ide;
                    switch (_ide)
                    {
                        case IDE.Rhapsody:
                            //ide = rotEnumerator.Value as rhapsody.RPApplication;
                            //if (openSolutionsOnly)
                            //{
                            //    // Unboxing is necessary in here
                            //    if (((rhapsody.RPApplication)ide).activeProject() != null)
                            //    {
                            //        runningIDEInstances[candidateName] = ide;
                            //    }
                            //}
                            //else
                            //{
                            //    runningIDEInstances[candidateName] = ide;
                            //}
                            //break;
                        case IDE.VisualStudio:
                            ide = rotEnumerator.Value as _DTE;
                            if (openSolutionsOnly)
                            {
                                // Unboxing is necessary in here
                                if (!string.IsNullOrEmpty(((_DTE)ide).Solution.FullName))
                                {
                                    string temp = ((_DTE)ide).Solution.FullName;
                                    runningIDEInstances[candidateName] = ide;
                                }
                            }
                            else
                            {
                                runningIDEInstances[candidateName] = ide;
                            }
                            break;
                        default:
                            break;
                    }
                }
                return runningIDEInstances;
            }

            /// <summary>
            /// IDE Types
            /// </summary>
            public enum IDE
            {
                Rhapsody,
                VisualStudio
            }
        }
    }
}
