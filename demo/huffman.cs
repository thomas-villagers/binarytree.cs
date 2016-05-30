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

  public int Count {
    get { return list.Count; }
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
}

class Huffman {

  public static void Main() {
    StringHistogram hist = new StringHistogram("a fast runner need never be afraid of the dark"); 

    Func<StringIntPair, StringIntPair, int> comparer = (x,y) => x.Value - y.Value; 
    var PQ = new PriorityQueue<BinaryTree<StringIntPair>>((x,y) => comparer(x.value, y.value));
    foreach(var element in hist.dict) {
      PQ.Enqueue(new BinaryTree<StringIntPair>(new StringIntPair(((char)element.Key).ToString(),element.Value), comparer));
    }
  
    while (PQ.Count > 1) {
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
