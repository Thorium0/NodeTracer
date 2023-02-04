using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest {
    
    public interface INode {
        int Id { get; }
        
    }
    
    internal class Node: INode {
        private int id;

        private Dictionary<Node, int> connections = new Dictionary<Node, int>();

        public bool addConnection(Node node, int length) {
            if (this.connections.ContainsKey(node)) {
                throw new InvalidOperationException("Node with that id already exists in connection-list.");
            }
            try {
                this.connections.Add(node, length);
                node.addConnection(this, length);
            } catch {
                return false;
            }
            return true;
        }

        public Dictionary<Node, int> getConnections() { 
            return this.connections; 
        }

        public int getLengthForConnection(Node node) {
            if (this.connections.ContainsKey(node)) {
                return this.connections[node];
            }
            return 0;
        }

        public int Id { 
            get { 
                return id; 
            } 
        }

        public Node(int id) {
            this.id = id;
        }

       


    }
}
