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
    var element = list[0];
    list.RemoveAt(0);
    return element; 
  }

  public void Enqueue(T element) { 
    list.Add(element);
    list.Sort((x,y) => comparer(x,y)); 
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
