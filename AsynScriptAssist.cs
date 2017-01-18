using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Eureka.AsynScriptAssist
{
    struct BreakPoint
    {
        public String filePath;
        public String lineNumber;
    }

    class AsynScriptAssist
    {
        public AsynScriptAssist(AsynScriptAssistPackage serviceProvider, EnvDTE.DTE dte)
        {
            m_serviceProvider = serviceProvider;
            m_dte = dte;
        }

        public bool load()
        {
            var solution = m_dte.Solution;
            if (solution == null)
            {
                return false;
            }

            m_breakPoints = null;
            var directory = System.IO.Path.GetDirectoryName(solution.FullName);
            var configFile = directory + "\\asysAssist.xml";
            if (!File.Exists(configFile))
            {
                return false;
            }

            try{
                XmlDocument doc = new XmlDocument();
                doc.Load(configFile);

                XmlNodeList logs = doc.DocumentElement.SelectNodes("/Assist/Log");

                foreach (XmlNode log in logs)
                {
                    string logFile = log.Attributes["name"].Value;
                    var fullLogFilePath = directory + "\\" + logFile;

                    System.IO.StreamReader file = new System.IO.StreamReader(fullLogFilePath);
                    string line;
                    while ((line = file.ReadLine()) != null)
                    {
                       var items = line.Split(null as string[], StringSplitOptions.RemoveEmptyEntries);
                       if (items.Length >= 3)
                       {
                           BreakPoint breakPoint;
                           breakPoint.filePath = items[0];
                           breakPoint.lineNumber = items[2];

                           if (m_breakPoints == null)
                           {
                               m_breakPoints = new List<BreakPoint>();
                           }
                           m_breakPoints.Add(breakPoint);
                       }
                    }

                    file.Close();

                    break;
                }
              }
            catch (Exception e)
            {
                e.ToString();
            }

            return false;
        }

        public bool next()
        {
            if (m_breakPoints == null)
            {
                if (!load())
                {
                    return false;
                }
            }

            m_breakPointIndex++;
            if (m_breakPointIndex >= m_breakPoints.Count ||
                m_breakPointIndex < 0)
            {
                m_breakPointIndex = 0;
            }

            return gotoCurBreakPoint();
        }

        public bool prec()
        {
            if (m_breakPoints == null)
            {
                if (!load())
                {
                    return false;
                }
            }

            m_breakPointIndex--;
            if (m_breakPointIndex >= m_breakPoints.Count ||
                m_breakPointIndex < 0)
            {
                m_breakPointIndex = 0;
            }

            return gotoCurBreakPoint();
        }

        private bool gotoCurBreakPoint()
        {
            if (m_breakPointIndex >= m_breakPoints.Count ||
               m_breakPointIndex < 0)
            {
                return false;
            }

            var breakPoint = m_breakPoints[m_breakPointIndex];
            Microsoft.VisualStudio.Shell.VsShellUtilities.OpenDocument(m_serviceProvider, breakPoint.filePath);
            m_dte.ExecuteCommand("Edit.Goto", breakPoint.lineNumber);

            return true;
        }

        private List<BreakPoint> m_breakPoints;
        private int m_breakPointIndex = -1;
        private EnvDTE.DTE m_dte;
        private AsynScriptAssistPackage m_serviceProvider;
    }
}
