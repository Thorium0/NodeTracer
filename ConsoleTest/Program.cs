using System;
using ConsoleTest;
using NodeTracer;

namespace main {

    class Program {
        private static bool printLengthOfConnections = true;
        private static readonly PathType pathType = PathType.Length;
        private static Route route = new Route(); 
        private static List<Route> triedRoutes = new List<Route>();
        private enum PathType {
            First,
            Jumps,
            Length
        }
        static public void Main(String[] args) { 
            Random random = new Random();
            List<Node> nodes = new List<Node>();

            /*
            for (int x = 0; x <= 100; x++) {
                nodes.Add(new Node(x));
                if (x > 1) {
                    nodes[x].addConnection(nodes[x-1], random.Next(1, 10));
                    
                    for (int z = 0; z <= random.Next(5); z++) {
                        int temp = random.Next(0, x - 2);
                        
                        nodes[x].addConnection(nodes[temp], random.Next(1, 10));
                        
                    }
                }
                
            }
            */
            
            
            Node test1 = new Node(1);
            Node test2 = new Node(2);
            Node test3 = new Node(3);
            Node test4 = new Node(4);
            Node test5 = new Node(5);
            Node test6 = new Node(6);
            Node test7 = new Node(7);
            Node test8 = new Node(8);
            Node test9 = new Node(9);
            Node test10 = new Node(10);


            test1.addConnection(test2, 2);
            test2.addConnection(test3, 6);
            test3.addConnection(test4, 3);
            test3.addConnection(test10, 30);
            test3.addConnection(test7, 4);
            test4.addConnection(test5, 1);
            test7.addConnection(test8, 1);
            test7.addConnection(test9, 1);
            test10.addConnection(test5, 1);
            






            getRoute(test5, test7);

            //getRoute(nodes[1], nodes[nodes.Count-1]);


            


            Console.Write("route: ");

            Route _route = new Route();

            switch (pathType) {
                case PathType.First:
                    _route = route;
                    break;

                case PathType.Length:
                    _route = getShortestRoute(triedRoutes);
                    break;
                
                case PathType.Jumps:
                    _route = getClosestRoute(triedRoutes);
                    break;  
            }
            
               
           for (int i = 0; i < _route.nodeList.Count; i++) {
                Node tempNode = _route.nodeList.ElementAt(i);
                Console.Write(tempNode.Id);
                if (i < _route.nodeList.Count - 1) {
                    if (printLengthOfConnections) {
                        for (int j = 0; j < tempNode.getLengthForConnection(_route.nodeList.ElementAt(i + 1)); j++) {
                            Console.Write("=");
                        }
                    } else {
                        Console.Write("=");
                    }
                }
            }
           
            switch(pathType) {
                case PathType.First:
                case PathType.Length:
                    Console.WriteLine("\nlength: " + _route.totalLength);
                    break;
                case PathType.Jumps:
                    Console.WriteLine("\njumps: " + _route.jumps);
                    break;
            }
            
                    
                      
            
            
            Console.ReadKey();
        }


        private static Route getShortestRoute(List<Route> triedRoutes) {
            Route lowest = triedRoutes[0];
            foreach(Route _route in triedRoutes) {
                if (_route.totalLength < lowest.totalLength) { 
                    lowest = _route;
                }
            }
            return lowest;
        }

        private static Route getClosestRoute(List<Route> triedRoutes) {
            Route lowest = triedRoutes[0];
            foreach (Route _route in triedRoutes) {
                if (_route.jumps < lowest.jumps) {
                    lowest = _route;
                }
            }
            return lowest;
        }



        private static int calcRouteLength(Route route) {
            int length = 0;
            for (int i = 0; i < route.nodeList.Count-1; i++) {
                length += route.nodeList[i].getLengthForConnection(route.nodeList[i+1]);
            } 
            return length;
        }



        private static void getRoute(Node startNode, Node endNode) {
            Console.WriteLine("Tracing route from node " + startNode.Id + " to node " + endNode.Id);
            try {
                traceRoute(startNode, endNode);
            } catch {
                Console.WriteLine("Error: StartNode has no connection to EndNode");
                return;
            }
        }


        private static bool traceRoute(Node currentNode, Node endNode) {
            route.nodeList.Add(currentNode);
            
            foreach ((Node node, int _length) in currentNode.getConnections()) {
                if (node.Id == endNode.Id) {
                    switch (pathType) {
                        case PathType.First:
                            route.nodeList.Add(endNode);
                            route.totalLength = calcRouteLength(route);
                            return true;
                        case PathType.Jumps:
                        case PathType.Length:
                            if (!triedRoutes.Contains(route)) {
                                Route cloneRoute = route;
                                cloneRoute.nodeList = new List<Node>(route.nodeList);
                                cloneRoute.nodeList.Add(endNode);
                                cloneRoute.totalLength = calcRouteLength(cloneRoute);
                                cloneRoute.jumps = cloneRoute.nodeList.Count - 1;
                                
                                triedRoutes.Add(cloneRoute);

                                break;
                            } else {
                                return true;
                            }
                    }
                }
                if (route.nodeList.Contains(node)) {
                    continue;
                }

                if (node != endNode) {
                    bool result = traceRoute(node, endNode);
                    if (result) {
                        return true;
                    }
                }

                
            }
            route.nodeList.Remove(currentNode);
      
            
            return false;
        }

        
    }
}