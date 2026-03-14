using System;
using System.Collections.Generic;
using System.Reflection;
using Priority_Queue;



namespace PushPullBlocksGame
{
    public class Algorithm
    {

        public static State DFS(State root)
        {
            if (root == null) return null;

            Stack<State> stack = new Stack<State>();    
            HashSet<State> visited = new HashSet<State>();
            stack.Push(root);


            int Counter = 0;
            while (stack.Count > 0) {
                Counter++;
                State node = stack.Pop();

                if(State.CheckGoal(node))
                {
                    Console.WriteLine("\t\t\t\t\tCounter For DFS = " + Counter.ToString());
                    node.IsLeaf = true;
                    node.IsGoal = true;
                    return node;
                }


                if (visited.Contains(node)) continue;
                visited.Add(node);


                node.derviedStates = State.GenerateStates(node);

                if (node.derviedStates == null || node.derviedStates.Count == 0) continue;

                foreach(State state in node.derviedStates)
                {
                    if (state == null) continue;
                    if (visited.Contains(state)) continue;
                    state.parent = node;
                    stack.Push(state);
                }

            }

            return null;
        }
        public static State BFS(State root) 
        {
            if (root == null) return null;

            Queue<State> queue = new Queue<State>();
            HashSet<State> visited = new HashSet<State>();
            queue.Enqueue(root);


            int Counter = 0;
            while (queue.Count > 0)
            {
                Counter++;
                State node = queue.Dequeue();

                if (State.CheckGoal(node))
                {
                    Console.WriteLine("\t\t\t\t\tCounter For BFS = " + Counter.ToString());
                    node.IsLeaf = true;
                    node.IsGoal = true;
                    return node;
                }


                if (visited.Contains(node)) continue;
                visited.Add(node);


                node.derviedStates = State.GenerateStates(node);

                if (node.derviedStates == null || node.derviedStates.Count == 0) continue;

                foreach (State state in node.derviedStates)
                {
                    if (state == null) continue;
                    if (visited.Contains(state)) continue;
                    state.parent = node;
                    queue.Enqueue(state);
                }

            }

            
            return null;
        }
      

        public static State UCS(State root)
        {

            if (root == null) return null;

            PriorityQueue<State> pq = new PriorityQueue<State>();
            root.cost = root.ComputePathCost();
            pq.Enqueue(root,root.cost);

            HashSet<State> visited = new HashSet<State>();

            int Counter = 0;
            while (pq.Count > 0)
            {
                Counter++;
                State node = pq.Dequeue();

                if (State.CheckGoal(node))
                {
                    Console.WriteLine("\t\t\t\t\tCounter For UCS = " + Counter.ToString());
                    node.IsLeaf = true;
                    node.IsGoal = true;
                    return node;
                }

                if (visited.Contains(node))
                    continue;

                visited.Add(node);
                node.derviedStates = State.GenerateStates(node);

                if (node.derviedStates == null) continue;

                foreach (State child in node.derviedStates)
                {
                    if (child == null || visited.Contains(child)) continue;
                    child.parent = node;
                    child.cost = child.ComputePathCost();
                    pq.Enqueue(child,child.cost);
                }
            }

            return null;
        }

        public static State HillClimbing(State root)
        {
            if (root == null) return null;

            State current = root;
            current.cost = current.ComputeHeuristicCost();

            int Counter = 0;
            while (true)
            {
                Counter++;
                if (State.CheckGoal(current))
                {
                    Console.WriteLine("\t\t\t\t\tCounter For Hill Climbing = " + Counter.ToString());
                    current.IsGoal = true;
                    current.IsLeaf = true;
                    return current;
                }

                HashSet<State> derivedStates = State.GenerateStates(current);
                if (derivedStates == null || derivedStates.Count == 0)
                {
                    Console.WriteLine("\t\t\t\t\tCounter For Hill Climbing = " + Counter.ToString());
                    return null; // stuck at local minimum
                }

                State bestState = null;
                int bestCost = int.MaxValue;

                foreach (State state in derivedStates)
                {
                    state.parent = current;
                    state.cost = state.ComputeHeuristicCost();

                    if (state.cost < bestCost)
                    {
                        bestCost = state.cost;
                        bestState = state;
                    }
                }

                // Stop if no improvement
                if (bestState == null || bestState.cost >= current.cost)
                {
                    Console.WriteLine("\t\t\t\t\tCounter For Hill Climbing = " + Counter.ToString());
                    return null;
                }

                current = bestState;
            }
        }

        public static State AStar(State root)
        {
            if (root == null) return null;

            PriorityQueue<State> pq = new PriorityQueue<State>();
            root.cost = root.ComputePathCost() + root.ComputeHeuristicCost();
            pq.Enqueue(root, root.cost);

            HashSet<State> visited = new HashSet<State>();

            int Counter = 0;
            while (pq.Count > 0)
            {
                Counter++;
                State node = pq.Dequeue();


                if (State.CheckGoal(node))
                {
                    Console.WriteLine("\t\t\t\t\tCounter For AStar = " + Counter.ToString());
                    node.IsLeaf = true;
                    node.IsGoal = true;
                    return node;
                }

                if (visited.Contains(node))
                    continue;

                visited.Add(node);
                node.derviedStates = State.GenerateStates(node);

                if (node.derviedStates == null) continue;

                foreach (State child in node.derviedStates)
                {
                    if (child == null || visited.Contains(child)) continue;
                    child.parent = node;
                    int g = child.ComputePathCost();
                    int h = child.ComputeHeuristicCost();
                    child.cost = g + h;
                    pq.Enqueue(child, child.cost);
                }
            }

            return null;
        }
    }
}
