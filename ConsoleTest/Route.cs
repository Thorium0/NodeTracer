using ConsoleTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NodeTracer {
    internal struct Route {
        public List<Node> nodeList = new List<Node>();
        public int totalLength = 0;
        public int jumps = 0;
    }
}
