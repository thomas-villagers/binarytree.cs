<div id="table-of-contents">
<h2>Table of Contents</h2>
<div id="text-table-of-contents">
<ul>
<li><a href="#orgheadline1">1. Generic Tree Class</a></li>
<li><a href="#orgheadline5">2. Demo</a>
<ul>
<li><a href="#orgheadline2">2.1. Tree Insertion and LMR-Traversion</a></li>
<li><a href="#orgheadline3">2.2. Graphviz-Output</a></li>
<li><a href="#orgheadline4">2.3. Application: Huffman-Encoding</a></li>
</ul>
</li>
</ul>
</div>
</div>


# Generic Tree Class<a id="orgheadline1"></a>

A simple generic unbalanced binary tree. 

    using System;
    using System.Collections.Generic; 
    
      public class BinaryTree<T> {
    
        public BinaryTree<T> left;
        public BinaryTree<T> right;
    
        public readonly T value;
        Func<T, T, int> comparer; 
    
        public BinaryTree(T value, Func<T, T, int> comparer) {
          this.value = value;
          this.comparer = comparer;
          left = null;
          right = null;
        }
    
        public BinaryTree(T value) : this(value, (x,y) => Comparer<T>.Default.Compare(x,y)) { }
    
        public void Insert(T value) {
          if (comparer(value, this.value) < 0)
          {
            if (left == null) left = new BinaryTree<T>(value, comparer);
            else left.Insert(value);
          }
          else  if (right == null)
            right = new BinaryTree<T>(value, comparer);
          else right.Insert(value);
        }
    
        public void Insert(params T[] values) {
          foreach(T value in values) Insert(value);
        }
    
        override public string ToString() {
          string s = "";
          if (left != null)  s += left.ToString();
          s += string.Format("{0};", value);
          if (right != null) s += right.ToString();
          return s;
        }
      }

# Demo<a id="orgheadline5"></a>

## Tree Insertion and LMR-Traversion<a id="orgheadline2"></a>

    using System; 
    
    class TreeTest {
    
      public static void Main()
      {
        var root = new BinaryTree<int>(5); 
        root.Insert(3);
        root.Insert(7);
        root.Insert(1);
        root.Insert(4);
        root.Insert(6);
        root.Insert(2);  
        Console.WriteLine(root);
    
        var floattree = new BinaryTree<float>(3.14f); 
        floattree.Insert(0.99f, 2.34f, 3.1415f);
        Console.WriteLine(floattree);
    
        var lannisters = new BinaryTree<string>("Tywin");
        lannisters.Insert("Cersei","Tyrion","Joffrey");
        lannisters.Insert("Tommen");
        lannisters.Insert("Myrcella");
        lannisters.Insert("Jamie");
        Console.WriteLine(lannisters);
      }
    }

    mcs demo/treetest1.cs src/binarytree.cs
    mono demo/treetest1.exe

    1;2;3;4;5;6;7;
    0,99;2,34;3,14;3,1415;
    Cersei;Jamie;Joffrey;Myrcella;Tommen;Tyrion;Tywin;

## Graphviz-Output<a id="orgheadline3"></a>

    using System; 
    
    public static class BinaryTreeExtensions {
    
      private static void PrintNode<T>(T rootValue, T childValue) {
        Console.WriteLine("  \"{0}\" -> \"{1}\"", rootValue, childValue);
      }
    
      private static void PrintNode<T>(T value, int empties) {
        Console.WriteLine("  empty{0} [label=\"\", style=invis];", empties);
        Console.WriteLine("  \"{0}\" -> empty{1}", value,  empties);
      }
    
      private static void PrintSubTree<T>(BinaryTree<T> tree, ref int empties) {
    
        if (tree.left == null && tree.right == null)
          Console.WriteLine("  \"{0}\" [shape=rectangle];", tree.value);
    
        if (tree.left != null) {
          PrintNode(tree.value, tree.left.value);
          PrintSubTree(tree.left, ref empties);
        } else  if (tree.right != null) {
          PrintNode(tree.value, empties++);
        }
    
        if (tree.right != null) {
          PrintNode(tree.value, tree.right.value);
          PrintSubTree(tree.right, ref empties);
        } else if (tree.left != null) {
          PrintNode(tree.value, empties++);
        }
      }
    
      public static void PrintDot<T>(this BinaryTree<T> tree) {
        Console.WriteLine("digraph G {");
        int empties = 0;
        PrintSubTree(tree, ref empties); 
        Console.WriteLine("}"); 
      }
    }

Call extension method `PrintDot` and feed the results into [Graphviz](http://www.graphviz.org/): 

    class TreeTest {
      public static void Main() {
        var root = new BinaryTree<int>(5); 
        root.Insert(3,7,1,4,6,2);
        root.PrintDot(); 
      }
    }

    mcs demo/treetest2.cs src/binarytree.cs src/binarytreeextensions.cs 
    mono demo/treetest2.exe

![img](images/tree1.png)

Another Example: 

    using System; 
    
    class TreeTest {
    
      public static void Main() {
        var lannisters = new BinaryTree<string>("Tywin");
        lannisters.Insert("Cersei");
        lannisters.Insert("Tyrion");
        lannisters.Insert("Joffrey");
        lannisters.Insert("Tommen");
        lannisters.Insert("Myrcella");
        lannisters.Insert("Jamie");
        lannisters.PrintDot();
      }
    }

    mcs demo/treetest3.cs src/binarytree.cs src/binarytreeextensions.cs 
    mono demo/treetest3.exe

![img](images/tree2.png)

## Application: Huffman-Encoding<a id="orgheadline4"></a>

    using System;
    using System.Collections.Generic; 
    using System.Linq; 
    
    using StringIntPair = System.Collections.Generic.KeyValuePair<string,int>;
    
    class PriorityQueue<T> { // A poor man's priority queue... 
    
      List<T> list;
      readonly Func<T, T, int> comparer; 
    
      public PriorityQueue(Func<T, T, int> comparer) {
        this.comparer = comparer;
        list = new List<T>();
      }
    
      public T Dequeue() {
        var element = list[list.Count-1];
        list.RemoveAt(list.Count-1); // removal of last element is O(1)
        return element; 
      }
    
      public void Enqueue(T element) { 
        list.Add(element);
        list.Sort((x,y) => -1*comparer(x,y)); // reverse sort order such that smallest element is at end of list
      } 
    
      public int Count() {
        return list.Count();
      }
    }
    
    class StringHistogram {
    
      public Dictionary<int, int> dict; 
    
      public StringHistogram(string str) {
        dict = new Dictionary<int, int>(); 
        foreach(var c in str.ToCharArray()) {
          dict[c] = dict.ContainsKey(c) ? dict[c]+1 : 1; 
        }
      }
    
      override public string ToString() {
        string s=""; 
        foreach(var entry in dict) {
          s+= string.Format("{0}|{1}\n", (char)entry.Key, entry.Value);
        }
        return s;
      }
    }
    
    class Huffman {
    
      public static void Main() {
        //  StringHistogram hist = new StringHistogram("Hello World!")
        StringHistogram hist = new StringHistogram("a fast runner need never be afraid of the dark"); 
    
        Func<StringIntPair, StringIntPair, int> comparer = (x,y) => x.Value - y.Value; 
        var PQ = new PriorityQueue<BinaryTree<StringIntPair>>((x,y) => comparer(x.value, y.value));
        foreach(var element in hist.dict) {
          PQ.Enqueue(new BinaryTree<StringIntPair>(new StringIntPair(((char)element.Key).ToString(),element.Value), comparer));
        }
    
        while (PQ.Count() > 1) {
          var T1 = PQ.Dequeue();
          var T2 = PQ.Dequeue();
          var newRoot = new BinaryTree<StringIntPair>(new StringIntPair(T1.value.Key + T2.value.Key, T1.value.Value+T2.value.Value), comparer);
          newRoot.left = T1;
          newRoot.right= T2;
          PQ.Enqueue(newRoot);
        }
        PQ.Dequeue().PrintDot();
      }
    }

    mcs demo/huffman.cs src/binarytree.cs src/binarytreeextensions.cs
    mono demo/huffman.exe

![img](images/tree3.png)