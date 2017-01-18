using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eureka.AsynScriptAssist
{
    struct BreakPoint
    {
        String filePath;
        int lineNumber;
    }

    class AsynScriptAssist
    {
        public AsynScriptAssist(EnvDTE.DTE dte)
        {
            m_dte = dte;
        }

        public bool load()
        {
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
            return false;
        }

        private List<BreakPoint> m_breakPoints;
        private int m_breakPointIndex = -1;
        private EnvDTE.DTE m_dte;
    }
}
