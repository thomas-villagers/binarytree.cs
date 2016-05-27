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
